var Counter = /** @class */ (function () {
    function Counter() {
        this.counter = 0;
    }
    Counter.prototype.increment = function () { this.counter++; };
    Counter.prototype.decrement = function () { this.counter--; };
    Counter.prototype.reset = function () { this.counter = 0; };
    return Counter;
}());
var cnt = new Counter();
var incrementBtn = document.getElementById("increment");
var decrementBtn = document.getElementById("decrement");
var resetBtn = document.getElementById("reset");
var counterDisplay = document.getElementById("counter");
if (incrementBtn && decrementBtn && resetBtn && counterDisplay) {
    incrementBtn.addEventListener("click", function () {
        cnt.increment();
        counterDisplay.textContent = cnt.counter.toString();
    });
    decrementBtn.addEventListener("click", function () {
        cnt.decrement();
        counterDisplay.textContent = cnt.counter.toString();
    });
    resetBtn.addEventListener("click", function () {
        cnt.reset();
        counterDisplay.textContent = cnt.counter.toString();
    });
}
