let audio = document.getElementById("audio");
let seekBar = document.getElementById("seekBar");

let songs = [
    './assets/Clair Obscur： Expedition 33 - Gustave.mp3',
    './assets/Clair Obscur： Expedition 33 - Alicia.mp3',
    './assets/Clair Obscur： Expedition 33 - Lumiere.mp3',
    './assets/Clair Obscur： Expedition 33 - Shadow of the Monolith.mp3',
]

audio.addEventListener("loadedmetadata", function () {
    seekBar.max = audio.duration
});

audio.addEventListener("timeupdate", () => {
    seekBar.max = audio.duration;
    seekBar.value = audio.currentTime;
});

seekBar.addEventListener("input", () => {
    audio.currentTime = seekBar.value;
});

function loadSong(idx) {
    audio.src = songs[idx];
    audio.play();
}

function playAudio() {
    audio.play();
}

function pauseAudio() {
    audio.pause();
}

function stopAudio() {
    audio.pause();
    audio.currentTime = 0;
}
