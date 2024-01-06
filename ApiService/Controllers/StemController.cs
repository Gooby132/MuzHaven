using ApiService.Application.Stems.Commands;
using ApiService.Application.Stems.Queries;
using DomainSeed;
using HashidsNet;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectService.Domain;
using StemService.Contacts.Dtos;
using StemService.Contacts.Requests;
using StemService.Contacts.Responses;
using StemService.Domain.Entities;

namespace ApiService.Controllers;


[Route("api/[controller]")]
[ApiController]
public class StemController : ControllerBase
{

    #region Fields

    private readonly ILogger<StemController> _logger;
    private readonly IMediator _mediator;
    private readonly IHashids _ids;

    #endregion

    #region Constructor

    public StemController(ILogger<StemController> logger, IMediator mediator, IHashids ids)
    {
        _logger = logger;
        _mediator = mediator;
        _ids = ids;
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
            Stems = res.Value.Select(r => new CompleteStemDto
            {
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
        var id = _ids.DecodeSingle(request.ProjectId);

        var res = await _mediator.Send(new GetStemsByProjectIdQuery.Query(id), token);

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
                CreatorId = stem.UserId,
                Comments = stem.Comments.OrderByDescending(comment => comment.CreatedOnUtc).Select(comment => new CommentDto
                {
                    Commenter = new CommenterDto
                    {
                        Id = comment.Commenter.Id,
                        FirstName = comment.Commenter.FirstName,
                        LastName = comment.Commenter.LastName,
                        StageName = comment.Commenter.StageName,
                    },
                    CreatedOnUtc = comment.CreatedOnUtc.ToString("O"),
                    Text = comment.Text,
                    Time = comment.Time,
                }),
                Description = stem.Desciption.Text,
                MusicFile = stem.MusicFile is not null ? new MusicFileDto
                {
                    Format = stem.MusicFile.Format,
                    Length = stem.MusicFile.Length,
                } : null,
            })
        });
    }

    [HttpPost("upload-stem")]
    public async Task<IActionResult> UploadStem([FromForm] UploadStemRequest request, CancellationToken token = default)
    {
        var id = _ids.DecodeSingle(request.ProjectId);

        var res = await _mediator.Send(new UploadStemCommand.Command(
            id,
            request.CreatorId,
            request.File.OpenReadStream(),
            request.File.FileName,
            request.Name,
            request.Instrument,
            request.Description
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

    [HttpGet("get-playback")]
    public async Task<IActionResult> GetStreamById([FromQuery] GetStreamRequest request, CancellationToken token = default)
    {

        var res = await _mediator.Send(new OpenStemStreamByIdQuery.Query(User, request.StemId));

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

        var res = await _mediator.Send(new GetStemByIdQuery.Query(request.StemId));

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
                Instrument = res.Value.Instrument,
                Name = res.Value.Name,
                Description = res.Value.Desciption.Text,
                MusicFile = res.Value.MusicFile is not null ? new MusicFileDto
                {
                    Format = res.Value.MusicFile.Format,
                    Length = res.Value.MusicFile.Length
                } : null
            }
        });
    }

    [HttpPost("create-comment")]
    public async Task<IActionResult> CreateComment([FromBody] CreateCommentRequest request)
    {
        var res = await _mediator.Send(new CreateCommentCommand.Request(request.StemId, request.CommenterId, request.Text, request.StageName, request.Time));

        if (res.IsFailed)
        {
            // defined errors

            return Problem(); // undefined errors
        }

        return Ok(new CreateCommentResponse
        {

        });
    }

    #endregion

}
