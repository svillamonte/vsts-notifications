using System;
using VstsNotifications.Models.Payloads;
using VstsNotifications.Services.Interfaces;

namespace VstsNotifications.Services.Mappers
{
    public class LinksMapper : ILinksMapper
    {
        public Uri GetWebUrl(Links links)
        {
            return links.Web.Url;
        }
    }
}
