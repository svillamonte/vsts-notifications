using System;
using VstsNotifications.Models.Payloads;

namespace VstsNotifications.Services.Interfaces
{
    public interface ILinksMapper
    {
        Uri GetWebUrl(Links links);
    }
}
