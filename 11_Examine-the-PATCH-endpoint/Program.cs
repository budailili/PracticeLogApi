using Microsoft.EntityFrameworkCore;
using PracticeLogApi;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PracticeLogDb>(opt => opt.UseInMemoryDatabase("PracticeLogList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

app.MapGet("/practicesessions", async (PracticeLogDb db) =>
    await db.Sessions.ToListAsync());

app.MapGet("/practicesessions/mastered", async (PracticeLogDb db) =>
    await db.Sessions.Where(t => t.IsMastered).ToListAsync());

app.MapGet("/practicesessions/{id}", async (int id, PracticeLogDb db) =>
    await db.Sessions.FindAsync(id)
        is PracticeSession session
            ? Results.Ok(session)
            : Results.NotFound());

app.MapPost("/practicesessions", async (PracticeSession session, PracticeLogDb db) =>
{
    db.Sessions.Add(session);
    await db.SaveChangesAsync();

    return Results.Created($"/practicesessions/{session.Id}", session);
});

app.MapPut("/practicesessions/{id}", async (int id, PracticeSession inputSession, PracticeLogDb db) =>
{
    var session = await db.Sessions.FindAsync(id);

    if (session is null) return Results.NotFound();

    session.SongTitle = inputSession.SongTitle;
    session.Tuning = inputSession.Tuning;
    session.TargetBpm = inputSession.TargetBpm;
    session.IsMastered = inputSession.IsMastered;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapPatch("/practicesessions/{id}", async (int id, PracticeSessionPatchDTO patchDTO, PracticeLogDb db) =>
{
    var session = await db.Sessions.FindAsync(id);

    if (session is null) return Results.NotFound();

    if (patchDTO.SongTitle is not null)
        session.SongTitle = patchDTO.SongTitle;

    if (patchDTO.Tuning is not null)
        session.Tuning = patchDTO.Tuning;

    if (patchDTO.TargetBpm.HasValue)
        session.TargetBpm = patchDTO.TargetBpm.Value;

    if (patchDTO.IsMastered.HasValue)
        session.IsMastered = patchDTO.IsMastered.Value;

    await db.SaveChangesAsync();

    return Results.Ok(session);
});

app.MapDelete("/practicesessions/{id}", async (int id, PracticeLogDb db) =>
{
    if (await db.Sessions.FindAsync(id) is PracticeSession session)
    {
        db.Sessions.Remove(session);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});

app.Run();