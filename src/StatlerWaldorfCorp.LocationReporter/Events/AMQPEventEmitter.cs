using System;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Steeltoe.Extensions.Configuration.CloudFoundry;

namespace StatlerWaldorfCorp.LocationReporter.Events
{    
    
    public class AMQPEventEmitter : IEventEmitter
    {
        private readonly ILogger logger;

        private Service rabbitServiceBinding;

        private ConnectionFactory connectionFactory;

        public AMQPEventEmitter(ILogger<AMQPEventEmitter> logger,
            IOptions<CloudFoundryServicesOptions> servicesOptions)
        {
            this.logger = logger;
            this.rabbitServiceBinding = servicesOptions.Value.Services.FirstOrDefault( s => s.Name == "rabbitmq");

            connectionFactory = new ConnectionFactory();
            
            connectionFactory.UserName = rabbitServiceBinding.Credentials["username"].Value;
            connectionFactory.Password = rabbitServiceBinding.Credentials["password"].Value;
            connectionFactory.VirtualHost = rabbitServiceBinding.Credentials["vhost"].Value;
            connectionFactory.HostName = rabbitServiceBinding.Credentials["hostname"].Value;
            connectionFactory.Uri = rabbitServiceBinding.Credentials["uri"].Value;
            
            logger.LogInformation("AMQP Event Emitter started with URI {0}", rabbitServiceBinding.Credentials["uri"].Value);
        }
        public const string QUEUE_LOCATIONRECORDED = "memberlocationrecorded";

        public void EmitLocationRecordedEvent(MemberLocationRecordedEvent locationRecordedEvent)
        {                    
            using (IConnection conn = connectionFactory.CreateConnection()) {
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