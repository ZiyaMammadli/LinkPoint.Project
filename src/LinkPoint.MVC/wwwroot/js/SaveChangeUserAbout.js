$(document).ready(function () {
    var token = $('#Token-id').val();
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
            title: "Succesfully!",
            text: "Your information has been changed",
            icon: "success"
        })

    }
    $('#UserAboutButton').click(function () {
        var useraboutId = $('#UserAbout-iD').val();
        var userId = $('#User-iD').val();
        var aboutMe = $('#my-info').val();
        var country = $('#country').val();
        var city = $('#city').val();
        var male = document.getElementById('maleid');
        var female = document.getElementById('femaleid');

        var day = document.getElementById('day').value;
        if (day > 0 && day < 10) {
            day="0"+day
        }
        var month = document.getElementById('month').value;
        if (month > 0 && month < 10) {
            month ="0"+month
        }
        const year = document.getElementById('year').value;

        if (day && month && year) {
            var dateOfBirth = `${year}-${month}-${day}`;
        }

        var userAboutPutDto = {
            Id: useraboutId,
            UserId: userId,
            AboutMe: aboutMe,
            CityName: city,
            CountryName: country,
            Male: male.checked,
            Female: female.checked,
            BirthDate: dateOfBirth
        };

        showPreloader();

        $.ajax({
            url: `https://localhost:7255/api/AccountSettings/UpdateUserAbout/${useraboutId}`,
            type: 'PUT',
            contentType: 'application/json',
            headers: {
                'Authorization': 'Bearer ' + token
            },
            data: JSON.stringify(userAboutPutDto),
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

