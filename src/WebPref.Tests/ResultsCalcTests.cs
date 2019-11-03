using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WebPref.Core.Utils;

namespace Tests
{
    /// <summary>
    ///     Тесты расчетчика результатов партии
    /// </summary>
    [Ignore("You are not prepared")]
    public class ResultsCalcTests
    {
        const string p1 = "p1";
        const string p2 = "p2";
        const string p3 = "p3";
        const string p4 = "p4";

        [Test]
        public void TestInit()
        {
            var calc = new ResultsCalc();
            Assert.IsTrue(calc.Init(new List<string> { p1, p2, p3 }));
            Assert.IsTrue(calc.Init(new List<string> { p1, p2, p3, p4 }));
            Assert.IsFalse(calc.Init(new List<string> { p1, p2, p3, p2 }));
        }

        /// <summary>
        ///     p1 сыграл 6, разошлись 6-2-2
        /// </summary>
        [Test]
        public void Test6_6_2_2()
        {
            IResultsCalc calc = new ResultsCalc();
            calc.Init(new List<string> { p1, p2, p3 });
            calc.Gain(p1, 2);
            calc.Whist(p2, p1, 4);
            calc.Whist(p3, p1, 4);
            var res = calc.Calculate();
            var p1res = res.FirstOrDefault(r => r.PlayerId == p1);
            var p2res = res.FirstOrDefault(r => r.PlayerId == p2);
            var p3res = res.FirstOrDefault(r => r.PlayerId == p3);
            Assert.IsNotNull(p1res);
            Assert.IsNotNull(p2res);
            Assert.IsNotNull(p3res);

            foreach (var r in res)
            {
                Trace.WriteLine(r);
            }

            Assert.AreEqual(0, p1res.GetWhists(p2));
            Assert.AreEqual(0, p1res.GetWhists(p3));
            Assert.AreEqual(4, p2res.GetWhists(p1));
            Assert.AreEqual(0, p2res.GetWhists(p3));
            Assert.AreEqual(4, p3res.GetWhists(p1));
            Assert.AreEqual(0, p3res.GetWhists(p2));
        }

        /// <summary>
        ///     p1 сыграл 6, разошлись 7-1-2
        /// </summary>
        [Test]
        public void Test6_7_1_2()
        {
            IResultsCalc calc = new ResultsCalc();
            calc.Init(new List<string> { p1, p2, p3 });
            calc.Gain(p1, 2);
            calc.Whist(p2, p1, 2);
            calc.Penalty(p2, 2);
            calc.Whist(p3, p1, 4);
            var res = calc.Calculate();
            var p1res = res.FirstOrDefault(r => r.PlayerId == p1);
            var p2res = res.FirstOrDefault(r => r.PlayerId == p2);
            var p3res = res.FirstOrDefault(r => r.PlayerId == p3);
            Assert.IsNotNull(p1res);
            Assert.IsNotNull(p2res);
            Assert.IsNotNull(p3res);

            Assert.AreEqual(2, p1res.GetGains());
            Assert.AreEqual(0, p2res.GetGains());
            Assert.AreEqual(0, p3res.GetGains());

            Assert.AreEqual(0, p1res.GetPenalties());
            Assert.AreEqual(0, p2res.GetPenalties());
            Assert.AreEqual(0, p3res.GetPenalties());

            Assert.AreEqual(0, p1res.GetWhists(p2));
            Assert.AreEqual(0, p1res.GetWhists(p3));
            Assert.AreEqual(4, p2res.GetWhists(p1));
            Assert.AreEqual(0, p2res.GetWhists(p3));
            Assert.AreEqual(4, p3res.GetWhists(p1));
            Assert.AreEqual(0, p3res.GetWhists(p2));

            calc.Init(new List<string> { p1, p2, p3 });

            //p1 сыграл 6, разошлись 7-1-2
            calc.Gain(p1, 2);
            calc.Whist(p2, p1, 4);
            calc.Whist(p3, p1, 4);
            res = calc.Calculate();
            p1res = res.FirstOrDefault(r => r.PlayerId == p1);
            p2res = res.FirstOrDefault(r => r.PlayerId == p2);
            p3res = res.FirstOrDefault(r => r.PlayerId == p3);
            Assert.IsNotNull(p1res);
            Assert.IsNotNull(p2res);
            Assert.IsNotNull(p3res);

            Assert.AreEqual(2, p1res.GetGains());
            Assert.AreEqual(0, p2res.GetGains());
            Assert.AreEqual(0, p3res.GetGains());

            Assert.AreEqual(0, p1res.GetPenalties());
            Assert.AreEqual(0, p2res.GetPenalties());
            Assert.AreEqual(0, p3res.GetPenalties());

            Assert.AreEqual(0, p1res.GetWhists(p2));
            Assert.AreEqual(0, p1res.GetWhists(p3));
            Assert.AreEqual(4, p2res.GetWhists(p1));
            Assert.AreEqual(0, p2res.GetWhists(p3));
            Assert.AreEqual(4, p3res.GetWhists(p1));
            Assert.AreEqual(0, p3res.GetWhists(p2));
        }
        
        [Test]
        public void Test3_2()
        {
            IResultsCalc calc = new ResultsCalc();
            calc.Init(new List<string> { p1, p2, p3 });

            calc.Gain(p1, 20);
            calc.Gain(p2, 20);
            calc.Gain(p3, 20);

            calc.Penalty(p1, 8);
            calc.Penalty(p2, 19);
            calc.Penalty(p3, 14);

            calc.Whist(p1, p2, 40);
            calc.Whist(p1, p3, 24);
            calc.Whist(p2, p1, 52);
            calc.Whist(p2, p3, 28);
            calc.Whist(p3, p1, 38);
            calc.Whist(p3, p2, 16);

            var res = calc.Calculate();
            var p1res = res.FirstOrDefault(r => r.PlayerId == p1);
            var p2res = res.FirstOrDefault(r => r.PlayerId == p2);
            var p3res = res.FirstOrDefault(r => r.PlayerId == p3);
            Assert.IsNotNull(p1res);
            Assert.IsNotNull(p2res);
            Assert.IsNotNull(p3res);

        }
    }
}