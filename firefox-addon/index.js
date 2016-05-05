var notifications = require("sdk/notifications");
var requests = require("sdk/request");

var i = 0;
var currentBuildId = 0;

var logCompletedNewBuild = function (response) {
  var buildDetails = response.json.value[0];

  if (currentBuildId === buildDetails.id) {
    return;
  }

  currentBuildId = buildDetails.id;

  notifications.notify({
    title: buildDetails.definition.name + " " + buildDetails.result,
    text: "Requested for " + buildDetails.requestedFor.displayName
  });
}

while (i++ < 10) {
  var lastCompletedBuildRequest = requests.Request({
    url: "https://timely.visualstudio.com/DefaultCollection/Timely/_apis/build/builds?$top=1&statusFilter=completed&api-version=2.0",
    headers: { authorization: "Basic c2ViYUBnZXR0aW1lbHkuY29tOnVsNE1iNHQwcg=="},
    onComplete: logCompletedNewBuild
  });

  lastCompletedBuildRequest.get();
}
