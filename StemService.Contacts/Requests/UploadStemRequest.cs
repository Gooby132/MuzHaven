using Microsoft.AspNetCore.Http;
using StemService.Contacts.Dtos;

namespace StemService.Contacts.Requests;

public class UploadStemRequest
{
    public string ProjectId { get; init; }
    public Guid CreatorId { get; init; }
    public string Name { get; init; }
    public string Instrument { get; init; }
    public string? Description { get; init; }
    public IFormFile File { get; init; }
}
