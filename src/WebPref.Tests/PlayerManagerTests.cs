using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using WebPref.Core;
using WebPref.Core.Playing;

namespace WebPref.Tests
{
    [TestFixture]
    public class PlayerManagerTests
    {
        private PlayerManager playerManager;
        private List<Player> createdPlayers = new List<Player>();

        [OneTimeSetUp]
        public void Init()
        {
            playerManager = PrefCore.GetPlayerManager();
        }

        [Test]
        public void CreatePlayerTest()
        {
            string playerId = Guid.NewGuid().ToString("N");
            Player player = playerManager.GetCreatePlayer(playerId, "Test player");
            //посмотрим, что там создалось
            Assert.IsNotNull(player);
            Assert.AreEqual(playerId, player.Id);
            //проверим, записался ли
            Player second = playerManager.GetPlayer(playerId);
            Assert.AreEqual(player, second);
            createdPlayers.Add(player); //потом прибрать чтобы
        }

        [Test]
        public void GetPlayerTest()
        {

        }
        [Test]
        public void GetUnexistingPlayerTest()
        {

        }
        /// <summary>
        /// Удаление пока на минималках, потом, когда добавится статистика, надо будет смотреть и проверять.
        /// </summary>
        [Test]
        public void DeletePlayerTest()
        {

        }

        /// <summary>
        /// p
        /// </summary>
        [OneTimeTearDown]
        public void CleanUp()
        {
            foreach(Player p in createdPlayers)
            {
                playerManager.DeletePlayer(p.Id);
            }
        }
    }
}
