using ChartJs.Blazor.Util;
using PortfolioTrackerShared.Models;
using System.Drawing;

namespace PortfolioTrackerClient.Other.PieChartStrategies;

public class FilterByAbsolutePerformance : IPieChartStrategy
{
    private readonly List<PortfolioStock> _portfolioStocks;
    private Random _random = new();

    public List<string> Labels { get; set; } = new();
    public List<string> SliceColors { get; set; } = new();
    public List<decimal?> SliceValues { get; set; } = new();

    public FilterByAbsolutePerformance(List<PortfolioStock> portfolioStocks)
    {
        _portfolioStocks = portfolioStocks;
    }

    public void GeneratePieChart()
    {
        var sortedPortfolio = _portfolioStocks.OrderByDescending(stock => stock.AbsolutePerformance).ToList();

        foreach (PortfolioStock stock in sortedPortfolio)
        {
            Labels.Add(stock.Ticker);
            SliceValues.Add(Math.Abs(stock.AbsolutePerformance ?? 0));
            Color randomColor = stock.AbsolutePerformance > 0 ? Color.Green : Color.Red;
            string colorHex = ColorUtil.ColorHexString(randomColor.R, randomColor.G, randomColor.B);
            SliceColors.Add(colorHex);
        }
    }
}
