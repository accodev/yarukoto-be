namespace Yarukoto.Data.Model;

public class Workspace
{
    public required string WorkspaceId { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    
    public virtual List<Note> Notes { get; set; }
}

