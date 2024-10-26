using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Dima.Core.Handlers;
using Dima.Core.Models.Identity;
using Dima.Core.Request.Account;
using Dima.Core.Response;

namespace Dima.Web.Handlers;

public class AccountHandler(IHttpClientFactory httpClientFactory) : IAccountHandler
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(Configuration.HttpClientName);
    public async Task<Response<string>> LoginAsync(LoginRequest request)
    {
        var result = await _httpClient.PostAsJsonAsync("v1/identity/login?useCookies=true", request);
        return result.IsSuccessStatusCode
            ? new Response<string>("Login realizado com sucesso!", 200, "Login realizado com sucesso!")
            : new Response<string>(null, (int)result.StatusCode, "Não foi possível realizar login");
    }
    public async Task<Response<string>> RegisterAsync(RegisterRequest request)
    {
        var result = await _httpClient.PostAsJsonAsync("v1/identity/register", request);
        if (result.IsSuccessStatusCode)
        {
            return new Response<string>("Cadastro realizado com sucesso!", 201, "Cadastro realizado com sucesso!");
        }
        var content = await result.Content.ReadAsStringAsync();
        var identityRegisterError = JsonSerializer.Deserialize<IdentityRegisterError>(content);
        return new Response<string>(null, (int)result.StatusCode, identityRegisterError?.Errors?.ToString());
    }
    public async Task LogoutAsync()
    {
        var emptyContent = new StringContent("{}", Encoding.UTF8, "application/json");
        await _httpClient.PostAsync("v1/identity/logout", emptyContent);
    }
}