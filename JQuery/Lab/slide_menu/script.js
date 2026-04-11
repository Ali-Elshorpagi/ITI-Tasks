$(document).ready(function () {
    $('.container').hover(
        function () {
            $(this).addClass('expanded');
        },
        function () {
            $(this).removeClass('expanded');
        }
    );

    $('#item1, #item2').click(function () {
        let subId = '#items' + $(this).attr('id').slice(-1); // 1 or 2
        let text = $(this).attr('id') === 'item1' ? 'Hello' : 'Wow';

        if ($(subId).length) {
            $(subId).remove();
        } else {
            $('<p></p>')
                .attr('id', subId.slice(1))
                .addClass('sub-item')
                .text(text)
                .insertAfter($(this));
        }
    });
});