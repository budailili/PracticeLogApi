using Microsoft.EntityFrameworkCore;
using PracticeLogApi;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PracticeLogDb>(opt => opt.UseInMemoryDatabase("PracticeLogList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

RouteGroupBuilder practiceItems = app.MapGroup("/practicesessions");

practiceItems.MapGet("/", GetAllSessions);
practiceItems.MapGet("/mastered", GetMasteredSessions);
practiceItems.MapGet("/{id}", GetSession);
practiceItems.MapPost("/", CreateSession);
practiceItems.MapPut("/{id}", UpdateSession);
practiceItems.MapPatch("/{id}", PatchSession);
practiceItems.MapDelete("/{id}", DeleteSession);

app.Run();

static async Task<IResult> GetAllSessions(PracticeLogDb db)
{
    return TypedResults.Ok(await db.Sessions.Select(x => new PracticeSessionDTO(x)).ToArrayAsync());
}

static async Task<IResult> GetMasteredSessions(PracticeLogDb db)
{
    return TypedResults.Ok(await db.Sessions.Where(t => t.IsMastered).Select(x => new PracticeSessionDTO(x)).ToListAsync());
}

static async Task<IResult> GetSession(int id, PracticeLogDb db)
{
    return await db.Sessions.FindAsync(id)
        is PracticeSession session
            ? TypedResults.Ok(new PracticeSessionDTO(session))
            : TypedResults.NotFound();
}

static async Task<IResult> CreateSession(PracticeSessionDTO sessionDTO, PracticeLogDb db)
{
    var sessionItem = new PracticeSession
    {
        SongTitle = sessionDTO.SongTitle,
        Tuning = sessionDTO.Tuning,
        TargetBpm = sessionDTO.TargetBpm,
        IsMastered = sessionDTO.IsMastered
    };

    db.Sessions.Add(sessionItem);
    await db.SaveChangesAsync();

    sessionDTO = new PracticeSessionDTO(sessionItem);

    return TypedResults.Created($"/practicesessions/{sessionItem.Id}", sessionDTO);
}

static async Task<IResult> UpdateSession(int id, PracticeSessionDTO sessionDTO, PracticeLogDb db)
{
    var session = await db.Sessions.FindAsync(id);

    if (session is null) return TypedResults.NotFound();

    session.SongTitle = sessionDTO.SongTitle;
    session.Tuning = sessionDTO.Tuning;
    session.TargetBpm = sessionDTO.TargetBpm;
    session.IsMastered = sessionDTO.IsMastered;

    await db.SaveChangesAsync();

    return TypedResults.NoContent();
}

static async Task<IResult> PatchSession(int id, PracticeSessionPatchDTO patchDTO, PracticeLogDb db)
{
    var session = await db.Sessions.FindAsync(id);

    if (session is null) return TypedResults.NotFound();

    if (patchDTO.SongTitle is not null)
        session.SongTitle = patchDTO.SongTitle;

    if (patchDTO.Tuning is not null)
        session.Tuning = patchDTO.Tuning;

    if (patchDTO.TargetBpm.HasValue)
        session.TargetBpm = patchDTO.TargetBpm.Value;

    if (patchDTO.IsMastered.HasValue)
        session.IsMastered = patchDTO.IsMastered.Value;

    await db.SaveChangesAsync();

    return TypedResults.Ok(new PracticeSessionDTO(session));
}

static async Task<IResult> DeleteSession(int id, PracticeLogDb db)
{
    if (await db.Sessions.FindAsync(id) is PracticeSession session)
    {
        db.Sessions.Remove(session);
        await db.SaveChangesAsync();
        return TypedResults.NoContent();
    }

    return TypedResults.NotFound();
}