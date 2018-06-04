using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using static System.Math;

namespace Diagrams
{
    public class PieChart : AbstractDiagram
    {
        private DataProvider Provider;
        
        public PieChart(DataProvider data) : base(data)
        {
            Provider = new DataProvider(data.Series[0]);
        }

        public override void Draw(Canvas canvas)
        {  
            var center = new Point(canvas.ActualWidth / 2, canvas.ActualHeight / 2);
            var radius = Min(canvas.ActualWidth, canvas.ActualHeight) / 2;
            var startAngle = 0.0;
            var geometry = new PathGeometry();
            var sum = Provider.Data.Sum();
            for (var i = 0; i < Provider.Data.Length; i++)
            {
                var angle = 2 * PI * Provider.Data[i] / sum;
                var startPoint = PointByValue(center, radius, startAngle);
                var targetPoint = PointByValue(center, radius, startAngle - angle);
                
                var figure = new PathFigure {IsClosed = true, StartPoint = center, IsFilled = true};
                
                figure.Segments.Add(new LineSegment(startPoint, true));
                figure.Segments.Add(new ArcSegment
                    (targetPoint, new Size(radius, radius), angle, angle > PI, SweepDirection.Counterclockwise, true ));
                //point - конечная точка дуги
                //size - радиус арки x y
                //rotationAngle - поворот эллипса по оси x
                //isLargeArc - должна ли быть дуга больше 180
                //sweepDirection - направление отрисовки(по часовой\против)
                //isStroked - Задайте значение true, чтобы вычертить дугу, если тип Pen используется для отрисовки сегмента; в противном случае — значение false.
                geometry.Figures.Add(figure);

                var path = new Path
                {
                    Stroke = Brushes.Black,
                    Fill = new SolidColorBrush(Provider.ColorOfDiagram[i]),
                    Data = geometry
                };

                
                canvas.Children.Add(path);
                startAngle -= angle;
            }
            canvas.InvalidateVisual();
        }

        private Point PointByValue(Point center, double radius, double angle)
        {
            var x = center.X + radius * Cos(angle);
            var y = center.Y + radius * Sin(angle);
            return new Point(x, y);
        }

    }
}