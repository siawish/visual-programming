namespace StockROICalculator
{
    public class Company
    {
        public string Name { get; set; }
        public double CompanyTaxRate { get; set; }
        public double CurrentStockPrice { get; set; }
        public double MarketValue { get; set; }

        public Company(string name, double companyTaxRate, double currentStockPrice, double marketValue)
        {
            Name = name;
            CompanyTaxRate = companyTaxRate;
            CurrentStockPrice = currentStockPrice;
            MarketValue = marketValue;
        }

        public double CalculateROI(double investmentAmount, double futureStockPrice, bool isFiler)
        {
            // Calculate number of shares that can be bought
            double sharesBought = investmentAmount / CurrentStockPrice;
            
            // Calculate future value
            double futureValue = sharesBought * futureStockPrice;
            
            // Calculate profit
            double profit = futureValue - investmentAmount;
            
            if (profit <= 0)
            {
                // No profit, no tax
                return (profit / investmentAmount) * 100;
            }
            
            // Calculate taxes
            double companyTax = profit * (CompanyTaxRate / 100);
            double governmentTax = profit * (isFiler ? 0.02 : 0.04); // 2% for filer, 4% for non-filer
            
            double totalTax = companyTax + governmentTax;
            double netProfit = profit - totalTax;
            
            // Calculate ROI percentage
            return (netProfit / investmentAmount) * 100;
        }

        public override string ToString()
        {
            return Name;
        }

        public string GetDisplayInfo()
        {
            return $"Tax Rate: {CompanyTaxRate}% | Stock Price: ${CurrentStockPrice:F2} | Market Value: ${MarketValue:N0}";
        }
    }
}