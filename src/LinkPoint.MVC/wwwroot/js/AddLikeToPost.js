document.addEventListener('DOMContentLoaded', function () {
    document.querySelectorAll('.addlike').forEach(button => {
        button.addEventListener('click', function (event) {
            event.preventDefault(); 
            const postContainer = this.closest('.reaction');
            const postId = postContainer.getAttribute('data-post-id');
            const userId = postContainer.getAttribute('data-user-id');
            const likeCountSpan = postContainer.querySelector('.like-count');
            var token = $('#tokenidd').val();
            let endpoint;

            if (this.classList.contains('text-green')) {
                endpoint = `https://localhost:7255/api/Likes/AddLikeToPost/${userId}/${postId}`;
                this.classList.remove('text-green');
                this.classList.add('text-red');
            } else if (this.classList.contains('text-red')) {
                endpoint = `https://localhost:7255/api/Likes/RemoveLikeFromPost/${userId}/${postId}`;
                this.classList.remove('text-red');
                this.classList.add('text-green');
            }

            fetch(endpoint, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${token}`
                }
            })
                .then(response => response.json())
                .then(data => {
                        likeCountSpan.textContent = data;
                })
                .catch(error => {
                    Swal.fire({
                        position: "top-end",
                        icon: "error",
                        title: ``+ error +``,
                        showConfirmButton: false,
                        timer: 1500
                    });
                    console.error('Request failed:', error);
                });
        });
    });
});