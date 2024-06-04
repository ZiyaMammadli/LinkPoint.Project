$(document).ready(function () {
    $('#message-button').click(function () {
        var userId = $('#us-er-Id').val();
        var authUserId = $('#AuthUs-er-Id').val();
        var token = $('#tokenidd').val();

        $.ajax({
            url: `https://localhost:7255/api/Conversations/CreateConversation/${authUserId}/${userId}`,
            type: 'POST',
            contentType: 'application/json',
            headers: { 'Authorization': 'Bearer ' + token },
            success: function (response) {

                window.location.href = '/NewsFeed/Messages';
            },
            error: function (xhr, status, error) {
                console.error('Error creating conversation:', error);

                var errors = xhr.responseJSON;
                var errorMessage = 'Something went wrong!';

                if (errors && errors.errors) {
                    errorMessage = '<ul>';

                    $.each(errors.errors, function (key, value) {
                        errorMessage += '<li>' + value + '</li>';
                    });
                    errorMessage += '</ul>';
                }

                Swal.fire({
                    icon: "error",
                    title: "Oops...",
                    html: errorMessage,
                });
            }
        });
    });
});

