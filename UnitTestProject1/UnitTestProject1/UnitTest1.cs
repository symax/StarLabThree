using System;
using System.Windows.Forms;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    }
}
