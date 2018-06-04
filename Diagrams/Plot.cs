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
            
        }
    }
}