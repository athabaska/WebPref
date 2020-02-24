using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using WebPref.Core.Playing;

namespace WebPref.Tests
{
    public class GameTests
    {
        private Player p1 = new Player("p1", "p1");
        private Player p2 = new Player("p2", "p2");
        private Player p3 = new Player("p3", "p3");
        private Player p4 = new Player("p4", "p4");

        [Test]
        public void Test1()
        {
            var players = new List<Player> { p1, p2, p3 };
            var settings = new GameSettings(PlayersCountEnum.Three, GameTypeEnum.Leningrad);
            var game = new Game(players, settings);

            var deal = game.StartNewDeal();
            //ход принят, следующий игрок 2
            Assert.IsTrue(game.ProcessMove(new Move(p1.Id, Card.SevenOfDiamonds)));

            //ход не принят, следующий игрок 2
            Assert.IsFalse(game.ProcessMove(new Move(p2.Id, Card.JackOfSpades)));
            //ход принят, следующий игрок 3
            Assert.IsTrue(game.ProcessMove(new Move(p2.Id, Card.JackOfDiamonds)));

            //ход не принят, следующий игрок определяется по взятке
            Assert.IsTrue(game.ProcessMove(new Move(p2.Id, Card.AceOfDiamonds)));
        }
        
    }
}
