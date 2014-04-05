using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scheduller
{
    public partial class Form1 : Form
    {
        public static Schedul schedul = new Schedul();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Add().ShowDialog();
            dataGridView1.Rows.Add(schedul.date.ToString("dd.MM.yyyy") + " " + schedul.time, schedul.file);
            start_process();
        }

        public class Schedul
        {
            public DateTime date;
            public string time;
            public string file;
            public bool isStarted = false;

            public Schedul()
            { }
            public Schedul(DateTime d, string t, string f)
            {
                date = d;
                time = t;
                file = f;
            }
        }

        private void start_process()
        {
            Timer _timer = new Timer();
            _timer.Interval = 20000;
            _timer.Tick += _timer_Tick;
            _timer.Start();
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            if (schedul.date.Date == DateTime.Now.Date)
                if (schedul.time == DateTime.Now.ToString("HH:mm"))
                    if (!schedul.isStarted)
                    {
                        System.Diagnostics.Process p = new System.Diagnostics.Process();
                        p.StartInfo.FileName = schedul.file;
                        p.Start();
                        schedul.isStarted = true;
                    }
        }
    }
}
