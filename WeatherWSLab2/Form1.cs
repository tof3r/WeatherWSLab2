using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;
using System.Diagnostics;


namespace WeatherWSLab2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnQuotes_Click(object sender, EventArgs e)
        {
            var serviceClient = new ServiceReferenceQuotes.DelayedStockQuoteSoapClient("DelayedStockQuoteSoap");
            var watch = new Stopwatch();
            watch.Start();
            txtQuotesOutput.Text = serviceClient.GetQuickQuote(txtQuotesInput.Text,"0").ToString();
            watch.Stop();
            txtQuotesWatch.Text = watch.ElapsedMilliseconds.ToString();
            /*
             * RESULTS:
             * GOOG = 549,50,
             * MSFT = 48,83,
             * IBM = 163,0364,
             * AAPL = 108,87,
             * YHOO = 48,69
             */
        }

        private void btnQuotesFull_Click(object sender, EventArgs e)
        {
            var serviceClient = new ServiceReferenceQuotes.DelayedStockQuoteSoapClient("DelayedStockQuoteSoap");
            var watch = new Stopwatch();
            watch.Start();
            var quoteData = serviceClient.GetQuote(txtQuotesFullInput.Text, "0");
            watch.Stop();
            txtQuotesFullWatch.Text = watch.ElapsedMilliseconds.ToString();
            txtQuotesFullOutput.Text = quoteData.ChangePercent;
            companyName.Text = quoteData.CompanyName;
            earnPerShare.Text = quoteData.EarnPerShare.ToString();
            fiftyTwoWeekRange.Text = quoteData.FiftyTwoWeekRange;

            /*
             * RESULTS:
             * GOOG +0,51% Google Inc. 19,002 502,80 - 604,83
             * MSFT -0,04% Microsoft Corporation 2,551 34,63 - 49,15
             * IBM -0,12% International Business Machines Corporation 12,342 160,05 - 199,21
             * AAPL +0,80% Apple Inc. 6,45 70,5071 - 110,30
             * YHOO -0,73% Yahoo! Inc. 7,479 32,15 - 49,63 
             */

        }

        private void btnResolve_Click(object sender, EventArgs e)
        {
            var serviceClient = new ServiceResolve.P2GeoSoapClient("IP2GeoSoap");
            var watch = new Stopwatch();
            watch.Start();
            var ipData = serviceClient.ResolveIP(txtResolveInput.Text, "0");
            watch.Stop();
            txtResolveWatch.Text = watch.ElapsedMilliseconds.ToString();
            txtResolveOutput.Text = ipData.City;

            /*
             * wp.pl 212.77.100.101 Polska
             * facebook.com 69.63.187.18 Menlo Park
             * google.com 66.249.64.0 Mountain View
             * twitter.com 128.242.240.244 Englewood
             * ebay.com 66.135.210.61 Campbell
             */
        }

        private void btnTemperature_Click(object sender, EventArgs e)
        {
            var serviceClient = new ServiceTemperature.ConvertTemperatureSoapClient("ConvertTemperatureSoap");
            var watch = new Stopwatch();
            if (chooseTemp.SelectedItem.Equals("Kelvin - Celsius"))
            {
                watch.Start();
                txtTemperatureOutput.Text = serviceClient.ConvertTemp(Convert.ToDouble(txtTemperatureInput.Text), ServiceTemperature.TemperatureUnit.kelvin, ServiceTemperature.TemperatureUnit.degreeCelsius).ToString();
                watch.Stop();
            }else if (chooseTemp.SelectedItem.Equals("Kelvin - Farenheit"))
            {
                watch.Start();
                txtTemperatureOutput.Text = serviceClient.ConvertTemp(Convert.ToDouble(txtTemperatureInput.Text), ServiceTemperature.TemperatureUnit.kelvin, ServiceTemperature.TemperatureUnit.degreeFahrenheit).ToString();
                watch.Stop();
            }else if (chooseTemp.SelectedItem.Equals("Celsius - Kelvin"))
            {
                watch.Start();
                txtTemperatureOutput.Text = serviceClient.ConvertTemp(Convert.ToDouble(txtTemperatureInput.Text), ServiceTemperature.TemperatureUnit.degreeCelsius, ServiceTemperature.TemperatureUnit.kelvin).ToString();
                watch.Stop();
            }else if (chooseTemp.SelectedItem.Equals("Celsius - Farenheit"))
            {
                watch.Start();
                txtTemperatureOutput.Text = serviceClient.ConvertTemp(Convert.ToDouble(txtTemperatureInput.Text), ServiceTemperature.TemperatureUnit.degreeCelsius, ServiceTemperature.TemperatureUnit.degreeFahrenheit).ToString();
                watch.Stop();
            }else if (chooseTemp.SelectedItem.Equals("Farenheit - Kelvin"))
            {
                watch.Start();
                txtTemperatureOutput.Text = serviceClient.ConvertTemp(Convert.ToDouble(txtTemperatureInput.Text), ServiceTemperature.TemperatureUnit.degreeFahrenheit, ServiceTemperature.TemperatureUnit.kelvin).ToString();
                watch.Stop();
            } else
            {
                watch.Start();
                txtTemperatureOutput.Text = serviceClient.ConvertTemp(Convert.ToDouble(txtTemperatureInput.Text), ServiceTemperature.TemperatureUnit.degreeFahrenheit, ServiceTemperature.TemperatureUnit.degreeCelsius).ToString();
                watch.Stop();
            }
            txtTemperatureWatch.Text = watch.ElapsedMilliseconds.ToString();
            /*
             * 0 K - -273,15 C
             * 0 K - -459,67 F
             * 0 K - -218,52 Rankine
             * 0 K - 0 Reaumur
             */

            /*
             * 0 C - 32 F
             * 0 C - 0 Rankine
             * 0 C - 273,15 Reaumur
             * 0 C - 273,15 K
             */
        }

        private void btnWeather_Click(object sender, EventArgs e)
        {
            var serviceWeather = new ServiceWeather.GlobalWeatherSoapClient("GlobalWeatherSoap");
            var watch = new Stopwatch();
            watch.Start();
            var myXml = serviceWeather.GetWeather(txtWeatherInputCity.Text,txtWeatherInputCountry.Text);
            var xdoc = XDocument.Load(new StringReader(myXml));
            var entry = from x in xdoc.Descendants("CurrentWeather")
                        select new
                        {
                            Location = (string)x.Element("Location"),
                            Time = (string)x.Element("Time"),
                            Wind = (string)x.Element("Wind"),
                            Visibility = (string)x.Element("Visibility"),
                            SkyConditions = (string)x.Element("SkyConditions"),
                            Temperature = (string)x.Element("Temperature"),
                            DewPoint = (string)x.Element("DewPoint"),
                            RelativeHumidity = (string)x.Element("RelativeHumidity"),
                            Pressure = (string)x.Element("Pressure")
                        };

            String input =  entry.First().Temperature.ToString();
            string temperatureC = input.Split(new char[] { '(', 'C' })[1];
            if (Convert.ToInt32(temperatureC) < 0) 
            { 
                txtWeatherOutput.BackColor = Color.Red; 
            } else if (Convert.ToInt32(temperatureC) >= 0 && Convert.ToInt32(temperatureC) <= 5) 
            { 
                txtWeatherOutput.BackColor = Color.Blue; 
            } else 
            { 
                txtWeatherOutput.BackColor = Color.Green; 
            }
            txtWeatherOutput.Text = entry.First().Temperature;
            txtWeatherDewPoint.Text = entry.First().DewPoint;
            txtWeatherHumidity.Text = entry.First().RelativeHumidity;
            txtWeatherPressure.Text = entry.First().Pressure;
            txtWeatherSkyCond.Text = entry.First().SkyConditions;
            txtWeatherTime.Text = entry.First().Time;
            txtWeatherVisibility.Text = entry.First().Visibility;
            txtWeatherWind.Text = entry.First().Wind;
            watch.Stop();
            txtWeatherWatch.Text = watch.ElapsedMilliseconds.ToString();
            /*
             * Szczecin -  50 F (10 C)
             * Kraków -  46 F (8 C)
             * Poznań -  50 F (10 C)
             * Frankfurt -  48 F (9 C)
             * Barcelona -  93 F (34 C)
             */
        }

        private void btnCurrency_Click(object sender, EventArgs e)
        {
            var serviceClient = new ServiceCurrency.CurrencyConvertorSoapClient("CurrencyConvertorSoap");
            var watch = new Stopwatch();
            if (chooseCurrency.SelectedItem.Equals("PLN - EUR"))
            {
                watch.Start();
                txtCurrencyOutput.Text = serviceClient.ConversionRate(ServiceCurrency.Currency.PLN, ServiceCurrency.Currency.EUR).ToString();
                watch.Stop();
            }
            else if (chooseCurrency.SelectedItem.Equals("PLN - USD"))
            {
                watch.Start();
                txtCurrencyOutput.Text = serviceClient.ConversionRate(ServiceCurrency.Currency.PLN, ServiceCurrency.Currency.USD).ToString();
                watch.Stop();
            }
            else if (chooseCurrency.SelectedItem.Equals("EUR - PLN"))
            {
                watch.Start();
                txtCurrencyOutput.Text = serviceClient.ConversionRate(ServiceCurrency.Currency.EUR, ServiceCurrency.Currency.PLN).ToString();
                watch.Stop();
            }
            else if (chooseCurrency.SelectedItem.Equals("EUR - USD"))
            {
                watch.Start();
                txtCurrencyOutput.Text = serviceClient.ConversionRate(ServiceCurrency.Currency.EUR, ServiceCurrency.Currency.USD).ToString();
                watch.Stop();
            }
            else 
            { 
                MessageBox.Show("Choose currency", "No data", MessageBoxButtons.OK);

            }
            
            txtCurrencyWatch.Text = watch.ElapsedMilliseconds.ToString();

            /*
             * EURUSD - 1,2423
             * USDEUR - 0,8048
             * PLNUSD - 0,2945
             * PLNEUR - 0,237
             * USDJPY - 115,675
             */

        }

        private void checkAllFunctions_Click(object sender, EventArgs e)
        {
            bool isQuotesFilled, isQuotesFullFilled, isResolveFilled, isTemperatureFilled, isWeatherFilled, isQ2Filled;
            isQuotesFilled = isQuotesFullFilled = isResolveFilled = isTemperatureFilled = isWeatherFilled = isQ2Filled = false;
            if (txtQuotesInput.Text == "")
            {
                MessageBox.Show("Nie wypełnione pole ze wskazaniem akcji (Quotes)", "Brak danych", MessageBoxButtons.OK);
                //return;
            }else{
                isQuotesFilled = true;
            }
            if (txtQuotesFullInput.Text == "")
            {
                MessageBox.Show("Nie wypełnione pole ze wskazaniem akcji do pobrania pełnej informacji (QuotesFull)", "Brak danych", MessageBoxButtons.OK);
                //return;
            }else { 
                isQuotesFullFilled = true; 
            }
            if (txtResolveInput.Text == "")
            {
                MessageBox.Show("Nie wypełnione pole ze wskazaniem akcji do określenia adresu IP (ResolveIP)", "Brak danych", MessageBoxButtons.OK);
                //return;
            }else { 
                isResolveFilled = true; 
            }
            if (txtWeatherInputCity.Text == "")
            {
                MessageBox.Show("Nie wypełnione pole z nazwą miasta ze wskazaniem akcji do pobrania pogody (Weather)", "Brak danych", MessageBoxButtons.OK);
                //return;
            }else { 
                isWeatherFilled = true; 
            }
            if (txtTemperatureInput.Text == "")
            {
                MessageBox.Show("Nie wypełnione pole ze wskazaniem akcji do konwersji temperatury (Temperature)", "Brak danych", MessageBoxButtons.OK);
                //return;
            }else { 
                isTemperatureFilled = true; 
            }
            if (txtInput.Text == "")
            {
                MessageBox.Show("Nie wypełnione pole ze wskazaniem akcji do pobrania pełnej informacji (QuotesFull v2)", "Brak danych", MessageBoxButtons.OK);
                //return;
            }
            else
            {
                isQ2Filled = true;
            }


            if(isQuotesFilled && isQuotesFullFilled && isTemperatureFilled && isResolveFilled && isWeatherFilled && isQ2Filled){
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                btnQuotes_Click(sender,e);
                btnQuotesFull_Click(sender, e);
                btnResolve_Click(sender, e);
                btnCurrency_Click(sender, e);
                btnTemperature_Click(sender, e);
                btnWeather_Click(sender, e);
                btnQF_Click(sender, e);
                stopwatch.Stop();
                txtTotal.Text = stopwatch.ElapsedMilliseconds.ToString();
            }
        }

        private void GetStockValue(string stockSymbol, TextBox txtFullOutput, TextBox txtOutputDayHigh, TextBox txtOutputEarnPerShare, TextBox txtOutputFiftyTwoWeekRange)
        {
            var serviceClient = new ServiceReferenceQuotes.DelayedStockQuoteSoapClient("DelayedStockQuoteSoap");
            ServiceReferenceQuotes.QuoteData quoteData = serviceClient.GetQuote(stockSymbol, "0");
            txtFullOutput.Text = quoteData.LastTradeAmount.ToString();
            txtOutputDayHigh.Text = quoteData.DayHigh.ToString();
            txtOutputEarnPerShare.Text = quoteData.EarnPerShare.ToString();
            txtOutputFiftyTwoWeekRange.Text = quoteData.FiftyTwoWeekRange;
        }

        private void btnQF_Click(object sender, EventArgs e)
        {
            var watch = new Stopwatch();
            watch.Start();
            GetStockValue(txtInput.Text, txtO1,txtO2, txtO3,txtO4);
            watch.Stop();
            txtWatch2.Text = watch.ElapsedMilliseconds.ToString();

        }
    }
}
