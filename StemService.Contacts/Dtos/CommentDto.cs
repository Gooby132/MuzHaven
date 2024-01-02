namespace StemService.Contacts.Dtos;

public class CommentDto
{
    public CommenterDto Commenter { get; set; }
    public string CreatedOnUtc { get; set; }
    public string Text { get; init; }
    public int? Time { get; init; }

}
