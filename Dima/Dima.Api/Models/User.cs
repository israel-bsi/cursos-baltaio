using Microsoft.AspNetCore.Identity;

namespace Dima.Api.Models;

public class User : IdentityUser<long>
{
    //RBAC - Role Based Access Control
    //Claims - direitos do usuário
    public List<IdentityRole<long>>? Roles { get; set; }
}