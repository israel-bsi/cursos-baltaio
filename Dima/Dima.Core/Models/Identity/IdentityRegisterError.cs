using System.Text.Json.Serialization;

namespace Dima.Core.Models.Identity;

public class IdentityRegisterError
{
    [JsonPropertyName("type")]
    public string? Type { get; set; }
    [JsonPropertyName("title")]
    public string? Title { get; set; }
    [JsonPropertyName("status")]
    public int Status { get; set; }
    [JsonPropertyName("errors")]
    public List<string>? Errors { get; set; }
}