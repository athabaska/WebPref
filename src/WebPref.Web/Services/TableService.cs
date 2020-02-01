using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPref.Core.Lobby;
using WebPref.Core.Playing;

namespace WebPref.Web.Services
{
    public class TableService
    {
        private TableManager tableManager;
        private PlayerManager playerManager;
        public TableService()
        {
            this.tableManager = WebPref.Core.PrefCore.GetTableManager();
            this.playerManager = WebPref.Core.PrefCore.GetPlayerManager();
        }

        public IEnumerable<Table> GetTables()
        {
            return tableManager.GetCurrentTables();
        }

        /// <summary>
        /// Создание нового стола, пока на минималках.
        /// </summary>
        /// <param name="playerId">Идентификатор игрока-владельца создаваемого стола</param>
        /// <param name="playerName">Имя пользователя, на случай, если его пока в базе игр нет и надо добавлять</param>
        /// <param name="tableSettings">Свойства стола, должны передваться откуда-то из клиентской части</param>
        /// <param name="resultDescription">Описание результата создания стола или возникших ошибок</param>
        /// <param name="newTable"></param>
        /// <returns></returns>
        public bool CreateTable(string playerId, string playerName, TableSettings tableSettings, out string resultDescription, out Table newTable)
        {
            newTable = null;
            try
            {                
                Player owner = playerManager.GetCreatePlayer(playerId, playerName);
                if (owner == null)
                {
                    resultDescription = "Игрок с переданным ID " + playerId + " не найден в БД игр";
                    return false;
                }

                newTable = tableManager.CreateTable(owner, tableSettings);
                resultDescription = "Стол успешно создан";
                return true;
            }
            catch(Exception ex)
            {
                resultDescription = "Ошибка создания стола: " + ex.Message;
                return false;
            }
        }

        public bool AddPlayerToTable(string playerId, string tableId, out string resultDescription)
        {
            try
            {
                Player player = playerManager.GetPlayer(playerId);
                if (player == null)
                {
                    resultDescription = "Игрок с переданным ID " + playerId + " не найден в БД игр";
                    return false;
                }
                Guid tableGuid = new Guid(tableId);
                if (tableManager.AddPlayerToTable(player, tableGuid))
                {
                    resultDescription = "Игрок успешно добавлен к столу";
                    return true;
                }
                else
                {
                    resultDescription = "Не удалось добавить игрока к столу";
                    return false;
                }
            }
            catch (Exception ex)
            {
                resultDescription = "Ошибка добавления игрока к столу: " + ex.Message;
                return false;
            }
        }
    }
}
