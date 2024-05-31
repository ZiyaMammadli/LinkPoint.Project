$(document).ready(function () {
    var token = $('#TokeN-iD').val();
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
    $('#workNull-button').click(function () {
        var userId = $('#UseR-ID').val();
        var company = $('#company').val();
        var from = $('#from-date').val();
        var to = $('#to-date').val();
        var desc = $('#work-description').val();
        var designation = $('#designation').val();

        if (from == "") {
            from=null
        }
        if (to == "") {
            to = null
        }

        var userWorkPostDto = {
            UserId: userId,
            FromDate: from,
            ToDate: to,
            Company: company,
            Description: desc,
            Designation: designation,
        };

        showPreloader();

        $.ajax({
            url: `https://localhost:7255/api/AccountSettings/CreateUserWork`,
            type: 'POST',
            contentType: 'application/json',
            headers: {
                'Authorization': 'Bearer ' + token
            },
            data: JSON.stringify(userWorkPostDto),
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
    $('#work-button').click(function () {
        var userworkId = $('#UserWork-iD').val();
        var userId = $('#UseR-ID').val();
        var company = $('#company').val();
        var from = $('#from-date').val();
        var to = $('#to-date').val();
        var desc = $('#work-description').val();
        var designation = $('#designation').val();

        var userWorkPutDto = {
            Id: userworkId,
            UserId: userId,
            FromDate: from,
            ToDate: to,
            Company: company,
            Description: desc,
            Designation: designation,
        };

        showPreloader();

        $.ajax({
            url: `https://localhost:7255/api/AccountSettings/UpdateUserWork/${userworkId}`,
            type: 'PUT',
            contentType: 'application/json',
            headers: {
                'Authorization': 'Bearer ' + token
            },
            data: JSON.stringify(userWorkPutDto),
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



