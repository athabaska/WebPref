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
            var players = new List<Player> { p1, p2, p3 };
            var settings = new GameSettings(PlayersCountEnum.Three, GameTypeEnum.Leningrad);
            var game = new Game(players, settings);

            //торги 1, первое слово p1
            var trading = game.StartTrading();

            Assert.IsTrue(trading.CheckBid(Bid.PassBid(p1)));
            Assert.IsTrue(trading.CheckBid(Bid.PassBid(p2)));
            Assert.IsTrue(trading.CheckBid(Bid.PassBid(p3)));

            Assert.IsTrue(trading.IsFinished);
        }

        [Test]
        public void Test1()
        {
            var players = new List<Player> { p1, p2, p3 };
            var settings = new GameSettings(PlayersCountEnum.Three, GameTypeEnum.Leningrad);
            var game = new Game(players, settings);

            //торги 1, первое слово p1
            var trading = game.StartTrading();

            Assert.IsTrue(trading.CheckBid(new Bid(p1, BidTypeEnum.Play, ContractEnum.Six, SuitEnum.Spades)));
            Assert.IsTrue(trading.CheckBid(new Bid(p2, BidTypeEnum.Play, ContractEnum.Six, SuitEnum.Clubs)));
            Assert.IsTrue(trading.CheckBid(Bid.PassBid(p3)));

            Assert.IsTrue(trading.CheckBid(new Bid(p1, BidTypeEnum.Play, ContractEnum.Six, SuitEnum.Hearts)));
            Assert.IsFalse(trading.CheckBid(new Bid(p2, BidTypeEnum.Play, ContractEnum.Six, SuitEnum.Spades)));
            Assert.IsTrue(trading.CheckBid(new Bid(p2, BidTypeEnum.Play, ContractEnum.Seven, SuitEnum.Diamonds)));

            Assert.IsFalse(trading.CheckBid(new Bid(p1, BidTypeEnum.Play, ContractEnum.Six, SuitEnum.Spades)));
            Assert.IsFalse(trading.CheckBid(new Bid(p1, BidTypeEnum.Play, ContractEnum.Seven, SuitEnum.Clubs)));
            Assert.IsTrue(trading.CheckBid(Bid.PassBid(p1)));

            Assert.IsTrue(trading.IsFinished);
            Assert.IsTrue(trading.Highest.Equals(new Bid(p2, BidTypeEnum.Play, ContractEnum.Seven, SuitEnum.Diamonds)));
            Assert.IsTrue(trading.IsFinished);

            //торги 2, первое слово p2
            trading = game.StartTrading();
            Assert.IsFalse(trading.CheckBid(Bid.PassBid(p3)));
            Assert.IsTrue(trading.CheckBid(new Bid(p2, BidTypeEnum.Play, ContractEnum.Six, SuitEnum.Spades)));
            Assert.IsTrue(trading.CheckBid(Bid.PassBid(p3)));
            Assert.IsTrue(trading.CheckBid(Bid.PassBid(p1)));
            Assert.IsTrue(trading.IsFinished);

            //торги 3, первое слово p3
            trading = game.StartTrading();
            Assert.IsTrue(trading.CheckBid(Bid.PassBid(p3)));
            Assert.IsFalse(trading.CheckBid(new Bid(p2, BidTypeEnum.Play, ContractEnum.Six, SuitEnum.Spades)));
            Assert.IsTrue(trading.CheckBid(new Bid(p1, BidTypeEnum.Play, ContractEnum.Six, SuitEnum.Spades)));
            Assert.IsTrue(trading.CheckBid(new Bid(p2, BidTypeEnum.Play, ContractEnum.Eight, SuitEnum.Spades)));
            Assert.IsTrue(trading.IsFinished);
        }
    }
}