using System.Windows.Controls;

namespace Diagrams
{
    public enum DiagramType
    {
        PieChart,
        NormalizedHistogram,
        StackedHistogram,
        Plot
    }

    public abstract class AbstractDiagram : Canvas
    {
        private DataProvider DataInfo;
        
        public abstract void Draw(Canvas canvas);

        public AbstractDiagram(DataProvider data)
        {
            DataInfo = data;
        }
    }
}