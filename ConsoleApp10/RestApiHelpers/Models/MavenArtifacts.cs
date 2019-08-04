using System.Collections.Generic;

namespace ConsoleApp10.RestApiHelpers.Models
{
    public class UriLink
    {
        public string uriAddress { get; set; }
    }

    public class MavenArtifacts
    {
        public List<UriLink> uriLinks { get; set; }
    }
}
