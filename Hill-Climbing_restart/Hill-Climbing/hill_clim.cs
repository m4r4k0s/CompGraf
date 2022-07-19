using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace Hill_Climbing
{
    class hill_clim
    {
        private double bestRes, best, StepSize, best_try, StepSize_keeper;
        private int[] bord;
        private System.Timers.Timer timer;
        private Random rnd;
        private model model;
        private string Funct;

        public hill_clim(double step, string funct, int[] bord, model m)
        {
            this.model = m;
            this.rnd = new Random();
            this.bord = bord;
            this.best = rnd.Next(bord[0], bord[1]);
            this.StepSize = step;
            this.StepSize_keeper = step;
            this.Funct = funct;
            timer = new System.Timers.Timer();
            timer.Enabled = false;
            timer.AutoReset = true;
            timer.Elapsed += new ElapsedEventHandler(Search);
            timer.Interval = 100;
            timer.Enabled = true;
        }

        public void stop()
        {
            timer.Enabled = model.worc;
        }

        public void Search(object sender, ElapsedEventArgs e)
        {
            model.prev = model.result;
            model.result = Start_Climding();
            model.UpdateObservers();
            model.i++;
            if (model.i > model.iterat && model.restarts == 0)
            {
                model.worc = false;
                if (MathParser.Result(Funct, model.result) < MathParser.Result(Funct, best_try))
                {
                    model.result = best_try;
                    model.i--;
                    model.UpdateObservers();
                }
                timer.Enabled = model.worc;
            }
            else if(model.i > model.iterat && model.restarts != 0)
            {
                model.i = 0;
                model.count = 0;
                StepSize = StepSize_keeper;
                best = rnd.Next(bord[0], bord[1]);
                model.restarts--;
                if (MathParser.Result(Funct, model.result) > MathParser.Result(Funct, best_try))
                    best_try = model.result;
            }
            if (model.prev == model.result)
                model.count++;
            else
                model.count = 0;
            if (model.count > 20 && model.restarts != 0)
            {
                model.i = 0;
                model.count = 0;
                StepSize = StepSize_keeper;
                best = rnd.Next(bord[0], bord[1]);
                model.restarts--;
                if (MathParser.Result(Funct, model.result) > MathParser.Result(Funct, best_try))
                    best_try = model.result;
            }
            else if(model.count > 20 && model.restarts == 0)
            {
                model.worc = false;
                if (MathParser.Result(Funct, model.result) < MathParser.Result(Funct, best_try))
                {
                    model.result = best_try;
                    model.i--;
                    model.UpdateObservers();
                }
                timer.Enabled = model.worc;
                MessageBox.Show("maximum or local maximum is reached and the program is stopped ahead of schedule, if the current result does not suit you, try running it again with other coefficients", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //метод шага 
        public double Start_Climding()
        {
            bestRes = MathParser.Result(Funct, best);
            double NeighborS, NeighborM;
            NeighborS = best - StepSize;
            NeighborM = best + StepSize;
            if (MathParser.Result(Funct, NeighborS) > bestRes)
            {
                bestRes = MathParser.Result(Funct, NeighborS);
                best = NeighborS;
            }
            else if (MathParser.Result(Funct, NeighborM) > bestRes)
            {
                bestRes = MathParser.Result(Funct, NeighborM);
                best = NeighborM;
            }
            else
            {
                StepSize = StepSize * 0.9;
            }
            return best;
        }
    }
}
