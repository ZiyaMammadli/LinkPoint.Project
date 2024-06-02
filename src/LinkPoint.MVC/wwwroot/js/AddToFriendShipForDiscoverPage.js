$(document).ready(function () {
    var token = $('#tokenidd').val();

    // id'si "AddFriendBTN_" ile başlayan her eleman üzerinde iterasyon
    $('[id^=AddFriendBTN_]').each(function () {
        var button = $(this);
        var userId = $('#user-idd').val();
        var followingUserId = button.data('following-user-id');

        // Arkadaşlık durumu kontrolü
        $.ajax({
            url: `https://localhost:7255/api/FriendShips/CheckFriendShipStatus/${userId}/${followingUserId}`,
            type: 'GET',
            headers: {
                'Authorization': 'Bearer ' + token
            },
            success: function (isFriend) {
                if (isFriend) {
                    button.text('Pending');
                }
            },
            error: function (xhr, status, error) {
                console.error('Request failed:', error);
            }
        });

        // Butona tıklama işlemi
        button.on('click', function (event) {
            event.preventDefault();

            if (button.text() === 'Pending') {
                Swal.fire({
                    title: 'Are you sure?',
                    text: "Do you want to cancel the friend request?",
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Yes, cancel request!'
                }).then((result) => {
                    if (result.isConfirmed) {
                        $.ajax({
                            url: `https://localhost:7255/api/FriendShips/CancelFriendShip/${userId}/${followingUserId}`,
                            type: 'DELETE',
                            headers: {
                                'Authorization': 'Bearer ' + token
                            },
                            success: function (data) {
                                button.text('Add Friend');
                                Swal.fire(
                                    'Cancelled!',
                                    'The friend request has been cancelled.',
                                    'success'
                                );
                            },
                            error: function (xhr, status, error) {
                                console.error('Request failed:', error);
                                Swal.fire(
                                    'Error!',
                                    'An error occurred while cancelling the friend request.',
                                    'error'
                                );
                            }
                        });
                    }
                });
            } else {
                Swal.fire({
                    title: 'Are you sure?',
                    text: "Do you want to add this user as a friend?",
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Yes, add friend!'
                }).then((result) => {
                    if (result.isConfirmed) {
                        $.ajax({
                            url: `https://localhost:7255/api/FriendShips/AddToFriendShip/${userId}/${followingUserId}`,
                            type: 'POST',
                            headers: {
                                'Authorization': 'Bearer ' + token
                            },
                            success: function (data) {
                                button.text('Pending');
                                Swal.fire(
                                    'Added!',
                                    'The user has been added as a friend.',
                                    'success'
                                );
                            },
                            error: function (xhr, status, error) {
                                console.error('Request failed:', error);
                                Swal.fire(
                                    'Error!',
                                    'An error occurred while adding the friend.',
                                    'error'
                                );
                            }
                        });
                    }
                });
            }
        });
    });
});