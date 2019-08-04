using System;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using ConsoleApp10.RestApiHelpers.Models;

namespace ConsoleApp10.RestApiHelpers
{
    public class CommunicationApi
    {
        public CommunicationApi()
        {
            client = new HttpClient();
            byte[] cred = UTF8Encoding.UTF8.GetBytes(credentialsFromConnectionString);
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(cred));

        }

        /// <summary>
        /// Search in a Repository 
        /// <param name="repository"></param>
        /// <returns></returns>
        public Repsitory PostRepositories(string repository)
        {
            string data = string.Format("items.find({{\"repo\":{{\"$eq\":\"{0:S}\"}}}})", repository);
            var url = baseUrlFromConnectionString + "search/aql";
            try
            {
                HttpContent content = new StringContent(data, UTF8Encoding.UTF8, "text/plain");
                var postResponse = client.PostAsync(url, content).Result;
                if (postResponse.IsSuccessStatusCode)
                {
                    var messageContent = postResponse.Content.ReadAsStringAsync().Result;
                     return JsonConvert.DeserializeObject<Repsitory>(messageContent);
                }

                return new Repsitory();

            }
            catch
            {
                throw new InvalidOperationException("Post call failed");
            }

        }

        /// <summary>
        /// Search by Maven coordinates: GroupId, ArtifactId, Version & Classifier.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public MavenArtifacts GetArtifactsForMavenRepository(string name)
        {
            //TODO: Add List of parameter to pass GroupId, ArtifactId, Version & Classifier.
            HttpClient client = new HttpClient();
            var url = baseUrlFromConnectionString + $"search/gavc?g=org.jfrog.test&repos={name} ";
            try
            {
                var result = client.GetStringAsync(url).Result;

                return JsonConvert.DeserializeObject<MavenArtifacts>(result);

            }
            catch
            {
                throw new InvalidOperationException("Get call failed");
            }
        }

        /// <summary>
        /// Retrieves Artifact details such as number of Downloads
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ArtifactDetail GetAtrifactDetails(string name)
        {
            var url = baseUrlFromConnectionString + $"storage/{name}?stats";
            try
            {
                var result = client.GetStringAsync(url).Result;
                return JsonConvert.DeserializeObject<ArtifactDetail>(result);

            }
            catch
            {
                throw new InvalidOperationException("Get call failed");
            }
        }

        private string baseUrlFromConnectionString = "http://34.67.247.50/artifactory/api/";
        private string credentialsFromConnectionString = "admin:2JvFZt70g1";
        private HttpClient client { get; set; }
    }
}
