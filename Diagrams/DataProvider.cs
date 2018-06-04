using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace Diagrams
{
    public class DataProvider
    {
        private double[] data;
        private List<double[]> series = new List<double[]>();
        public readonly Color[] ColorOfDiagram = new Color[] { Colors.LightSkyBlue, Colors.YellowGreen, Colors.Orange, Colors.LightCoral, Colors.MediumSlateBlue, Colors.LightSeaGreen, Colors.DarkGray };

        public double[] Data
        {
            get { return data; }
            set
            {
                if (ReferenceEquals(value, null))
                    throw new ArgumentException(nameof(data));
                data = value;
            }
        }

        public List<double[]> Series
        {
            get { return series; }
            set
            {
                if(ReferenceEquals(value, null))
                    throw new ArgumentException(nameof(series));
                series = value;
            }
        }

        public DataProvider(double[] data)
        {
            Data = data;
        }

        public DataProvider(List<double[]> series)
        {
            Series = new List<double[]>(series);
        }
    }
}