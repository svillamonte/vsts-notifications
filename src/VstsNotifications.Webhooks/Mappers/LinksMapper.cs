using System;
using VstsNotifications.Models.Payloads;
using VstsNotifications.Webhooks.Interfaces;

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
