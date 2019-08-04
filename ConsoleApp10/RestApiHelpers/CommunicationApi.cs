using System;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using ConsoleApp10.RestApiHelpers.Models;
using ConsoleApp10.Configuration;

namespace ConsoleApp10.RestApiHelpers
{
    public class CommunicationApi
    {
        public CommunicationApi(IConfig configuration)
        {
            config = configuration ?? throw new ArgumentNullException(nameof(configuration));
            Initialize();

        }

        /// <summary>
        /// Search in a Repository 
        /// <param name="repository"></param>
        /// <returns></returns>
        public Repsitory PostRepositories(string repository)
        {

            string data = string.Format("items.find({{\"repo\":{{\"$eq\":\"{0:S}\"}}}})", repository);
            var url = config.GetBaseUrl() + "search/aql";
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
            var url = config.GetBaseUrl() + $"search/gavc?g=org.jfrog.test&repos={name} ";
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
            var url = config.GetBaseUrl() + $"storage/{name}?stats";
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

        public void Dipose()
        {
            client.Dispose();
        }

        private void Initialize()
        {
            client = new HttpClient();
            
            var credentialsFromConfig = string.Format("{0}:{1}", config.GetUserName(), config.GetPassword());

            byte[] cred = UTF8Encoding.UTF8.GetBytes(credentialsFromConfig);
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(cred));
            
        }

        private readonly IConfig config;
        private HttpClient client { get; set; }
    }
}
