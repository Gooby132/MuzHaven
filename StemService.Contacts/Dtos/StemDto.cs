namespace StemService.Contacts.Dtos;

public class StemDto
{
    public int ProjectId { get; init; }
    public Guid CreatorId { get; init; }
    public string Name { get; init; }
    public string? Description { get; init; }
    public string Instrument { get; init; }
    public MusicFileDto? MusicFile { get; init; }
}
