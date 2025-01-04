using System.Text.Json.Serialization;
using Yarukoto.Host.DTO;

namespace Yarukoto.Host;

[JsonSerializable(typeof(WorkspaceDto))]
[JsonSerializable(typeof(NoteDto))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}