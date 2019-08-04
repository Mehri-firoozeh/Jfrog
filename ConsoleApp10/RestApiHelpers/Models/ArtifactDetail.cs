using System;

namespace ConsoleApp10.RestApiHelpers.Models
{
    public class ArtifactDetail
    {
        public string uri { get; set; }
        public int downloadCount { get; set; }
        public Int64 lastDownloaded { get; set; }
        public int remoteDownloadCount { get; set; }
        public int remoteLastDownloaded { get; set; }
    }
}
