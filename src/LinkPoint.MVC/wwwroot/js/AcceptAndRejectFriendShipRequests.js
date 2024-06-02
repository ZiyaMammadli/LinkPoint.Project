$(document).ready(function () {
    var token = $('#tokenidd').val();
    var userId = $('#us-er-Id').val(); 

    // Pending kullanıcı isteklerini API'den almak
    $.ajax({
        url: `https://localhost:7255/api/FriendShips/GetAllPendingFollowerUsers/${userId}`,
        type: 'GET',
        headers: {
            'Authorization': 'Bearer ' + token
        },
        success: function (data) {
            const pendingRequestsContainer = $('#pendingRequestsContainer');
            pendingRequestsContainer.html(''); // Önceki içerikleri temizle

            data.forEach(user => {
                const userElement = `
                        <div class="user-profile" style="display: flex; align-items: center; margin: 10px 0;">
                            <img src="${user.profileImageUrl}" alt="User Profile Photo" class="profile-photo-sm" style="width: 50px; height: 50px; border-radius: 50%; margin-right: 10px;" />
                            <div style="flex-grow: 1;">
                                <span style="font-weight: bold;">${user.userName}</span>
                                <div style="display: flex; flex-direction: column; margin-top: 5px;">
                                    <button class="accept-btn btn btn-success" data-user-id="${user.friendShipId}" style="margin-bottom: 5px;padding: 1px">Accept</button>
                                    <button class="reject-btn btn btn-danger" data-user-id="${user.friendShipId}" style="padding: 1px">Reject</button>
                                </div>
                            </div>
                        </div>
                    `;
                pendingRequestsContainer.append(userElement);
            });

            // Accept butonuna tıklama işlemi
            $('.accept-btn').on('click', function () {
                const friendShipId = $(this).data('user-id');

                $.ajax({
                    url: `https://localhost:7255/api/FriendShips/AcceptFriendShipRequest/${friendShipId}`,
                    type: 'PUT',
                    headers: {
                        'Authorization': 'Bearer ' + token
                    },
                    success: function () {
                        alert('Friend request accepted!');
                        // Buton durumunu güncelleme veya div'i gizleme
                        $(this).closest('.user-profile').remove();
                    }.bind(this), // .bind(this) ile $(this) içindeki butona referans kalır
                    error: function (xhr, status, error) {
                        console.error('Request failed:', error);
                    }
                });
            });

            // Reject butonuna tıklama işlemi
            $('.reject-btn').on('click', function () {
                const friendShipId = $(this).data('user-id');

                $.ajax({
                    url: `https://localhost:7255/api/FriendShips/RejectFriendShipRequest/${friendShipId}`,
                    type: 'PUT',
                    headers: {
                        'Authorization': 'Bearer ' + token
                    },
                    success: function () {
                        alert('Friend request rejected!');
                        // Buton durumunu güncelleme veya div'i gizleme
                        $(this).closest('.user-profile').remove();
                    }.bind(this), // .bind(this) ile $(this) içindeki butona referans kalır
                    error: function (xhr, status, error) {
                        console.error('Request failed:', error);
                    }
                });
            });
        },
        error: function (xhr, status, error) {
            console.error('Request failed:', error);
        }
    });
});