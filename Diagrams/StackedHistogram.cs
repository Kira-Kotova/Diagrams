using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Diagrams
{
    public class StackedHistogram : AbstractDiagram
    {
        private DataProvider Provider;
        
        public StackedHistogram(DataProvider data) : base(data)
        {
            Provider = new DataProvider(data.Series);
        }

        public override void Draw(Canvas canvas)
        {
            var validHeight = canvas.ActualHeight;
            var delta = (canvas.ActualWidth / Provider.Series.Count);
            var dx = delta - 10; // *1
            var x = 0.0;

            for (var j =0; j < Provider.Series.Count; j++)
            {
                var array = Provider.Series[j];
                var y = 0.0;
                var dy = 0.0;  
                var sum = array.Sum();

                for(var i = 0; i < array.Length; i++)
                {
                    var max = array.Sum();
                    if (max > validHeight)
                        max = validHeight;
                    dy += (array[i] * max/ sum) * 10; // *1
                    var a = new Point(x, y);
                    var b = new Point(dx, y);
                    var c = new Point(dx, dy);
                    var d = new Point(x, dy);
                    var figure = new Polygon()
                    {
                        Fill = new SolidColorBrush(Provider.ColorOfDiagram[i]),
                        Points = new PointCollection(new List<Point>(){a, b, c, d})
                    };
                    y = dy;

                    canvas.Children.Add(figure);
                }
                x += delta;
                dx += delta;
            }
        }
    }
}

// *1) 10-ки для грамотного отображения графика на холсте