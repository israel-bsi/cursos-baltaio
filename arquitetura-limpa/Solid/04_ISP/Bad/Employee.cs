namespace Solid._04_ISP.Bad;

public interface IEmployee
{
    string Name { get; set; }
    void CalculateSalary();
    void CalculateBenefits();
}

public class FullTimeEmployee : IEmployee
{
    public string Name { get; set; } = string.Empty;
    public void CalculateSalary()
    {
        Console.WriteLine("Calculate Salary");
    }

    public void CalculateBenefits()
    {
        Console.WriteLine("Calculate Benefits");
    }
}

public class ContractEmployee : IEmployee
{
    public string Name { get; set; } = string.Empty;
    public void CalculateSalary()
    {
        Console.WriteLine("Calculate Salary");
    }
    public void CalculateBenefits()
    {
        throw new NotImplementedException();
    }
}