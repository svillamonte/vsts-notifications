using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace VstsNotifications.Webhooks.Models.PullRequest
{
    public class PullRequestResource
    {
        public PullRequestResource()
        {          
            Reviewers = new Collection<Reviewer>();
        }

        [JsonProperty("repository")]
        public Repository Repository { get; set; }

        [JsonProperty("pullRequestId")]
        public int PullRequestId { get; set; }

        [JsonProperty("codeReviewId")]
        public int CodeReviewId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("createdBy")]
        public Contributor CreatedBy { get; set; }

        [JsonProperty("creationDate")]
        public DateTime CreationDate { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("sourceRefName")]
        public string SourceRefName { get; set; }

        [JsonProperty("targetRefName")]
        public string TargetRefName { get; set; }

        [JsonProperty("mergeStatus")]
        public string MergeStatus { get; set; }

        [JsonProperty("mergeId")]
        public string MergeId { get; set; }

        [JsonProperty("lastMergeSourceCommit")]
        public Commit LastMergeSourceCommit { get; set; }

        [JsonProperty("lastMergeTargetCommit")]
        public Commit LastMergeTargetCommit { get; set; }

        [JsonProperty("reviewers")]
        public ICollection<Reviewer> Reviewers { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("supportsIterations")]
        public bool SupportsIterations { get; set; }
    }
}
