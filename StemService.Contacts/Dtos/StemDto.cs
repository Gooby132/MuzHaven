namespace StemService.Contacts.Dtos;

public class StemDto
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Instrument { get; set; }
}
