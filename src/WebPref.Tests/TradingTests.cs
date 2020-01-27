using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using WebPref.Core.Playing;

namespace WebPref.Tests
{
    public class TradingTests
    {
        private Player p1 = new Player("p1", "p1");
        private Player p2 = new Player("p2", "p2");
        private Player p3 = new Player("p3", "p3");
        private Player p4 = new Player("p4", "p4");

        [Test]
        public void TestAllPass()
        {
            var players = new List<Player> { p1, p2, p3};

            var trading = new Trading(players, p2, ContractEnum.Six);

            trading.CheckBid(new Bid(p1, BidTypeEnum.Play, SuitEnum.Spades, ContractEnum.Six));
        }
    }
}
