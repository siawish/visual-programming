using System;
using System.Collections.Generic;

namespace StockROICalculator
{
    public class InvestmentScenario
    {
        public string Name { get; set; }
        public double InvestmentAmount { get; set; }
        public double FutureStockPrice { get; set; }
        public bool IsFiler { get; set; }
        public string SelectedCompany { get; set; }
        public DateTime CreatedDate { get; set; }
        public Dictionary<string, double> CompanyROIs { get; set; }

        public InvestmentScenario()
        {
            CompanyROIs = new Dictionary<string, double>();
            CreatedDate = DateTime.Now;
        }

        public InvestmentScenario(string name, double investment, double futurePrice, bool isFiler, string company)
        {
            Name = name;
            InvestmentAmount = investment;
            FutureStockPrice = futurePrice;
            IsFiler = isFiler;
            SelectedCompany = company;
            CreatedDate = DateTime.Now;
            CompanyROIs = new Dictionary<string, double>();
        }
    }

    public class CalculationResult
    {
        public Company Company { get; set; }
        public double InvestmentAmount { get; set; }
        public double FutureStockPrice { get; set; }
        public bool IsFiler { get; set; }
        public double SharesBought { get; set; }
        public double FutureValue { get; set; }
        public double GrossProfit { get; set; }
        public double CompanyTax { get; set; }
        public double GovernmentTax { get; set; }
        public double TotalTax { get; set; }
        public double NetProfit { get; set; }
        public double ROI { get; set; }
        public DateTime CalculationDate { get; set; }

        public CalculationResult()
        {
            CalculationDate = DateTime.Now;
        }
    }
}