using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp10.Configuration;
using ConsoleApp10.RestApiHelpers;

namespace ConsoleApp10
{
    class Program
    {
        static void Main(string[] args)
        {
            var repositoryName = "automation-mvn-sol-snapshot-local"; //Input from user
            int firstNElements = 2; //Input from user (first two with highest download counts)
          

            var wantedOutput = GetDescendingOrderOfArtifacts(repositoryName, firstNElements);

        }

        private static object GetDescendingOrderOfArtifacts(string repositoryName, int firstNElements)
        {
            var output = new Dictionary<string, int>();
            var api = new CommunicationApi(new Config());
            var artifacts = api.PostRepositories(repositoryName).results;

            foreach (var artifact in artifacts)
            {
                if (artifact.name.EndsWith(".jar"))
                {
                    var name = (artifact.repo + "/" + artifact.path + "/" + artifact.name);
                    var downloads = api.GetAtrifactDetails(name);
                    //Based on my understanding the artifacts will be unique
                    output.Add(downloads.uri, downloads.downloadCount);
                }

            }
            api.Dipose();

          return output.OrderByDescending(x => x.Value).Take(firstNElements);
        }
    }
}
