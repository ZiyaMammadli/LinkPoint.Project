document.addEventListener('DOMContentLoaded', function () {
    var videos = document.querySelectorAll('.post-video');
    var userInteracted = false;

    function isElementInViewport(el) {
        var rect = el.getBoundingClientRect();
        var windowHeight = (window.innerHeight || document.documentElement.clientHeight);
        var windowWidth = (window.innerWidth || document.documentElement.clientWidth);

        var centerY = rect.top + rect.height / 2;
        var centerX = rect.left + rect.width / 2;

        return (
            centerY >= 0 &&
            centerY <= windowHeight &&
            centerX >= 0 &&
            centerX <= windowWidth
        );
    }

    function checkVideos() {
        if (!userInteracted) return;

        videos.forEach(function (video) {
            if (isElementInViewport(video)) {
                video.play().catch(function (error) {
                    console.error('Video play failed:', error);
                });
            } else {
                if (!video.paused) {
                    video.pause();
                    video.currentTime = 0;
                }
            }
        });
    }

    function handleUserInteraction() {
        userInteracted = true;
        document.removeEventListener('click', handleUserInteraction);
        document.removeEventListener('keydown', handleUserInteraction);
        checkVideos();
    }

    document.addEventListener('click', handleUserInteraction);
    document.addEventListener('keydown', handleUserInteraction);

    window.addEventListener('scroll', checkVideos);
    window.addEventListener('resize', checkVideos);

    // Initial check in case some videos are in viewport on page load
    checkVideos();
});
