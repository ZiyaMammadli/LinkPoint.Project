$(document).ready(function () {
    $('.adddd-comment-button').click(function () {
        var postId = $(this).data('post-id');
        var commentText = $('#comment-text-' + postId).val();
        var userId = $('#user-id-' + postId).val();
        var token = $('#tokenid').val(); 

        console.log("Button clicked for post: " + postId); 

        $.ajax({
            url: 'https://localhost:7255/api/Comments/CreateComment', // API endpoint
            type: 'POST',
            contentType: 'application/json',
            headers: {
                'Authorization': 'Bearer ' + token 
            },
            data: JSON.stringify({
                text: commentText,
                userId: userId,
                postId: postId
            }),
            success: function (response) {
                console.log("Response received: ", response);
                var commentHtml = `<div class="post-comment">
                                        <img src="`+ response.userProfileImage + `" alt="" class="profile-photo-sm" />
                                        <p><a href="timeline.html" class="profile-link">`+ response.userName  + `</a> ` +  response.text + `</p>
                                    </div>`;

                $('#comments-' + postId).append(commentHtml);
                $('#comment-text-' + postId).val(''); 
            },
            error: function (xhr, status, error) {
                var errors = JSON.parse(xhr.responseText).errors;
                $('#commentValidationErrors-' + postId).html('');
                for (var key in errors) {
                    if (errors.hasOwnProperty(key)) {
                        $('#commentValidationErrors-' + postId).append('<p>' + errors[key] + '</p>');
                    }
                }
            }
        });
    });
});