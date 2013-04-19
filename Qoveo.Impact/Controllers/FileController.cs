using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Qoveo.Impact.Controllers
{
    public class FileController : ApiController
    {
        public Task<List<FileDetails>> Post()
        {
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var multipartStreamProvider = new AzureBlobStorageMultipartProvider(BlobHelper.GetWebImpactContainer());
            return Request.Content.ReadAsMultipartAsync<AzureBlobStorageMultipartProvider>(multipartStreamProvider).ContinueWith<List<FileDetails>>(t =>
            {
                if (t.IsFaulted)
                {
                    throw t.Exception;
                }

                AzureBlobStorageMultipartProvider provider = t.Result;
                return provider.Files;
            });
        }

        public IEnumerable<FileDetails> Get()
        {
            CloudBlobContainer container = BlobHelper.GetWebImpactContainer();
            foreach (CloudBlockBlob blob in container.ListBlobs())
            {
                yield return new FileDetails
                {
                    Name = blob.Name,
                    Size = blob.Properties.Length,
                    ContentType = blob.Properties.ContentType,
                    Location = blob.Uri.AbsoluteUri
                };
            }
        }
    }

    public class FileDetails
    {
        public string Name { get; set; }
        public long Size { get; set; }
        public string ContentType { get; set; }
        public string Location { get; set; }
    }
}
