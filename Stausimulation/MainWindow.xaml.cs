using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Stausimulation
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        Car[] autos = new Car[20];
        
        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromMilliseconds(17);
            timer.Start();
            timer.Tick += animate;


            for (int i = 0; i < autos.Length; i++)
            {
                autos[i] = new Car();
            }
        }

        double[] averages = new double[1000];
        
        void animate(object sender, EventArgs e)
        {
            //Zeichenflaeche.Children.Clear();//alees loeschen
            Zeichenflaeche.Children.RemoveRange(3, autos.Length);
            //Autos durchgehen und zeichnen
            foreach (Car auto in autos)
            {
                //2 den naehsten Vorgaenger finden
                double minDistance = 42.0;// nicht 1.0 zur Sicherheit
                Car naehstesAuto=null;
                foreach (Car anderesauto in autos)
                {
                    if (anderesauto == auto) continue;
                    //     -0.9     =      4.2             -    5.1    
                    double distance = anderesauto.position - auto.position;
                    //     0.1      = 0.9-(-1)
                    distance -= Math.Floor(distance);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        naehstesAuto = anderesauto;
                    }
                }
                //2 Geschwindigkeit reduzieren
                if (minDistance < 0.02 && auto.speed > naehstesAuto.speed)
                {
                    auto.speed = naehstesAuto.speed;
                }

                auto.Move(timer.Interval);

                //3 Berechnungen
                double speedmin=double.MaxValue, speedmean=0, speedsum=0, speedmax=0;
                foreach (Car item in autos)
                {
                    double s = item.speed;
                    speedmin = Math.Min(s, speedmin);
                    speedmax = Math.Max(s, speedmax);
                    speedsum += s;
                }
                Lbl_maxV.Content = Math.Round(speedmax,4);
                Lbl_minV.Content = Math.Round(speedmin, 4);
                speedmean = speedsum / autos.Length;
                Lbl_meanV.Content = Math.Round(speedmean, 4);                

                //Werte aus dem array holen
                //double min = autos.Min(x => x.speed);
                //double max = autos.Max(x => x.speed);
            }

            foreach (Car auto in autos)
            {
                auto.Draw(Zeichenflaeche);
            }
        }
    }
}
