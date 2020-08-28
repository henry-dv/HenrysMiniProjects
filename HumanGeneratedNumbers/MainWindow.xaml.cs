using HenrysWpfLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using static HenrysWpfLib.Constants;

namespace HumanGeneratedNumbers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AddTab(Analysator.SortIntoHundreds);
            AddTab(Analysator.SortIntoMiddleDigit);
            AddTab(Analysator.SortIntoLastDigit);
            AddTab(Analysator.SortIntoLastTwoDigits);
            AddTab(Analysator.SortIntoTens);
            AddTab(Analysator.GetCrossSums);
        }

        void AddTab(Func<int[], Dictionary<int, int>> analysisFunction)
        {
            int[] numbers = Analysator.LoadNumbers();

            Dictionary<int, int> data = analysisFunction(numbers);

            TabItem graphTab = new TabItem();
            graphTab.Header = analysisFunction.Method.Name;
            Frame tabFrame = new Frame();

            BarGraph barGraph = new BarGraph(data.Values.ToArray<int>().Select(val => (double)val).ToArray());
            barGraph.Scaling = Scaling.Relative;
            barGraph.BarColor = Color.FromRgb(199, 0, 57);
            if (data.Values.Count > 10)
                barGraph.BarMargin = 0;
            barGraph.Visibility = Visibility.Visible;
            tabFrame.Content = barGraph;

            graphTab.Content = tabFrame;

            MyTabControl.Items.Add(graphTab);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Int32.TryParse(NumberTextBox.Text, out int uInput))
                NumLabel.Content = Analysator.GetOccurences(Analysator.LoadNumbers(), uInput) + " occurences!!!!!";
            else
                NumLabel.Content = "Invalid input!";
        }
    }
}
