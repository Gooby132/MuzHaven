using DomainSeed.CommonErrors;
using DomainSeed.ValueObjects.Auth;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectService.Domain;
using ProjectService.Domain.Repositories;
using ProjectService.Persistence.Context;
using ProjectService.Persistence.Errors;
using System.Threading;

namespace ProjectService.Persistence.Repositories;

internal class ProjectRepository : IProjectRepository
{

    #region Fields

    private readonly ILogger<ProjectRepository> _logger;
    private readonly ProjectContext _context;

    #endregion

    #region Properties

    public string Name { get; set; } = nameof(ProjectRepository);

    #endregion

    #region Constructor

    public ProjectRepository(ILogger<ProjectRepository> logger, ProjectContext context)
    {
        _logger = logger;
        _context = context;
    }

    #endregion

    #region Methods

    public async Task<Result<Project>> CreateProject(Project project, CancellationToken token = default)
    {
        try
        {
            await _context.Projects.AddAsync(project, token);

            return Result.Ok(project);
        }
        catch (Exception e)
        {
            return Result.Fail(new DatabaseError(e));
        }
    }

    public async Task<Result<Project>> GetProjectById(int projectId, CancellationToken token = default)
    {
        try
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == projectId, token);

            if (project is null)
                return new NotFoundError();

            return project;
        }
        catch (Exception e)
        {
            return Result.Fail(new DatabaseError(e));
        }
    }

    public async Task<Result<IEnumerable<Project>>> GetProjectsByCreatorId(string creatorId, CancellationToken cancellationToken)
    {
        try
        {
            var projects = await _context.Projects
                .Where(project => project.CreatorId == creatorId)
                .ToListAsync(cancellationToken);

            return Result.Ok<IEnumerable<Project>>(projects);
        }
        catch (Exception e)
        {
            return Result.Fail(new DatabaseError(e));
        }
    }

    public async Task<Result<IEnumerable<Project>>> GetAllProjects(CancellationToken cancellationToken)
    {
        try
        {
            var projects = await _context.Projects
                .ToListAsync(cancellationToken);

            return Result.Ok<IEnumerable<Project>>(projects);
        }
        catch (Exception e)
        {
            return Result.Fail(new DatabaseError(e));
        }
    }

    public Task<Result> DeleteProject(Project project, CancellationToken token = default)
    {
        try
        {
            _context.Projects.Remove(project);

            return Task.FromResult(Result.Ok());
        }
        catch (Exception e)
        {
            return Task.FromResult(Result.Fail(new DatabaseError(e)));
        }
    }

    #endregion

}
