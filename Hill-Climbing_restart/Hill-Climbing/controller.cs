using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hill_Climbing
{
    public class controller
    {
        private string function;
        private model model;
        private Form1 view;

        //конструктор контроллера 
        public controller(Form1 view, model model)
        {
            this.model = model;
            this.view = view;
        }

        //метод начинающий поиск максимума функции 
        public void start_search(int iterat, int[] bord, string fun, double step, int restart)
        {
            model.Find_max(iterat, bord, fun, step, restart);
        }

        //уже забыл что это -_-
        public Func<double, double> CreateFunction(string function)
        {
            this.function = function;
            return Function;
        }

        //метод остановки оптимизации 
        public void Stop()
        {
            model.stop();
        }

        //возврашает значение функции 
        public double Function(double x)
        {
            return MathParser.Result(function, x);
        }
    }
}
