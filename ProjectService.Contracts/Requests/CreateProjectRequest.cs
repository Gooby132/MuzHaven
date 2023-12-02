namespace ProjectService.Contracts.Requests;

public class CreateProjectRequest
{

    public string Title { get; init; }
    public string Album { get; init; }
    public string Description { get; init; }
    public DateTime ReleseInUtc { get; init; }
    public float BeatsPerMinute { get; init; }
    public int MusicalKey { get; init; }   
    public int MusicalScale { get; init; }
}
