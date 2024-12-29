namespace Yarukoto_BE.Model;

public record Note(string? Id, string WorkspaceId, string? Title, DateTime Date, string Content, string Color);
