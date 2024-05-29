$(document).ready(function () {
    var token = $('#tokenidforprofileimage').val();
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
    $('#deleteProfileImageBtn').click(function () {
        var profileImageId = $('#profileImageId').val();
        var userId = $('#userId').val();

        var profileImageDeleteDto = {
            ImageId: profileImageId,
            UserId: userId
        };

        showPreloader();

        $.ajax({
            url: `https://localhost:7255/api/AccountSettings/DeleteUserProfileImage/${profileImageId}`,
            type: 'DELETE',
            contentType: 'application/json',
            headers: {
                'Authorization': 'Bearer ' + token
            },
            data: JSON.stringify(profileImageDeleteDto),
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

    $('#updateProfileImageBtn').click(function () {
        var profileImage = $('#profileImage')[0].files[0];
        var profileImageId = $('#profileImageId').val();
        var userId = $('#userId').val();

        var formData = new FormData();
        formData.append('ImageId', profileImageId);
        formData.append('ProfileImage', profileImage);

        showPreloader();

        $.ajax({
            url: `https://localhost:7255/api/AccountSettings/UpdateUserProfileImage/${profileImageId}`,
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
