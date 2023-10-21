using ChartJs.Blazor.Util;
using PortfolioTrackerShared.Models;
using System.Drawing;
using System;

namespace PortfolioTrackerClient.Other.PieChartStrategies
{
    public class FilterByIndustry : IPieChartStrategy
    {
        private readonly List<PortfolioStock> _portfolioStocks;

        public List<string> Labels { get; set; } = new();
        public List<string> SliceColors { get; set; } = new();
        public List<decimal?> SliceValues { get; set; } = new();

        public FilterByIndustry(List<PortfolioStock> portfolioStocks)
        {
            _portfolioStocks = portfolioStocks;
        }

        public void GeneratePieChart()
        {
            foreach (PortfolioStock stock in _portfolioStocks)
            {
                Labels.Add(stock.Ticker);
                SliceValues.Add(stock.PositionSize);
                Color randomColor = GetSliceColor(stock);
                string colorHex = ColorUtil.ColorHexString(randomColor.R, randomColor.G, randomColor.B);
                SliceColors.Add(colorHex);
            }
        }

        private Color GetSliceColor(PortfolioStock stock)
        {
            switch (stock.Industry)
            {
                case Industry.Technology:
                    return Color.Blue;
                case Industry.Healthcare:
                    return Color.Green;
                case Industry.Financial:
                    return Color.Gold;
                case Industry.Energy:
                    return Color.Orange;
                case Industry.Materials:
                    return Color.Gray;
                case Industry.Real_Estate:
                    return Color.Purple;
                case Industry.Consumer_Staples:
                    return Color.Pink;
                case Industry.Consumer_Discretionary:
                    return Color.Red;
                case Industry.Utilities:
                    return Color.Cyan;
                case Industry.Industrials:
                    return Color.Navy;
                default:
                    return Color.Black;
            }
        }
    }
}
