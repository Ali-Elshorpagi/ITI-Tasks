let canvas = document.getElementById("canva"),
    ctx = canvas.getContext("2d");

function drawCircls() {
    let color = document.getElementById('colorPicker').value;
    ctx.clearRect(0, 0, canvas.width, canvas.height);
    let numberNoCircles = getRandomNumber(100, 1000);
    ctx.strokeStyle = color;
    ctx.lineWidth = 1;
    for (let i = 0; i < numberNoCircles; ++i) {
        let radius = Math.random() * 20 + 5,
            x = Math.random() * (canvas.width - radius * 2) + radius,
            y = Math.random() * (canvas.height - radius * 2) + radius;

        ctx.beginPath();
        ctx.arc(x, y, radius, 0, 360);
        ctx.stroke();

    }
}

function getRandomNumber(start, end) {
    return Math.floor(Math.random() * (end - start + 1)) + start;
}
