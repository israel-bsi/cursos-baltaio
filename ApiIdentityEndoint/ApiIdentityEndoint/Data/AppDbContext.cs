using ApiIdentityEndoint.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApiIdentityEndoint.Data;

public class AppDbContext(DbContextOptions options) : IdentityDbContext<User>(options);