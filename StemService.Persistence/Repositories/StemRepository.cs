using FluentResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StemService.Domain;
using StemService.Domain.Errors;
using StemService.Domain.Repositories;
using StemService.Persistence.Context;
using StemService.Persistence.Errors;

namespace StemService.Persistence.Repositories;

internal class StemRepository : IStemRepository
{

    #region Fields

    private readonly ILogger<StemRepository> _logger;
    private readonly StemContext _context;

    #endregion

    #region Properties

    public string Name { get; set; } = nameof(StemRepository);

    #endregion

    #region Constructor

    public StemRepository(ILogger<StemRepository> logger, StemContext context)
    {
        _logger = logger;
        _context = context;
    }

    #endregion

    #region Methods

    public async Task<Result<IEnumerable<Stem>>> GetStemsByProjectId(int projectId, CancellationToken token = default)
    {
        try
        {
            var user = await _context.Stems
                .Where(u => u.ProjectId == projectId).ToArrayAsync(token);

            if (user is null)
                return new NotFoundError();

            return user;
        }
        catch (Exception e)
        {
            return Result.Fail(new DatabaseError(e));
        }
    }

    public async Task<Result<Stem>> Upload(Stem stem, CancellationToken token = default)
    {
        try
        {
            await _context.Stems.AddAsync(stem, token);
            return Result.Ok();
        }
        catch (Exception e)
        {
            return Result.Fail(new DatabaseError(e));
        }
    }

    public async Task<Result<Stem>> GetStemById(Guid stemId, CancellationToken token = default)
    {
        try
        {
            var stem = await _context.Stems
                .FirstOrDefaultAsync(stem => stem.Id == stemId, token);

            if(stem is null)
                return new NotFoundError();

            return stem;
        }
        catch (Exception e)
        {
            return Result.Fail(new DatabaseError(e));
        }
    }

    public async Task<Result<IEnumerable<Stem>>> GetAllStems(CancellationToken cancellationToken)
    {
        try
        {
            var stem = await _context.Stems
                .ToArrayAsync(cancellationToken);

            return stem;
        }
        catch (Exception e)
        {
            return Result.Fail(new DatabaseError(e));
        }
    }

    public override string ToString() => Name;

    #endregion
}
