using System.Collections.Generic;
using VstsNotifications.Entities;
using VstsNotifications.Services.Models;

namespace VstsNotifications.Services.Interfaces
{
    public interface IPullRequestMessageService
    {
        //
        // Summary:
        //     /// Verifies that a string starts with a given string, using the given comparison
        //     type. ///
        //
        // Parameters:
        //   expectedStartString:
        //     The string expected to be at the start of the string
        //
        //   actualString:
        //     The string to be inspected
        //
        //   comparisonType:
        //     The type of string comparison to perform
        //
        // Exceptions:
        //   T:Xunit.Sdk.ContainsException:
        //     Thrown when the string does not start with the expected string
        IEnumerable<PullRequestMessage> CreatePullRequestMessages(PullRequestInfo pullRequestInfo, IEnumerable<Contributor> contributorsInfo);
    }
}
