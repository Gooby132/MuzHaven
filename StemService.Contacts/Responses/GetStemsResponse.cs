using StemService.Contacts.Dtos;

namespace StemService.Contacts.Responses;

public class GetStemsResponse
{

    public IEnumerable<CompleteStemDto> Stems { get; set; }

}
