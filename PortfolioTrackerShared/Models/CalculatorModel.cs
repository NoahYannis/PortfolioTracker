using System.ComponentModel.DataAnnotations;

namespace CompoundInterestCalc
{
    public class CalculatorModel
    {
        [Required(ErrorMessage = "Starting sum required."), Range(0.01f, double.MaxValue, ErrorMessage = "Starting Sum Must Be Greater Than $0.")]
        public double StartingSum { get; set; } = 0.00;

        [Required(ErrorMessage = "Monthly Contribution Required."), Range(0, double.MaxValue, ErrorMessage = "Monthly Contribution Must Not Be Lesser Than $0.")]
        public double MonthlyContribution { get; set; } = 0.00;

        [Required(ErrorMessage = "Interest Rate Required."), Range(0, double.MaxValue, ErrorMessage = "Interest Rate Must Be Between 0 And 100%.")]
        public double InterestRate { get; set; } = 0.00;

        [Range(-20, double.MaxValue, ErrorMessage = "Please enter a valid inflation rate.")]
        public double InflationRate { get; set; } = 0.00;

        [Required(ErrorMessage = "Number Of Years Required."), Range(1, double.MaxValue, ErrorMessage = "Must Be At Least 1 Year.")]
        public int NumberOfYears { get; set; } = 0;

        public bool ConsiderInflation { get; set; } = false;
    }
}
