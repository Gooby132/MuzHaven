﻿using StemService.Contacts.Dtos;

namespace StemService.Contacts.Responses;

public class GetAllStemsResponse
{

    public IEnumerable<CompleteStemDto> Stems { get; set; }

}
