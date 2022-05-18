using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace ZadachiKashi
{
    //Метод Рунге-Кутта
    public partial class Form1 : Form
    {
        double xo, yo, e;
        static public double f(double x, double y)
        {
            return 2*x +y;
            //return Math.Sin(x);
        }
        static public double realF(double x)
        {
            return -2 * (x + 1) + Math.Exp(x)*4;
            //return -Math.Cos(x);
        }

        private void Clear(ZedGraphControl Zed_GraphControl)
        {
            zedGraphControl1.GraphPane.CurveList.Clear();
            zedGraphControl1.GraphPane.GraphObjList.Clear();

            zedGraphControl1.GraphPane.XAxis.Type = AxisType.Linear;
            zedGraphControl1.GraphPane.XAxis.Scale.TextLabels = null;
            zedGraphControl1.GraphPane.XAxis.MajorGrid.IsVisible = false;
            zedGraphControl1.GraphPane.YAxis.MajorGrid.IsVisible = false;
            zedGraphControl1.GraphPane.YAxis.MinorGrid.IsVisible = false;
            zedGraphControl1.GraphPane.XAxis.MinorGrid.IsVisible = false;
            zedGraphControl1.RestoreScale(zedGraphControl1.GraphPane);

            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }

        public void build(ZedGraphControl Zed_GraphControl)
        {

            PointPairList list = new PointPairList();
            PointPairList reallist = new PointPairList();

            double h, x=xo, y=yo;
            long n = (long)((50-xo)/e) + 1;
            h = (50 - xo) / n;

            while (x < 50+h/2)
            {
                if (x > 0)
                {
                    list.Add(x, y);
                    reallist.Add(x, realF(x));
                }

                double k1 = f(x, y);
                double k2 = f(x + h/2, y+k1*h/2);
                double k3 = f(x + h / 2, y + k2 * h / 2);
                double k4 = f(x + h , y + k3 * h);

                y = y + h / 6 * (k1 + 2 * k2 + 2 * k3 + k4);
                x = x + h;
            }

            GraphPane my_Pane = Zed_GraphControl.GraphPane;
            LineItem myCircle = my_Pane.AddCurve("Pribl", list, Color.Blue, SymbolType.Circle);
            LineItem myCircle2 = my_Pane.AddCurve("Func", reallist, Color.Green, SymbolType.Square);
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();

        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs ee)
        {
            if (textBox1.Text != "" && textBox4.Text != "" && textBox6.Text != "")
            {
                try
                {
                    xo = Convert.ToDouble(textBox1.Text);
                    yo = Convert.ToDouble(textBox4.Text);
                    e = Convert.ToDouble(textBox6.Text);
                }
                catch { return; }
                Clear(zedGraphControl1);
                build(zedGraphControl1);
                
            }
        }
    }
}
