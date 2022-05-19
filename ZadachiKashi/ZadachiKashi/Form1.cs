using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace metodEiler
{
    public partial class Form1 : Form
    {
        double n, e;
        // условия Каши и задание отрезка
        const double x0=0, y0=1, minX = 0, maxX = 1;
        // дифференциальное уравнение
        static public double f(double x, double y)
        {
            return Math.Exp(2 * x) * (2 + 3 * Math.Cos(x)) / (2 * y) - 3 * y * Math.Cos(x) / 2;
        }
        //точное решение задачи Каши
        static public double realF(double x)
        {
            return Math.Exp(x);
        }
        // метод очистки и настройки координатной плоскости
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
            zedGraphControl1.GraphPane.XAxis.Title.IsVisible = false;
            zedGraphControl1.GraphPane.YAxis.Title.IsVisible = false;
            zedGraphControl1.GraphPane.Title.IsVisible = false;
            zedGraphControl1.RestoreScale(zedGraphControl1.GraphPane);
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }
        // построение графиков
        public void build(ZedGraphControl Zed_GraphControl)
        {

            PointPairList list = new PointPairList();
            PointPairList reallist = new PointPairList();

            double h, h1, x=x0, y=y0;
            double nowE, maxE = 0;
            h = (maxX - minX) / (n-1);
            h1 = (double)(maxX - minX) / 1000D;
            // нахождение точек точной функции
            while (x < x0+h1*1000D)
            {
                reallist.Add(x, realF(x));
                x += h1;
            }
            // нахождение точек приближённой функции
            x = x0;
            while (x < x0 + h * n)
            {
                list.Add(x, y);
                // вычисление максимальной невязки
                nowE = Math.Abs(y - realF(x));
                if (nowE > maxE)
                    maxE = nowE;

                y += h * f(x, y);
                x += h;
            }

            textBox3.Text = Convert.ToString(maxE);
            // отрисовка графиков функций
            GraphPane my_Pane = Zed_GraphControl.GraphPane;
            LineItem myCircle = my_Pane.AddCurve("Приближённое решение", list, Color.Blue, SymbolType.None);
            LineItem myCircle2 = my_Pane.AddCurve("Точное решение", reallist, Color.Green, SymbolType.None);
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }
        public Form1()
        {
            InitializeComponent();
            Clear(zedGraphControl1);
        }
        // при нажатии на кнопку расчёт
        private void button1_Click(object sender, EventArgs ee)
        {
            if (textBox6.Text != "")
                try
                { n = Convert.ToDouble(textBox6.Text); }
                catch { return; }
                Clear(zedGraphControl1);
                build(zedGraphControl1);      
        }
    }
}
