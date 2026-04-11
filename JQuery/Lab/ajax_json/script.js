$(document).ready(function () {
    let requestUrl = 'https://dummyjson.com/posts';

    $.ajax({
        url: requestUrl,
        method: 'GET',
        success: function (data) {
            console.log(data);

            let posts = data.posts;
            let tableBody = $('#postsTable');

            for (const post of posts) {

                let row = $('<tr>');

                row.append($('<td>').text(post.id));
                row.append($('<td>').text(post.title));
                row.append($('<td>').text(post.body));
                row.append($('<td>').text(post.tags.join(', ')));
                row.append($('<td>').text(post.reactions.likes));
                row.append($('<td>').text(post.views));

                row.append('</tr>');

                tableBody.append(row);
            }
        }
    });
});