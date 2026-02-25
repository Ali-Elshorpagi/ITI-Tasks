var arr = []
for (var i = 1; i < 101; ++i) {
    arr.push(i);
}

var parentDiv = document.getElementById("container");

var idx = 0;
var intervalId = setInterval(function () {
    if (idx > 99) {
        clearInterval(intervalId);
        return;
    }
    
    var p = document.createElement("p");
    p.textContent = arr[idx];
    p.style.width = "60px";
    p.style.height = "50px";
    p.style.textAlign = "center";
    p.style.alignContent = "center";
    p.style.fontSize = "40px";

    if (idx % 2 == 0)
        p.style.border = "dashed red 5px", p.style.borderRadius = "20px";
    else
        p.style.border = "solid blue 5px";

    parentDiv.appendChild(p);
    ++idx;

}, 500);