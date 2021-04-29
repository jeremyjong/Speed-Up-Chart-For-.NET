using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using CsvHelper;

namespace chart_app
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();         
            
            fdlg.Filter = "CSV (*.csv)|*.csv|CSV files (*.csv)|*.csv";
            fdlg.FilterIndex = 2;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = fdlg.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (chart1.Series.IndexOf("Series1") != -1)
            {
                chart1.Series.RemoveAt(0);
            }

            chart1.Series.Add("Series1");
            chart1.Series["Series1"].ChartType = SeriesChartType.FastPoint;

            var firstColumn = new List<string>();

            using (var reader = new StreamReader(textBox1.Text))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))


            {
                while (csv.Read())
                {
                    var field = csv.GetField<string>(2);
                    firstColumn.Add(field);
                }
            }

            string[] pdValues_s = firstColumn.ToArray();
            double[] pdValues = Array.ConvertAll(pdValues_s, new Converter<string, double>(Double.Parse));

            chart1.Series["Series1"].Points.SuspendUpdates();
            for (int j = 0; j < 1000; j++)
            {
                for (int i = 0; i < pdValues.Length; i++)
                {
                    chart1.Series[0].Points.Add(pdValues[i]);
                }
            }

            chart1.Series["Series1"].Points.ResumeUpdates();


        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (chart1.Series.IndexOf("Series1") != -1)
            {
                chart1.Series.RemoveAt(0);
            }
        }
    }
}
