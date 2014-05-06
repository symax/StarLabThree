using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace UnitTestProject1
{
    class Class1
    {
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

        public void start_process()
        {
            Timer _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Tick += _timer_Tick;
            _timer.Start();
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            if (this.date.Date == DateTime.Now.Date)
                if (this.time == DateTime.Now.ToString("HH:mm"))
                    if (!this.isStarted)
                    {
                        System.Diagnostics.Process p = new System.Diagnostics.Process();
                        p.StartInfo.FileName = this.file;
                        p.Start();
                        this.isStarted = true;
                    }
        }
    }

        public class Composite
        {
            List<Schedul> c = new List<Schedul>();

            public void add(Schedul s)
            {
                c.Add(s);
            }

            public List<Schedul> getCompose()
            {
                return c;
            }

            public void addCompose(Composite com)
            {
                this.c.AddRange(com.c);
            }

            public void Save()
            {
                FileStream fs = new FileStream("db.xml", FileMode.OpenOrCreate);
                fs.Close();
                XmlSerializer xs = new XmlSerializer(typeof(List<Schedul>));
                StreamWriter file = new StreamWriter("db.xml");
                xs.Serialize(file, this.getCompose());
                file.Close();
            }
        }

        public interface Iterator
        {
            bool hasNext();
            Schedul next();
        }

        public class ConcreteIterator : Iterator
        {
            List<Schedul> t = new List<Schedul>();

            int index;

            public ConcreteIterator(Composite c)
            {
                t = c.getCompose();
                index = 0;
            }

            public ConcreteIterator(List<Schedul> l)
            {
                t = l;
                index = 0;
            }

            public Schedul next()
            {
                index++;
                return t[index - 1];
            }

            public void toFirst()
            {
                index = 0;
            }

            public bool hasNext()
            {
                if (t.Count > index)
                    return true;
                else
                    return false;
            }

            public bool EqualSchedul(List<Schedul> SL)
            {
                bool IsEqual = true;
                Schedul s = new Schedul();
                foreach (Schedul sch in SL)
                    if (this.hasNext())
                    {
                        s = this.next();
                        if (s.file.CompareTo(sch.file) != 0 || s.isStarted != sch.isStarted || s.time.CompareTo(sch.time) != 0
                            || s.date.Date != sch.date.Date)
                        {
                            IsEqual = false;
                            break;
                        }
                    }
                return IsEqual;
            }
        }
}
