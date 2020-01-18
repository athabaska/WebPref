using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WebPref.Core.Calculations;
using WebPref.Core.Playing;
using WebPref.Core.Utils;

namespace Tests
{
    /// <summary>
    ///     Тесты расчетчика результатов партии
    /// </summary>    
    public class LeningradCalcTests
    {
        const string p1 = "p1";
        const string p2 = "p2";
        const string p3 = "p3";
        const string p4 = "p4";

        [Test]
        public void TestInit()
        {
            var calc = new LeningradCalc();
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
            IResultsCalc calc = new LeningradCalc();
            calc.Init(new List<string> { p1, p2, p3 });
            calc.GameSuccess(p1, ContractEnum.Six);
            calc.WhistSuccess(p2, p1, ContractEnum.Six, 2);
            calc.WhistSuccess(p3, p1, ContractEnum.Six, 2);
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
    }
}