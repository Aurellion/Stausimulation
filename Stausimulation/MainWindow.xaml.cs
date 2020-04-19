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
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Start();
            timer.Tick += animate;


            for (int i = 0; i < autos.Length; i++)
            {
                autos[i] = new Car();
            }
        }

        void animate(object sender, EventArgs e)
        {
            Zeichenflaeche.Children.Clear();
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
            }

            foreach (Car auto in autos)
            {
                auto.Draw(Zeichenflaeche);
            }
        }
    }
}
