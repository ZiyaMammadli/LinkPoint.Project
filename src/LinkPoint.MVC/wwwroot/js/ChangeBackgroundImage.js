$(document).ready(function () {
    var token = $('#tokenidforbackgroundimage').val();
    var preloader = $('#spinner-wrapper');
    var preloaderFadeOutTime = 500;

    function showPreloader() {
        preloader.show();
    }

    function hidePreloader() {
        preloader.fadeOut(preloaderFadeOutTime);
    }

    function reloadWithAlert() {

        location.reload();

    }
    $('#deleteBackgroundImageBtn').click(function () {
        var backgroundImageId = $('#backgroundImageId').val();
        var userId = $('#userIdd').val();

        var backgroundImageDeleteDto = {
            ImageId: backgroundImageId,
            UserId: userId
        };

        showPreloader();

        $.ajax({
            url: `https://localhost:7255/api/AccountSettings/DeleteUserBackgroundImage/${backgroundImageId}`,
            type: 'DELETE',
            contentType: 'application/json',
            headers: {
                'Authorization': 'Bearer ' + token
            },
            data: JSON.stringify(backgroundImageDeleteDto),
            success: function (response) {
                hidePreloader();
                reloadWithAlert();
            },
            error: function (xhr, status, error) {
                hidePreloader();
                console.error('Error:', error);
                Swal.fire({
                    icon: "error",
                    title: "Oops...",
                    text: "Something went wrong!",
                });
            }
        });
    });

    $('#updateBackgroundImageBtn').click(function () {
        var backgroundImage = $('#backgroundImage')[0].files[0];
        var backgroundImageId = $('#backgroundImageId').val();
        var userId = $('#userIdd').val();

        var formData = new FormData();
        formData.append('ImageId', backgroundImageId);
        formData.append('BackgroundImage', backgroundImage);

        showPreloader();

        $.ajax({
            url: `https://localhost:7255/api/AccountSettings/UpdateUserBacgroundImage/${backgroundImageId}`,
            type: 'PUT',
            data: formData,
            contentType: false,
            headers: {
                'Authorization': 'Bearer ' + token
            },
            processData: false, 
            success: function (response) {
                hidePreloader();
                reloadWithAlert();
            },
            error: function (xhr, status, error) {
                hidePreloader();
                console.error('Error:', error);
                Swal.fire({
                    icon: "error",
                    title: "Oops...",
                    text: "Something went wrong!",
                });
            }
        });
    });
});

