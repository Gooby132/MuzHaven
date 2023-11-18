using StemService.Contacts.Dtos;

namespace StemService.Contacts.Responses;

public class GetAllStemsResponse
{

    public IEnumerable<StemDto> Stems { get; set; }

}
