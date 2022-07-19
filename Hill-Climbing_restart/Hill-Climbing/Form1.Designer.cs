namespace Hill_Climbing
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.func = new System.Windows.Forms.TextBox();
            this.to = new System.Windows.Forms.TextBox();
            this.from = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.start = new System.Windows.Forms.Button();
            this.iterations = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.label4 = new System.Windows.Forms.Label();
            this.step = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.restarts = new System.Windows.Forms.TextBox();
            this.iter = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // func
            // 
            this.func.Location = new System.Drawing.Point(12, 48);
            this.func.Name = "func";
            this.func.Size = new System.Drawing.Size(197, 20);
            this.func.TabIndex = 0;
            this.func.Text = "10*sin(x)+x";
            this.func.TextChanged += new System.EventHandler(this.func_TextChanged);
            // 
            // to
            // 
            this.to.Location = new System.Drawing.Point(158, 74);
            this.to.Name = "to";
            this.to.Size = new System.Drawing.Size(51, 20);
            this.to.TabIndex = 1;
            this.to.Text = "20";
            // 
            // from
            // 
            this.from.Location = new System.Drawing.Point(81, 74);
            this.from.Name = "from";
            this.from.Size = new System.Drawing.Size(49, 20);
            this.from.TabIndex = 2;
            this.from.Text = "-1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(9, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "x varies from";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(136, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "to";
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(12, 381);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(97, 23);
            this.start.TabIndex = 5;
            this.start.Text = "start";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // iterations
            // 
            this.iterations.Location = new System.Drawing.Point(118, 124);
            this.iterations.Name = "iterations";
            this.iterations.Size = new System.Drawing.Size(92, 20);
            this.iterations.TabIndex = 6;
            this.iterations.Text = "35";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "number of iterations:";
            // 
            // chart1
            // 
            chartArea3.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chart1.Legends.Add(legend3);
            this.chart1.Location = new System.Drawing.Point(216, 25);
            this.chart1.Name = "chart1";
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series5.IsVisibleInLegend = false;
            series5.Legend = "Legend1";
            series5.Name = "Series1";
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series6.IsVisibleInLegend = false;
            series6.Legend = "Legend1";
            series6.MarkerSize = 10;
            series6.Name = "ParticleViz";
            this.chart1.Series.Add(series5);
            this.chart1.Series.Add(series6);
            this.chart1.Size = new System.Drawing.Size(572, 413);
            this.chart1.TabIndex = 12;
            title3.Name = "График";
            this.chart1.Titles.Add(title3);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 176);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(197, 199);
            this.listBox1.TabIndex = 9;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 10;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::Hill_Climbing.Properties.Resources.info;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "function";
            // 
            // step
            // 
            this.step.Location = new System.Drawing.Point(45, 100);
            this.step.Name = "step";
            this.step.Size = new System.Drawing.Size(164, 20);
            this.step.TabIndex = 12;
            this.step.Text = "2";
            this.step.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 107);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "step";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 414);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(198, 23);
            this.progressBar1.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 162);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "number of resturts:";
            // 
            // restarts
            // 
            this.restarts.Location = new System.Drawing.Point(118, 155);
            this.restarts.Name = "restarts";
            this.restarts.Size = new System.Drawing.Size(92, 20);
            this.restarts.TabIndex = 16;
            this.restarts.Text = "6";
            // 
            // iter
            // 
            this.iter.Location = new System.Drawing.Point(180, 381);
            this.iter.Name = "iter";
            this.iter.ReadOnly = true;
            this.iter.Size = new System.Drawing.Size(29, 20);
            this.iter.TabIndex = 17;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.iter);
            this.Controls.Add(this.restarts);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.step);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.iterations);
            this.Controls.Add(this.start);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.from);
            this.Controls.Add(this.to);
            this.Controls.Add(this.func);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox func;
        private System.Windows.Forms.TextBox to;
        private System.Windows.Forms.TextBox from;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button start;
        private System.Windows.Forms.TextBox iterations;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox step;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox restarts;
        private System.Windows.Forms.TextBox iter;
    }
}

