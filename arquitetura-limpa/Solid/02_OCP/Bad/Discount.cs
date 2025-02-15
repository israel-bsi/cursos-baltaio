namespace Solid._02_OCP.Bad;

public enum EProductType
{
    Electronics = 1,
    Health = 2,
    Bealty = 3,
    Fashion = 4
}

public class Discount
{
    public decimal Calculate(EProductType type, decimal price)
    {
        if (type == EProductType.Electronics)
            return price * 0.2m;

        if (type == EProductType.Health)
            return price * 0.3m;

        if (type == EProductType.Bealty)
            return price * 0.4m;

        if (type == EProductType.Fashion)
            return price * 0.5m;

        return price;
    }
}