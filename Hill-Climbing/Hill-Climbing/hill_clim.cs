using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hill_Climbing
{
    class hill_clim
    {
        private double bestRes, best, StepSize;
        private string Funct;

        public hill_clim(double step, string funct, int[] bord)
        {
            Random rnd = new Random();
            this.best = rnd.Next(bord[0], bord[1]);
            this.StepSize = step;
            this.Funct = funct;
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
