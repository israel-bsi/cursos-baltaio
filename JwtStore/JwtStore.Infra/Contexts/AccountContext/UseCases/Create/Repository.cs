using JwtStore.Core.Contexts.AccountContext.Entities;
using JwtStore.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using JwtStore.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace JwtStore.Infra.Contexts.AccountContext.UseCases.Create;

public class Repository(AppDbContext context) : IRepository
{
    public async Task<bool> AnyAsync(string email, CancellationToken cancellationToken)
    {
        var response = await context
            .Users
            .AsNoTracking()
            .AnyAsync(x => x.Email.Address == email, cancellationToken: cancellationToken);
        return response;
    }

    public async Task SaveAsync(User user, CancellationToken cancellationToken)
    {
        await context.Users.AddAsync(user, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}