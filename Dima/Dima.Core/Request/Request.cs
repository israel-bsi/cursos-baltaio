using System.Text.Json.Serialization;

namespace Dima.Core.Request;

public abstract class Request
{
    [JsonIgnore]
    public string UserId { get; set; } = string.Empty;
}