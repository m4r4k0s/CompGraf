using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace Hill_Climbing
{
    public class model
    {
        private ArrayList listeners = new ArrayList();
        public double result, step, prev;
        public bool worc;
        public int iterat, i, count, restarts;
        private hill_clim hc;

        public model()
        {
            this.worc = false;
        }

        public void Find_max(int iterat, int[] bord, string fun, double step, int restart)
        {
            this.i = 0;
            this.result = 0;
            this.count = 0;
            this.prev = this.result;
            this.iterat = iterat;
            worc = true;
            this.restarts = restart;
            this.hc = new hill_clim(step, fun, bord, this);
        }

        //остнавливаем поиск
        public void stop()
        {
            worc = false;
            hc.stop();
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
