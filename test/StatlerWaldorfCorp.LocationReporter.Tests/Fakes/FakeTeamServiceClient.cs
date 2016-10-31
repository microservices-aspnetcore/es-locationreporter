using System;
using StatlerWaldorfCorp.LocationReporter.Services;

namespace StatlerWaldorfCorp.LocationReporter.Tests.Fakes
{
    public class FakeTeamServiceClient : ITeamServiceClient
    {
        private Guid teamGuid;

        public FakeTeamServiceClient() {
            teamGuid = Guid.NewGuid();
        }
    
        public Guid GetTeamForMember(Guid memberId)
        {
            return teamGuid;
        }

        public Guid FixedID 
        {
            get {
                return teamGuid;
            }
        }
    }
}