addEventListener("load", function () {
    prevbtn = document.querySelector("#prev");
    nextbtn = document.querySelector("#next");
    stopbtn = document.querySelector("#stop");
    slidebtn = document.querySelector("#slide");
    imgobj = document.getElementsByTagName("img")[0];
    counter = 1;
    intervalId = -1;
    nextbtn.addEventListener("click", function () {
        if (counter == 7)
            counter = 1
        imgobj.src = `Images/${counter++}.png`
    });
    prevbtn.addEventListener("click", function () {
        if (counter == 0)
            counter = 6
        imgobj.src = `Images/${counter--}.png`
    });
    slidebtn.addEventListener("click", function () {
        if (intervalId == -1) {
            intervalId = setInterval(function () {
                if (counter == 7)
                    counter = 1
                imgobj.src = `Images/${counter++}.png`
            }, 500);
        }
    });

    stopbtn.addEventListener("click", function () {
        if (intervalId != -1) {
            clearInterval(intervalId);
            intervalId = -1;
        }
    });
})