using ChartJs.Blazor.Util;
using PortfolioTrackerShared.Models;
using System.Drawing;

namespace PortfolioTrackerClient.Other.PieChartStrategies
{
    public class FilterByPositionSize : IPieChartStrategy
    {
        private Random _random = new();
        private readonly List<PortfolioStock> _portfolioStocks;

        public List<string> Labels { get; set; } = new();
        public List<string> SliceColors { get; set; } = new();
        public List<decimal?> SliceValues { get; set; } = new();

        public FilterByPositionSize(List<PortfolioStock> portfolioStocks)
        {
            _portfolioStocks = portfolioStocks;
        }

        public void GeneratePieChart()
        {
            foreach (PortfolioStock stock in _portfolioStocks)
            {
                Labels.Add(stock.Ticker);
                SliceValues.Add(stock.PositionSize);
                Color randomColor = Color.FromArgb(_random.Next(256), _random.Next(256), _random.Next(256));
                string colorHex = ColorUtil.ColorHexString(randomColor.R, randomColor.G, randomColor.B);
                SliceColors.Add(colorHex);
            }
        }
    }
}
