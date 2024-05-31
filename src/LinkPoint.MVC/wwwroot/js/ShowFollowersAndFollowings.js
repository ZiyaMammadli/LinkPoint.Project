$(document).ready(function () {
    $('#followingsId').on('click', function (event) {
        event.preventDefault();
        var token = $('#tokenidd').val();
        var userId = $('#us-er-Id').val();

        if (!token || !userId) {
            console.error('Token veya User ID eksik!');
            return;
        }

        $.ajax({
            url: `https://localhost:7255/api/FriendShips/GetAllAcceptedFollowingUsers/${userId}`,
            type: 'GET',
            headers: {
                'Authorization': 'Bearer ' + token
            },
            success: function (data) {
                const followingUsersContainer = $('#following-users-container');
                followingUsersContainer.html('');

                data.forEach(d => {
                    const userElement = `
                        <div class="following-user">
                            <img src="${d.profileImageUrl}" alt="${d.userName}'s profile image" class="profile-photo-sm"/>
                            <a href="" class="profile-link">${d.userName}</a>
                        </div>
                    `;
                    followingUsersContainer.append(userElement);
                });

                $('#followingUsersModal').modal('show');
            },
            error: function (xhr, status, error) {
                console.error('İstek başarısız:', error);
            }
        });
    });
    $('#followersId').on('click', function (event) {
        event.preventDefault();
        var token = $('#tokenidd').val();
        var userId = $('#us-er-Id').val();

        console.log('Token:', token);
        console.log('UserId:', userId);

        if (!token || !userId) {
            console.error('Token veya User ID eksik!');
            return;
        }

        $.ajax({
            url: `https://localhost:7255/api/FriendShips/GetAllAcceptedFollowerUsers/${userId}`,
            type: 'GET',
            headers: {
                'Authorization': 'Bearer ' + token
            },
            success: function (data) {
                const followersUsersContainer = $('#followers-users-container');
                followersUsersContainer.html('');

                data.forEach(d => {
                    const userElement = `
                        <div class="follower-user">
                            <img src="${d.profileImageUrl}" alt="${d.userName}'s profile image" class="profile-photo-sm"/>
                            <a href="" class="profile-link">${d.userName}</a>
                        </div>
                    `;
                    followersUsersContainer.append(userElement);
                });

                $('#followersUsersModal').modal('show');
            },
            error: function (xhr, status, error) {
                console.error('İstek başarısız:', error);
            }
        });
    });
});
