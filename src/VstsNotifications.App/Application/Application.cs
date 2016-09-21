using System;
using Microsoft.Extensions.DependencyInjection;
using VstsNotifications.Services;
using VstsNotifications.Services.Interfaces;

namespace VstsNotifications.App.Application
{
    public class Application
    {
        private readonly ISlackClient _slackClient;

        public Application ()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<ISlackClient, SlackClient>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            _slackClient = serviceProvider.GetService<ISlackClient>();
        }

        public void Run()
        {
            _slackClient.PostMessage();
            Console.WriteLine("Hello World!");
        }
    }
}