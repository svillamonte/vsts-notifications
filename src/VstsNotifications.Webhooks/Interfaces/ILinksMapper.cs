using System;
using VstsNotifications.Models.Payloads;

namespace VstsNotifications.Webhooks.Interfaces
{
    public interface ILinksMapper
    {
        Uri GetWebUrl(Links links);
    }
}
