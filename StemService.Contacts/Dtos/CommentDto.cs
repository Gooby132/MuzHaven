namespace StemService.Contacts.Dtos;

public class CommentDto
{
    public Guid CommenterId { get; set; }
    public string CreatedOnUtc { get; set; }
    public string Text { get; init; }
    public int? Time { get; init; }

}
