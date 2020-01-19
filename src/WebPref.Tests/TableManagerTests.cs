using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using WebPref.Core;
using WebPref.Core.Lobby;
using WebPref.Core.Playing;

namespace Tests
{
    [TestFixture]
    public class TableManagerTests
    {
        private Player owner;
        private PlayerManager playerManager;
        [OneTimeSetUp]
        public void Init()
        {
            //создадим игрока 
            playerManager = PrefCore.GetPlayerManager();
            owner = playerManager.GetCreatePlayer(Guid.NewGuid().ToString("N"), "Test table owner");
            
        }

        [Test]
        public void AddTableTestOwnerExists()
        {
            TableManager tableManager = PrefCore.GetTableManager();
            
            Table newTable = tableManager.CreateTable(owner, new TableSettings(PlayersCountEnum.Three, GameTypeEnum.Leningrad, false));
            Assert.IsTrue(tableManager.GetCurrentTables(false).Length == 1, "Количество столов не равно 1");
            Assert.AreEqual(newTable, tableManager.GetCurrentTables()[0], "Столы не равны");            
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            //playerManager.DeletePlayer(owner.Id);
        }

    }
}
