using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Diagrams;

namespace WDiagrams
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private DiagramType currentType;

        private void PieChartButtonClick(object sender, RoutedEventArgs e)
        {
            currentType = DiagramType.PieChart;
            new DrawDiagramDialog(currentType).ShowDialog();
        }
        
        private void NormalizedHistogramButtonClick(object sender, RoutedEventArgs e)
        {
            currentType = DiagramType.NormalizedHistogram;
            new DrawDiagramDialog(currentType).ShowDialog();
        }
        
        private void StackedHistogramButtonClick(object sender, RoutedEventArgs e)
        {
            currentType = DiagramType.StackedHistogram;
            new DrawDiagramDialog(currentType).ShowDialog();
        }

        private void ExitButtonOnClick(object sender, RoutedEventArgs e) => Close();


        private void PlotButtonOnClic(object sender, RoutedEventArgs e)
        {
            currentType = DiagramType.Plot;
            new DrawDiagramDialog(currentType).ShowDialog();
        }

        private void ScatterButtonOnClic(object sender, RoutedEventArgs e)
        {
            currentType = DiagramType.Scatter;
            new DrawDiagramDialog(currentType).ShowDialog();
        }

    }
}
