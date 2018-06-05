using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Diagrams
{
    public class Plot : AbstractDiagram
    {
        private DataProvider Provider;

        public Plot(DataProvider data) : base(data)
        {
            Provider = new DataProvider(data.Series);
        }

        public override void Draw(Canvas canvas)
        {
            var delta = canvas.ActualWidth / Provider.Series.Count;
            
            for (var i = 0; i < Provider.Series.Count; i++)
            {
                var x = 0.0;
                var dx = delta;
                var array = Provider.Series[i];
                for (var j = 0; j < array.Length - 1; j++)
                {
                    var y = array[j] * 20;
                    var dy = array[j + 1] * 20;
                    var figure = new Polyline
                    {
                        Points = new PointCollection(new List<Point>() {new Point(x, y), new Point(dx, dy)}),
                        Stroke = new SolidColorBrush(Provider.ColorOfDiagram[i]),
                        StrokeThickness = 3
                    };
                    var point = new Polyline
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
                    canvas.Children.Add(point);
                }
            }
        }
    }
}
