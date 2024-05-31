$(document).ready(function () {
    var token = $('#TokENiD').val();
    var preloader = $('#spinner-wrapper');
    var preloaderFadeOutTime = 500;

    function showPreloader() {
        preloader.show();
    }

    function hidePreloader() {
        preloader.fadeOut(preloaderFadeOutTime);
    }

    function reloadWithAlert() {
        Swal.fire({
            title: "Successfully!",
            text: "Your information has been changed",
            icon: "success"
        }).then((result) => {
            if (result.isConfirmed || result.isDismissed) {
                location.reload();
            }
        });
    }
    $('#updatePasswordBtn').click(function () {
        var userId = $('#UsERID').val();
        var oldPassword = $('#my-password').val();
        var newPassword = $('#newPassword').val();
        var confirmPassword = $('#confirmPassword').val();

        var changePasswordDto = {
            UserId: userId,
            OldPassword: oldPassword,
            NewPassword: newPassword,
            ConfirmNewPassword: confirmPassword,
        };

        showPreloader();

        $.ajax({
            url: `https://localhost:7255/api/AccountSettings/ChangePassword/${userId}`,
            type: 'PUT',
            contentType: 'application/json',
            headers: {
                'Authorization': 'Bearer ' + token
            },
            data: JSON.stringify(changePasswordDto),
            success: function (response) {
                hidePreloader();
                reloadWithAlert();

            },
            error: function (xhr, status, error) {
                hidePreloader();
                console.error('Error:', error);

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




