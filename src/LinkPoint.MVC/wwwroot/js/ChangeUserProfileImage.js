$(document).ready(function () {
    // Profil resmini silme
    $('#deleteProfileImageBtn').click(function () {
        var profileImageId = $('#profileImageId').val();
        var userId = $('#userId').val();

        var profileImageDeleteDto = {
            ImageId: profileImageId,
            UserId: userId
        };

        $.ajax({
            url: `https://localhost:7255/api/AccountSettings/DeleteUserProfileImage/${profileImageId}`,
            type: 'DELETE',
            contentType: 'application/json',
            data: JSON.stringify(profileImageDeleteDto),
            success: function (response) {
                Swal.fire({
                    position: "top-end",
                    icon: "success",
                    title: "Successfully Deleted",
                    showConfirmButton: false,
                    timer: 1500
                }).then(() => {
                    location.reload();
                });
            },
            error: function (xhr, status, error) {
                console.error('Error:', error);
                Swal.fire({
                    icon: "error",
                    title: "Oops...",
                    text: "Something went wrong!",
                });
            }
        });
    });

    // Profil resmini güncelleme
    $('#updateProfileImageBtn').click(function () {
        var profileImage = $('#profileImage')[0].files[0];
        var profileImageId = $('#profileImageId').val();
        var userId = $('#userId').val();

        var formData = new FormData();
        formData.append('ImageId', profileImageId);
        formData.append('ProfileImage', profileImage);

        $.ajax({
            url: `https://localhost:7255/api/AccountSettings/UpdateUserProfileImage/${profileImageId}`,
            type: 'PUT',
            data: formData,
            contentType: false, // FormData ile çalışırken false olmalı
            processData: false, // FormData ile çalışırken false olmalı
            success: function (response) {
                Swal.fire({
                    position: "top-end",
                    icon: "success",
                    title: "Successfully Updated",
                    showConfirmButton: false,
                    timer: 1500
                }).then(() => {
                    location.reload();
                });
            },
            error: function (xhr, status, error) {
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
