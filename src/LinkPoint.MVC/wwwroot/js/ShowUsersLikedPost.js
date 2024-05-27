$(document).ready(function () {
    $('.like-count').on('click', function (event) {
        event.preventDefault(); 

        const reactionContainer = $(this).closest('.reaction');
        let postId = reactionContainer.data('post-id');
        const token = $('#tokenidd').val();

        $.ajax({
            url: `https://localhost:7255/api/Likes/GetAllUsersLikedPost/${postId}`,
            type: 'GET',
            headers: {
                'Authorization': 'Bearer ' + token
            },
            success: function (data) {
                    const likeUsersContainer = $('#like-users-container');
                    likeUsersContainer.html(''); 

                data.forEach(d => {

                    const userElement = `
                        <div class="like-user">
                            <img src="${d.userProfilImage}" alt="${d.userName}'s profile image" class="profile-photo-sm"/>
                            <a href="" class="profile-link">${d.userName}</a>
                        </div>
                        `;
                        likeUsersContainer.append(userElement);
                    });

                    $('#likeUsersModal').modal('show'); 
            },
            error: function (xhr, status, error) {
                console.error('İstek başarısız:', error);
            }
        });
    });
});
