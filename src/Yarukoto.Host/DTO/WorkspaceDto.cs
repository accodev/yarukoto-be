namespace Yarukoto.Host.DTO;

public class WorkspaceDto
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;

    public WorkspaceDto() { }

    public WorkspaceDto(string id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = email;
    }
}
