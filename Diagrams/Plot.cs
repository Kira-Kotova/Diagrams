using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Diagrams
{
    public class Plot : AbstractDiagram
    {
        /// <summary>
        /// Хранилище данных для данной диаграммы.
        /// </summary>
        private DataProvider Provider;

        /// <summary>
        /// Конструктор Обычного графика.
        /// </summary>
        /// <param name="data">Данные для прорисовки.</param>
        public Plot(DataProvider data) : base(data)
        {
            Provider = new DataProvider(data.Series);
        }

        public override void Draw(Canvas canvas)
        {
            var delta = canvas.ActualWidth / (Provider.Series[0].Length - 1) - 50;
            
            for (var i = 0; i < Provider.Series.Count; i++)
            {
                var x = 10.0;
                var dx = delta;
                const int h = 20; // для грамотного отображения графика(при малых значениях)
                var array = Provider.Series[i];
                
                // Warning! Холст перевёрнут! Трансформация необходима для грамотного отображения содержимого.
                var transform = new TransformGroup();
                transform.Children.Add(new ScaleTransform(-1, 1));
                transform.Children.Add(new RotateTransform(180));
                
                // Axises
                for (var k = 0; k < array.Length; k++)
                {
                    var axisX = new TextBlock
                    {
                        Text = k.ToString(),
                        LayoutTransform = transform, 
                        Margin = new Thickness(delta * k,0,0,0)
                    };
                    
                    var axisY = new TextBlock
                    {
                        Text = array[k].ToString(),
                        LayoutTransform  = transform,
                        Margin = new Thickness(0,array[k] * h + 10,0,0)
                    };

                    var marker = new Line
                    {
                        Stroke = new SolidColorBrush(Colors.LightGray),
                        StrokeDashArray = new DoubleCollection(new double[]{1,2}),
                        X1 = 0,
                        X2 = canvas.ActualWidth - 50,
                        Y1 = array[k] * h,
                        Y2 = array[k] * h 
                    };
                    
                    //Legend
                    var series = new TextBlock
                    {
                        Text = (i+1).ToString(),
                        LayoutTransform = transform,
                        Margin = new Thickness(canvas.ActualWidth - 30,100 + i * 20,0,0)
                    };
                    
                    //Legend
                    var pic = new TextBlock
                    {
                        Background = new SolidColorBrush(Provider.ColorOfDiagram[i]),
                        Height = 12,
                        Width = 12,
                        Margin = new Thickness(canvas.ActualWidth - 50,100 + i * 20,0,0)
                    };
                    
                    canvas.Children.Add(axisX);
                    canvas.Children.Add(axisY);
                    canvas.Children.Add(marker);
                    canvas.Children.Add(series);
                    canvas.Children.Add(pic);
                }
                
                //Lines
                for (var j = 0; j < array.Length - 1; j++)
                {
                    var y = array[j] * h + 10;
                    var dy = array[j + 1] * h + 10;
                    
                    var figure = new Polyline
                    {
                        Points = new PointCollection(new List<Point>() {new Point(x, y), new Point(dx, dy)}),
                        Stroke = new SolidColorBrush(Provider.ColorOfDiagram[i]),
                        StrokeThickness = 3
                    };
                    
                    var vertex = new Polyline
                    {
                        Points = new PointCollection(figure.Points),
                        Stroke = new SolidColorBrush(Colors.Black),
                        StrokeThickness = 3,
                        StrokeDashCap = PenLineCap.Round,
                        StrokeDashArray = new DoubleCollection(new double[]{1,100})
                    };
                    
                    x = dx;
                    dx += delta;
                    canvas.Children.Add(figure);
                    canvas.Children.Add(vertex);
                } 
            }
        }
    }
}
