namespace Solid._02_OCP.Good;

public enum EProductType
{
    Electronics = 1,
    Health = 2,
    Bealty = 3,
    Fashion = 4
}

public abstract class Discount
{
    public abstract decimal Calculate(decimal price);
}

public class ElectronicsDiscounts : Discount
{
    public override decimal Calculate(decimal price)
    {
        return price * 0.2m;
    }
}

public class HealthDiscounts : Discount
{
    public override decimal Calculate(decimal price)
    {
        return price * 0.3m;
    }
}

public class BealtyDiscounts : Discount
{
    public override decimal Calculate(decimal price)
    {
        return price * 0.3m;
    }
}

public class FashionDiscounts : Discount
{
    public override decimal Calculate(decimal price)
    {
        return price * 0.4m;
    }
}