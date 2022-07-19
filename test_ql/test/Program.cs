using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Diagnostics;

namespace test
{
    class Program
    {
        public class Unit
        {
            private int[] position;
            public List<int[]> actions;
            private int n;

            public Unit(int x, int y, int n)
            {
                this.position = new int[2];
                this.position[0] = x;
                this.position[1] = y;
                this.actions = new List<int[]>();
                fill();
                this.n = n;
            }

            public void fill()
            {
                int[] ac = new int[] { 0, 0 };
                this.actions.Add(ac);
                ac = new int[] { -1, -1 };
                this.actions.Add(ac);
                ac = new int[] { 0, -1 };
                this.actions.Add(ac);
                ac = new int[] { 1, -1 };
                this.actions.Add(ac);
                ac = new int[] { -1, 0 };
                this.actions.Add(ac);
                ac = new int[] { 1, 0 };
                this.actions.Add(ac);
                ac = new int[] { -1, 1 };
                this.actions.Add(ac);
                ac = new int[] { 0, 1 };
                this.actions.Add(ac);
                ac = new int[] { 1, 1 };
                this.actions.Add(ac);
            }

            public int[] Get_Position()
            {
                return this.position;
            }

            public void Set_Position(int x, int y)
            {
                this.position[0] = x;
                this.position[1] = y;
            }

            public int Get_N()
            {
                return this.n;
            }
        }

        class Enemy : Unit
        {
            Random rnd;

            public Enemy(int x, int y, int n) : base(x, y, n) { rnd = new Random(); }

            public void Make_Step()
            {
                int i = 0;
                bool expr = false;
                while (!expr)
                {
                    i = rnd.Next(9);
                    int nx, ny;
                    nx = Get_Position()[0] + actions[i][0];
                    ny = Get_Position()[1] + actions[i][1];
                    expr = (nx < Get_N()) && (ny < Get_N()) && (0 <= nx) && (0 <= ny);
                    if (expr)
                        Set_Position(nx, ny);
                }
            }
        }

        class Q_model
        {
            private double alpha, gamma;
            private Protagonist pr;
            public Dictionary<string, double> Generational_Experience;

            public Q_model()
            {
                this.alpha = 0.95;
                this.gamma = 0.95;
                this.Generational_Experience = new Dictionary<string, double>();
            }

            public void Set_Protagonist(Protagonist pr)
            {
                this.pr = pr;
            }

            public void Ran_Model(bool ran)
            {
                pr.prev_state = pr.curr_state.Cut(2);
                pr.prev_state.Push(pr.dx);
                pr.prev_state.Push(pr.dy);
                pr.curr_state = pr.Get_Features();
                pr.curr_state.Push(pr.dx);
                pr.curr_state.Push(pr.dy);
                if (ran)
                {
                    Debug.WriteLine(pr.prev_state.CopyToString());
                    Debug.WriteLine(pr.curr_state.CopyToString());
                }
                int r = pr.reward;
                if (!Generational_Experience.ContainsKey(pr.prev_state.CopyToString()))
                    Generational_Experience.Add(pr.prev_state.CopyToString(), 0);
                Stack<int> may_cond = new Stack<int>();
                Stack<double> nvec = new Stack<double>();
                foreach (int[] mmv in pr.actions)
                {
                    may_cond = pr.curr_state.Cut(2);
                    may_cond.Push(mmv[0]);
                    may_cond.Push(mmv[1]);
                    if (!Generational_Experience.ContainsKey(may_cond.CopyToString()))
                        Generational_Experience.Add(may_cond.CopyToString(), 0);
                    nvec.Push(Generational_Experience[may_cond.CopyToString()]);
                }
                //nvec = nvec.Max();
                Generational_Experience[pr.prev_state.CopyToString()] = Generational_Experience[pr.prev_state.CopyToString()] + alpha * (-Generational_Experience[pr.prev_state.CopyToString()] + r + gamma * nvec.Max());
            }
        }

        class Protagonist : Unit
        {
            public Stack<int> prev_state, curr_state;
            private Environment env;
            private ArrayList enemys;
            public int dx, dy, reward;
            private Random rnd;
            private Q_model qmodel;
            public double eps;

            public Protagonist(int x, int y, int n, ArrayList enemys, Environment env, Q_model qmod) : base(x, y, n)
            {
                this.enemys = enemys;
                this.env = env;
                this.dx = 0;
                this.dy = 0;
                this.eps = 0.95;
                this.rnd = new Random();
                this.qmodel = qmod;
                this.prev_state = Get_Features();
                this.prev_state.Push(dx);
                this.prev_state.Push(dy);
                this.curr_state = Get_Features();
                this.curr_state.Push(dx);
                this.curr_state.Push(dy);
            }

            public int[] strategy()
            {
                int i = rnd.Next(9);
                double[] best = new double[] { 0, 0, 0 };
                Stack<int> namel;
                Stack<int> name;
                int[] act = new int[] { 0, 0 };
                if (rnd.NextDouble() < eps)
                    act = actions[i];
                else
                {
                    namel = Get_Features();
                    foreach (int[] acti in actions)
                    {
                        name = namel.Copy();
                        name.Push(acti[0]);
                        name.Push(acti[1]);
                        if (!qmodel.Generational_Experience.ContainsKey(name.CopyToString()))
                            qmodel.Generational_Experience.Add(name.CopyToString(), 0);
                        if (best[2] < qmodel.Generational_Experience[name.CopyToString()])
                        {
                            best[0] = acti[0];
                            best[1] = acti[1];
                            best[2] = qmodel.Generational_Experience[name.CopyToString()];
                        }
                    }
                    act[0] = (int)best[0];
                    act[1] = (int)best[1];
                }
                return act;
            }

            public void Make_Step()
            {
                int[] a = strategy();
                int nx, ny;
                nx = Get_Position()[0] + a[0];
                ny = Get_Position()[1] + a[1];
                bool expr = (nx < Get_N()) && (ny < Get_N()) && (0 <= nx) && (0 <= ny);
                if (expr)
                    Set_Position(nx, ny);
            }

            public Stack<int> Get_Features()
            {
                Stack<int> Features = new Stack<int>();
                foreach (Enemy en in this.enemys)
                {
                    Features.Push(en.Get_Position()[0]);
                    Features.Push(en.Get_Position()[1]);
                }
                Features.Push(this.Get_Position()[0]);
                Features.Push(this.Get_Position()[1]);
                return Features;
            }
        }

        class Environment
        {
            private int[,] field;
            public Protagonist protagonist;
            private ArrayList enemys;
            private Q_model model;
            private bool is_end;

            public Environment(int n, int[] PP, int[,] EP, Q_model model)
            {
                this.enemys = new ArrayList();
                this.model = model;
                this.field = new int[n, n];
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        this.field[i, j] = 0;
                this.protagonist = new Protagonist(PP[0], PP[1], n, enemys, this, model);
                model.Set_Protagonist(this.protagonist);
                for (int i = 0; i < EP.GetLength(0); i++)
                {
                    Enemy en = new Enemy(EP[i, 0], EP[i, 1], n);
                    this.enemys.Add(en);
                }
            }

            public void Iteration()
            {
                foreach (Enemy en in this.enemys)
                    en.Make_Step();
                this.protagonist.Make_Step();
                Update_Field();
            }

            public void Get_Reward(bool is_end)
            {
                if (is_end)
                    this.protagonist.reward = 1;
                else
                    this.protagonist.reward = -1;
            }

            public void Update_Field()
            {
                int n = this.field.GetLength(0);
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        this.field[i, j] = 0;
                this.field[this.protagonist.Get_Position()[0], this.protagonist.Get_Position()[1]] = 1;
                foreach (Enemy en in this.enemys)
                    this.field[en.Get_Position()[0], en.Get_Position()[1]] = 2;
            }

            public void Print_Fild()
            {
                int k = 0;
                foreach (int i in field)
                {
                    if (k % field.GetLength(0) == 0)
                        Console.Write("\n");
                    Console.Write(i);
                    k++;
                }
                Console.WriteLine("___");
            }

            public bool Is_finished()
            {
                bool end = false;
                foreach(Enemy en in enemys)
                {
                    if ((protagonist.Get_Position()[0] == en.Get_Position()[0]) && (protagonist.Get_Position()[1] == en.Get_Position()[1]))
                        end = true;
                }
                return end;
            }

            public int play(bool silent)
            {
                is_end = Is_finished();
                int iter = 0;
                while (!is_end)
                {
                    Iteration();
                    is_end = Is_finished();
                    Get_Reward(is_end);
                    if (silent)
                        model.Ran_Model(silent);
                    else
                    {
                        Print_Fild();
                        Console.WriteLine("_____");
                    }
                    iter++;
                }
                return iter;
            }
        }

        static void Main(string[] args)
        {
            Q_model mod = new Q_model();
            Environment env;
            int iters = 0;
            int[] pp = new int[] { 1, 1 };
            int[,] ep = new int[,] { { 4, 4 }, { 4, 3 } };
            for (int i=0; i<500;i++)
            {
                env = new Environment(5, pp, ep, mod);
                env.protagonist.eps = 0.2;
                iters = env.play(true);
                Console.WriteLine(i + " " + iters);
            }

            for (int i = 0; i < 5; i++)
            {
                env = new Environment(5, pp, ep, mod);
                env.protagonist.eps = 0.2;
                env.play(false);
            }
            Console.ReadKey();
        }
    }
}
