$(document).ready(function () {
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
    // Add interest
    $('#add-interest-btn').click(function () {
        var interest = $('#add-interest').val();
        var token = $('#TokeNiD').val();
        var userId = $('#UseRID').val();

        var userInterestPostDto = {
            UserId: userId,
            Interest: interest
        }

        if (interest) {
            showPreloader();
            $.ajax({
                url: 'https://localhost:7255/api/AccountSettings/CreateUserInterest',
                type: 'POST',
                contentType: 'application/json',
                headers: { 'Authorization': 'Bearer ' + token },
                data: JSON.stringify( userInterestPostDto ),
                success: function (response) {
                    // Add new interest to the list
                    $('ul.interests').append(
                        `<li>
                            <a href="#"><i class="icon ion-android-bicycle"></i>${interest}</a>
                            <i class="icon ion-close-circled delete-interest" data-interest="${interest}"></i>
                        </li>`
                    );
                    // Clear the input field
                    $('#add-interest').val('');
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
        }
    });

    // Delete interest
    $(document).on('click', '.delete-interest', function () {
        var token = $('#TokeNiD').val();
        var $li = $(this).closest('li');
        var interestId = $li.find('.interest-id').text();

        var userInterestDeleteDto = {
            Id: interestId
        }

        showPreloader();

        $.ajax({
            url: `https://localhost:7255/api/AccountSettings/DeleteUserInterest/${interestId}`, 
            type: 'DELETE',
            contentType: 'application/json',
            headers: { 'Authorization': 'Bearer ' + token },
            data: JSON.stringify(userInterestDeleteDto),
            success: function (response) {
                // Remove the interest from the list
                $li.remove();
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

