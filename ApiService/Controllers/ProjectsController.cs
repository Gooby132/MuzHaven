using ApiService.Application.Projects.Queries;
using ProjectService.Contracts.Requests;
using ProjectService.Contracts.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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

    [HttpPost("create-project")]
    public async Task<IActionResult> Create(CreateProjectRequest request, CancellationToken token = default)
    {

        var res = await _mediator.Send(new Application.Projects.Commands.CreateProject.Command
        {
            Name = request.Name
        });

        if (res.IsFailed)
        {
            // defined errors

            return Problem(); // undefined errors
        }

        return CreatedAtAction(
           nameof(GetById),
           new { res.Value.Id },
           new CreateProjectResponse
           {
               Project = new ProjectService.Contracts.Dtos.ProjectDto
               {
                   Id = res.Value.Id,
                   Name = res.Value.MetaData.Name,
               }
           });
    }

    [HttpGet("get-by-id")]
    public async Task<IActionResult> GetById(GetProjectByIdRequest request, CancellationToken token = default)
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
            Project = new ProjectService.Contracts.Dtos.ProjectDto
            {
                Id = res.Value.Id,
                Name = res.Value.MetaData.Name,
            }
        });
    }

    #endregion


}
