using PortfolioTrackerShared.Models;

namespace PortfolioTrackerClient.Other.PieChartStrategies
{
    public interface IPieChartStrategy
    {
        List<string> Labels { get; set; }
        List<string> SliceColors { get; set; }
        List<decimal?> SliceValues { get; set; }
        void GeneratePieChart();
    }
}
