using ApiService.Application.Projects.Queries;
using ProjectService.Contracts.Requests;
using ProjectService.Contracts.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PermissionService.Infrastructure.Authorization.Abstracts;
using DomainSeed;

namespace ApiService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{

    #region Fields

    private readonly ILogger<ProjectsController> _logger;
    private readonly IMediator _mediator;

    #endregion

    #region Constructor

    public ProjectsController(ILogger<ProjectsController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    #endregion

    #region Methods

    [Authorize(IPermissionTokenProvider.PermissionSchemeName)]
    [HttpPost("create-project")]
    public async Task<IActionResult> Create(CreateProjectRequest request, CancellationToken token = default)
    {
        var res = await _mediator.Send(new Application.Projects.Commands.CreateProject.Command
        (
            request.Project.Title,
            request.Project.Album,
            request.Project.Description,
            request.Project.ReleaseInUtc,
            request.Project.BeatsPerMinute,
            request.Project.MusicalProfile.Key,
            request.Project.MusicalProfile.Scale
        ), token);

        if (res.IsFailed)
        {
            if (res.HasError<ErrorBase>())
            {
                return BadRequest(res.Errors.Select(r => new
                {
                    r.Message,
                    Code = (r as ErrorBase)?.ErrorCode,
                    Group = (r as ErrorBase)?.GroupCode,
                }));
            }

            _logger.LogError("{this} project creation failed. errors - '{errors}'",
                this, string.Join(", ", res.Reasons.Select(r => r.Message)));

            return Problem(); // undefined errors
        }

        return CreatedAtAction(
           nameof(GetById),
           new { res.Value.Id },
           new CreateProjectResponse
           {
               Project = new ProjectService.Contracts.Dtos.CompleteProjectDto
               {
                   Id = res.Value.Id,
                   Title = res.Value.Title.Text,
                   Album = res.Value.Album,
                   MusicalProfile = new ProjectService.Contracts.Dtos.MusicalProfileDto
                   {
                       Key = (int)res.Value.MusicalProfile.Key,
                       Scale = (int)res.Value.MusicalProfile.Scale,
                   },
                   BeatsPerMinute = res.Value.BeatsPerMinute,
                   CreatedInUtc = res.Value.CreatedInUtc.ToString("O"),
                   Description = res.Value.Description.Text,
                   ReleaseInUtc = res.Value.ReleaseInUtc.ToString("O"),
               }
           });
    }

    [HttpGet("get-projects")]
    public async Task<IActionResult> GetProjects([FromQuery] string token, CancellationToken cancellationToken = default)
    {

        return Ok();
    }

    [HttpGet("get-by-id")]
    public async Task<IActionResult> GetById([FromQuery] GetProjectByIdRequest request, CancellationToken token = default)
    {

        var res = await _mediator.Send(new GetProjectById.Query
        {
            Id = request.Id,
        });

        if (res.IsFailed)
        {
            // defined errors

            return Problem(); // undefined errors
        }

        return Ok(new GetProjectResponse
        {
            Project = new ProjectService.Contracts.Dtos.CompleteProjectDto
            {
                Id = res.Value.Id,
                Title = res.Value.Title.Text,
                Album = res.Value.Album,
                BeatsPerMinute = res.Value.BeatsPerMinute,
                CreatedInUtc = res.Value.CreatedInUtc.ToString("O"),
                Description = res.Value.Description.Text,
                ReleaseInUtc = res.Value.ReleaseInUtc.ToString("O"),
                MusicalProfile = new ProjectService.Contracts.Dtos.MusicalProfileDto
                {
                    Scale = (int)res.Value.MusicalProfile.Scale,
                    Key = (int)res.Value.MusicalProfile.Key,
                },
            }
        });
    }

    #endregion

}
