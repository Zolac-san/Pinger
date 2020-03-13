using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using LiveCharts;
using LiveCharts.Wpf;

namespace Pinger2000
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Pinger pinger;
        private DispatcherTimer _timer = null;
        private Adress _adresses;
        private ChartValues<long> valuesPing;

        public MainWindow()
        {
            InitializeComponent();
            pinger = new Pinger();
            _adresses = new Adress();
            
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(1000);
            _timer.Tick += UpdateUI;
            _timer.Start();
            valuesPing = new ChartValues<long>();
            ChartPing.Series.Add(new LineSeries() { Name="Pings", Values = valuesPing });
        }

        private void UpdateUI(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(ListChoiceApp.SelectedValue?.ToString()))
            {
                pinger.PingHost(_adresses.getIPOf(ListChoiceApp.SelectedValue.ToString()));

            }


            /*valuesPing.Clear();
            valuesPing.AddRange(pinger.ListDelay);
            */

            
            /*if (ChartPing.Series[0].Values.Count > 20)
            {
                ChartPing.Series[0].Values.RemoveAt(0);
            }*/


            /*valuesPing.Clear();
            valuesPing.AddRange(pinger.ListDelay);
            */

            if (pinger.ListDelay.Count == 0)
                this.LastPing.Content = "Miaou";
            else if (pinger.ListDelay.Count > 0)
            {
                ChartPing.Series[0].Values.Add(pinger.ListDelay.Last());
                this.LastPing.Content = pinger.ListDelay.Last().ToString();
                if (ChartPing.Series[0].Values.Count > 20)
                    ChartPing.Series[0].Values.RemoveAt(0);
        }
                
            
            
                
            
        }

        

        private void UpdateListApp(object sender, MouseButtonEventArgs e)
        {
            _adresses.Load();
            this.ListChoiceApp.Items.Clear();
            foreach (string name in _adresses.GetAllNames())
            {
                ListChoiceApp.Items.Add(name);
            }
            pinger.Clear();
            
        }
    }
}
