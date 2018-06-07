using System.Windows.Controls;

namespace Diagrams
{
    /// <summary>
    /// Тип поддерживаемых диаграмм
    /// </summary>
    public enum DiagramType
    {
        PieChart,
        NormalizedHistogram,
        StackedHistogram,
        Plot,
        Scatter
    }

    public abstract class AbstractDiagram : Canvas
    {
        private DataProvider DataInfo;
        /// <summary>
        /// Метод прорисовки диаграммы
        /// </summary>
        /// <param name="canvas">Холст для прорисовки</param>
        public abstract void Draw(Canvas canvas);

        /// <summary>
        /// Конструктор диаграммы
        /// </summary>
        /// <param name="data">Серии</param>
        public AbstractDiagram(DataProvider data)
        {
            DataInfo = data;
        }
    }
}
