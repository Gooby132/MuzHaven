﻿using ApiService.Application.Projects.Queries;
using ProjectService.Contracts.Requests;
using ProjectService.Contracts.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PermissionService.Infrastructure.Authorization.Abstracts;
using DomainSeed;
using ProjectService.Domain;
using DomainSeed.CommonErrors;
using HashidsNet;
using ApiService.Application.Projects.Commands;

namespace ApiService.Controllers;

/// <summary>
/// REST controller for remote project domain operations
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{

    #region Fields

    private readonly ILogger<ProjectsController> _logger;
    private readonly IMediator _mediator;
    private readonly IPermissionTokenProvider _tokenProvider;
    private readonly IHashids _ids;

    #endregion

    #region Constructor

    public ProjectsController(ILogger<ProjectsController> logger, IMediator mediator, IPermissionTokenProvider tokenProvider, IHashids ids)
    {
        _logger = logger;
        _mediator = mediator;
        _tokenProvider = tokenProvider;
        _ids = ids;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Creates a new project under a specific user admin
    /// </summary>
    /// <param name="request">project data</param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpPost("create-project")]
    public async Task<IActionResult> Create(CreateProjectRequest request, CancellationToken token = default)
    {
        var id = HttpContext.User
            .Claims
            .FirstOrDefault(claim => claim.Type == IPermissionTokenProvider.UserIdClaim);

        if (id?.Value is null)
            return Unauthorized();

        var project = Project.Create(
             id.Value,
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

        var res = await _mediator.Send(new CreateProjectCommand.Command
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
                   Id = _ids.Encode(res.Value.Id),
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

    /// <summary>
    /// Admin operation for fetching *ALL* available projects 
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpGet("get-all-projects")]
    [Authorize(AuthenticationSchemes = IPermissionTokenProvider.PermissionSchemeName, Roles = "Admin")]
    public async Task<IActionResult> GetAllProjects(CancellationToken token = default)
    {
        var id = HttpContext.User
            .Claims
            .FirstOrDefault(claim => claim.Type == IPermissionTokenProvider.PermissionTypeClaim);

        if (id?.Value is null || id.Value != IPermissionTokenProvider.PermissionAdminRoleTypeClaim)
            return Unauthorized();

        var res = await _mediator.Send(new GetAllProjectsQuery.Query(), token);

        return Ok(new GetProjectsResponse
        {
            Projects = res.Value.Select(project => new ProjectService.Contracts.Dtos.CompleteProjectDto
            {
                Id = _ids.Encode(project.Id),
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

    /// <summary>
    /// Fetches user create projects
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("get-projects")]
    [Authorize(AuthenticationSchemes = IPermissionTokenProvider.PermissionSchemeName)]
    public async Task<IActionResult> GetProjects(CancellationToken cancellationToken = default)
    {

        var id = HttpContext.User
            .Claims
            .FirstOrDefault(claim => claim.Type == IPermissionTokenProvider.UserIdClaim);

        if (id?.Value is null)
            return Unauthorized();

        var res = await _mediator.Send(new GetProjectsByCreatorIdQuery.Query(id.Value), cancellationToken);

        if (res.IsFailed)
        {
            // defined errors

            return Problem();
        }

        return Ok(new GetProjectsResponse
        {
            Projects = res.Value.Select(project => new ProjectService.Contracts.Dtos.CompleteProjectDto
            {
                Id = _ids.Encode(project.Id),
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

    /// <summary>
    /// Fetches creator autherized project
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpGet("get-by-id")]
    [Authorize(AuthenticationSchemes = IPermissionTokenProvider.PermissionSchemeName)]
    public async Task<IActionResult> GetById([FromQuery] GetProjectByIdRequest request, CancellationToken token = default)
    {
        var id = HttpContext.User
            .Claims
            .FirstOrDefault(claim => claim.Type == IPermissionTokenProvider.UserIdClaim);

        if (id?.Value is null)
            return Unauthorized();

        var res = await _mediator.Send(new GetProjectByIdQuery.Query
        {
            Id = _ids.DecodeSingle(request.Id),
        });

        if (res.IsFailed)
        {
            if (res.HasError<NotFoundError>())
                return NotFound(request.Id);

            // defined errors

            return Problem(); // undefined errors
        }

        return Ok(new GetProjectResponse
        {
            Project = new ProjectService.Contracts.Dtos.CompleteProjectDto
            {
                Id = _ids.Encode(res.Value.Id),
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

    /// <summary>
    /// Deletes creator autherized project
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpDelete("delete-project")]
    [Authorize(AuthenticationSchemes = IPermissionTokenProvider.PermissionSchemeName)]
    public async Task<IActionResult> DeleteProject([FromQuery] DeleteProjectRequest request, CancellationToken token = default)
    {
        var id = HttpContext.User
            .Claims
            .FirstOrDefault(claim => claim.Type == IPermissionTokenProvider.UserIdClaim);

        if (id?.Value is null)
            return Unauthorized();

        var res = await _mediator.Send(new DeleteProjectCommand.Command
        {
            ProjectId = _ids.DecodeSingle(request.Id),
            CreatorId = id.Value,
        });

        if (res.IsFailed)
        {
            if (res.HasError<NotFoundError>())
                return NotFound(request.Id);

            // defined errors

            return Problem(); // undefined errors
        }

        return Ok(new DeleteProjectResponse
        {

        });
    }

    #endregion

}
