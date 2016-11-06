using System;
using System.Text;
using RabbitMQ.Client;

namespace StatlerWaldorfCorp.LocationReporter.Events
{
    public class AMQPEventEmitter : IEventEmitter
    {
        public const string QUEUE_LOCATIONRECORDED = "memberlocationrecorded";

        public void EmitLocationRecordedEvent(MemberLocationRecordedEvent locationRecordedEvent)
        {
            // TODO Change this to obtain connection details from environment binding
            var factory = new ConnectionFactory();
            factory.UserName = "guest";
            factory.Password = "guest";
            factory.VirtualHost = ConnectionFactory.DefaultVHost;
            factory.HostName = "localhost";
            factory.Port = AmqpTcpEndpoint.UseDefaultPort;

            using (IConnection conn = factory.CreateConnection()) {
                using (IModel channel = conn.CreateModel()) {
                    channel.QueueDeclare(
                        queue: QUEUE_LOCATIONRECORDED,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                    );
                    string jsonPayload = locationRecordedEvent.toJson();
                    var body = Encoding.UTF8.GetBytes(jsonPayload);
                    channel.BasicPublish(
                        exchange: "",
                        routingKey: QUEUE_LOCATIONRECORDED,
                        basicProperties: null,
                        body: body
                    );
                }
            }
        }
    }
}