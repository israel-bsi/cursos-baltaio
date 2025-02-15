namespace Solid._04_ISP.Good;

public interface ISalaryCalculator
{
    void CalculateSalary();
}

public interface IBenefitsCalculator
{
    void CalculateBenefits();
}

public class FullTimeEmployee : ISalaryCalculator, IBenefitsCalculator
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

public class ContractTimeEmployee : ISalaryCalculator
{
    public string Name { get; set; } = string.Empty;
    public void CalculateSalary()
    {
        Console.WriteLine("Calculate Salary");
    }
}