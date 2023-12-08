using ApiService.Application.Projects.Queries;
using ProjectService.Contracts.Requests;
using ProjectService.Contracts.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PermissionService.Infrastructure.Authorization.Abstracts;
using DomainSeed;
using System.Linq;
using ProjectService.Domain;
using FluentResults;

namespace ApiService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{

    #region Fields

    private readonly ILogger<ProjectsController> _logger;
    private readonly IMediator _mediator;
    private readonly IPermissionTokenProvider _tokenProvider;

    #endregion

    #region Constructor

    public ProjectsController(ILogger<ProjectsController> logger, IMediator mediator, IPermissionTokenProvider tokenProvider)
    {
        _logger = logger;
        _mediator = mediator;
        _tokenProvider = tokenProvider;
    }

    #endregion

    #region Methods

    [HttpPost("create-project")]
    public async Task<IActionResult> Create(CreateProjectRequest request, CancellationToken token = default)
    {

        var project = Project.Create(
             string.Empty,
             request.Project.Title,
             request.Project.Album,
             request.Project.Description,
             request.Project.ReleaseInUtc,
             request.Project.BeatsPerMinute,
             request.Project.MusicalProfile?.Key,
             request.Project.MusicalProfile?.Scale
             );

        if (project.IsFailed)
        {
            _logger.LogTrace("{this} bad request creating project. error(s) - '{errors}'",
                this, string.Join(", ", project.Reasons.Select(r => r.Message)));

            if (project.HasError<ErrorBase>())
            {
                return BadRequest(project.Errors.Select(r => new
                {
                    r.Message,
                    Code = (r as ErrorBase)?.ErrorCode,
                    Group = (r as ErrorBase)?.GroupCode,
                }));
            }
        }

        var res = await _mediator.Send(new Application.Projects.Commands.CreateProject.Command
        (
            project.Value
        ), token);

        if (res.IsFailed)
        {
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
                   BeatsPerMinute = res.Value.BeatsPerMinute,
                   CreatedInUtc = res.Value.CreatedInUtc.ToString("O"),
                   Description = res.Value.Description.Text,
                   ReleaseInUtc = res.Value.ReleaseInUtc?.ToString("O"),
                   MusicalProfile = res.Value.MusicalProfile is not null ? new ProjectService.Contracts.Dtos.MusicalProfileDto
                   {
                       Scale = (int)res.Value.MusicalProfile.Scale,
                       Key = (int)res.Value.MusicalProfile.Key,
                   } : null,
               }
           });
    }

    [HttpGet("get-projects")]
    [Authorize(AuthenticationSchemes = IPermissionTokenProvider.PermissionSchemeName)]
    public async Task<IActionResult> GetProjects(CancellationToken cancellationToken = default)
    {

        var id = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == IPermissionTokenProvider.UserIdClaim);

        if (id?.Value is null)
            return Unauthorized();

        var res = await _mediator.Send(new GetProjectsByCreatorId.Query(id.Value), cancellationToken);

        if (res.IsFailed)
        {
            // defined errors

            return Problem();
        }

        return Ok(new GetProjectsResponse
        {
            Projects = res.Value.Select(project => new ProjectService.Contracts.Dtos.CompleteProjectDto
            {
                Id = project.Id,
                Title = project.Title.Text,
                Album = project.Album,
                BeatsPerMinute = project.BeatsPerMinute,
                CreatedInUtc = project.CreatedInUtc.ToString("O"),
                Description = project.Description.Text,
                ReleaseInUtc = project.ReleaseInUtc?.ToString("O"),
                MusicalProfile = project.MusicalProfile is not null ? new ProjectService.Contracts.Dtos.MusicalProfileDto
                {
                    Scale = (int)project.MusicalProfile.Scale,
                    Key = (int)project.MusicalProfile.Key,
                } : null,
            })
        });
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
                ReleaseInUtc = res.Value.ReleaseInUtc?.ToString("O"),
                MusicalProfile = res.Value.MusicalProfile is not null ? new ProjectService.Contracts.Dtos.MusicalProfileDto
                {
                    Scale = (int)res.Value.MusicalProfile.Scale,
                    Key = (int)res.Value.MusicalProfile.Key,
                } : null,
            }
        });
    }

    #endregion

}
