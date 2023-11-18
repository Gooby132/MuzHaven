using Microsoft.AspNetCore.Http;

namespace StemService.Contacts.Requests;

public class UploadStemRequest
{

    public Guid ProjectId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; }
    public string Instrument { get; init; }
    public IFormFile StemStream { get; init; }
}
