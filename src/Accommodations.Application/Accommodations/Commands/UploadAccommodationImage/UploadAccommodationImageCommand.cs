using MediatR;

namespace Accommodations.App.Accommodations.Commands.UploadAccommodationImage
{
    public class UploadAccommodationImageCommand : IRequest
    {
        public Guid Guid { get; set; }
        public string FileName { get; set; } = default!;
        public Stream File { get; set; } = default!;
    }
}
