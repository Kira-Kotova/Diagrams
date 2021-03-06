using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Diagrams
{
    public class Scatter : AbstractDiagram
    {
        /// <summary>
        /// Хранилище данных для данной диаграммы.
        /// </summary>
        private DataProvider Provider;

        /// <summary>
        /// Конструктор Точечной диаграммы.
        /// </summary>
        /// <param name="data">Данные для прорисовки.</param>
        public Scatter(DataProvider data) : base(data)
        {
            Provider = new DataProvider(data.Series[0]);
        }

        public override void Draw(Canvas canvas)
        {
            var delta = canvas.ActualWidth / (Provider.Data.Length + 1);
            var x = 15.0;
            const int h = 20;
            
            // Warning! Холст перевёрнут! Трансформация необходима для грамотного отображения содержимого.
            var transform = new TransformGroup();
            transform.Children.Add(new ScaleTransform(-1, 1));
            transform.Children.Add(new RotateTransform(180));

            for (var i = 0; i < Provider.Data.Length; i++)
            {
                var y = Provider.Data[i] * h + 15;
                
                var figure = new Ellipse
                {
                    Width = 5,
                    Height = 5,
                    Fill = new SolidColorBrush(Provider.ColorOfDiagram[4]),
                    Margin = new Thickness(x,y,0,0)
                };
                
                var axisX = new TextBlock
                {
                    Text = i.ToString(),
                    LayoutTransform = transform,
                    Margin = new Thickness(delta * i, 0, 0, 0)
                };
                
                var axisY = new TextBlock
                {
                    Text = Provider.Data[i].ToString(),
                    LayoutTransform  = transform,
                    Margin = new Thickness(0,y,0,0)
                };

                var markerH = new Line
                {
                    Stroke = new SolidColorBrush(Colors.LightGray),
                    StrokeDashArray = new DoubleCollection(new double[] {1, 2}),
                    X1 = 0,
                    X2 = canvas.ActualWidth,
                    Y1 = y,
                    Y2 = y
                };

                var markerV = new Line
                {
                    Stroke = new SolidColorBrush(Colors.LightGray),
                    StrokeDashArray = new DoubleCollection(new double[] {1, 2}),
                    X1 = x,
                    X2 = x,
                    Y1 = 0,
                    Y2 = canvas.ActualHeight
                };

                x += delta;

                canvas.Children.Add(axisX);
                canvas.Children.Add(axisY);
                canvas.Children.Add(markerH);
                canvas.Children.Add(markerV);
                canvas.Children.Add(figure);
            }
        }
    }
}
