using JwtStore.Core.Contexts.AccountContext.Entities;
using JwtStore.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using JwtStore.Core.Contexts.AccountContext.ValueObjects;
using MediatR;
using Email = JwtStore.Core.Contexts.AccountContext.ValueObjects.Email;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Create;

public class Handler(IRepository repository, IService service) : IRequestHandler<Request, Response>
{
    
    public async Task<Response> Handle(
        Request request, CancellationToken cancellationToken)
    {
        #region 01 - Valida a requisição

        try
        {
            var res = Specification.Ensure(request);
            if (!res.IsValid)
                return new Response("Requisição inválida", 400, res.Notifications);
        }
        catch
        {
            return new Response(
                "Não foi possível validar sua requisição", 500);
        }

        #endregion

        #region 02 - Gerar os objetos

        Email email;
        Password password;
        User user;

        try
        {
            email = new Email(request.Email);
            password = new Password(request.Password);
            user = new User(request.Name, email, password);
        }
        catch (Exception ex)
        {
            return new Response(ex.Message, 500);
        }

        #endregion

        #region 03 - Verificar se o usuário já existe

        try
        {
            var exists = await repository.AnyAsync(request.Email, cancellationToken);
            if (exists)
                return new Response("Usuário já cadastrado", 400);
        }
        catch
        {
            return new Response("Falha ao verificar E-mail cadastrado", 500);
        }

        #endregion

        #region 04 - Persistir os dados

        try
        {
            await repository.SaveAsync(user, cancellationToken);
        }
        catch
        {
            return new Response("Falha ao salvar o usuário", 500);
        }

        #endregion

        #region 05 - Enviar o email de ativação

        try
        {
            await service.SendVerificationEmailAsync(user, cancellationToken);
        }
        catch
        {
            // Ignore
        }

        #endregion

        return new Response("Usuário cadastrado com sucesso", 
            new ResponseData(user.Id, user.Name, user.Email));
    }
}