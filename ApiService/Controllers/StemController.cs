using ApiService.Application.Stems.Features;
using ApiService.Application.Stems.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StemService.Contacts.Dtos;
using StemService.Contacts.Requests;
using StemService.Contacts.Responses;
using StemService.Domain.Services;

namespace ApiService.Controllers;


[Route("api/[controller]")]
[ApiController]
public class StemController : ControllerBase
{

    #region Fields

    private readonly ILogger<StemController> _logger;
    private readonly IMediator _mediator;

    #endregion

    #region Constructor

    public StemController(ILogger<StemController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    #endregion

    #region Methods

    [HttpGet("all")]
    public async Task<IActionResult> GetAll(CancellationToken token = default)
    {
        var res = await _mediator.Send(new GetAllQuery.Query(), token);

        if (res.IsFailed)
        {
            // defined errors

            return Problem(); // undefined errors
        }

        return Ok(new GetAllStemsResponse
        {
            Stems = res.Value.Select(r => new StemDto { 
                Id = r.Id, 
                Name = r.Name,
                Instrument = r.Instrument,
                ProjectId = r.ProjectId,
                UserId = r.UserId
            })
        });
    }

    [HttpGet("get-stems-authorization-by-project-id")]
    public async Task<IActionResult> StemsAuthorizationByProjectId([FromQuery] GetStemsAuthorizationByProjectIdRequest request, CancellationToken token = default)
    {
        var res = await _mediator.Send(new GetStemsAuthorizationByProjectIdFeature.Request(request.ProjectId), token);

        if (res.IsFailed)
        {
            // defined errors

            return Problem(); // undefined errors
        }

        Response.Headers.Remove(IFileAuthorizer.StemHeaderName);
        Response.Headers.Add(IFileAuthorizer.StemHeaderName, res.Value);

        return Ok(new GetStemsAuthorizationByProjectIdResponse
        {
            Token = res.Value
        });
    }

    [HttpPost("upload-stem")]
    public async Task<IActionResult> Upload([FromForm] UploadStemRequest request, CancellationToken token = default)
    {

        var res = await _mediator.Send(new Application.Stems.Commands.UploadStem.Command(
            request.ProjectId,
            request.UserId,
            request.StemStream.OpenReadStream(),
            request.Name,
            request.Instrument
            ), token);

        if (res.IsFailed)
        {
            // defined errors

            return Problem(); // undefined errors
        }

        return CreatedAtAction(
           nameof(GetById),
           new { res.Value.Id },
           new CreateStemResponse
           {
               Stem = new StemDto
               {
                   Id = res.Value.Id,
                   ProjectId = res.Value.ProjectId,
                   UserId = res.Value.UserId,
                   Name = res.Value.Name,
                   Instrument = res.Value.Instrument,
               }
           });
    }

    [Authorize(AuthenticationSchemes = IFileAuthorizer.StemSchemeName)]
    [HttpGet("get-stream-by-id")]
    public async Task<IActionResult> GetStreamById([FromQuery] OpenStemStreamRequest request, CancellationToken token = default)
    {

        var res = await _mediator.Send(new OpenStemStreamById.Query(User, request.StemId));

        if (res.IsFailed)
        {
            // defined errors

            return Problem(); // undefined errors
        }

        return File(res.Value.Stream, "audio/wav");
    }

    [HttpGet("get-stem-by-id")]
    public async Task<IActionResult> GetById([FromQuery] OpenStemStreamRequest request, CancellationToken token = default)
    {

        var res = await _mediator.Send(new GetStemById.Query(request.StemId));

        if (res.IsFailed)
        {
            // defined errors

            return Problem(); // undefined errors
        }

        return Ok(new GetStemByIdResponse
        {
            Stem = new StemDto
            {
                ProjectId = res.Value.ProjectId,
                UserId = res.Value.UserId,
                Name = res.Value.Name,
                Instrument = res.Value.Instrument,
            }
        });
    }

    #endregion

}
