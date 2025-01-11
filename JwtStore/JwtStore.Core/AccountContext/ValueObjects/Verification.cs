using JwtStore.Core.SharedContext.ValueObjects;

namespace JwtStore.Core.AccountContext.ValueObjects;

public class Verification : ValueObject
{
    public string Code { get; } = Guid.NewGuid().ToString("N")[..6].ToUpper();
    public DateTime? ExpiresAt { get; private set; } = DateTime.UtcNow.AddMinutes(5);
    public DateTime? VerifiedAt { get; private set; } = null;
    public bool IsActive => VerifiedAt != null && ExpiresAt == null;

    public Verification() { }

    public void Verify(string code)
    {
        if (IsActive)
            throw new Exception("Esse código já foi ativado");

        if (ExpiresAt < DateTime.Now)
            throw new Exception("Código expirado");

        if (!string.Equals(Code.Trim(), code.Trim(), StringComparison.CurrentCultureIgnoreCase))
            throw new Exception("Código de verificação inválido");

        VerifiedAt = DateTime.UtcNow;
        ExpiresAt = null;
    }
}