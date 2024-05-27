document.addEventListener('DOMContentLoaded', function () {
    document.querySelectorAll('.like-count').forEach(likeCountElement => {
        likeCountElement.addEventListener('click', function (event) {
            event.preventDefault(); // Sayfanın yeniden yüklenmesini önler
            const reactionContainer = this.closest('.reaction');
            const postId = reactionContainer.getAttribute('data-post-id');
            const userId = reactionContainer.getAttribute('data-user-id');
            const token = document.getElementById('tokenidd').value;
            const dropdown = document.getElementById(`like-users-dropdown-` + postId);
            fetch(`https://localhost:7255/api/Likes/GetAllUsersLikedPost/${postId}`, {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${token}`
                }
            })
                .then(response => response.json())
                .then(data => {
                        dropdown.innerHTML = ''; // Mevcut içeriği temizle
                        data.forEach(d => {
                            const userElement = document.createElement('div');
                            userElement.classList.add('like-user');
                            userElement.innerHTML = `
                                <img src="${d.userProfilImage}" alt="${d.userName}'s profile image" class="profile-photo-sm"/>
                                <a href="" class="profile-link">${d.userName}</a>
                            `;
                            dropdown.appendChild(userElement);
                        });
                        dropdown.classList.add('show'); // Dropdown menüyü göster
                    
                })
                .catch(error => {
                    console.error('Request failed:', error);
                });
        });
    });

    // Sayfa dışında bir yere tıklanırsa dropdown menüyü kapat
    document.addEventListener('click', function (event) {
        const dropdowns = document.querySelectorAll('.like-users-dropdown');
        dropdowns.forEach(dropdown => {
            if (!dropdown.contains(event.target) && !event.target.classList.contains('like-count')) {
                dropdown.classList.remove('show');
            }
        });
    });
});