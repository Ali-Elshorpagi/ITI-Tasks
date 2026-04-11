$(document).ready(function () {
    let images = $('.images img');
    let currentIndex = 0;

    images.click(function () {
        currentIndex = images.index(this);
        $('.slider-image').attr('src', $(this).attr('src'));
        $('.container').addClass('active').fadeIn();
    });

    $('.next').click(function (e) {
        currentIndex = (currentIndex + 1) % images.length; // 0 -> 5
        updateSlider();
    });

    $('.prev').click(function (e) {
        currentIndex = (currentIndex - 1) % images.length;
        updateSlider();
    });

    function updateSlider() {
        $('.slider-image').attr('src', images.eq(currentIndex).attr('src'));
    }

    $('.container').click(function () {
        $(this).fadeOut().removeClass('active');
    });

    $('.slider-frame').click(function (e) {
        e.stopPropagation();
    });

});
