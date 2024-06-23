$(document).ready(function () {
    var button = $('#UnfollowButton');
    var userId = button.data('user-id');
    var followingUserId = button.data('following-user-id');
    var token = $('#tokenidd').val();

    var preloader = $('#spinner-wrapper');
    var preloaderFadeOutTime = 500;
    function reloadWithAlert() {

        location.reload();

    }
    button.on('click', function (event) {
        event.preventDefault();



        Swal.fire({
            title: 'Are you sure?',
            text: "Do you want to Unfollow?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, want unfollow!'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: `https://localhost:7255/api/FriendShips/Unfollow/${userId}/${followingUserId}`,
                    type: 'DELETE',
                    headers: {
                        'Authorization': 'Bearer ' + token
                    },
                    success: function (data) {
                        button.text('Add Friend');                        
                        reloadWithAlert();
                        
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
    });
});
