using System.Configuration;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace DemoApp.Controllers
{
    public static class JobHelper
    {
        public static async Task SendNotification(string email)
        {
            var connection = ConfigurationManager.AppSettings["webjobs.Storage"];
            var storageAccount = CloudStorageAccount.Parse(connection);

            // Create the queue client.
            var queueClient = storageAccount.CreateCloudQueueClient();

            // Retrieve a reference to a queue.
            var queue = queueClient.GetQueueReference("emailjob");

            // Create the queue if it doesn't already exist.
            queue.CreateIfNotExists();

            // Create a message and add it to the queue.
            var message = new CloudQueueMessage(email);
            await queue.AddMessageAsync(message);
        }
    }
}
