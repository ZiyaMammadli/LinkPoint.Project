$(document).ready(function () {
    // Icons click event
    $('#compose-icon').on('click', function (event) {
        event.preventDefault();
        $('#textModal').modal('show');
    });

    $('#images-icon').on('click', function (event) {
        event.preventDefault();
        $('#imageModal').modal('show');
    });

    $('#video-icon').on('click', function (event) {
        event.preventDefault();
        $('#videoModal').modal('show');
    });
})


//Test Create Box
$('#CreateTextPost').on('click', function () {
    var textData = {
        userId: $('#user-idd').val(),
        text: $('#postTextt').val(),
    };
    var token = $('#tokenidd').val();

    $.ajax({
        url: 'https://localhost:7255/api/Posts/CreatePostWithText',
        type: 'POST',
        contentType: 'application/json',
        headers: {
            'Authorization': 'Bearer ' + token
        },
        data: JSON.stringify(textData),
        success: function (response) {
            Swal.fire({
                position: "top-end",
                icon: "success",
                title: "Succesfully Created",
                showConfirmButton: false,
                timer: 1500
            });
        },
        error: function (xhr, status, error) {
            var errors = JSON.parse(xhr.responseText).errors;
            $('#textValidationErrorss').html('');
            for (var key in errors) {
                if (errors.hasOwnProperty(key)) {
                    $('#textValidationErrorss').append('<p>' + errors[key] + '</p>');
                }
            }
        }
    });
});

// Submit Text Post
$('#submitTextPost').on('click', function () {
    var textData = {
        userId: $('#user-id').val(),       
        text: $('#postText').val(),
    };
    var token = $('#tokenid').val();

    $.ajax({
        url: 'https://localhost:7255/api/Posts/CreatePostWithText',
        type: 'POST',
        contentType: 'application/json',
        headers: {
            'Authorization': 'Bearer ' + token
        },
        data: JSON.stringify(textData),
        success: function (response) {
            Swal.fire({
                position: "top-end",
                icon: "success",
                title: "Succesfully Created",
                showConfirmButton: false,
                timer: 1500
            });
            $('#textModal').modal('hide');
        },  
        error: function (xhr, status, error) {
            var errors = JSON.parse(xhr.responseText).errors;
            $('#textValidationErrors').html('');
            for (var key in errors) {
                if (errors.hasOwnProperty(key)) {
                    $('#textValidationErrors').append('<p>' + errors[key] + '</p>');
                }
            }
        }
    });
});

// Submit Image Post
$('#submitImagePost').on('click', function () {
    var formData = new FormData();
    formData.append('UserId', $('#user-id-forimage').val());
    formData.append('Text', $('#imageText').val());
    formData.append('PostImageFile', $('#imageFile')[0].files[0]);

    var token = $('#tokenid-forimage').val();
    $('#spinner-wrapper').show();

    $.ajax({
        url: 'https://localhost:7255/api/Posts/CreatePostWithImage',
        type: 'POST',
        data: formData,
        processData: false,
        contentType: false,
        headers: {
            'Authorization': 'Bearer ' + token
        },
        success: function (response) {
            location.reload();
            $('#spinner-wrapper').hide();
            Swal.fire({
                position: "top-end",
                icon: "success",
                title: "Succesfully Created",
                showConfirmButton: false,
                timer: 1500
            });
            $('#imageModal').modal('hide');
        },
        error: function (xhr, status, error) {
            $('#spinner-wrapper').hide();
            var errors = JSON.parse(xhr.responseText).errors;
            $('#imageValidationErrors').html('');
            for (var key in errors) {
                if (errors.hasOwnProperty(key)) {
                    $('#imageValidationErrors').append('<p>' + errors[key] + '</p>');
                }
            }
        }
    });
});

// Submit Video Post
$('#submitVideoPost').on('click', function () {
    var formData = new FormData();
    formData.append('UserId', $('#user-id-forvideo').val());
    formData.append('Text', $('#videoText').val());
    formData.append('PostVideoFile', $('#videoFile')[0].files[0]);

    var token = $('#tokenid-forvideo').val();
    $('#spinner-wrapper').show();

    $.ajax({
        url: 'https://localhost:7255/api/Posts/CreatePostWithVideo',
        type: 'POST',
        data: formData,
        processData: false,
        contentType: false,
        headers: {
            'Authorization': 'Bearer ' + token
        },
        success: function (response) {
            location.reload();
            $('#spinner-wrapper').hide();
            Swal.fire({
                position: "top-end",
                icon: "success",
                title: "Succesfully Created",
                showConfirmButton: false,
                timer: 1500
            });
            $('#videoModal').modal('hide');
        },
        error: function (xhr, status, error) {
            $('#spinner-wrapper').hide();
            var errors = JSON.parse(xhr.responseText).errors;
            $('#videoValidationErrors').html('');
            for (var key in errors) {
                if (errors.hasOwnProperty(key)) {
                    $('#videoValidationErrors').append('<p>' + errors[key] + '</p>');
                }
            }
        }
    });
});