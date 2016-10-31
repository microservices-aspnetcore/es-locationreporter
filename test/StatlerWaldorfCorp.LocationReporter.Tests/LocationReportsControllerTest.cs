using Xunit;
using System;
using StatlerWaldorfCorp.LocationReporter.Controllers;
using StatlerWaldorfCorp.LocationReporter.Models;
using StatlerWaldorfCorp.LocationReporter.Tests.Fakes;
using Microsoft.AspNetCore.Mvc;

namespace StatlerWaldorfCorp.LocationReporter.Tests
{
    public class LocationReportsControllerTest
    {
        [Fact]
        public void ControllerInvokesEventEmitterAndReturnsCreated()
        {
            CommandEventConverter converter = new CommandEventConverter();
            FakeEventEmitter emitter = new FakeEventEmitter();

            LocationReportsController controller = new LocationReportsController(converter, emitter);

            LocationReport report = new LocationReport {
                Latitude = 10.0,
                Longitude = 10.0,
                Origin = "TESTS",
                MemberID = Guid.NewGuid(),
                ReportID = Guid.NewGuid()
            };

            var result = controller.PostLocationReport(report.MemberID, report);
            Assert.True( emitter.MemberLocationRecordedEvents.Count == 1 );
            Assert.Equal( emitter.MemberLocationRecordedEvents[0].Latitude, report.Latitude );
            Assert.Equal( emitter.MemberLocationRecordedEvents[0].Longitude, report.Longitude);
            Assert.Equal( emitter.MemberLocationRecordedEvents[0].Origin, report.Origin);
            Assert.Equal( emitter.MemberLocationRecordedEvents[0].MemberID, report.MemberID);
            Assert.Equal( emitter.MemberLocationRecordedEvents[0].ReportID, report.ReportID);
            
            Assert.Equal(201, (result as ObjectResult).StatusCode.Value);
        }
    }
}