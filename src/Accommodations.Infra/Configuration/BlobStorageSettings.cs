
namespace Accommodations.Infra.Configuration
{
    public class BlobStorageSettings
    {
        public string ConnectionString { get; set; } = default!;
        public string AccommodationsContainerName { get; set; } = default!;
    }
}
