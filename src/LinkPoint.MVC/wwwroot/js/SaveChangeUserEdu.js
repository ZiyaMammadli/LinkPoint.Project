$(document).ready(function () {
    var token = $('#TokeN-id').val();
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
    $('#eduNull-button').click(function () {
        var userId = $('#UseR-iD').val();
        var university = $('#school').val();
        var from = $('#date-from').val();
        var to = $('#date-to').val();
        var desc = $('#edu-description').val();
        var female = document.getElementById('graduate');

        if (from == "") {
            from = null
        }
        if (to == "") {
            to = null
        }

        var userEducationPostDto = {
            UserId: userId,
            FromDate: from,
            ToDate: to,
            University: university,
            Description: desc,
            Graduated: female.checked,
        };

        showPreloader();

        $.ajax({
            url: `https://localhost:7255/api/AccountSettings/CreateUserEducation`,
            type: 'POST',
            contentType: 'application/json',
            headers: {
                'Authorization': 'Bearer ' + token
            },
            data: JSON.stringify(userEducationPostDto),
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
    $('#edu-button').click(function () {
        var usereduId = $('#UserEducation-iD').val();
        var userId = $('#UseR-iD').val();
        var university = $('#school').val();
        var from = $('#date-from').val();
        var to = $('#date-to').val();
        var desc = $('#edu-description').val();
        var female = document.getElementById('graduate');

        var userEducationPutDto = {
            Id: usereduId,
            UserId: userId,
            FromDate: from,
            ToDate: to,
            University: university,
            Description: desc,
            Graduated: female.checked,
        };

        showPreloader();

        $.ajax({
            url: `https://localhost:7255/api/AccountSettings/UpdateUserEducation/${usereduId}`,
            type: 'PUT',
            contentType: 'application/json',
            headers: {
                'Authorization': 'Bearer ' + token
            },
            data: JSON.stringify(userEducationPutDto),
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


