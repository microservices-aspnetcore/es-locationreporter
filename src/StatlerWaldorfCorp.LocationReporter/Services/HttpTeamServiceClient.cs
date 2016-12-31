using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Linq;
using Steeltoe.Extensions.Configuration.CloudFoundry;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace StatlerWaldorfCorp.LocationReporter.Services
{
    public class HttpTeamServiceClient : ITeamServiceClient
    {
        private readonly Service teamServiceBinding;

        private readonly ILogger logger;

        private HttpClient httpClient;

        public HttpTeamServiceClient(
            IOptions<CloudFoundryServicesOptions> servicesOptions,
            ILogger<HttpTeamServiceClient> logger)
        {
            this.logger = logger;
               
            this.teamServiceBinding = servicesOptions.Value.Services.FirstOrDefault( s => s.Name == "teamservice");

            logger.LogInformation("Team Service HTTP client using URL {0}",
                teamServiceBinding.Credentials["url"].Value);

            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(teamServiceBinding.Credentials["url"].Value);
        }
        public Guid GetTeamForMember(Guid memberId)
        {                            
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = httpClient.GetAsync(String.Format("/members/{0}/team", memberId)).Result;

            TeamIDResponse teamIdResponse;
            if (response.IsSuccessStatusCode) {
                string json = response.Content.ReadAsStringAsync().Result;
                teamIdResponse = JsonConvert.DeserializeObject<TeamIDResponse>(json);
                return teamIdResponse.TeamID;
            }
            else {
                return Guid.Empty;
            }            
        }
    }

    public class TeamIDResponse
    {
        public Guid TeamID { get; set; }
    }
}