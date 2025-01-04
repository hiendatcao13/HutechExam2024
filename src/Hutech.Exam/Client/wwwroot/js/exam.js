
function playAudio(elementId, so_lan_nghe) {
    const audioElement = document.getElementById(elementId);
    audioElement.currentTime = 0;
    audioElement.play();
    if (so_lan_nghe >= 3) {
        audioElement.pause(); // Dừng audio lại nếu sv không muốn nhiều ở lần thứ 3
        audioElement.currentTime = 0; 
    }
}

function changeColorTime() {
    const clockElement = document.getElementById("time-clock");
    clockElement.style.color = "red";
    clockElement.style.fontWeight = 600;
}

// focus trang thi
function focusPage(dotNetObjRef) {
    $(document).on('visibilitychange', function () {

        if (document.visibilityState == 'hidden') {
            console.log("hello");
            dotNetObjRef.invokeMethodAsync('ketThucThoiGianLamBai');
        } else {
            // page is visible
        }
    });
}

