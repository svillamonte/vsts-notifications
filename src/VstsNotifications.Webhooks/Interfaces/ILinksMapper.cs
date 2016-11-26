using System;
using VstsNotifications.Webhooks.Models;

namespace VstsNotifications.Webhooks.Interfaces
{
    public interface ILinksMapper
    {
        Uri GetWebUrl(Links links);
    }
}
