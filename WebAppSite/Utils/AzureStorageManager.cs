using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppSite.Utils
{
    public class AzureStorageManager
    {
        public string Upload(IFormFile file)
        {

            var fileextension = Path.GetExtension(file.FileName);
            var novonome = String.Concat("/images/", Guid.NewGuid().ToString(), fileextension);

            MemoryStream ms = new MemoryStream();
            file.CopyTo(ms);

            string url = Path.GetFullPath(novonome);

            FileStream newfile = new FileStream(novonome, FileMode.Create, FileAccess.Write);
            ms.WriteTo(newfile);
            ms.Close();
            ms.Flush();

            newfile.Close();

            BlobServiceClient blobServiceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=asprojeto;AccountKey=a0B+PPewtIG4+ngBo/4uXdEnNq/RGCvVESJat3kcNOdmYTydATc8ik9Y7oumfAJOEJXvfyF5lP3zjOGROOPguA==;EndpointSuffix=core.windows.net");

            string containername = "brunoimages";

            var blobContainers = blobServiceClient.GetBlobContainers();

            var s = from bc in blobContainers where bc.Name.Equals(containername) select bc;

            BlobContainerClient blobContainerClient;
            if (s != null)
            {
                blobContainerClient = blobServiceClient.GetBlobContainerClient(containername);
            }
            else
            {
                blobContainerClient = blobServiceClient.CreateBlobContainer(containername);
            }
            //upload


            string localfilename = Path.GetFileName(url);
            BlobClient blobClient = blobContainerClient.GetBlobClient(localfilename);
            FileStream fileStream = File.OpenRead(url);
            blobClient.Upload(fileStream, true);
            fileStream.Close();

            url = blobClient.Uri.ToString();
            Console.WriteLine(url);



            return url;
        }

        public void Download()
        {

        }

    }
}
