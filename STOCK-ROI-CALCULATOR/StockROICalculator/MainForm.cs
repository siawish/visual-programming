using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text.Json;

namespace StockROICalculator
{
    public partial class MainForm : Form
    {
        private List<Company> companies;
        private List<InvestmentScenario> scenarios;
        private List<string> calculationHistory;
        
        // UI Components
        private TableLayoutPanel mainLayout;
        private Panel titlePanel;
        private Label titleLabel;
        private ModernGroupBox companyInfoGroup;
        private ModernGroupBox calculationGroup;
        private TabControl resultsTabControl;
        private ModernComboBox companyComboBox;
        private ModernTextBox investmentTextBox;
        private ModernTextBox futureStockPriceTextBox;
        private TrackBar investmentSlider;
        private Label sliderValueLabel;
        private RadioButton filerRadioButton;
        private RadioButton nonFilerRadioButton;
        private ModernButton calculateButton;
        private ModernButton compareAllButton;
        private ModernButton clearButton;
        private ModernButton saveScenarioButton;
        private ModernButton loadScenarioButton;
        private ModernButton exportButton;
        private RichTextBox resultsTextBox;
        private RichTextBox detailsTextBox;
        private DataGridView companyDataGrid;
        private ProgressBar calculationProgress;
        private ToolTip toolTip;
        private System.Windows.Forms.Timer animationTimer;
        private System.Windows.Forms.Timer priceUpdateTimer;
        private Random priceRandom;
        private Dictionary<string, List<double>> priceHistory;
        private Panel chartPanel;
        
        // Quick preset buttons
        private ModernButton preset1K;
        private ModernButton preset5K;
        private ModernButton preset10K;
        private ModernButton preset25K;
        
        // Color scheme
        private static readonly Color PrimaryColor = Color.FromArgb(37, 99, 235);
        private static readonly Color SecondaryColor = Color.FromArgb(16, 185, 129);
        private static readonly Color AccentColor = Color.FromArgb(245, 158, 11);
        private static readonly Color SuccessColor = Color.FromArgb(5, 150, 105);
        private static readonly Color ErrorColor = Color.FromArgb(220, 38, 38);
        private static readonly Color BackgroundColor = Color.FromArgb(248, 250, 252);
        private static readonly Color SurfaceColor = Color.White;
        private static readonly Color TextColor = Color.FromArgb(30, 41, 59);

        public MainForm()
        {
            InitializeComponent();
            InitializeCompanies();
            InitializeCollections();
            InitializePriceHistory();
            SetupUI();
            SetupEventHandlers();
            SetupKeyboardShortcuts();
            LoadUserPreferences();
        }

        private void InitializeCompanies()
        {
            companies = new List<Company>
            {
                new Company("Apple Inc. (AAPL)", 5.0, 189.50, 2950000),
                new Company("Microsoft Corp. (MSFT)", 5.5, 378.85, 2800000),
                new Company("Amazon.com Inc. (AMZN)", 7.5, 145.30, 1520000),
                new Company("Tesla Inc. (TSLA)", 6.3, 248.75, 785000),
                new Company("NVIDIA Corp. (NVDA)", 9.9, 875.25, 2150000)
            };
        }

        private void InitializeCollections()
        {
            scenarios = new List<InvestmentScenario>();
            calculationHistory = new List<string>();
            toolTip = new ToolTip();
            animationTimer = new System.Windows.Forms.Timer();
            animationTimer.Interval = 50;
            animationTimer.Tick += AnimationTimer_Tick;
            
            // Initialize real-time price updates
            priceUpdateTimer = new System.Windows.Forms.Timer();
            priceUpdateTimer.Interval = 2000; // Update every 2 seconds
            priceUpdateTimer.Tick += PriceUpdateTimer_Tick;
            priceRandom = new Random();
            priceHistory = new Dictionary<string, List<double>>();
        }

        private void InitializePriceHistory()
        {
            // Initialize price history for each company
            foreach (var company in companies)
            {
                priceHistory[company.Name] = new List<double> { company.CurrentStockPrice };
            }
            
            priceUpdateTimer.Start();
        }

        private void SetupUI()
        {
            // Main form properties
            this.Text = "Stock ROI Calculator - Advanced Investment Analysis";
            this.Size = new Size(1400, 900);
            this.MinimumSize = new Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = BackgroundColor;
            this.Font = new Font("Segoe UI", 10F);

            // Main layout
            mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(16),
                ColumnCount = 2,
                RowCount = 3
            };
            
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 45F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 55F));
            
            this.Controls.Add(mainLayout);

            SetupTitleSection();
            SetupCompanyInfoSection();
            SetupCalculationSection();
            SetupResultsSection();
        }

        private void SetupTitleSection()
        {
            titlePanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = SurfaceColor,
                Margin = new Padding(0, 0, 0, 8)
            };
            
            titleLabel = new Label
            {
                Text = "📈 Advanced Stock ROI Calculator",
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = PrimaryColor,
                AutoSize = true,
                Location = new Point(20, 20)
            };
            titlePanel.Controls.Add(titleLabel);

            var subtitleLabel = new Label
            {
                Text = "Comprehensive investment analysis with tax optimization",
                Font = new Font("Segoe UI", 11F),
                ForeColor = Color.FromArgb(100, 116, 139),
                AutoSize = true,
                Location = new Point(20, 50)
            };
            titlePanel.Controls.Add(subtitleLabel);

            mainLayout.Controls.Add(titlePanel, 0, 0);
            mainLayout.SetColumnSpan(titlePanel, 2);
        }

        private void SetupCompanyInfoSection()
        {
            companyInfoGroup = new ModernGroupBox
            {
                Text = "📊 Company Selection",
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 8, 8),
                Padding = new Padding(16)
            };

            var companyLabel = new Label
            {
                Text = "Select Company:",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = TextColor,
                Location = new Point(16, 35),
                AutoSize = true
            };
            companyInfoGroup.Controls.Add(companyLabel);

            companyComboBox = new ModernComboBox
            {
                Location = new Point(16, 60),
                Size = new Size(300, 30),
                Font = new Font("Segoe UI", 10F)
            };

            foreach (var company in companies)
            {
                companyComboBox.Items.Add(company);
            }
            companyComboBox.SelectedIndex = 0;
            companyInfoGroup.Controls.Add(companyComboBox);

            // Company details panel
            var detailsPanel = new Panel
            {
                Location = new Point(16, 100),
                Size = new Size(400, 120),
                BackColor = Color.FromArgb(248, 250, 252),
                BorderStyle = BorderStyle.FixedSingle
            };

            var detailsLabel = new Label
            {
                Text = "Company Details:",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Location = new Point(8, 8),
                AutoSize = true
            };
            detailsPanel.Controls.Add(detailsLabel);

            var companyDetailsLabel = new Label
            {
                Name = "companyDetails",
                Location = new Point(8, 30),
                Size = new Size(380, 80),
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(71, 85, 105)
            };
            detailsPanel.Controls.Add(companyDetailsLabel);
            companyInfoGroup.Controls.Add(detailsPanel);

            companyComboBox.SelectedIndexChanged += CompanyComboBox_SelectedIndexChanged;
            UpdateCompanyDetails();

            mainLayout.Controls.Add(companyInfoGroup, 0, 1);
        }

        private void SetupCalculationSection()
        {
            calculationGroup = new ModernGroupBox
            {
                Text = "💰 Investment Parameters",
                Dock = DockStyle.Fill,
                Margin = new Padding(8, 0, 0, 8),
                Padding = new Padding(16)
            };

            // Investment amount with slider
            var investmentLabel = new Label
            {
                Text = "Investment Amount ($):",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = TextColor,
                Location = new Point(16, 35),
                AutoSize = true
            };
            calculationGroup.Controls.Add(investmentLabel);

            investmentTextBox = new ModernTextBox
            {
                Location = new Point(16, 60),
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 10F),
                Text = "1000"
            };
            investmentTextBox.TextChanged += InvestmentTextBox_TextChanged;
            calculationGroup.Controls.Add(investmentTextBox);

            // Quick preset buttons
            var presetPanel = new Panel
            {
                Location = new Point(150, 60),
                Size = new Size(280, 25)
            };

            preset1K = new ModernButton
            {
                Text = "$1K",
                Size = new Size(45, 25),
                Location = new Point(0, 0),
                BackColor = AccentColor,
                ForeColor = Color.White
            };
            preset1K.Click += (s, e) => SetInvestmentAmount(1000);
            presetPanel.Controls.Add(preset1K);

            preset5K = new ModernButton
            {
                Text = "$5K",
                Size = new Size(45, 25),
                Location = new Point(50, 0),
                BackColor = AccentColor,
                ForeColor = Color.White
            };
            preset5K.Click += (s, e) => SetInvestmentAmount(5000);
            presetPanel.Controls.Add(preset5K);

            preset10K = new ModernButton
            {
                Text = "$10K",
                Size = new Size(50, 25),
                Location = new Point(100, 0),
                BackColor = AccentColor,
                ForeColor = Color.White
            };
            preset10K.Click += (s, e) => SetInvestmentAmount(10000);
            presetPanel.Controls.Add(preset10K);

            preset25K = new ModernButton
            {
                Text = "$25K",
                Size = new Size(50, 25),
                Location = new Point(155, 0),
                BackColor = AccentColor,
                ForeColor = Color.White
            };
            preset25K.Click += (s, e) => SetInvestmentAmount(25000);
            presetPanel.Controls.Add(preset25K);

            calculationGroup.Controls.Add(presetPanel);

            // Investment slider
            investmentSlider = new TrackBar
            {
                Location = new Point(16, 95),
                Size = new Size(350, 45),
                Minimum = 100,
                Maximum = 50000,
                Value = 1000,
                TickFrequency = 5000,
                SmallChange = 100,
                LargeChange = 1000
            };
            investmentSlider.ValueChanged += InvestmentSlider_ValueChanged;
            calculationGroup.Controls.Add(investmentSlider);

            sliderValueLabel = new Label
            {
                Location = new Point(375, 105),
                Size = new Size(80, 20),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = PrimaryColor,
                Text = "$1,000"
            };
            calculationGroup.Controls.Add(sliderValueLabel);

            // Future stock price
            var futureStockLabel = new Label
            {
                Text = "Expected Future Price ($):",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = TextColor,
                Location = new Point(16, 145),
                AutoSize = true
            };
            calculationGroup.Controls.Add(futureStockLabel);

            futureStockPriceTextBox = new ModernTextBox
            {
                Location = new Point(16, 170),
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 10F)
            };
            futureStockPriceTextBox.TextChanged += FutureStockPriceTextBox_TextChanged;
            calculationGroup.Controls.Add(futureStockPriceTextBox);

            // Tax status
            var taxStatusLabel = new Label
            {
                Text = "Tax Filing Status:",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = TextColor,
                Location = new Point(250, 145),
                AutoSize = true
            };
            calculationGroup.Controls.Add(taxStatusLabel);

            filerRadioButton = new RadioButton
            {
                Text = "Filer (2% Gov. Tax)",
                Location = new Point(250, 170),
                Size = new Size(140, 24),
                Checked = true,
                Font = new Font("Segoe UI", 10F),
                ForeColor = TextColor
            };
            calculationGroup.Controls.Add(filerRadioButton);

            nonFilerRadioButton = new RadioButton
            {
                Text = "Non-Filer (4% Gov. Tax)",
                Location = new Point(250, 195),
                Size = new Size(160, 24),
                Font = new Font("Segoe UI", 10F),
                ForeColor = TextColor
            };
            calculationGroup.Controls.Add(nonFilerRadioButton);

            // Action buttons - arranged vertically on the far right side
            calculateButton = new ModernButton
            {
                Text = "🔍 Calculate ROI",
                Size = new Size(130, 35),
                Location = new Point(480, 60),
                BackColor = PrimaryColor,
                ForeColor = Color.White
            };
            calculateButton.Click += CalculateButton_Click;
            calculationGroup.Controls.Add(calculateButton);



            clearButton = new ModernButton
            {
                Text = "🗑️ Clear",
                Size = new Size(130, 35),
                Location = new Point(480, 105),
                BackColor = ErrorColor,
                ForeColor = Color.White
            };
            clearButton.Click += ClearButton_Click;
            calculationGroup.Controls.Add(clearButton);

            saveScenarioButton = new ModernButton
            {
                Text = "💾 Save Scenario",
                Size = new Size(130, 35),
                Location = new Point(480, 150),
                BackColor = Color.FromArgb(99, 102, 241),
                ForeColor = Color.White
            };
            saveScenarioButton.Click += SaveScenarioButton_Click;
            calculationGroup.Controls.Add(saveScenarioButton);

            loadScenarioButton = new ModernButton
            {
                Text = "📁 Load Scenario",
                Size = new Size(130, 35),
                Location = new Point(480, 195),
                BackColor = Color.FromArgb(99, 102, 241),
                ForeColor = Color.White
            };
            loadScenarioButton.Click += LoadScenarioButton_Click;
            calculationGroup.Controls.Add(loadScenarioButton);

            // Progress bar
            calculationProgress = new ProgressBar
            {
                Location = new Point(16, 220),
                Size = new Size(450, 8),
                Style = ProgressBarStyle.Continuous,
                Visible = false
            };
            calculationGroup.Controls.Add(calculationProgress);

            // Setup tooltips
            toolTip.SetToolTip(investmentTextBox, "Enter the amount you want to invest");
            toolTip.SetToolTip(futureStockPriceTextBox, "Enter your expected future stock price");
            toolTip.SetToolTip(filerRadioButton, "Select if you file taxes (2% government tax)");
            toolTip.SetToolTip(nonFilerRadioButton, "Select if you don't file taxes (4% government tax)");

            mainLayout.Controls.Add(calculationGroup, 1, 1);
        }

        private void SetupResultsSection()
        {
            resultsTabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 8, 0, 0),
                Font = new Font("Segoe UI", 10F)
            };
            resultsTabControl.SelectedIndexChanged += ResultsTabControl_SelectedIndexChanged;

            // Summary Tab
            var summaryTab = new TabPage("📊 Summary");
            resultsTextBox = new RichTextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Consolas", 10F),
                BackColor = SurfaceColor,
                ReadOnly = true,
                Margin = new Padding(8)
            };
            summaryTab.Controls.Add(resultsTextBox);
            resultsTabControl.TabPages.Add(summaryTab);

            // Details Tab
            var detailsTab = new TabPage("📋 Details");
            detailsTextBox = new RichTextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Consolas", 9F),
                BackColor = SurfaceColor,
                ReadOnly = true,
                Margin = new Padding(8)
            };
            detailsTab.Controls.Add(detailsTextBox);
            resultsTabControl.TabPages.Add(detailsTab);

            // Charts Tab
            var chartsTab = new TabPage("📈 Live Charts");
            SetupChartsPanel();
            chartsTab.Controls.Add(chartPanel);
            resultsTabControl.TabPages.Add(chartsTab);

            // Comparison Tab
            var comparisonTab = new TabPage("⚖️ Comparison");
            SetupDataGrid();
            comparisonTab.Controls.Add(companyDataGrid);
            resultsTabControl.TabPages.Add(comparisonTab);

            // Export Tab
            var exportTab = new TabPage("📤 Export");
            var exportPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(16)
            };

            exportButton = new ModernButton
            {
                Text = "📄 Export to PDF",
                Size = new Size(150, 40),
                Location = new Point(16, 16),
                BackColor = Color.FromArgb(139, 69, 19),
                ForeColor = Color.White
            };
            exportButton.Click += ExportButton_Click;
            exportPanel.Controls.Add(exportButton);

            var exportCsvButton = new ModernButton
            {
                Text = "📊 Export to CSV",
                Size = new Size(150, 40),
                Location = new Point(180, 16),
                BackColor = Color.FromArgb(34, 139, 34),
                ForeColor = Color.White
            };
            exportCsvButton.Click += ExportCsvButton_Click;
            exportPanel.Controls.Add(exportCsvButton);

            var printButton = new ModernButton
            {
                Text = "🖨️ Print Report",
                Size = new Size(150, 40),
                Location = new Point(344, 16),
                BackColor = Color.FromArgb(75, 0, 130),
                ForeColor = Color.White
            };
            printButton.Click += PrintButton_Click;
            exportPanel.Controls.Add(printButton);

            exportTab.Controls.Add(exportPanel);
            resultsTabControl.TabPages.Add(exportTab);

            mainLayout.Controls.Add(resultsTabControl, 0, 2);
            mainLayout.SetColumnSpan(resultsTabControl, 2);
        }

        private void SetupDataGrid()
        {
            companyDataGrid = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = SurfaceColor,
                BorderStyle = BorderStyle.None,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false,
                Font = new Font("Segoe UI", 10F),
                Margin = new Padding(8)
            };

            // Style the grid
            companyDataGrid.EnableHeadersVisualStyles = false;
            companyDataGrid.ColumnHeadersDefaultCellStyle.BackColor = PrimaryColor;
            companyDataGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            companyDataGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            companyDataGrid.ColumnHeadersHeight = 35;

            companyDataGrid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
            companyDataGrid.DefaultCellStyle.SelectionForeColor = TextColor;
            companyDataGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);

            // Add columns with progress bars
            companyDataGrid.Columns.Add("Company", "Company Name");
            companyDataGrid.Columns.Add("TaxRate", "Tax Rate (%)");
            companyDataGrid.Columns.Add("StockPrice", "Current Price ($)");
            companyDataGrid.Columns.Add("MarketValue", "Market Value ($)");
            companyDataGrid.Columns.Add("ROI", "ROI (%)");
            companyDataGrid.Columns.Add("ROIBar", "Performance");

            // Set column widths
            companyDataGrid.Columns["Company"].Width = 200;
            companyDataGrid.Columns["TaxRate"].Width = 100;
            companyDataGrid.Columns["StockPrice"].Width = 120;
            companyDataGrid.Columns["MarketValue"].Width = 150;
            companyDataGrid.Columns["ROI"].Width = 100;
            companyDataGrid.Columns["ROIBar"].Width = 150;

            foreach (var company in companies)
            {
                companyDataGrid.Rows.Add(company.Name, $"{company.CompanyTaxRate}%", 
                    $"${company.CurrentStockPrice:F2}", 
                    $"${company.MarketValue:N0}", "N/A", "");
            }

            companyDataGrid.CellPainting += CompanyDataGrid_CellPainting;
        }

        private void SetupChartsPanel()
        {
            chartPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = SurfaceColor,
                Margin = new Padding(8),
                AutoScroll = true
            };

            RefreshSelectedCompanyChart();
        }

        private void RefreshSelectedCompanyChart()
        {
            // Clear existing chart
            chartPanel.Controls.Clear();

            if (companyComboBox.SelectedIndex < 0) return;

            var selectedCompany = companies[companyComboBox.SelectedIndex];

            // Create a single large chart for the selected company
            var companyChartPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(20)
            };

            // Company title and current price
            var titleLabel = new Label
            {
                Text = selectedCompany.Name,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = PrimaryColor,
                Location = new Point(20, 20),
                Size = new Size(400, 30),
                TextAlign = ContentAlignment.TopLeft
            };
            companyChartPanel.Controls.Add(titleLabel);

            var priceLabel = new Label
            {
                Text = $"Current Price: ${selectedCompany.CurrentStockPrice:F2}",
                Font = new Font("Segoe UI", 12F, FontStyle.Regular),
                ForeColor = TextColor,
                Location = new Point(20, 55),
                Size = new Size(200, 25),
                TextAlign = ContentAlignment.TopLeft
            };
            companyChartPanel.Controls.Add(priceLabel);

            // Price change indicator
            var changeLabel = new Label
            {
                Name = "selectedCompanyChange",
                Text = CalculatePriceChange(selectedCompany),
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(250, 55),
                Size = new Size(100, 25),
                TextAlign = ContentAlignment.TopLeft
            };
            
            // Set color based on change
            var changeText = changeLabel.Text;
            if (changeText.StartsWith("+"))
                changeLabel.ForeColor = SuccessColor;
            else if (changeText.StartsWith("-"))
                changeLabel.ForeColor = ErrorColor;
            else
                changeLabel.ForeColor = TextColor;
                
            companyChartPanel.Controls.Add(changeLabel);

            // Chart area - larger for better visibility
            var chartArea = new Panel
            {
                Location = new Point(20, 90),
                Size = new Size(companyChartPanel.Width - 40, companyChartPanel.Height - 120),
                BackColor = Color.FromArgb(248, 250, 252),
                BorderStyle = BorderStyle.FixedSingle,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };
            chartArea.Paint += (s, e) => DrawLargePriceChart(e.Graphics, chartArea, selectedCompany);
            companyChartPanel.Controls.Add(chartArea);

            chartPanel.Controls.Add(companyChartPanel);
        }

        private string CalculatePriceChange(Company company)
        {
            if (!priceHistory.ContainsKey(company.Name) || priceHistory[company.Name].Count < 2)
                return "0.00%";

            var prices = priceHistory[company.Name];
            var currentPrice = prices.Last();
            var previousPrice = prices[prices.Count - 2];
            var changePercent = ((currentPrice - previousPrice) / previousPrice) * 100;

            return $"{(changePercent >= 0 ? "+" : "")}{changePercent:F2}%";
        }

        private void DrawLargePriceChart(Graphics g, Panel chartArea, Company company)
        {
            if (!priceHistory.ContainsKey(company.Name) || priceHistory[company.Name].Count < 2)
            {
                // Draw "No data available" message
                using (var font = new Font("Segoe UI", 14F))
                using (var brush = new SolidBrush(Color.Gray))
                {
                    var message = "Collecting price data...";
                    var size = g.MeasureString(message, font);
                    var x = (chartArea.Width - size.Width) / 2;
                    var y = (chartArea.Height - size.Height) / 2;
                    g.DrawString(message, font, brush, x, y);
                }
                return;
            }

            var prices = priceHistory[company.Name];
            var width = chartArea.Width - 60;
            var height = chartArea.Height - 80;
            var maxPrice = prices.Max();
            var minPrice = prices.Min();
            var priceRange = maxPrice - minPrice;
            
            if (priceRange == 0) priceRange = 1; // Avoid division by zero

            // Draw background
            using (var backgroundBrush = new SolidBrush(Color.White))
            {
                g.FillRectangle(backgroundBrush, 30, 30, width, height);
            }

            // Draw grid lines with labels
            using (var gridPen = new Pen(Color.LightGray, 1))
            using (var labelFont = new Font("Segoe UI", 9F))
            using (var labelBrush = new SolidBrush(Color.Gray))
            {
                // Horizontal grid lines (price levels)
                for (int i = 0; i <= 5; i++)
                {
                    int y = 30 + (height * i / 5);
                    g.DrawLine(gridPen, 30, y, width + 30, y);
                    
                    // Price labels
                    double priceAtLevel = maxPrice - (priceRange * i / 5);
                    g.DrawString($"${priceAtLevel:F2}", labelFont, labelBrush, 5, y - 8);
                }

                // Vertical grid lines (time)
                int timePoints = Math.Min(prices.Count, 10);
                for (int i = 0; i <= timePoints; i++)
                {
                    int x = 30 + (width * i / timePoints);
                    g.DrawLine(gridPen, x, 30, x, height + 30);
                }
            }

            // Draw price line with gradient
            using (var pricePen = new Pen(PrimaryColor, 3))
            {
                var points = new List<PointF>();
                for (int i = 0; i < prices.Count; i++)
                {
                    float x = 30 + (width * i / Math.Max(prices.Count - 1, 1));
                    float y = 30 + height - (float)((prices[i] - minPrice) / priceRange * height);
                    points.Add(new PointF(x, y));
                }

                if (points.Count > 1)
                {
                    // Draw smooth curve
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.DrawLines(pricePen, points.ToArray());
                }

                // Draw points with larger circles
                using (var pointBrush = new SolidBrush(AccentColor))
                using (var pointPen = new Pen(Color.White, 2))
                {
                    foreach (var point in points)
                    {
                        g.FillEllipse(pointBrush, point.X - 4, point.Y - 4, 8, 8);
                        g.DrawEllipse(pointPen, point.X - 4, point.Y - 4, 8, 8);
                    }
                }

                // Highlight current price point
                if (points.Count > 0)
                {
                    var lastPoint = points.Last();
                    using (var currentBrush = new SolidBrush(SuccessColor))
                    using (var currentPen = new Pen(Color.White, 3))
                    {
                        g.FillEllipse(currentBrush, lastPoint.X - 6, lastPoint.Y - 6, 12, 12);
                        g.DrawEllipse(currentPen, lastPoint.X - 6, lastPoint.Y - 6, 12, 12);
                    }
                }
            }

            // Draw chart title and statistics
            using (var titleFont = new Font("Segoe UI", 12F, FontStyle.Bold))
            using (var titleBrush = new SolidBrush(PrimaryColor))
            using (var statFont = new Font("Segoe UI", 10F))
            using (var statBrush = new SolidBrush(TextColor))
            {
                g.DrawString("Price History", titleFont, titleBrush, 30, 5);
                
                var stats = $"High: ${maxPrice:F2} | Low: ${minPrice:F2} | Range: ${priceRange:F2}";
                g.DrawString(stats, statFont, statBrush, 30, height + 40);
                
                var dataPoints = $"Data Points: {prices.Count} | Latest: ${prices.Last():F2}";
                g.DrawString(dataPoints, statFont, statBrush, 30, height + 60);
            }
        }

        private void DrawPriceChart(Graphics g, Panel chartArea, Company company)
        {
            if (!priceHistory.ContainsKey(company.Name) || priceHistory[company.Name].Count < 2)
                return;

            var prices = priceHistory[company.Name];
            var width = chartArea.Width - 20;
            var height = chartArea.Height - 20;
            var maxPrice = prices.Max();
            var minPrice = prices.Min();
            var priceRange = maxPrice - minPrice;
            
            if (priceRange == 0) priceRange = 1; // Avoid division by zero

            // Draw grid lines
            using (var gridPen = new Pen(Color.LightGray, 1))
            {
                for (int i = 0; i <= 5; i++)
                {
                    int y = 10 + (height * i / 5);
                    g.DrawLine(gridPen, 10, y, width + 10, y);
                }
            }

            // Draw price line
            using (var pricePen = new Pen(PrimaryColor, 2))
            {
                var points = new List<PointF>();
                for (int i = 0; i < prices.Count; i++)
                {
                    float x = 10 + (width * i / Math.Max(prices.Count - 1, 1));
                    float y = 10 + height - (float)((prices[i] - minPrice) / priceRange * height);
                    points.Add(new PointF(x, y));
                }

                if (points.Count > 1)
                {
                    g.DrawLines(pricePen, points.ToArray());
                }

                // Draw points
                using (var pointBrush = new SolidBrush(AccentColor))
                {
                    foreach (var point in points)
                    {
                        g.FillEllipse(pointBrush, point.X - 2, point.Y - 2, 4, 4);
                    }
                }
            }

            // Draw price labels
            using (var labelBrush = new SolidBrush(TextColor))
            using (var font = new Font("Segoe UI", 8F))
            {
                g.DrawString($"${maxPrice:F2}", font, labelBrush, width - 40, 5);
                g.DrawString($"${minPrice:F2}", font, labelBrush, width - 40, height - 5);
                g.DrawString($"Current: ${company.CurrentStockPrice:F2}", font, labelBrush, 10, height + 5);
            }
        }

        private void RefreshCharts()
        {
            if (chartPanel != null)
            {
                chartPanel.Invalidate(true);
            }
        }

        private void PriceUpdateTimer_Tick(object sender, EventArgs e)
        {
            // Update stock prices with realistic fluctuations
            foreach (var company in companies)
            {
                // Generate price change between -2% to +2%
                double changePercent = (priceRandom.NextDouble() - 0.5) * 0.04; // -2% to +2%
                double newPrice = company.CurrentStockPrice * (1 + changePercent);
                
                // Ensure price doesn't go below $1
                newPrice = Math.Max(newPrice, 1.0);
                
                company.CurrentStockPrice = newPrice;
                
                // Add to price history (keep last 50 points)
                if (priceHistory[company.Name].Count >= 50)
                {
                    priceHistory[company.Name].RemoveAt(0);
                }
                priceHistory[company.Name].Add(newPrice);
            }

            // Update company details if visible
            UpdateCompanyDetails();
            
            // Update data grid if it exists
            UpdateDataGridPrices();
            
            // Refresh charts if visible
            if (resultsTabControl.SelectedIndex == 2)
            {
                RefreshCharts();
            }
        }

        private void UpdateDataGridPrices()
        {
            if (companyDataGrid != null && companyDataGrid.Rows.Count == companies.Count)
            {
                for (int i = 0; i < companies.Count; i++)
                {
                    companyDataGrid.Rows[i].Cells["StockPrice"].Value = $"${companies[i].CurrentStockPrice:F2}";
                }
            }
        }

        // Event Handlers
        private void SetupEventHandlers()
        {
            investmentTextBox.TextChanged += InvestmentTextBox_TextChanged;
            futureStockPriceTextBox.TextChanged += FutureStockPriceTextBox_TextChanged;
            companyComboBox.SelectedIndexChanged += CompanyComboBox_SelectedIndexChanged;
        }

        private void SetupKeyboardShortcuts()
        {
            this.KeyPreview = true;
            this.KeyDown += MainForm_KeyDown;
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        CalculateButton_Click(sender, e);
                        break;

                    case Keys.S:
                        SaveScenarioButton_Click(sender, e);
                        break;
                    case Keys.L:
                        LoadScenarioButton_Click(sender, e);
                        break;
                    case Keys.E:
                        ExportButton_Click(sender, e);
                        break;
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                CalculateButton_Click(sender, e);
            }
        }

        private void CalculateButton_Click(object sender, EventArgs e)
        {
            if (companyComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a company from the list.", "Selection Required", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInputs())
                return;

            ShowProgress(true);

            var selectedCompany = companies[companyComboBox.SelectedIndex];
            double investmentAmount = double.Parse(investmentTextBox.Text);
            double futureStockPrice = double.Parse(futureStockPriceTextBox.Text);
            bool isFiler = filerRadioButton.Checked;

            var result = CalculateDetailedROI(selectedCompany, investmentAmount, futureStockPrice, isFiler);
            
            DisplaySingleResult(result);
            UpdateDataGridSingle(companyComboBox.SelectedIndex, result.ROI);
            
            // Add to history
            AddToHistory($"Single calculation: {selectedCompany.Name} - ROI: {result.ROI:F2}%");
            
            ShowProgress(false);
        }

        private void ResultsTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            // When Comparison tab (index 3) is selected, automatically run comparison
            if (resultsTabControl.SelectedIndex == 3)
            {
                // Only run comparison if we have valid inputs
                if (ValidateInputs())
                {
                    RunComparisonAnalysis();
                }
            }
            // When Charts tab (index 2) is selected, refresh the charts
            else if (resultsTabControl.SelectedIndex == 2)
            {
                RefreshCharts();
            }
        }

        private void RunComparisonAnalysis()
        {
            ShowProgress(true);

            double investmentAmount = double.Parse(investmentTextBox.Text);
            double futureStockPrice = double.Parse(futureStockPriceTextBox.Text);
            bool isFiler = filerRadioButton.Checked;

            var results = new List<CalculationResult>();

            for (int i = 0; i < companies.Count; i++)
            {
                var result = CalculateDetailedROI(companies[i], investmentAmount, futureStockPrice, isFiler);
                results.Add(result);
                UpdateDataGridSingle(i, result.ROI);
            }

            DisplayComparisonResults(results);
            AddToHistory($"Comparison analysis: {results.Count} companies analyzed");
            
            ShowProgress(false);
        }

        private void CompareAllButton_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;

            RunComparisonAnalysis();
            
            // Automatically switch to the Comparison tab to show results
            resultsTabControl.SelectedIndex = 3; // Index 3 is the Comparison tab
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            resultsTextBox.Clear();
            detailsTextBox.Clear();
            
            // Reset form values
            SetInvestmentAmount(1000);
            futureStockPriceTextBox.Clear();
            companyComboBox.SelectedIndex = 0;
            filerRadioButton.Checked = true;
            
            // Reset validation states
            investmentTextBox.IsValid = true;
            futureStockPriceTextBox.IsValid = true;
            
            // Reset ROI column in data grid
            for (int i = 0; i < companyDataGrid.Rows.Count; i++)
            {
                companyDataGrid.Rows[i].Cells["ROI"].Value = "N/A";
                companyDataGrid.Rows[i].Cells["ROIBar"].Value = "";
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(investmentTextBox.Text) || 
                !double.TryParse(investmentTextBox.Text, out double investment) || investment <= 0)
            {
                MessageBox.Show("Please enter a valid investment amount greater than 0.", 
                    "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                investmentTextBox.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(futureStockPriceTextBox.Text) || 
                !double.TryParse(futureStockPriceTextBox.Text, out double futurePrice) || futurePrice <= 0)
            {
                MessageBox.Show("Please enter a valid future stock price greater than 0.", 
                    "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                futureStockPriceTextBox.Focus();
                return false;
            }

            return true;
        }

        // Helper Methods and Event Handlers continue in next part...
        private void InvestmentTextBox_TextChanged(object sender, EventArgs e)
        {
            ValidateInvestmentInput();
            
            // Update slider only if the text is a valid number and within range
            if (double.TryParse(investmentTextBox.Text, out double value) && 
                value >= investmentSlider.Minimum && 
                value <= investmentSlider.Maximum)
            {
                // Temporarily remove event handler to prevent recursion
                investmentSlider.ValueChanged -= InvestmentSlider_ValueChanged;
                investmentSlider.Value = (int)value;
                sliderValueLabel.Text = $"${value:N0}";
                investmentSlider.ValueChanged += InvestmentSlider_ValueChanged;
            }
            else if (double.TryParse(investmentTextBox.Text, out value))
            {
                // Update label even if outside slider range
                sliderValueLabel.Text = $"${value:N0}";
            }
        }

        private void FutureStockPriceTextBox_TextChanged(object sender, EventArgs e)
        {
            ValidateFutureStockPriceInput();
        }

        private void InvestmentSlider_ValueChanged(object sender, EventArgs e)
        {
            investmentTextBox.Text = investmentSlider.Value.ToString();
            sliderValueLabel.Text = $"${investmentSlider.Value:N0}";
        }

        private void CompanyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCompanyDetails();
        }

        private void DisplayComparisonResults(List<CalculationResult> results)
        {
            // Update Summary Tab
            resultsTextBox.Clear();
            resultsTextBox.SelectionFont = new Font("Consolas", 12F, FontStyle.Bold);
            resultsTextBox.SelectionColor = PrimaryColor;
            resultsTextBox.AppendText("═══════════════════════════════════════════════════════\n");
            resultsTextBox.AppendText("                 COMPANY ROI COMPARISON\n");
            resultsTextBox.AppendText("═══════════════════════════════════════════════════════\n\n");

            resultsTextBox.SelectionFont = new Font("Consolas", 10F, FontStyle.Regular);
            resultsTextBox.SelectionColor = TextColor;
            resultsTextBox.AppendText($"Investment: ${results[0].InvestmentAmount:F2} | Future Price: ${results[0].FutureStockPrice:F2} | ");
            resultsTextBox.AppendText($"Status: {(results[0].IsFiler ? "Filer" : "Non-Filer")}\n\n");

            // Sort by ROI descending
            var sortedResults = results.OrderByDescending(r => r.ROI).ToList();

            resultsTextBox.AppendText("RANK | COMPANY NAME           | TAX RATE | ROI      | NET PROFIT\n");
            resultsTextBox.AppendText("-----|------------------------|----------|----------|------------\n");

            for (int i = 0; i < sortedResults.Count; i++)
            {
                var result = sortedResults[i];
                string rank = (i + 1).ToString().PadLeft(4);
                string name = result.Company.Name.Length > 22 ? result.Company.Name.Substring(0, 19) + "..." : result.Company.Name.PadRight(22);
                string taxRate = $"{result.Company.CompanyTaxRate}%".PadRight(8);
                
                resultsTextBox.SelectionFont = new Font("Consolas", 10F, FontStyle.Regular);
                resultsTextBox.SelectionColor = TextColor;
                resultsTextBox.AppendText($"{rank} | {name} | {taxRate} | ");
                
                resultsTextBox.SelectionFont = new Font("Consolas", 10F, FontStyle.Bold);
                if (result.ROI > 0)
                {
                    resultsTextBox.SelectionColor = SuccessColor;
                    resultsTextBox.AppendText($"{result.ROI:F2}%".PadRight(8));
                }
                else
                {
                    resultsTextBox.SelectionColor = ErrorColor;
                    resultsTextBox.AppendText($"{result.ROI:F2}%".PadRight(8));
                }

                resultsTextBox.SelectionFont = new Font("Consolas", 10F, FontStyle.Regular);
                resultsTextBox.SelectionColor = TextColor;
                resultsTextBox.AppendText($" | ${result.NetProfit:F2}\n");
            }

            resultsTextBox.SelectionFont = new Font("Consolas", 12F, FontStyle.Bold);
            resultsTextBox.SelectionColor = PrimaryColor;
            resultsTextBox.AppendText($"\n🏆 BEST INVESTMENT: {sortedResults[0].Company.Name}\n");
            resultsTextBox.AppendText($"💰 EXPECTED ROI: {sortedResults[0].ROI:F2}%\n");
            resultsTextBox.AppendText($"💵 NET PROFIT: ${sortedResults[0].NetProfit:F2}\n");

            // Update Details Tab with comparison details
            UpdateComparisonDetailsTab(sortedResults);
        }

        private void UpdateComparisonDetailsTab(List<CalculationResult> sortedResults)
        {
            detailsTextBox.Clear();
            detailsTextBox.SelectionFont = new Font("Consolas", 10F, FontStyle.Bold);
            detailsTextBox.SelectionColor = PrimaryColor;
            detailsTextBox.AppendText("DETAILED COMPARISON ANALYSIS\n");
            detailsTextBox.AppendText("═══════════════════════════════════════\n\n");

            foreach (var result in sortedResults)
            {
                detailsTextBox.SelectionFont = new Font("Consolas", 9F, FontStyle.Bold);
                detailsTextBox.SelectionColor = PrimaryColor;
                detailsTextBox.AppendText($"{result.Company.Name}\n");
                detailsTextBox.AppendText("─────────────────────────────────────\n");

                detailsTextBox.SelectionFont = new Font("Consolas", 9F, FontStyle.Regular);
                detailsTextBox.SelectionColor = TextColor;
                detailsTextBox.AppendText($"Shares Bought: {result.SharesBought:F4}\n");
                detailsTextBox.AppendText($"Future Value: ${result.FutureValue:F2}\n");
                detailsTextBox.AppendText($"Gross Profit: ${result.GrossProfit:F2}\n");
                
                if (result.GrossProfit > 0)
                {
                    detailsTextBox.AppendText($"Company Tax: ${result.CompanyTax:F2}\n");
                    detailsTextBox.AppendText($"Government Tax: ${result.GovernmentTax:F2}\n");
                    detailsTextBox.AppendText($"Total Tax: ${result.TotalTax:F2}\n");
                }
                
                detailsTextBox.AppendText($"Net Profit: ${result.NetProfit:F2}\n");
                
                detailsTextBox.SelectionFont = new Font("Consolas", 9F, FontStyle.Bold);
                if (result.ROI > 0)
                {
                    detailsTextBox.SelectionColor = SuccessColor;
                    detailsTextBox.AppendText($"ROI: {result.ROI:F2}%\n\n");
                }
                else
                {
                    detailsTextBox.SelectionColor = ErrorColor;
                    detailsTextBox.AppendText($"ROI: {result.ROI:F2}%\n\n");
                }
            }
        }

        // Continue with remaining methods in next part...
        private void SaveScenarioButton_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            // Simple input dialog replacement
            string scenarioName = $"Scenario_{DateTime.Now:yyyyMMdd_HHmmss}";
            
            var scenario = new InvestmentScenario(
                scenarioName,
                double.Parse(investmentTextBox.Text),
                double.Parse(futureStockPriceTextBox.Text),
                filerRadioButton.Checked,
                companyComboBox.SelectedItem.ToString()
            );
            
            scenarios.Add(scenario);
            SaveScenariosToFile();
            MessageBox.Show("Scenario saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LoadScenarioButton_Click(object sender, EventArgs e)
        {
            if (scenarios.Count == 0)
            {
                MessageBox.Show("No saved scenarios found.", "No Scenarios", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // For simplicity, load the most recent scenario
            var scenario = scenarios.LastOrDefault();
            if (scenario != null)
            {
                LoadScenario(scenario);
                MessageBox.Show("Most recent scenario loaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
                DefaultExt = "txt",
                FileName = $"ROI_Analysis_{DateTime.Now:yyyyMMdd_HHmmss}.txt"
            };

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    File.WriteAllText(saveDialog.FileName, resultsTextBox.Text);
                    MessageBox.Show("Report exported successfully!", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error exporting report: {ex.Message}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ExportCsvButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",
                DefaultExt = "csv",
                FileName = $"ROI_Data_{DateTime.Now:yyyyMMdd_HHmmss}.csv"
            };

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var csv = new System.Text.StringBuilder();
                    csv.AppendLine("Company,Tax Rate (%),Current Price ($),Market Value ($),ROI (%)");
                    
                    for (int i = 0; i < companyDataGrid.Rows.Count; i++)
                    {
                        var row = companyDataGrid.Rows[i];
                        csv.AppendLine($"{row.Cells[0].Value},{row.Cells[1].Value},{row.Cells[2].Value},{row.Cells[3].Value},{row.Cells[4].Value}");
                    }
                    
                    File.WriteAllText(saveDialog.FileName, csv.ToString());
                    MessageBox.Show("Data exported successfully!", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error exporting data: {ex.Message}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void PrintButton_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                // Simple print implementation
                MessageBox.Show("Print functionality would be implemented here with PrintDocument", "Print", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void CompanyDataGrid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == companyDataGrid.Columns["ROIBar"].Index && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var roiText = companyDataGrid.Rows[e.RowIndex].Cells["ROI"].Value?.ToString();
                if (roiText != null && roiText != "N/A" && double.TryParse(roiText.Replace("%", ""), out double roi))
                {
                    var rect = e.CellBounds;
                    rect.Inflate(-2, -2);

                    // Calculate bar width (assuming max ROI of 50% for scaling)
                    int barWidth = (int)(Math.Abs(roi) / 50.0 * rect.Width);
                    barWidth = Math.Min(barWidth, rect.Width);

                    Color barColor = roi >= 0 ? SuccessColor : ErrorColor;
                    using (SolidBrush brush = new SolidBrush(barColor))
                    {
                        Rectangle barRect = new Rectangle(rect.X, rect.Y, barWidth, rect.Height);
                        e.Graphics.FillRectangle(brush, barRect);
                    }

                    // Draw ROI text
                    using (SolidBrush textBrush = new SolidBrush(Color.White))
                    {
                        var textRect = new Rectangle(rect.X + 4, rect.Y, rect.Width - 8, rect.Height);
                        e.Graphics.DrawString($"{roi:F1}%", companyDataGrid.Font, textBrush, textRect, 
                            new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center });
                    }
                }

                e.Handled = true;
            }
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            // Simple animation for progress bar
            if (calculationProgress.Visible)
            {
                calculationProgress.PerformStep();
                if (calculationProgress.Value >= calculationProgress.Maximum)
                {
                    calculationProgress.Value = 0;
                }
            }
        }

        // Helper Methods
        private void SetInvestmentAmount(double amount)
        {
            // Temporarily remove event handlers to prevent recursion
            investmentTextBox.TextChanged -= InvestmentTextBox_TextChanged;
            investmentSlider.ValueChanged -= InvestmentSlider_ValueChanged;
            
            investmentTextBox.Text = amount.ToString();
            investmentSlider.Value = (int)Math.Min(Math.Max(amount, investmentSlider.Minimum), investmentSlider.Maximum);
            sliderValueLabel.Text = $"${amount:N0}";
            
            // Re-add event handlers
            investmentTextBox.TextChanged += InvestmentTextBox_TextChanged;
            investmentSlider.ValueChanged += InvestmentSlider_ValueChanged;
        }

        private void ShowProgress(bool show)
        {
            calculationProgress.Visible = show;
            if (show)
            {
                calculationProgress.Value = 0;
                animationTimer.Start();
            }
            else
            {
                animationTimer.Stop();
            }
        }

        private void ValidateInvestmentInput()
        {
            // Only validate if the field has content and user is not actively typing
            if (string.IsNullOrWhiteSpace(investmentTextBox.Text))
            {
                investmentTextBox.IsValid = true;
                return;
            }

            bool isValid = double.TryParse(investmentTextBox.Text, out double value) && value > 0;
            investmentTextBox.IsValid = isValid;
            investmentTextBox.ValidationMessage = isValid ? "" : "Please enter a valid investment amount greater than 0";
        }

        private void ValidateFutureStockPriceInput()
        {
            // Only validate if the field has content and user is not actively typing
            if (string.IsNullOrWhiteSpace(futureStockPriceTextBox.Text))
            {
                futureStockPriceTextBox.IsValid = true;
                return;
            }

            bool isValid = double.TryParse(futureStockPriceTextBox.Text, out double value) && value > 0;
            futureStockPriceTextBox.IsValid = isValid;
            futureStockPriceTextBox.ValidationMessage = isValid ? "" : "Please enter a valid future stock price greater than 0";
        }

        private void UpdateCompanyDetails()
        {
            if (companyComboBox.SelectedIndex >= 0)
            {
                var company = companies[companyComboBox.SelectedIndex];
                var detailsLabel = companyInfoGroup.Controls.Find("companyDetails", true).FirstOrDefault() as Label;
                if (detailsLabel != null)
                {
                    detailsLabel.Text = $"Tax Rate: {company.CompanyTaxRate}%\n" +
                                      $"Current Stock Price: ${company.CurrentStockPrice:F2}\n" +
                                      $"Market Value: ${company.MarketValue:N0}\n" +
                                      $"Shares Available: {(company.MarketValue / company.CurrentStockPrice):N0}";
                }
            }
        }

        private CalculationResult CalculateDetailedROI(Company company, double investment, double futurePrice, bool isFiler)
        {
            var result = new CalculationResult
            {
                Company = company,
                InvestmentAmount = investment,
                FutureStockPrice = futurePrice,
                IsFiler = isFiler
            };

            result.SharesBought = investment / company.CurrentStockPrice;
            result.FutureValue = result.SharesBought * futurePrice;
            result.GrossProfit = result.FutureValue - investment;

            if (result.GrossProfit > 0)
            {
                result.CompanyTax = result.GrossProfit * (company.CompanyTaxRate / 100);
                result.GovernmentTax = result.GrossProfit * (isFiler ? 0.02 : 0.04);
                result.TotalTax = result.CompanyTax + result.GovernmentTax;
                result.NetProfit = result.GrossProfit - result.TotalTax;
            }
            else
            {
                result.NetProfit = result.GrossProfit; // No tax on losses
            }

            result.ROI = (result.NetProfit / investment) * 100;
            return result;
        }

        private void DisplaySingleResult(CalculationResult result)
        {
            // Update Summary Tab
            resultsTextBox.Clear();
            resultsTextBox.SelectionFont = new Font("Consolas", 12F, FontStyle.Bold);
            resultsTextBox.SelectionColor = PrimaryColor;
            resultsTextBox.AppendText("═══════════════════════════════════════════════════════\n");
            resultsTextBox.AppendText("                    ROI CALCULATION RESULT\n");
            resultsTextBox.AppendText("═══════════════════════════════════════════════════════\n\n");

            resultsTextBox.SelectionFont = new Font("Consolas", 10F, FontStyle.Regular);
            resultsTextBox.SelectionColor = TextColor;
            
            resultsTextBox.AppendText($"Company: {result.Company.Name}\n");
            resultsTextBox.AppendText($"Investment Amount: ${result.InvestmentAmount:F2}\n");
            resultsTextBox.AppendText($"Current Stock Price: ${result.Company.CurrentStockPrice:F2}\n");
            resultsTextBox.AppendText($"Expected Future Price: ${result.FutureStockPrice:F2}\n");
            resultsTextBox.AppendText($"Shares Purchased: {result.SharesBought:F4}\n");
            resultsTextBox.AppendText($"Future Portfolio Value: ${result.FutureValue:F2}\n");
            resultsTextBox.AppendText($"Gross Profit: ${result.GrossProfit:F2}\n\n");

            if (result.GrossProfit > 0)
            {
                resultsTextBox.AppendText("TAX BREAKDOWN:\n");
                resultsTextBox.AppendText($"Company Tax ({result.Company.CompanyTaxRate}%): ${result.CompanyTax:F2}\n");
                resultsTextBox.AppendText($"Government Tax ({(result.IsFiler ? 2 : 4)}%): ${result.GovernmentTax:F2}\n");
                resultsTextBox.AppendText($"Total Tax: ${result.TotalTax:F2}\n");
                resultsTextBox.AppendText($"Net Profit: ${result.NetProfit:F2}\n\n");
            }

            resultsTextBox.SelectionFont = new Font("Consolas", 14F, FontStyle.Bold);
            if (result.ROI > 0)
            {
                resultsTextBox.SelectionColor = SuccessColor;
                resultsTextBox.AppendText($"🎉 ROI: {result.ROI:F2}% (PROFIT)\n");
            }
            else
            {
                resultsTextBox.SelectionColor = ErrorColor;
                resultsTextBox.AppendText($"📉 ROI: {result.ROI:F2}% (LOSS)\n");
            }

            // Update Details Tab
            UpdateDetailsTab(result);
        }

        private void UpdateDetailsTab(CalculationResult result)
        {
            detailsTextBox.Clear();
            detailsTextBox.SelectionFont = new Font("Consolas", 10F, FontStyle.Bold);
            detailsTextBox.SelectionColor = PrimaryColor;
            detailsTextBox.AppendText("DETAILED CALCULATION BREAKDOWN\n");
            detailsTextBox.AppendText("═══════════════════════════════════════\n\n");

            detailsTextBox.SelectionFont = new Font("Consolas", 9F, FontStyle.Regular);
            detailsTextBox.SelectionColor = TextColor;

            detailsTextBox.AppendText("STEP 1: SHARE CALCULATION\n");
            detailsTextBox.AppendText($"Shares Bought = Investment ÷ Current Price\n");
            detailsTextBox.AppendText($"Shares Bought = ${result.InvestmentAmount:F2} ÷ ${result.Company.CurrentStockPrice:F2}\n");
            detailsTextBox.AppendText($"Shares Bought = {result.SharesBought:F6}\n\n");

            detailsTextBox.AppendText("STEP 2: FUTURE VALUE CALCULATION\n");
            detailsTextBox.AppendText($"Future Value = Shares × Future Price\n");
            detailsTextBox.AppendText($"Future Value = {result.SharesBought:F6} × ${result.FutureStockPrice:F2}\n");
            detailsTextBox.AppendText($"Future Value = ${result.FutureValue:F2}\n\n");

            detailsTextBox.AppendText("STEP 3: PROFIT CALCULATION\n");
            detailsTextBox.AppendText($"Gross Profit = Future Value - Investment\n");
            detailsTextBox.AppendText($"Gross Profit = ${result.FutureValue:F2} - ${result.InvestmentAmount:F2}\n");
            detailsTextBox.AppendText($"Gross Profit = ${result.GrossProfit:F2}\n\n");

            if (result.GrossProfit > 0)
            {
                detailsTextBox.AppendText("STEP 4: TAX CALCULATION\n");
                detailsTextBox.AppendText($"Company Tax = Gross Profit × {result.Company.CompanyTaxRate}%\n");
                detailsTextBox.AppendText($"Company Tax = ${result.GrossProfit:F2} × {result.Company.CompanyTaxRate / 100:F3}\n");
                detailsTextBox.AppendText($"Company Tax = ${result.CompanyTax:F2}\n\n");

                detailsTextBox.AppendText($"Government Tax = Gross Profit × {(result.IsFiler ? 2 : 4)}%\n");
                detailsTextBox.AppendText($"Government Tax = ${result.GrossProfit:F2} × {(result.IsFiler ? 0.02 : 0.04):F2}\n");
                detailsTextBox.AppendText($"Government Tax = ${result.GovernmentTax:F2}\n\n");

                detailsTextBox.AppendText("STEP 5: NET PROFIT CALCULATION\n");
                detailsTextBox.AppendText($"Net Profit = Gross Profit - Total Tax\n");
                detailsTextBox.AppendText($"Net Profit = ${result.GrossProfit:F2} - ${result.TotalTax:F2}\n");
                detailsTextBox.AppendText($"Net Profit = ${result.NetProfit:F2}\n\n");
            }

            detailsTextBox.AppendText("STEP 6: ROI CALCULATION\n");
            detailsTextBox.AppendText($"ROI = (Net Profit ÷ Investment) × 100\n");
            detailsTextBox.AppendText($"ROI = (${result.NetProfit:F2} ÷ ${result.InvestmentAmount:F2}) × 100\n");
            detailsTextBox.AppendText($"ROI = {result.ROI:F2}%\n");
        }

        private void UpdateDataGridSingle(int companyIndex, double roi)
        {
            if (companyIndex >= 0 && companyIndex < companyDataGrid.Rows.Count)
            {
                companyDataGrid.Rows[companyIndex].Cells["ROI"].Value = $"{roi:F2}%";
                
                // Color coding
                if (roi > 0)
                    companyDataGrid.Rows[companyIndex].Cells["ROI"].Style.ForeColor = SuccessColor;
                else
                    companyDataGrid.Rows[companyIndex].Cells["ROI"].Style.ForeColor = ErrorColor;
            }
        }

        private void AddToHistory(string entry)
        {
            calculationHistory.Add($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {entry}");
            if (calculationHistory.Count > 50) // Keep only last 50 entries
            {
                calculationHistory.RemoveAt(0);
            }
        }

        private void SaveScenariosToFile()
        {
            try
            {
                string json = JsonSerializer.Serialize(scenarios, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText("scenarios.json", json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving scenarios: {ex.Message}", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadUserPreferences()
        {
            try
            {
                if (File.Exists("scenarios.json"))
                {
                    string json = File.ReadAllText("scenarios.json");
                    scenarios = JsonSerializer.Deserialize<List<InvestmentScenario>>(json) ?? new List<InvestmentScenario>();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading scenarios: {ex.Message}", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadScenario(InvestmentScenario scenario)
        {
            investmentTextBox.Text = scenario.InvestmentAmount.ToString();
            futureStockPriceTextBox.Text = scenario.FutureStockPrice.ToString();
            filerRadioButton.Checked = scenario.IsFiler;
            nonFilerRadioButton.Checked = !scenario.IsFiler;
            
            for (int i = 0; i < companies.Count; i++)
            {
                if (companies[i].Name == scenario.SelectedCompany)
                {
                    companyComboBox.SelectedIndex = i;
                    break;
                }
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1400, 900);
            this.Name = "MainForm";
            this.ResumeLayout(false);
        }
    }
}