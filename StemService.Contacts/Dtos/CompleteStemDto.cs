namespace StemService.Contacts.Dtos;

public class CompleteStemDto
{
    public Guid Id { get; init; }
    public int ProjectId { get; init; }
    public Guid CreatorId { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public string Instrument { get; init; }
    public IEnumerable<CommentDto> Comments { get; init; }
    public MusicFileDto? MusicFile { get; set; }
}
