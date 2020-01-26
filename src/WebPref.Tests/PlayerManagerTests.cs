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
        private Player deletePlayer;
        private Player searchPlayer;

        [OneTimeSetUp]
        public void Init()
        {
            playerManager = PrefCore.GetPlayerManager();
            //заготовим игроков для тестов поиска и удаления
            Player newPlayer = new Player();
            newPlayer.Id = "id";
            newPlayer.Name = "name";
            string id = Guid.NewGuid().ToString("N");
            deletePlayer = playerManager.GetCreatePlayer(id, "Delete player");
            createdPlayers.Add(deletePlayer);
            id = Guid.NewGuid().ToString("N");
            searchPlayer = playerManager.GetCreatePlayer(id, "Search player");
            createdPlayers.Add(searchPlayer);
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
            Assert.AreEqual(player, second, "Player in memory is not equal to player in db");
            createdPlayers.Add(player); //потом прибрать чтобы
        }

        [Test]
        public void GetExistingPlayerTest()
        {
            Player dbPlayer = playerManager.GetPlayer(searchPlayer.Id);
            Assert.IsNotNull(dbPlayer, "Player not found in DB");
            Assert.AreEqual(searchPlayer, dbPlayer, "Players are not equal");
        }
        [Test]
        public void GetUnexistingPlayerTest()
        {
            string id = Guid.NewGuid().ToString("N");
            Player player = playerManager.GetPlayer(id);
            Assert.IsNull(player, "Unexpected player object returned");
        }
        /// <summary>
        /// Удаление пока на минималках, потом, когда добавится статистика, надо будет смотреть и проверять.
        /// </summary>
        [Test]
        public void DeletePlayerTest()
        {
            bool result = playerManager.DeletePlayer(deletePlayer.Id);
            Assert.IsTrue(result, "Error deleting player");
            Player player = playerManager.GetPlayer(deletePlayer.Id);
            Assert.IsNull(player, "Player was not deleted from db");
        }

        /// <summary>
        /// p
        /// </summary>
        [OneTimeTearDown]
        public void CleanUp()
        {
            //удаляем всех без затей, если игрока в базе нет, то просто ничего не произойдет
            foreach(Player p in createdPlayers)
            {
                playerManager.DeletePlayer(p.Id);
            }
        }
    }
}
