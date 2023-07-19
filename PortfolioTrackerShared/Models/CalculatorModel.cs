namespace CompoundInterestCalc
{
    public class CalculatorModel
    {
        public double StartingSum { get; set; } = 0.00;
        public double MonthlyContribution { get; set; } = 0.00;
        public double InterestRate { get; set; } = 7.00;
        public double InflationRate { get; set; } = 0.00;
        public double TaxRate { get; set; } = 0.00;
        public int NumberOfYears { get; set; } = 40;
        public bool ConsiderInflation { get; set; } = false;
        public bool ConsiderTaxes { get; set;} = false;
    }
}
