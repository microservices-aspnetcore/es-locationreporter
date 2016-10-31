using System;

namespace StatlerWaldorfCorp.LocationReporter.Events
{
    public class AMQPEventEmitter : IEventEmitter
    {
        public void EmitLocationRecordedEvent(MemberLocationRecordedEvent locationRecordedEvent)
        {
            throw new NotImplementedException();
        }
    }
}