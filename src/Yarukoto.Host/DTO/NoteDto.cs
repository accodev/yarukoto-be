namespace Yarukoto.Host.DTO;

public class NoteDto
{
    public int? Id { get; set; }
    public string WorkspaceId { get; set; } = null!;
    public int Order { get; set; }
    public string? Title { get; set; }
    public DateTime Date { get; set; }
    public string Content { get; set; } = null!;
    public string Color { get; set; } = null!;

    public NoteDto() { }

    public NoteDto(int? id, string workspaceId, string? title, DateTime date, string content, string color)
    {
        Id = id;
        WorkspaceId = workspaceId;
        Title = title;
        Date = date;
        Content = content;
        Color = color;
    }
}
