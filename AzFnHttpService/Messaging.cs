using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AzFnHttpService.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System.Text.Json;
namespace AzFnHttpService
{
    public class Messaging
    {
        public async Task WriteQueueAsync(Product product)
        {
            // 1. Create CloudStorageAccount instance, this is proxy to connect to Storage
            // Account based on the connection string

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("QUEUE-CONNECTION-STRINGT");

            // 2. Create a CloudQueueClient under the Storage account
            // So that the Queue Storage can be accessed that is created
            // under the storage

            CloudQueueClient cloudQueueClient =  storageAccount.CreateCloudQueueClient();

            // 3. Create an instnace of the CloudQueue that will be used to create 
            // queue if not exist else will create queue

            CloudQueue cloudQueue = cloudQueueClient.GetQueueReference("produict-data-queue");
            await cloudQueue.CreateIfNotExistsAsync();

            // 4. Define a message that is to be added in queue

            string productData = JsonSerializer.Serialize(product);

            CloudQueueMessage message = new CloudQueueMessage(productData);

            // 5. Write data in queue
            await cloudQueue.AddMessageAsync(message);


        }
    }
}
