using ApiService.Application.Stems.Queries;
using DomainSeed;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectService.Domain;
using StemService.Contacts.Dtos;
using StemService.Contacts.Requests;
using StemService.Contacts.Responses;

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
            Stems = res.Value.Select(r => new CompleteStemDto { 
                Id = r.Id, 
                Name = r.Name,
                Instrument = r.Instrument,
                ProjectId = r.ProjectId,
                CreatorId = r.UserId
            })
        });
    }


    [HttpGet("get-stems")]
    public async Task<IActionResult> GetStems([FromQuery] GetStemsByProjectIdRequest request, CancellationToken token = default)
    {
        var res = await _mediator.Send(new GetStemsByProjectId.Query(request.ProjectId), token);

        if (res.IsFailed)
        {
            // defined errors

            return Problem(); // undefined errors
        }

        return Ok(new GetStemsResponse
        {
            Stems = res.Value.Select(stem => new CompleteStemDto
            {
                Id = stem.Id,
                Name = stem.Name,
                Instrument = stem.Instrument,
                ProjectId = stem.ProjectId,
                CreatorId = stem.UserId
            })
        });
    }

    [HttpPost("upload-stem")]
    public async Task<IActionResult> UploadStem([FromForm] UploadStemRequest request, CancellationToken token = default)
    {
        var res = await _mediator.Send(new Application.Stems.Commands.UploadStem.Command(
            request.ProjectId,
            request.CreatorId,
            request.File.OpenReadStream(),
            request.Description,
            request.Name,
            request.Instrument
            ), token);

        if (res.IsFailed)
        {
            if (res.HasError<ErrorBase>())
                return BadRequest(res.Errors.Select(r => new
                {
                    r.Message,
                    Code = (r as ErrorBase)?.ErrorCode,
                    Group = (r as ErrorBase)?.GroupCode,
                }));

            // defined errors

            return Problem(); // undefined errors
        }

        return CreatedAtAction(
           nameof(GetById),
           new { res.Value.Id },
           new UploadStemResponse
           {
               Stem = new CompleteStemDto
               {
                   Id = res.Value.Id,
                   ProjectId = res.Value.ProjectId,
                   CreatorId = res.Value.UserId,
                   Name = res.Value.Name,
                   Instrument = res.Value.Instrument,
               }
           });
    }

    [HttpGet("get-stream")]
    public async Task<IActionResult> GetStreamById([FromQuery] GetStreamRequest request, CancellationToken token = default)
    {

        var res = await _mediator.Send(new OpenStemStreamById.Query(User, request.StemId));

        if (res.IsFailed)
        {
            // defined errors

            return Problem(); // undefined errors
        }

        return File(res.Value.Stream, "audio/wav");
    }

    [HttpGet("get-stem")]
    public async Task<IActionResult> GetById([FromQuery] GetStreamRequest request, CancellationToken token = default)
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
                CreatorId = res.Value.UserId,
                Name = res.Value.Name,
                Instrument = res.Value.Instrument,
            }
        });
    }

    #endregion

}
