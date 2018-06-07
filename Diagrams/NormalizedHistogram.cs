using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Diagrams
{
    public class NormalizedHistogram : AbstractDiagram
    {
        /// <summary>
        /// Хранилище данных для данной диаграммы.
        /// </summary>
        private DataProvider Provider;

        /// <summary>
        /// Конструктор Нормированной гистограммы.
        /// </summary>
        /// <param name="data">Данные для прорисовки.</param>
        public NormalizedHistogram(DataProvider data) : base(data)
        {
            Provider = new DataProvider(data.Series);
        }
        
        public override void Draw(Canvas canvas)
        {
            var validHeight = canvas.ActualHeight;
            var delta = canvas.ActualWidth / Provider.Series.Count - 50;
            const int h = 10; // для грамотного отображения графика(при малых значениях)
            var dx = delta - h;
            var x = 0.0;
            
            // Warning! Холст перевёрнут! Трансформация необходима для грамотного отображения содержимого.
            var transform = new TransformGroup();
            transform.Children.Add(new ScaleTransform(-1, 1));
            transform.Children.Add(new RotateTransform(180));

            for (var i =0; i < Provider.Series.Count; i++)
            {
                var array = Provider.Series[i];
                var y = 15.0;
                var dy = 0.0;  
                var sum = array.Sum();

                for(var j = 0; j < array.Length; j++)
                {
                    dy += (array[j] * validHeight + 15)/ sum;
                    var a = new Point(x, y);
                    var b = new Point(dx, y);
                    var c = new Point(dx, dy);
                    var d = new Point(x, dy);
                    
                    var figure = new Polygon()
                    {
                        Fill = new SolidColorBrush(Provider.ColorOfDiagram[j]),
                        Points = new PointCollection(new List<Point>(){a, b, c, d})
                    };
                    
                    var axisX = new TextBlock
                    {
                        Text = i.ToString(),
                        LayoutTransform = transform,
                        Margin = new Thickness((delta - h)/2 + (delta * i),0,0,0)
                    };
                    
                    var marker = new TextBlock
                    {
                        Text = array[j].ToString(),
                        TextAlignment = TextAlignment.Center,
                        LayoutTransform = transform,
                        Margin = new Thickness((delta - h)/2 + (delta * i), y + (dy - y - 15)/2, 0, 0) 
                    };
                    
                    //Legend
                    var series = new TextBlock
                    {
                        Text = (j+1).ToString(),
                        LayoutTransform = transform,
                        Margin = new Thickness(canvas.ActualWidth - 30,100 + j * 20,0,0)
                    };
                    
                    //Legend
                    var pic = new TextBlock
                    {
                        Background = new SolidColorBrush(Provider.ColorOfDiagram[j]),
                        Height = 12,
                        Width = 12,
                        Margin = new Thickness(canvas.ActualWidth - 50,100 + j * 20,0,0)
                    };
                    
                    y = dy;

                    canvas.Children.Add(figure);
                    canvas.Children.Add(axisX);
                    canvas.Children.Add(marker);
                    canvas.Children.Add(series);
                    canvas.Children.Add(pic);
                }
                
                x += delta + 10;
                dx += delta;
            }
        }
    }
}
