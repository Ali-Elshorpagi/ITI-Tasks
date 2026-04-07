"use strict";

(() => {

    const numbersInputEl = document.getElementById("numbersInput");
    const algorithmSelectEl = document.getElementById("algorithmSelect");
    const sortButtonEl = document.getElementById("sortButton");
    const randomButtonEl = document.getElementById("vizRandomButton");
    const pauseButtonEl = document.getElementById("vizPauseButton");
    const resetButtonEl = document.getElementById("vizResetButton");
    const speedRangeEl = document.getElementById("vizSpeedRange");
    const speedValueEl = document.getElementById("vizSpeedValue");
    const soundEnabledEl = document.getElementById("soundEnabled");
    const soundVolumeEl = document.getElementById("soundVolume");
    const soundVolumeValueEl = document.getElementById("soundVolumeValue");
    const operationTextEl = document.getElementById("vizOperationText");
    const barContainerEl = document.getElementById("vizBarContainer");

    const VIS_MAX_BARS = 80;
    const DEFAULT_SIZE = 24;

    let values = [];
    let bars = [];
    let isAnimating = false;
    let isPaused = false;
    let stopRequested = false;
    let audioContext = null;

    if (
        numbersInputEl &&
        algorithmSelectEl &&
        sortButtonEl &&
        randomButtonEl &&
        pauseButtonEl &&
        resetButtonEl &&
        speedRangeEl &&
        speedValueEl &&
        soundEnabledEl &&
        soundVolumeEl &&
        soundVolumeValueEl &&
        operationTextEl &&
        barContainerEl
    ) {
        function setOperation(text) {
            operationTextEl.textContent = text;
        }

        function getDelayMs() {
            const speed = Number(speedRangeEl.value);
            return 20 + ((100 - speed) / 100) * 580;
        }

        function sleep(ms) {
            return new Promise((resolve) => setTimeout(resolve, ms));
        }

        function getSoundVolume() {
            return Number(soundVolumeEl.value) / 100;
        }

        function ensureAudioContext() {
            if (!soundEnabledEl.checked) return null;

            if (!audioContext) {
                const Ctx = window.AudioContext || window.webkitAudioContext;
                if (!Ctx) return null;
                audioContext = new Ctx();
            }

            if (audioContext.state === "suspended") {
                audioContext.resume().catch(() => {
                    // Ignore resume errors; sound will stay disabled for now.
                });
            }

            return audioContext;
        }

        function playTone(frequency, durationMs, type = "sine", gainScale = 1) {
            if (!soundEnabledEl.checked) return;
            const ctx = ensureAudioContext();
            if (!ctx) return;

            const osc = ctx.createOscillator();
            const gain = ctx.createGain();

            osc.type = type;
            osc.frequency.value = frequency;

            const now = ctx.currentTime;
            const baseVolume = Math.max(0, Math.min(1, getSoundVolume())) * gainScale;

            gain.gain.setValueAtTime(0.0001, now);
            gain.gain.exponentialRampToValueAtTime(Math.max(0.0001, baseVolume), now + 0.01);
            gain.gain.exponentialRampToValueAtTime(0.0001, now + durationMs / 1000);

            osc.connect(gain);
            gain.connect(ctx.destination);

            osc.start(now);
            osc.stop(now + durationMs / 1000 + 0.01);
        }

        function playCompareSound() {
            playTone(520, 45, "triangle", 0.35);
        }

        function playSwapSound() {
            playTone(220, 70, "square", 0.45);
        }

        function playWriteSound() {
            playTone(310, 55, "sawtooth", 0.4);
        }

        function playCompleteSound() {
            playTone(440, 90, "sine", 0.5);
            setTimeout(() => playTone(660, 120, "sine", 0.5), 100);
        }

        async function waitIfPaused() {
            while (isPaused && !stopRequested) {
                await sleep(50);
            }
        }

        function parseNumbers(raw) {
            if (window.SortingLabAPI && typeof window.SortingLabAPI.parseInputToNumberArray === "function") {
                return window.SortingLabAPI.parseInputToNumberArray(raw);
            }

            const tokens = raw.split(",").map((item) => item.trim());
            const out = [];
            for (let i = 0; i < tokens.length; i += 1) {
                if (tokens[i] === "") throw new Error(`Value #${i + 1} is empty.`);
                const value = Number(tokens[i]);
                if (!Number.isFinite(value)) throw new Error(`Value #${i + 1} is invalid.`);
                out.push(value);
            }
            return out;
        }

        function randomArray(size = DEFAULT_SIZE) {
            const out = [];
            for (let i = 0; i < size; i += 1) {
                out.push(Math.floor(Math.random() * 196) + 5);
            }
            return out;
        }

        function clearHighlight() {
            for (const bar of bars) {
                if (!bar.classList.contains("sorted")) {
                    bar.classList.remove("comparing", "swapping");
                }
            }
        }

        function renderBars(array) {
            values = array.slice();
            bars = [];
            barContainerEl.innerHTML = "";

            const min = Math.min(...values);
            const max = Math.max(...values);
            const range = Math.max(1, max - min);

            for (let i = 0; i < values.length; i += 1) {
                const bar = document.createElement("div");
                bar.className = "bar";
                const normalized = (values[i] - min) / range;
                bar.style.height = `${Math.max(8, normalized * 100)}%`;

                const label = document.createElement("span");
                label.textContent = String(values[i]);
                bar.appendChild(label);

                barContainerEl.appendChild(bar);
                bars.push(bar);
            }
        }

        function updateBars() {
            const min = Math.min(...values);
            const max = Math.max(...values);
            const range = Math.max(1, max - min);

            for (let i = 0; i < values.length; i += 1) {
                const normalized = (values[i] - min) / range;
                bars[i].style.height = `${Math.max(8, normalized * 100)}%`;
                bars[i].querySelector("span").textContent = String(values[i]);
            }
        }

        function swap(i, j) {
            const t = values[i];
            values[i] = values[j];
            values[j] = t;
            updateBars();
        }

        function markSorted(i) {
            bars[i].classList.remove("comparing", "swapping");
            bars[i].classList.add("sorted");
        }

        async function animateToValues(targetValues, operationLabel = "Applying result") {
            if (!Array.isArray(targetValues) || targetValues.length === 0) return;

            if (values.length !== targetValues.length) {
                renderBars(targetValues);
                for (let i = 0; i < bars.length; i += 1) markSorted(i);
                setOperation(`${operationLabel}: updated visualization`);
                return;
            }

            for (let i = 0; i < targetValues.length; i += 1) {
                if (stopRequested) break;
                await waitIfPaused();
                clearHighlight();

                bars[i].classList.add("comparing");
                setOperation(`${operationLabel}: checking index ${i}`);
                playCompareSound();
                await sleep(Math.max(20, getDelayMs() * 0.35));

                if (values[i] !== targetValues[i]) {
                    bars[i].classList.remove("comparing");
                    bars[i].classList.add("swapping");
                    values[i] = targetValues[i];
                    updateBars();
                    setOperation(`${operationLabel}: writing value at index ${i}`);
                    playWriteSound();
                    await sleep(Math.max(20, getDelayMs() * 0.4));
                }

                markSorted(i);
            }

            for (let i = 0; i < bars.length; i += 1) markSorted(i);
            setOperation(`${operationLabel}: completed`);
            playCompleteSound();
        }

        async function flashSuccess() {
            if (!bars.length) return;
            clearHighlight();
            for (let i = 0; i < bars.length; i += 1) {
                bars[i].classList.add("swapping");
            }
            await sleep(180);
            for (let i = 0; i < bars.length; i += 1) {
                bars[i].classList.remove("swapping");
                bars[i].classList.add("sorted");
            }
            setOperation("Self tests completed");
            playCompleteSound();
        }

        function bubbleActions(input) {
            const arr = input.slice();
            const actions = [];

            for (let i = 0; i < arr.length - 1; i += 1) {
                for (let j = 0; j < arr.length - 1 - i; j += 1) {
                    actions.push({ type: "compare", i: j, j: j + 1 });
                    if (arr[j] > arr[j + 1]) {
                        [arr[j], arr[j + 1]] = [arr[j + 1], arr[j]];
                        actions.push({ type: "swap", i: j, j: j + 1 });
                    }
                }
                actions.push({ type: "markSorted", index: arr.length - 1 - i });
            }
            actions.push({ type: "markSorted", index: 0 });

            return actions;
        }

        function quickActions(input) {
            const arr = input.slice();
            const actions = [];

            function partition(left, right) {
                const pivot = arr[right];
                let store = left;

                for (let j = left; j < right; j += 1) {
                    actions.push({ type: "compare", i: j, j: right });
                    if (arr[j] <= pivot) {
                        if (store !== j) {
                            [arr[store], arr[j]] = [arr[j], arr[store]];
                            actions.push({ type: "swap", i: store, j });
                        }
                        store += 1;
                    }
                }

                if (store !== right) {
                    [arr[store], arr[right]] = [arr[right], arr[store]];
                    actions.push({ type: "swap", i: store, j: right });
                }

                actions.push({ type: "markSorted", index: store });
                return store;
            }

            function run(left, right) {
                if (left > right) return;
                if (left === right) {
                    actions.push({ type: "markSorted", index: left });
                    return;
                }

                const p = partition(left, right);
                run(left, p - 1);
                run(p + 1, right);
            }

            run(0, arr.length - 1);
            return actions;
        }

        function mergeActions(input) {
            const arr = input.slice();
            const actions = [];

            function merge(left, mid, right) {
                const a = arr.slice(left, mid + 1);
                const b = arr.slice(mid + 1, right + 1);
                let i = 0;
                let j = 0;
                let k = left;

                while (i < a.length && j < b.length) {
                    actions.push({ type: "compare", i: left + i, j: mid + 1 + j });
                    if (a[i] <= b[j]) {
                        arr[k] = a[i];
                        actions.push({ type: "overwrite", index: k, value: a[i] });
                        i += 1;
                    } else {
                        arr[k] = b[j];
                        actions.push({ type: "overwrite", index: k, value: b[j] });
                        j += 1;
                    }
                    k += 1;
                }

                while (i < a.length) {
                    arr[k] = a[i];
                    actions.push({ type: "overwrite", index: k, value: a[i] });
                    i += 1;
                    k += 1;
                }

                while (j < b.length) {
                    arr[k] = b[j];
                    actions.push({ type: "overwrite", index: k, value: b[j] });
                    j += 1;
                    k += 1;
                }
            }

            function run(left, right) {
                if (left >= right) return;
                const mid = Math.floor((left + right) / 2);
                run(left, mid);
                run(mid + 1, right);
                merge(left, mid, right);
            }

            run(0, arr.length - 1);
            for (let i = 0; i < arr.length; i += 1) {
                actions.push({ type: "markSorted", index: i });
            }
            return actions;
        }

        function heapActions(input) {
            const arr = input.slice();
            const actions = [];

            function heapify(size, root) {
                let largest = root;
                const left = root * 2 + 1;
                const right = root * 2 + 2;

                if (left < size) {
                    actions.push({ type: "compare", i: left, j: largest });
                    if (arr[left] > arr[largest]) largest = left;
                }

                if (right < size) {
                    actions.push({ type: "compare", i: right, j: largest });
                    if (arr[right] > arr[largest]) largest = right;
                }

                if (largest !== root) {
                    [arr[root], arr[largest]] = [arr[largest], arr[root]];
                    actions.push({ type: "swap", i: root, j: largest });
                    heapify(size, largest);
                }
            }

            for (let i = Math.floor(arr.length / 2) - 1; i >= 0; i -= 1) {
                heapify(arr.length, i);
            }

            for (let end = arr.length - 1; end > 0; end -= 1) {
                [arr[0], arr[end]] = [arr[end], arr[0]];
                actions.push({ type: "swap", i: 0, j: end });
                actions.push({ type: "markSorted", index: end });
                heapify(end, 0);
            }

            actions.push({ type: "markSorted", index: 0 });
            return actions;
        }

        function buildActions(algorithmKey, data) {
            if (algorithmKey === "quickSort") return quickActions(data);
            if (algorithmKey === "mergeSort") return mergeActions(data);
            if (algorithmKey === "bubbleSort") return bubbleActions(data);
            if (algorithmKey === "heapSort") return heapActions(data);
            throw new Error("Unknown algorithm selected.");
        }

        async function play(actions) {
            for (const action of actions) {
                if (stopRequested) break;

                await waitIfPaused();
                clearHighlight();

                if (action.type === "compare") {
                    bars[action.i].classList.add("comparing");
                    bars[action.j].classList.add("comparing");
                    setOperation(`Comparing index ${action.i} and ${action.j}`);
                    playCompareSound();
                    await sleep(getDelayMs());
                    continue;
                }

                if (action.type === "swap") {
                    bars[action.i].classList.add("swapping");
                    bars[action.j].classList.add("swapping");
                    setOperation(`Swapping index ${action.i} and ${action.j}`);
                    playSwapSound();
                    swap(action.i, action.j);
                    await sleep(getDelayMs());
                    continue;
                }

                if (action.type === "overwrite") {
                    bars[action.index].classList.add("swapping");
                    values[action.index] = action.value;
                    updateBars();
                    setOperation(`Writing value ${action.value} at index ${action.index}`);
                    playWriteSound();
                    await sleep(getDelayMs());
                    continue;
                }

                if (action.type === "markSorted") {
                    markSorted(action.index);
                    setOperation(`Marking index ${action.index} as sorted`);
                    await sleep(Math.max(25, getDelayMs() * 0.35));
                }
            }

            for (let i = 0; i < bars.length; i += 1) {
                markSorted(i);
            }
            playCompleteSound();
        }

        async function runUnifiedSort() {
            if (isAnimating) return;

            try {
                isAnimating = true;
                stopRequested = false;
                isPaused = false;
                pauseButtonEl.textContent = "Pause";
                sortButtonEl.disabled = true;

                const numbers = parseNumbers(numbersInputEl.value);
                const sorter = window.SortingLabAPI
                    ? window.SortingLabAPI.getSelectedSorter()
                    : {
                        label: algorithmSelectEl.value,
                        sort: (arr) => arr.slice().sort((a, b) => a - b),
                    };

                renderBars(numbers);

                if (numbers.length > VIS_MAX_BARS) {
                    setOperation("Array too large for step-by-step animation. Running fast mode.");
                    const start = performance.now();
                    const sorted = sorter.sort(numbers);
                    const duration = performance.now() - start;
                    if (window.SortingLabAPI) {
                        window.SortingLabAPI.showSuccess(sorted, duration, sorter.label);
                    }
                    renderBars(sorted);
                    return;
                }

                const actions = buildActions(algorithmSelectEl.value, numbers);
                const start = performance.now();
                await play(actions);
                const duration = performance.now() - start;

                const sorted = sorter.sort(numbers);
                if (window.SortingLabAPI) {
                    window.SortingLabAPI.showSuccess(sorted, duration, sorter.label);
                }
                setOperation("Completed");
            } catch (error) {
                if (window.SortingLabAPI) {
                    window.SortingLabAPI.showError(error.message || "Sorting failed.");
                }
                setOperation(error.message || "Sorting failed.");
            } finally {
                isAnimating = false;
                isPaused = false;
                pauseButtonEl.textContent = "Pause";
                sortButtonEl.disabled = false;
            }
        }

        function resetBars() {
            stopRequested = true;
            isPaused = false;
            pauseButtonEl.textContent = "Pause";

            try {
                const arr = parseNumbers(numbersInputEl.value);
                renderBars(arr);
                setOperation("Reset complete");
            } catch (error) {
                setOperation(error.message || "Reset failed.");
            }
        }

        randomButtonEl.addEventListener("click", () => {
            if (isAnimating) return;
            ensureAudioContext();
            const arr = randomArray();
            numbersInputEl.value = arr.join(",");
            renderBars(arr);
            setOperation("Random array generated");
        });

        sortButtonEl.addEventListener("click", runUnifiedSort);

        pauseButtonEl.addEventListener("click", () => {
            if (!isAnimating) return;
            isPaused = !isPaused;
            pauseButtonEl.textContent = isPaused ? "Resume" : "Pause";
            setOperation(isPaused ? "Paused" : "Resumed");
        });

        resetButtonEl.addEventListener("click", () => {
            resetBars();
        });

        speedRangeEl.addEventListener("input", () => {
            speedValueEl.textContent = speedRangeEl.value;
        });

        soundVolumeEl.addEventListener("input", () => {
            soundVolumeValueEl.textContent = soundVolumeEl.value;
        });

        soundEnabledEl.addEventListener("change", () => {
            if (soundEnabledEl.checked) {
                ensureAudioContext();
                setOperation("Sound enabled");
            } else {
                setOperation("Sound disabled");
            }
        });

        numbersInputEl.addEventListener("change", () => {
            if (isAnimating) return;
            try {
                const arr = parseNumbers(numbersInputEl.value);
                renderBars(arr);
                setOperation("Array loaded");
            } catch (error) {
                setOperation(error.message || "Invalid input");
            }
        });

        (function init() {
            if (numbersInputEl.value.trim() === "") {
                const initial = randomArray();
                numbersInputEl.value = initial.join(",");
            }

            try {
                renderBars(parseNumbers(numbersInputEl.value));
            } catch {
                const fallback = randomArray();
                numbersInputEl.value = fallback.join(",");
                renderBars(fallback);
            }

            speedValueEl.textContent = speedRangeEl.value;
            soundVolumeValueEl.textContent = soundVolumeEl.value;
            setOperation("Idle");
        })();

        window.SortingVisualizerAPI = {
            animateToValues,
            flashSuccess,
            setOperation,
            renderBarsFromArray: renderBars,
        };
    }

})();
