using Accommodations.Domain.Interfaces;
using Accommodations.Infra.Configuration;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;

namespace Accommodations.Infra.Storage
{
    internal class BlobStorageService(IOptions<BlobStorageSettings> blobStorageSettingsOptions)
        : IBlobStorageService
    {
        private readonly BlobStorageSettings _blobStorageSettings = blobStorageSettingsOptions.Value;

        public async Task<string> UploadToBlobAsync(Stream data, string fileName)
        {
            var blocServiceClient = new BlobServiceClient(_blobStorageSettings.ConnectionString);
            var containerClient = blocServiceClient.GetBlobContainerClient(_blobStorageSettings.AccommodationsContainerName);

            var blobClient = containerClient.GetBlobClient(fileName);

            await blobClient.UploadAsync(data);

            var blobUrl = blobClient.Uri.ToString();

            return blobUrl;
        }
    }
}
