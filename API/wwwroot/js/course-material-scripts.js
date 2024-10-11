/* Under Maintenance */

// var tag = document.createElement('script');
// tag.src = "https://www.youtube.com/iframe_api";
// var firstScriptTag = document.getElementsByTagName('script')[0];
// firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);

// var player1;

// function onYouTubeIframeAPIReady() {
//     player1 = new YT.Player('video1', {
//         events: {
//             'onStateChange': onPlayerStateChange
//         }
//     });
// }

// function onPlayerStateChange(event) {
//     if (event.data == YT.PlayerState.ENDED) {
//         document.getElementById('lesson1').checked = true;
//     } else {
//         var duration = player1.getDuration();
//         var currentTime = player1.getCurrentTime();
        
//         if (duration - currentTime <= 60) {
//             document.getElementById('lesson1').checked = true;
//         }
//     }
// }

document.addEventListener('DOMContentLoaded', function() {
    // Select all watch buttons
    const watchButtons = document.querySelectorAll('.watch-btn');
    
    // Select the video iframe
    const videoPlayer = document.querySelector('.video-player iframe');
    
    // Loop through each button and add an event listener
    watchButtons.forEach(function(button) {
        button.addEventListener('click', function(event) {
            event.preventDefault(); // Prevent the default link behavior

            // Get the video ID from the data attribute
            const videoId = this.getAttribute('data-video-id');

            // Update the iframe src to load the new video
            videoPlayer.src = `https://www.youtube.com/embed/${videoId}?title=HTML`;
        });
    });
});

