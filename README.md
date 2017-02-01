# Visual Studio Team Services notifications

Desktop notifications for Visual Studio Team Services events.

[![Build Status](https://travis-ci.org/svillamonte/vsts-notifications.svg?branch=master)](https://travis-ci.org/svillamonte/vsts-notifications)

This is a simple ASP.NET Core webapp that posts messages into the configured Slack channel triggered by the following events (more to come!):
* _Pull requests created._ Notifies involved reviewers of the creation of a pull request assigned to them:

![Pull request message](http://httpsimage.com/img/vsts-notifications.png)

## Configuration

The `appsettings.json` file has to be edited adding the members of your team you want to be notified and the Slack integration webhook URL.

### Adding Contributors

To do this, simply replace the existing `Contributors` under the `Settings` section with a JSON object of the form

```json
{ "Id": "[VSTS identifier]", "SlackUserId": "[Slack identifier]" }
```

From this:
* VSTS identifier is usually the login of the user.
* Slack identifier is the `id` property from each user instance in the list of the `users.list` method from Slack API. Get a list of all users in your team [here](https://api.slack.com/methods/users.list).

### Slack integration

Under the `Settings` section, replace the `SlackWebhookUrl` with your integration of choice.

_With :heart:_
