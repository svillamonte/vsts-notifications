using System;
using VstsNotifications.Webhooks.Interfaces;
using VstsNotifications.Webhooks.Models;

namespace VstsNotifications.Webhooks.Mappers
{
    public class LinksMapper : ILinksMapper
    {
        public Uri GetWebUrl(Links links)
        {
            return links.Web.Url;
        }
    }
}
