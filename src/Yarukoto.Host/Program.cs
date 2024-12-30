using System.Text.Json.Serialization;
using Yarukoto.Host.Data;
using Yarukoto.Host.Model;

var builder = WebApplication.CreateSlimBuilder(args);

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

var workspaceApi = app.MapGroup("/workspace");
workspaceApi.MapPost("/", (Workspace workspace) =>
{
    if(MockData.Workspaces.Any(x => x.Id == workspace.Id))
        return Results.Conflict();
    MockData.Workspaces.Add(workspace);
    return Results.Ok(workspace);
});
workspaceApi.MapGet("/{workspaceId}", (string workspaceId) =>
{
    var workspace = MockData.Workspaces.FirstOrDefault(x => x.Id == workspaceId);
    return workspace is not null ? Results.Ok(workspace) : Results.NotFound();
});

var notesApi = workspaceApi.MapGroup("/{workspaceId}/notes");
notesApi.MapGet("/", (string workspaceId) =>
{
    var notes = MockData.Notes.Where(x => x.WorkspaceId == workspaceId).ToArray();
    return Results.Ok(notes);
});
notesApi.MapPost("/", (string workspaceId, Note note) =>
{
    var maxId = MockData.Notes
        .Where(x => x.WorkspaceId == workspaceId)
        .Max(x => x.Id);

    note = note with { Id = (maxId + 1) };

    MockData.Notes.Add(note);
    return Results.Ok(note);
});
notesApi.MapGet("/{noteId}", (string workspaceId, string noteId) =>
{
    var note = MockData.Notes.FirstOrDefault(x => x.WorkspaceId == workspaceId && x.Id == noteId);
    return note is not null ? Results.Ok(note) : Results.NotFound();
});
notesApi.MapDelete("/{noteId}", (string workspaceId, string noteId) =>
{
    var note = MockData.Notes.FirstOrDefault(x => x.WorkspaceId == workspaceId && x.Id == noteId);
    if(note is null)
        return Results.NotFound();
    MockData.Notes.Remove(note);
    return Results.Ok();
});

app.Run();

[JsonSerializable(typeof(Workspace))]
[JsonSerializable(typeof(Workspace[]))]
[JsonSerializable(typeof(Note))]
[JsonSerializable(typeof(Note[]))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}