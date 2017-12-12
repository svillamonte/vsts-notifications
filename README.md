# Visual Studio Team Services notifications

Desktop notifications for Visual Studio Team Services events.

[![Build Status](https://travis-ci.org/svillamonte/vsts-notifications.svg?branch=master)](https://travis-ci.org/svillamonte/vsts-notifications)

This is a simple ASP.NET Core webapp that posts messages into the configured Slack channel triggered by the following events (more to come!):
* _Pull requests created._ Notifies involved reviewers of the creation of a pull request assigned to them. If no reviewer matches any of the registered contributors, a notification is sent to a configured user group.

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

### Default user group

If no contributor is found, a user group is used for notifying instead, following this simple JSON structure:

```json
{ "SlackHandle": "[Slack handle]", "SlackUserGroupId": "[Slack user group identifier]" }
```

Both values can be found from the [list of user groups in your team](https://api.slack.com/methods/usergroups.list), where you are interested on the `handle` and `id` properties of the group you want to set as default.

### Slack integration

Under the `Settings` section, replace the `SlackWebhookUrl` with your integration of choice.

### VSTS webhooks

How to configure VSTS so that it is able to send requests to our webhooks:
* _Pull requests created._ You need to add a new service hook for the "Pull request created" event using the following URL: `http://[Your public domain]/api/pullrequestcreated`.

## Thank you!

_Hope you find this useful!_
