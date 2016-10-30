using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using VstsNotifications.Services;
using VstsNotifications.Services.Interfaces;
using VstsNotifications.Services.Wrappers;
using VstsNotifications.Webhooks.Interfaces;
using VstsNotifications.Webhooks.Mappers;
using VstsNotifications.Webhooks.Properties;

namespace VstsNotifications.Webhooks
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // App settings.
            services.Configure<Settings>(Configuration.GetSection("Settings"));

            AddMappers(services);
            AddServices(services);

            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }

        private void AddMappers(IServiceCollection services)
        {
            services.AddScoped<ICollaboratorMapper, CollaboratorMapper>();
            services.AddScoped<IMessageMapper, MessageMapper>();
            services.AddScoped<IPullRequestInfoMapper, PullRequestInfoMapper>();
        }

        private void AddServices(IServiceCollection services)
        {
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IPullRequestMessageService, PullRequestMessageService>();
            services.AddScoped<ISlackMessagePayloadService, SlackMessagePayloadService>();

            services.AddScoped<IHttpClient, HttpClientWrapper>();
            services.AddScoped<ISlackClient, SlackClient>();
        }
    }
}
