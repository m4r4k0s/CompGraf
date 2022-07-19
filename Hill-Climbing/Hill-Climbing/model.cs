using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Timers;

namespace Hill_Climbing
{
    public class model
    {
        private ArrayList listeners = new ArrayList();
        public double result, step, prev;
        private bool worc;
        public int iterat, i, count;
        private System.Timers.Timer timer;
        private hill_clim hc;

        public model()
        {
            this.worc = false;
            timer = new System.Timers.Timer();
            timer.Enabled = false;
            timer.AutoReset = true;
            timer.Elapsed += new ElapsedEventHandler(Search);
            timer.Interval = 100;
        }

        public void Find_max(int iterat, int[] bord, string fun, double step)
        {
            this.i = 0;
            this.result = 0;
            count = 0;
            prev = result;
            this.iterat = iterat;
            worc = true;
            this.hc = new hill_clim(step, fun, bord);
            timer.Enabled = worc;
        }

        //вызываем шаги алгоритма и обновляем наблюдателей 
        public void Search(object sender, ElapsedEventArgs e)
        {
            prev = result;
            result = hc.Start_Climding();
            UpdateObservers();
            i++;
            if (i > iterat)
            {
                worc = false;
                timer.Enabled = worc;
            }
            if (prev == result)
                count++;
            else
                count = 0;
            if (count>20)
            {
                worc = false;
                timer.Enabled = worc;
                MessageBox.Show("maximum or local maximum is reached and the program is stopped ahead of schedule, if the current result does not suit you, try running it again with other coefficients", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //остнавливаем поиск
        public void stop()
        {
            worc = false;
            timer.Enabled = worc;
        }

        //паттерн наблюдателя 
        public void Register(IObserver o)
        {
            listeners.Add(o);
            o.UpdateState();
        }

        //паттерн наблюдателя 
        public void Deregister(IObserver o)
        {
            listeners.Remove(o);
        }

        //паттерн наблюдателя 
        public void UpdateObservers()
        {
            foreach (IObserver ob in listeners)
            {
                ob.UpdateState();
            }
        }
    }
}
