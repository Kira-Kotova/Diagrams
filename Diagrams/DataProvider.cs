using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace Diagrams
{
    public class DataProvider
    {
        private double[] data;
        private List<double[]> series = new List<double[]>();
        public readonly Color[] ColorOfDiagram = { Colors.LightSkyBlue, Colors.YellowGreen, Colors.Orange, Colors.LightCoral, Colors.MediumSlateBlue, Colors.LightSeaGreen, Colors.DarkGray };

        /// <summary>
        /// Данные одной серии(если она одна).
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
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

        /// <summary>
        /// Данные нескольких серий(если их больше одной).
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
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

        /// <summary>
        /// Конструктор данных для диаграмм с одной серией.
        /// </summary>
        /// <param name="data"></param>
        public DataProvider(double[] data)
        {
            Data = data;
        }

        /// <summary>
        /// Конструктор данных для диаграмм с несколькими сериями.
        /// </summary>
        /// <param name="series"></param>
        public DataProvider(List<double[]> series)
        {
            Series = new List<double[]>(series);
        }
    }
}
