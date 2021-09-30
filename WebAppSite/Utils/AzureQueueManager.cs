using Azure.Storage.Queues;
using ClassLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppSite.Utils
{
    public class AzureQueueManager
    {
        public void QueueSend(Product product)
        {
            
            //QueueClient queueClient = new QueueClient("DefaultEndpointsProtocol=https;AccountName=ac2020storage;AccountKey=5fAS2v1hAZnoxilyas06ZvZwd7ehsftjBQkGlhsnW8+qtGiqboSO3UhsMS4+y59mx+DKJhmulzSx4NG2UF78SQ==;EndpointSuffix=core.windows.net", "bruno_simpleBotanics");
            QueueClient queueClient = new QueueClient("DefaultEndpointsProtocol=https;AccountName=asprojeto;AccountKey=a0B+PPewtIG4+ngBo/4uXdEnNq/RGCvVESJat3kcNOdmYTydATc8ik9Y7oumfAJOEJXvfyF5lP3zjOGROOPguA==;EndpointSuffix=core.windows.net", "brunosimplebotanics");
            queueClient.CreateIfNotExists();

            queueClient.SendMessage("product_id = " + product.ProductId + " acedido em : " + DateTime.Now);
        }
    }
}