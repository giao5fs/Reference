using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace DownloadWithRabbitMQ
{
    public class DownloadManager
    {
        private readonly string _downloadDirectory;
        private readonly string _queueName;
        private readonly IConnection _connection;

        public DownloadManager(string downloadDirectory, string queueName, string rabbitMqConnectionString)
        {
            _downloadDirectory = downloadDirectory;
            _queueName = queueName;

            var factory = new ConnectionFactory() { Uri = new Uri(rabbitMqConnectionString) };
            _connection = factory.CreateConnection();
        }

        public async Task PublishUrlsAsync(string[] urls)
        {
            using (var channel = _connection.CreateModel())
            {
                channel.QueueDeclare(_queueName, durable: true, exclusive: false, autoDelete: false);

                foreach (var url in urls)
                {
                    var body = Encoding.UTF8.GetBytes(url);
                    channel.BasicPublish("", _queueName, null, body);
                }
            }
        }
    }

    public class DownloadWorker
    {
        private readonly string _downloadDirectory;
        private readonly string _queueName;
        private readonly IConnection _connection;

        public DownloadWorker(string downloadDirectory, string queueName, string rabbitMqConnectionString)
        {
            _downloadDirectory = downloadDirectory;
            _queueName = queueName;

            var factory = new ConnectionFactory() { Uri = new Uri(rabbitMqConnectionString) };
            _connection = factory.CreateConnection();
        }

        public async Task StartDownloadLoopAsync(CancellationToken cancellationToken)
        {
            using (var channel = _connection.CreateModel())
            {
                channel.QueueDeclare(_queueName, durable: true, exclusive: false, autoDelete: false);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = Encoding.UTF8.GetString(ea.Body.ToArray());
                    // Download the file using the URL from the message body
                    DownloadFile(body);
                    channel.BasicAck(ea.DeliveryTag, false);
                };

                channel.BasicConsume(_queueName, false, consumer);

                while (!cancellationToken.IsCancellationRequested)
                {
                    await Task.Delay(1000, cancellationToken);
                }
            }
        }

        private void DownloadFile(string url)
        {
            // Implement logic to download the file using the URL
            // ...
        }
    }
}