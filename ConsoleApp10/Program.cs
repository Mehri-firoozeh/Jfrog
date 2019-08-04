using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp10.RestApiHelpers;

namespace ConsoleApp10
{
    class Program
    {
        static void Main(string[] args)
        {
            var repositoryFromUser = "automation-mvn-sol-snapshot-local"; //Input from user
            int firstNElements = 2; //Input from user ( first two with highest download counts)
            var output = new Dictionary<string, int>();
            var api = new CommunicationApi();

            var artifacts = api.PostRepositories(repositoryFromUser).results;

            foreach (var artifact in artifacts)
            {
                if (artifact.name.EndsWith(".jar"))
                {
                    var name = (artifact.repo + "/" + artifact.path + "/" + artifact.name);
                    var downloads = api.GetAtrifactDetails(name);
                    output.Add(downloads.uri, downloads.downloadCount);
                }

            }
            var nMostPopular = output.OrderByDescending(x => x.Value).Take(firstNElements);
        }


    }
}
