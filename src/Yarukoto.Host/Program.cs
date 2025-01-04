using AutoMapper;
using Yarukoto.Host;
using Yarukoto.Host.DTO;
using Yarukoto.Data;
using Yarukoto.Data.Model;
using Yarukoto.Host.Profiles;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<WorkspaceProfile>();
    cfg.AddProfile<NoteProfile>();
});
// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        corsPolicyBuilder =>
        {
            corsPolicyBuilder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

var app = builder.Build();

// Use CORS policy
app.UseCors("AllowAllOrigins");

// todo: this should be moved to a separate task
app.MapGet("/setup", async () =>
{
    try
    {
        await using var db = new YarukotoDbContext();
        await db.Database.EnsureCreatedAsync();
        await db.Database.MigrateAsync();
        return Results.Ok();
    }
    catch (Exception ex)
    {
        // Log the exception (you can use a logging framework here)
        return Results.Problem(ex.Message);
    }
});

var workspaceApi = app.MapGroup("/workspace");
workspaceApi.MapPost("/", async (IMapper mapper, WorkspaceDto workspace) =>
{
    await using var db = new YarukotoDbContext();
    if(db.Workspaces.Any(x => x.WorkspaceId == workspace.Id))
        return Results.Conflict();
    db.Workspaces.Add(mapper.Map<WorkspaceDto, Workspace>(workspace));
    await db.SaveChangesAsync();
    return Results.Ok(workspace);
});
workspaceApi.MapGet("/{workspaceId}", async (IMapper mapper, string workspaceId) =>
{
    await using var db = new YarukotoDbContext();
    var dbWorkspace = 
        db.Workspaces.FirstOrDefault(x => x.WorkspaceId == workspaceId);
    return dbWorkspace is not null ? Results.Ok(mapper.Map<Workspace, WorkspaceDto>(dbWorkspace)) : Results.NotFound();
});

var notesApi = workspaceApi.MapGroup("/{workspaceId}/notes");
notesApi.MapGet("/", async (IMapper mapper, string workspaceId) =>
{
    await using var db = new YarukotoDbContext();
    var notes = db.Notes
        .Where(x => x.WorkspaceId == workspaceId)
        .AsEnumerable()
        .Select(mapper.Map<Note, NoteDto>)
        .ToArray();
    return Results.Ok(notes);
});
notesApi.MapPost("/", async (IMapper mapper, string workspaceId, NoteDto note) =>
{
    await using var db = new YarukotoDbContext();
    var maxId = db.Notes
        .Where(x => x.WorkspaceId == workspaceId)
        .Max(x => x.NoteId);

    var dbNote = 
        mapper.Map<NoteDto, Note>(note);
    dbNote.NoteId = (int.Parse(maxId ?? "0") + 1).ToString();
    db.Notes.Add(dbNote);

    await db.SaveChangesAsync();
    
    return Results.Ok(mapper.Map<Note, NoteDto>(dbNote));
});
notesApi.MapGet("/{noteId}", async (IMapper mapper, string workspaceId, string noteId) =>
{
    await using var db = new YarukotoDbContext();
    var dbNote = db.Notes.FirstOrDefault(x => x.WorkspaceId == workspaceId && x.NoteId == noteId);
    return dbNote is not null ? Results.Ok(mapper.Map<Note, NoteDto>(dbNote)) : Results.NotFound();
});
notesApi.MapDelete("/{noteId}", async (string workspaceId, string noteId) =>
{
    await using var db = new YarukotoDbContext();
    var dbNote = db.Notes.FirstOrDefault(x => x.WorkspaceId == workspaceId && x.NoteId == noteId);
    if(dbNote is null)
        return Results.NotFound();
    db.Notes.Remove(dbNote);
    await db.SaveChangesAsync();
    return Results.Ok();
});
notesApi.MapPut("/{noteId}", async (IMapper mapper, string workspaceId, string noteId, NoteDto note) =>
{
    await using var db = new YarukotoDbContext();
    var dbNote = 
        db.Notes.FirstOrDefault(x => x.WorkspaceId == workspaceId && x.NoteId == noteId);
    if(dbNote is null)
        return Results.NotFound();
    dbNote.Title = note.Title;
    dbNote.Date = note.Date;
    dbNote.Content = note.Content;
    dbNote.Color = note.Color;
    db.Notes.Update(dbNote);
    await db.SaveChangesAsync();
    return Results.Ok();
});

app.Run();