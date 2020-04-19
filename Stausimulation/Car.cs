using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Stausimulation
{
    class Car
    {
        //private double position;//Zahl der Runden
        public double position { get; private set;}//2 Zahl der Runden

        //private double speed;//Geschwindigkeit (Betrag) in Runden pro Minute

        public double speed;//Geschwindigkeit (Betrag) in Runden pro Minute

        private static Random random = new Random();
        public Car()
        {
            position = random.NextDouble();
            //speed = 10 * random.NextDouble();
            speed = 8 + 4 * random.NextDouble();//2
        }

        public void Draw(Canvas zeichenflaeche)
        {
            Ellipse e = new Ellipse();
            e.Width = 5;
            e.Height = 5;
            e.Fill = Brushes.Blue;
            double angle = 2 * Math.PI * position;
            Canvas.SetLeft(e, 300 + 150 * Math.Sin(angle));
            Canvas.SetTop(e, 300 - 150 * Math.Cos(angle));
            zeichenflaeche.Children.Add(e);
        }

        internal void Move(TimeSpan interval)
        {
            position += speed * interval.TotalMinutes;
        }
    }
}
