using System;
using System.Windows.Forms;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Timer myTimer = new Timer();
            myTimer.Interval = 1000;
            Schedul s = new Schedul(DateTime.Now, DateTime.Now.ToString("HH:mm"), "random.exe");
            Schedul ideal = new Schedul(DateTime.Now, DateTime.Now.ToString("HH:mm"), "random.exe");
            ideal.isStarted = true;
            s.start_process();
            foreach(System.Diagnostics.Process p in System.Diagnostics.Process.GetProcesses())
                if(p.ProcessName.CompareTo("random.exe") == 0)
                    Assert.AreEqual(s, ideal);
        }

        [TestMethod]
        public void TestMethod2()
        {
            List<Schedul> idealList = new List<Schedul>();
            Composite com = new Composite();
            com.add(new Schedul(DateTime.Now, DateTime.Now.ToString("HH:mm"), "random.exe"));
            com.add(new Schedul(DateTime.Now, DateTime.Now.ToString("HH:mm"), "Timer.exe"));
            com.add(new Schedul(DateTime.Now, DateTime.Now.ToString("HH:mm"), "PatternCurs.exe"));
            idealList.Add(new Schedul(DateTime.Now, DateTime.Now.ToString("HH:mm"), "random.exe"));
            idealList.Add(new Schedul(DateTime.Now, DateTime.Now.ToString("HH:mm"), "Timer.exe"));
            idealList.Add(new Schedul(DateTime.Now, DateTime.Now.ToString("HH:mm"), "PatternCurs.exe"));

            ConcreteIterator ci = new ConcreteIterator(com);
            Assert.AreEqual(true, ci.EqualSchedul(idealList));
        }

        [TestMethod]
        public void TestMethod3()
        {
            List<Schedul> idealList = new List<Schedul>();
            Composite com = new Composite();
            com.add(new Schedul(DateTime.Now, DateTime.Now.ToString("HH:mm"), "random.exe"));
            com.add(new Schedul(DateTime.Now, DateTime.Now.ToString("HH:mm"), "Timer.exe"));
            com.add(new Schedul(DateTime.Now, DateTime.Now.ToString("HH:mm"), "PatternCurs.exe"));
            idealList.Add(new Schedul(DateTime.Now, DateTime.Now.ToString("HH:mm"), "random.exe"));
            idealList.Add(new Schedul(DateTime.Now, DateTime.Now.ToString("HH:mm"), "Timer.exe"));
            idealList.Add(new Schedul(DateTime.Now, DateTime.Now.ToString("HH:mm"), "PatternCurs.exe"));
            com.Save();

            FileStream fs = new FileStream("ideal.xml", FileMode.OpenOrCreate);
            fs.Close();
            XmlSerializer xs = new XmlSerializer(typeof(List<Schedul>));
            StreamWriter file = new StreamWriter("ideal.xml");
            xs.Serialize(file, idealList);
            file.Close();
            //как сравнить 2 полученные xml?
            //StreamReader idealSr = new StreamReader("ideal.xml");
            //StreamReader testSr = new StreamReader("db.xml");
            //Assert.AreEqual(idealSr, testSr);
            ConcreteIterator ci = new ConcreteIterator(com);
            Assert.AreEqual(true, ci.EqualSchedul(idealList));
        }

        [TestMethod]
        public void TestMethod4()
        {
            List<Schedul> idealList = new List<Schedul>();
            Composite com = new Composite();
            com.add(new Schedul(DateTime.Now, DateTime.Now.ToString("HH:mm"), "random.exe"));
            com.add(new Schedul(DateTime.Now, DateTime.Now.ToString("HH:mm"), "Timer.exe"));
            com.add(new Schedul(DateTime.Now, DateTime.Now.ToString("HH:mm"), "PatternCurs.exe"));
            idealList.Add(new Schedul(DateTime.Now, DateTime.Now.ToString("HH:mm"), "random.exe"));
            idealList.Add(new Schedul(DateTime.Now, DateTime.Now.ToString("HH:mm"), "Timer.exe"));
            idealList.Add(new Schedul(DateTime.Now, DateTime.Now.ToString("HH:mm"), "PatternCurs.exe"));
            com.Save();
            com = new Composite();
            com.Open();
            ConcreteIterator ci = new ConcreteIterator(com);
            Assert.AreEqual(true, ci.EqualSchedul(idealList));
        }
    }
}
