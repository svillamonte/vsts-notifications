var currentBuildId = 0;

function checkApi() {
    var xhr = new XMLHttpRequest();
    
    xhr.open("GET", "https://timely.visualstudio.com/DefaultCollection/Timely/_apis/build/builds?$top=1&statusFilter=completed&api-version=2.0", true);
    xhr.setRequestHeader("Authorization", "Basic c2ViYUBnZXR0aW1lbHkuY29tOnVsNE1iNHQwcg==");
    
    xhr.onreadystatechange = function () {
        var buildDetails = JSON.parse(xhr.responseText).value[0];
        
        if (currentBuildId === buildDetails.id){
            return;
        }
        
        currentBuildId = buildDetails.id;
        console.log(buildDetails.id);
        chrome.notifications.create('notification', {
            type: 'basic',
            iconUrl: 'assets/app_icon_128.png',
            title: buildDetails.definition.name + ' ' + buildDetails.result,
            message: 'Requested by ' + buildDetails.requestedFor.displayName            
        }, function (notificationId) {});          
    }
    
    xhr.send();
}

chrome.alarms.create('worker', {
    delayInMinutes: 0.1, periodInMinutes: 0.1
});

chrome.app.runtime.onLaunched.addListener(function(){
    chrome.app.window.create('index.html', {
        id: 'main',
        bounds: { width: 640, height: 480 }
    });
});

chrome.alarms.onAlarm.addListener(function(alarm){
    checkApi();
});