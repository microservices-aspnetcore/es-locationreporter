using System;

namespace StatlerWaldorfCorp.LocationReporter.Models
{
    public class MemberLocationRecordedEvent
    {
        public String Origin { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Guid MemberID { get; set; }
        public long RecordedTime { get; set; }
    }
}
