using System;

namespace StatlerWaldorfCorp.LocationReporter.Services
{
    public class HttpTeamServiceClient : ITeamServiceClient
    {
        public Guid GetTeamForMember(Guid memberId)
        {
            // TODO replace this with actual HTTP call.
            return Guid.Parse("4aa83886-b795-4e3a-aa9f-e532a32c7163");
        }
    }
}