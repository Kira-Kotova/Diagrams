using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Diagrams
{
    public class StackedHistogram : AbstractDiagram
    {
        /// <summary>
        /// Хранилище данных для данной диаграммы.
        /// </summary>
        private DataProvider Provider;
        
        /// <summary>
        /// Конструктор Нормированной гистограммы с накоплением.
        /// </summary>
        /// <param name="data">Данные для прорисовки.</param>
        public StackedHistogram(DataProvider data) : base(data)
        {
            Provider = new DataProvider(data.Series);
        }

        public override void Draw(Canvas canvas)
        {
            var delta = canvas.ActualWidth / Provider.Series.Count - 50;
            const int h = 10; // для грамотного отображения графика(при малых значениях)
            var dx = delta - h;
            var x = 0.0;;

            for (var j = 0; j < Provider.Series.Count; j++)
            {
                var array = Provider.Series[j];
                var y = 15.0;
                var dy = 0.0;  
                
                // Warning! Холст перевёрнут! Трансформация необходима для грамотного отображения содержимого.
                var transform = new TransformGroup();
                transform.Children.Add(new ScaleTransform(-1, 1));
                transform.Children.Add(new RotateTransform(180));
                
                for(var i = 0; i < array.Length; i++)
                {
                    dy += array[i] * h + 15.0;
                    var a = new Point(x, y);
                    var b = new Point(dx, y);
                    var c = new Point(dx, dy);
                    var d = new Point(x, dy);

                    var figure = new Polygon()
                    {
                        Fill = new SolidColorBrush(Provider.ColorOfDiagram[i]),
                        Points = new PointCollection(new List<Point>(){a, b, c, d})
                    };
                                       
                    var axisX = new TextBlock
                    {
                        Text = j.ToString(),
                        LayoutTransform = transform,
                        Margin = new Thickness((delta - h)/2 + (delta * j),0,0,0)
                    };
                    
                    var marker = new TextBlock
                    {
                        Text = array[i].ToString(),
                        TextAlignment = TextAlignment.Center,
                        LayoutTransform = transform,
                        Margin = new Thickness((delta - h)/2 + (delta * j), y + (dy - y - 15)/2, 0, 0) 
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
                    
                    y = dy;
                    
                    canvas.Children.Add(figure);
                    canvas.Children.Add(axisX);
                    canvas.Children.Add(marker);
                    canvas.Children.Add(series);
                    canvas.Children.Add(pic);
                }
                x += delta;
                dx += delta;
            }
        }
    }
}
