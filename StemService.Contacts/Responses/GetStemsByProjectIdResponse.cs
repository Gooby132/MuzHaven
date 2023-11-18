using StemService.Contacts.Dtos;

namespace StemService.Contacts.Responses;

public class GetStemsByProjectIdResponse
{

    public List<StemDto> Stems { get; set; }
    public string Key { get; set; }

}
