
using ClassLib.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace WebAppSite.Utils
{
    public class AzureTablesManager : ITableEntity
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public string ETag { get; set; }

        public IDictionary<string, EntityProperty> properties { get; set; }

        public AzureTablesManager()
        {
            properties = new Dictionary<string, EntityProperty>();
        }

        public AzureTablesManager(string iPartitionKey, string iRowKey)
        {
            PartitionKey = iPartitionKey;
            RowKey = iRowKey;
            properties = new Dictionary<string, EntityProperty>();
        }


        public void ReadEntity(IDictionary<string, EntityProperty> _properties, OperationContext operationContext)
        {
            properties = _properties;
        }

        public IDictionary<string, EntityProperty> WriteEntity(OperationContext operationContext)
        {
            return properties;
        }

        public EntityProperty ConvertToEntityProperty(string key, object value)
        {
            if (value == null) return new EntityProperty((string)null);

            if (value.GetType() == typeof(bool))
            {
                return new EntityProperty((bool)value);
            }

            if (value.GetType() == typeof(int))
            {
                return new EntityProperty((int)value);
            }

            if (value.GetType() == typeof(string))
            {
                return new EntityProperty((string)value);
            }

            throw new Exception("Tipo Indefinido");
        }

        public CloudTable ConnectAzure()
        {
            string connection = "DefaultEndpointsProtocol=https;AccountName=asprojeto;AccountKey=a0B+PPewtIG4+ngBo/4uXdEnNq/RGCvVESJat3kcNOdmYTydATc8ik9Y7oumfAJOEJXvfyF5lP3zjOGROOPguA==;EndpointSuffix=core.windows.net";

            string table = "brunoShoppingCart";

            CloudStorageAccount storageAccount;

            storageAccount = CloudStorageAccount.Parse(connection);
            CloudTableClient cloudTableClient = storageAccount.CreateCloudTableClient();

            CloudTable cloudTable = cloudTableClient.GetTableReference(table);

            cloudTable.CreateIfNotExistsAsync().GetAwaiter().GetResult();

            return cloudTable;

        }

        public void TableItemDelete(int id, string user)
        {

            CloudTable cloudTable = ConnectAzure();

            AzureTablesManager newte = new AzureTablesManager( user, id.ToString());

            newte.ConnectAzure().ExecuteAsync(TableOperation.Delete(new TableEntity(user, id.ToString()) { ETag = "*" } ));

        }

        public void TableCartDelete(int id, string user)
        {

            CloudTable cloudTable = ConnectAzure();

            AzureTablesManager newte = new AzureTablesManager(user, id.ToString());

            newte.ConnectAzure().ExecuteAsync(TableOperation.Delete(new TableEntity(user, id.ToString()) { ETag = "*" }));

        }

        public int CountShoppingCart(string user)
        {
            CloudTable cloudTable = ConnectAzure();

            string filtro = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, user);

            TableContinuationToken token = new TableContinuationToken();

            TableQuery<AzureTablesManager> consulta =
                new TableQuery<AzureTablesManager>().Where(filtro);


            var teste = cloudTable.ExecuteQuerySegmentedAsync(consulta, token).GetAwaiter().GetResult();


            var count = teste.Results.Count;

            return count;
        }

        public void IfExists(OrderDetail orderDetail)
        {
            int units = 0;

            CloudTable cloudTable = ConnectAzure();

            string filtro = TableQuery.CombineFilters(
                TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, orderDetail.User),
                TableOperators.And,
                TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, orderDetail.ProductId.ToString()));

            TableContinuationToken token = new TableContinuationToken();

            TableQuery<AzureTablesManager> consulta =
                new TableQuery<AzureTablesManager>().Where(filtro);

            AzureTablesManager newte = new AzureTablesManager(orderDetail.User, orderDetail.ProductId.ToString());

            foreach (AzureTablesManager te in cloudTable
                .ExecuteQuerySegmentedAsync(consulta, token).GetAwaiter().GetResult())
                {
                    Type entityType = typeof(AzureTablesManager);
                    PropertyInfo[] proplist = entityType.GetProperties();
                    foreach (PropertyInfo pro in proplist)
                    {
                        if (pro.Name == "properties")
                        {
                            IDictionary<string, EntityProperty> properties
                                = (IDictionary<string, EntityProperty>)pro.GetValue(te, null);
                            foreach (string key in properties.Keys)
                            {
                                if (key == "Quantity")
                                {
                                    units = Convert.ToInt32(properties[key].PropertyAsObject) + 1;

                                newte.properties.Add("Quantity", newte.ConvertToEntityProperty("Quantity", units));

                            }
                            }
                        }
                    }
                }

            newte.properties.Add("Name", newte.ConvertToEntityProperty("Name", orderDetail.Name.ToString()));
            newte.properties.Add("Price", newte.ConvertToEntityProperty("Price", orderDetail.Price.ToString()));
            newte.properties.Add("ImageUrl", newte.ConvertToEntityProperty("ImageUrl", orderDetail.ImageUrl.ToString()));
            newte.properties.Add("Total", newte.ConvertToEntityProperty("Total", orderDetail.Total.ToString()));
            if(units== 0)
            {
                newte.properties.Add("Quantity", newte.ConvertToEntityProperty("Quantity", orderDetail.Quantity));
            }

            newte.ConnectAzure().ExecuteAsync(TableOperation.InsertOrMerge(newte));

        }

    }


}

