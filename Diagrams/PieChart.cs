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
        /// <summary>
        /// Хранилище данных для данной диаграммы.
        /// </summary>
        private DataProvider Provider;
        
        /// <summary>
        /// Конструктор Круговой диаграммы.
        /// </summary>
        /// <param name="data">Данные для прорисовки.</param>
        public PieChart(DataProvider data) : base(data)
        {
            Provider = new DataProvider(data.Series[0]);
        }

        public override void Draw(Canvas canvas)
        {  
            var center = new Point(canvas.ActualWidth / 2, canvas.ActualHeight / 2);
            var radius = Min(canvas.ActualWidth, canvas.ActualHeight) / 2;
            var startAngle = 0.0;
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
                var geometry = new PathGeometry();
                geometry.Figures.Add(figure);                

                var path = new Path
                {
                    Fill = new SolidColorBrush(Provider.ColorOfDiagram[i]),
                    Stroke = new SolidColorBrush(Colors.White),
                    Data = geometry
                };
                
                // Warning! Холст перевёрнут! Трансформация необходима для грамотного отображения содержимого.
                var transform = new TransformGroup();
                transform.Children.Add(new ScaleTransform(-1, 1));
                transform.Children.Add(new RotateTransform(180));
                
                // Legend
                var series = new TextBlock
                {
                    Text = Provider.Data[i].ToString(),
                    LayoutTransform = transform,
                    Margin = new Thickness(60,100 + i * 20,0,0)
                };
                
                //Legend
                var pic = new TextBlock()
                {
                    Background = new SolidColorBrush(Provider.ColorOfDiagram[i]),   
                    Height = 12,
                    Width = 12,
                    Margin = new Thickness(40,100 + i * 20,0,0) 
                };
                
                canvas.Children.Add(series);
                canvas.Children.Add(pic);
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
