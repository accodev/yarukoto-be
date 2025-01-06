using Microsoft.EntityFrameworkCore;

namespace Yarukoto.Data.Model;

[PrimaryKey(nameof(NoteId), nameof(WorkspaceId))]
public class Note
{
    public required int NoteId { get; set; }
    public required string WorkspaceId { get; set; }
    public required int Order { get; set; } 
    public required string? Title { get; set; }
    public required DateTime Date { get; set; }
    public required string Content { get; set; }
    public required string Color { get; set; }
    
    public virtual Workspace Workspace { get; set; }
}
