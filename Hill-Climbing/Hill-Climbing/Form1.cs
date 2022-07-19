using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Hill_Climbing
{
    public partial class Form1 : Form, IObserver
    {
        //Invoke - обертка для многопоточности, обеспечивает нормальную работу интерфейса и алгоритма одновременно 
        delegate void InvokeC();
        delegate void InvokeL(double poi);
        delegate void InvokeS(double poi, int it);
        delegate void InvokeD(DataPoint dp);
        private controller controller;
        private model model;
        Func<double, double> function;
        private bool worc;

        public Form1(model model)
        {
            InitializeComponent();
            this.model = model;
            model.Register(this);
            attachController(makeController());
        }
        //MVC и Observer паттерн 
        public void attachController(controller controller)
        {
            this.controller = controller;
        }
        //MVC и Observer паттерн 
        protected controller makeController()
        {
            return new controller(this, model);
        }
        //MVC и Observer паттерн 
        public void UpdateState()
        {
            if (controller != null)
            {
                DrawParticle(model.result);
            }
        }

        //метод отрисовки графика с обертакми многопоточности и точкой на нем
        private void DrawParticle(double par)
        {
            chart1.BeginInvoke(new InvokeC(InvokeClear));
            listBox1.BeginInvoke(new InvokeS(InvokeLis), par, model.i);
            DataPoint dp = new DataPoint(par, MathParser.Result(func.Text,par));
            dp.MarkerStyle = MarkerStyle.Circle;
            chart1.BeginInvoke(new InvokeD(InvokeAdd), dp);
        }
        //обертка многопоточности 
        private void InvokeClear()
        {
            chart1.Series[1].Points.Clear();
        }
        //обертка многопоточности 
        private void InvokeLis(double par, int it)
        {
            listBox1.Items.Add($"X = {par}");
            progressBar1.Value = it;
        }
        //обертка многопоточности 
        private void InvokeAdd(DataPoint dp)
        {
            chart1.Series[1].Points.Add(dp);
        }

        //просто строим график 
        private void DrawGraphic(Func<double, double> func)
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.ChartAreas[0].AxisX.Minimum = Convert.ToInt32(from.Text);
            chart1.ChartAreas[0].AxisX.Maximum = Convert.ToInt32(to.Text);
            chart1.ChartAreas[0].AxisY.Maximum = Math.Abs(func(Convert.ToInt32(to.Text)));
            chart1.ChartAreas[0].AxisY.Minimum = -Math.Abs(func(Convert.ToInt32(from.Text)));

            for (int i = Convert.ToInt32(from.Text); i <= Convert.ToInt32(to.Text); i++)
            {

                chart1.Series[0].Points.AddXY(i, func(i));
            }
        }

        //запускаем и останавливаем работу алгоритма 
        private void start_Click(object sender, EventArgs e)
        {
            worc = !worc;
            if (worc)
            {
                progressBar1.Maximum = Convert.ToInt32(iterations.Text);
                listBox1.Items.Clear();
                function = controller.CreateFunction(func.Text);
                DrawGraphic(function);
                int[] bord = new int[] { (int)Convert.ToDouble(from.Text), (int)Convert.ToDouble(to.Text) };
                controller.start_search(Convert.ToInt32(iterations.Text), bord, func.Text, Convert.ToDouble(step.Text));
            }
            else
            {
                controller.Stop();
            }
        }
        //подсказки 
        private void Form1_Load(object sender, EventArgs e)
        {
            this.worc = false;
            toolTip1.SetToolTip(func, "function should be specified here (-n=0-n)");
            toolTip1.SetToolTip(from, "minimum value x");
            toolTip1.SetToolTip(to, "maximum value x");
            toolTip1.SetToolTip(step, "how fast is changing x");
            toolTip1.SetToolTip(iterations, "how many steps does the agent take");
            toolTip1.SetToolTip(listBox1, "agent-related values ​​during the search");
            toolTip1.SetToolTip(progressBar1, "how many iterations have been completed");
            toolTip1.SetToolTip(chart1, "schedule");
            toolTip1.SetToolTip(start, "starts execution; repeated presses will stop the current search");
        }
        //кто-то изменил функцию во время выполнения оптимизации, О ужас!
        private void func_TextChanged(object sender, EventArgs e)
        {
            if (worc)
            {
                worc = !worc;
                controller.Stop();
            }
        }
        //справка
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("the program searches for the maximum of the function using the hill approach algorithm, since the algorithm is quite indicative in 100% of cases the maximum will be found to increase the accuracy increase the step value", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
