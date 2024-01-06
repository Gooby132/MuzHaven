namespace StemService.Contacts.Requests;

public class CreateCommentRequest
{

    public Guid CommenterId { get; set; }
    public Guid StemId { get; set; }
    public string Text { get; set; }
    public string StageName { get; set; }
    public int? Time { get; set; }
}
