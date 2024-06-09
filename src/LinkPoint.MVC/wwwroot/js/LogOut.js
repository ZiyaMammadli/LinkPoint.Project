
    document.addEventListener('DOMContentLoaded', function () {
        var token = '@HttpContext.Session.GetString("JWToken")';
        if (token) {
            localStorage.setItem('token', token);
        }

        var logoutLink = document.querySelector('.dropdown a');
        if (logoutLink) {
            logoutLink.addEventListener('click', function (event) {
                event.preventDefault();
                logOut();
            });
        }
    });

    function logOut() {
        var token = localStorage.getItem('token');
        if (token) {
            fetch('https://localhost:7255/api/Account/LogOut', {
                method: 'GET',
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            })
                .then(response => {
                    if (response.ok) {  
                        localStorage.removeItem('token');
                        window.location.href = '/Account/Login';
                    } else {
                        console.error('Logout failed.');
                    }
                })
                .catch(error => {
                    console.error('Error:', error);
                });
        }
    }

