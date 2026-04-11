class Counter {
    counter: number = 0;
    public increment() { this.counter++; }
    public decrement() { this.counter--; }
    public reset() { this.counter = 0; }
}

var cnt = new Counter();

var incrementBtn = document.getElementById("increment");
var decrementBtn = document.getElementById("decrement");
var resetBtn = document.getElementById("reset");
var counterDisplay = document.getElementById("counter");

if (incrementBtn && decrementBtn && resetBtn && counterDisplay) {
    incrementBtn.addEventListener("click", function () {
        cnt.increment();
        counterDisplay!.textContent = cnt.counter.toString();
    });

    decrementBtn.addEventListener("click", function () {
        cnt.decrement();
        counterDisplay!.textContent = cnt.counter.toString();
    });

    resetBtn.addEventListener("click", function () {
        cnt.reset();
        counterDisplay!.textContent = cnt.counter.toString();
    });
}



