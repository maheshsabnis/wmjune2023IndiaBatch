using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AzFnQueueMessageReader
{
    public class Function1
    {
        /// <summary>
        /// QueueTrigger: This will Trigger the current function when the data is present
        /// is the "produict-data-queue" queue
        /// </summary>
        /// <param name="myQueueItem"></param>
        /// <param name="log"></param>
        [FunctionName("Function1")]
        public void Run([QueueTrigger("produict-data-queue", Connection = "myqueueconnection")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
