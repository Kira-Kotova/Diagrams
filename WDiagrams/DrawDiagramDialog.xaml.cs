using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Diagrams;

namespace WDiagrams
{
    public partial class DrawDiagramDialog : Window
    {
        private DiagramType Type;
        private double[] data;
        private DataProvider DataInfo = new DataProvider(new List<double[]>());
        
        public DrawDiagramDialog(DiagramType type)
        {
            Type = type;
            InitializeComponent();
            Repeat.IsEnabled = false;
        }

        private AbstractDiagram GetDiagram(DiagramType type)
        {
            AbstractDiagram diagram;
            switch (type)
            {
                 case DiagramType.PieChart:
                     diagram = new PieChart(DataInfo);
                     break;
                 case DiagramType.NormalizedHistogram:
                    diagram = new NormalizedHistogram(DataInfo);
                     break;
                 case DiagramType.StackedHistogram:
                     diagram = new StackedHistogram(DataInfo);
                     break;
                 case DiagramType.Plot:
                     diagram = new Plot(DataInfo);
                     break;
                 case DiagramType.Scatter:
                     diagram = new Scatter(DataInfo);
                     break;
                 default:
                     throw new ArgumentOutOfRangeException(nameof(type));     
            }
            return diagram;
        }

        private double[] GetData() => ListBoxWithData.Items.OfType<double>().ToArray();
        //создание нового листа<double>(Листбокс.Генерит коллекцию содержимого. Фильтрует по типу <string>. Проецирует каждый элемент в double.Создаёт массив элементов)
        
        private void DrawButtonOnClick(object sender, RoutedEventArgs e)
        {
            data = GetData();
            DataInfo.Series.Add(data);
            try
            {
                var diagram = GetDiagram(Type);
                diagram.Draw(MainCanvas);
                DataInfo.Series.Clear();
                ListBoxWithData.Items.Clear();
                Repeat.IsEnabled = true;
                AddButton.IsEnabled = false;
                SeriesButton.IsEnabled = false;
            }
            catch
            {
                MessageBox.Show("Fatality!","Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void AddButtonOnClick(object sender, RoutedEventArgs e)
        {
            double result;  
            if (double.TryParse(TextBoxWithData.Text, out result))
            {
                ListBoxWithData.Items.Add(result);
                TextBoxWithData.Text = string.Empty;
                if (DataInfo.Series.Count > 0 && ListBoxWithData.Items.Count == DataInfo.Series[0].Length)
                {
                    SeriesButton.IsEnabled = true;
                    AddButton.IsEnabled = false;
                }
            }
        }

        private void SeriesButtonOnClick(object sender, RoutedEventArgs e)
        {
            data = GetData();
            DataInfo.Series.Add(data);
            ListBoxWithData.Items.Clear();
            SeriesButton.IsEnabled = false;
            AddButton.IsEnabled = true;
        }

        private void DeleteButtonOnClick(object sender, RoutedEventArgs e)
        {
            ListBoxWithData.Items.RemoveAt(ListBoxWithData.SelectedIndex);
            DeleteButton.IsEnabled = false;
            AddButton.IsEnabled = true;
        }

        private void ListBoxWithData_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxWithData.SelectedIndex >= 0)
                DeleteButton.IsEnabled = true;
        }
        
        private void WindowKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && AddButton.IsEnabled)
                AddButtonOnClick(sender, new RoutedEventArgs());
            if (e.Key == Key.Delete && DeleteButton.IsEnabled)
                DeleteButtonOnClick(sender, new RoutedEventArgs());
            if (e.Key == Key.Escape)
                Close();
        }

        private void RepeatButtonOnClick(object sender, RoutedEventArgs e)
        {
            MainCanvas.Children.Clear();
            Repeat.IsEnabled = false;
            AddButton.IsEnabled = true;
            SeriesButton.IsEnabled = true;
        }

        // Запрет на масштабирование окна
        private void DrawDiagramDialog_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            MaxHeight = 500;
            MinHeight = 500;
            MaxWidth = 900; 
            MinWidth = 900;
        }

        private void TitleTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            TitleTextBox.Foreground = new SolidColorBrush(Colors.Black);
        }
    }
}
