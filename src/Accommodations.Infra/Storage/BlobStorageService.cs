﻿using Accommodations.Domain.Interfaces;
using Accommodations.Infra.Configuration;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
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

        public string? GetBlobSasUrl(string? blobUrl)
        {
            if (blobUrl == null) return null;

            var sasBuilder = new BlobSasBuilder()
            {
                BlobContainerName = _blobStorageSettings.AccommodationsContainerName,
                Resource = "b",
                StartsOn = DateTimeOffset.UtcNow,
                ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(30),
                BlobName = GetBlobNameFromUrl(blobUrl)
            };

            sasBuilder.SetPermissions(BlobSasPermissions.Read);

            var blobServiceClient = new BlobServiceClient(_blobStorageSettings.ConnectionString);

            var sasToken = sasBuilder
                .ToSasQueryParameters(new Azure.Storage.StorageSharedKeyCredential(blobServiceClient.AccountName, _blobStorageSettings.AccountKey))
                .ToString();

            return $"{blobUrl}?{sasToken}";
        }

        private string GetBlobNameFromUrl(string blobUrl)
        {
            var uri = new Uri(blobUrl);
            return uri.Segments.Last();
        }
    }
}
