using System;

namespace StatlerWaldorfCorp.LocationReporter.Models
{
    public class CommandEventConverter
    {
        public MemberLocationRecordedEvent CommandToEvent(LocationReport locationReport) 
        {
            MemberLocationRecordedEvent locationRecordedEvent = new MemberLocationRecordedEvent {
                Latitude = locationReport.Latitude,
                Longitude = locationReport.Longitude,
                Origin = locationReport.Origin,
                MemberID = locationReport.MemberID,
                RecordedTime = DateTime.Now.ToUniversalTime().Ticks
            };

           return locationRecordedEvent;
        }
    }
}