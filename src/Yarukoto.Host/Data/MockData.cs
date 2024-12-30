using Yarukoto.Host.Model;

namespace Yarukoto.Host.Data;

public static class MockData
{
    public static List<Workspace> Workspaces =
    [
        new("22371a1d-8420-4c5c-a200-8c6ab14e1e8f", "My Workspace", "your@email.com")
    ];

    private static readonly string[] RandomContents =
    [
        "Discuss project milestones",
        "Plan the next sprint",
        "Review the budget",
        "Analyze client feedback",
        "Prepare the weekly report",
        "Brainstorm new ideas",
        "Organize team meeting",
        "Update project documentation",
        "Evaluate project risks",
        "Plan product launch"
    ];

    private static readonly string[] Colors =
    [
        "indigo", "yellow", "blue", "purple", "green", "red"
    ];

    private static readonly Random Random = new();

    private static string GetRandomContent() => RandomContents[Random.Next(RandomContents.Length)];
    private static string GetRandomColor() => Colors[Random.Next(Colors.Length)];

    public static List<Note> Notes =
    [
        new Note("1", "22371a1d-8420-4c5c-a200-8c6ab14e1e8f", "Meeting Notes", DateTime.Now, GetRandomContent(), GetRandomColor()),
        new Note("2", "22371a1d-8420-4c5c-a200-8c6ab14e1e8f", "Project Plan", DateTime.Now.AddDays(1), GetRandomContent(), GetRandomColor()),
        new Note("3", "22371a1d-8420-4c5c-a200-8c6ab14e1e8f", "Research Ideas", DateTime.Now.AddDays(2), GetRandomContent(), GetRandomColor()),
        new Note("4", "22371a1d-8420-4c5c-a200-8c6ab14e1e8f", "Client Feedback", DateTime.Now.AddDays(3), GetRandomContent(), GetRandomColor()),
        new Note("5", "22371a1d-8420-4c5c-a200-8c6ab14e1e8f", "Weekly Report", DateTime.Now.AddDays(4), GetRandomContent(), GetRandomColor()),
        new Note("6", "22371a1d-8420-4c5c-a200-8c6ab14e1e8f", null, DateTime.Now.AddDays(5), GetRandomContent(), GetRandomColor()),
        new Note("7", "22371a1d-8420-4c5c-a200-8c6ab14e1e8f", null, DateTime.Now.AddDays(6), GetRandomContent(), GetRandomColor()),
        new Note("8", "22371a1d-8420-4c5c-a200-8c6ab14e1e8f", null, DateTime.Now.AddDays(7), GetRandomContent(), GetRandomColor())
    ];
}