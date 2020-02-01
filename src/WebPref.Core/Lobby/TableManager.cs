using System.Collections.Generic;
using WebPref.Core.Playing;
using System;
using System.Linq;

namespace WebPref.Core.Lobby
{
    /// <summary>
    ///     Диспетчер столов
    /// </summary>
    public class TableManager
    {
        private readonly IDictionary<Guid, Table> _currentTables;
        private PrefContext PrefContext;
        private PlayerManager PlayerManager;
        internal TableManager(PrefContext prefContext, PlayerManager playerManager)
        {
            _currentTables = new Dictionary<Guid, Table>();
            PrefContext = prefContext;
            PlayerManager = playerManager;
        }

        /// <summary>
        ///     Создать стол
        /// </summary>
        /// <param name="owner">Создатель стола</param>
        /// <param name="settings">Настройки стола</param>
        public Table CreateTable(Player owner, TableSettings settings)
        {
            try
            {
                //проверим, а нет ли уже стола, где этот игрок присутствует                
                if (PlayerIsSeated(owner.Id))
                    throw new Exception("Player has joined table already.");
                //смотрим, есть ли игрок в БД
                Player ownerPlayer = PlayerManager.GetCreatePlayer(owner.Id, owner.Name);

                var table = new Table(owner, settings, GetFreeNumber());
                _currentTables[table.Id] = table;
                PrefContext.Tables.Add(table);                
                PrefContext.SaveChanges();
                return table;
            }
            catch (Exception ex)
            {
                //TODO какие-то логи, наверное, надо прикрутить, пока пробросим выше
                throw new Exception("Table creation error: " + ex.Message);
            }
        }

        /// <summary>
        ///     Удалить стол
        /// </summary>
        /// <param name="tableId">Уникальный ID стола</param>
        public void RemoveTable(Guid tableId)
        {
            //todo проверка что стол закрыт и можно вообще удалять?            
            Table table = PrefContext.Tables.First(t => t.Id == tableId);
            if (table != null)
            {
                PrefContext.Remove(table);
                PrefContext.SaveChanges();
            }
            _currentTables.Remove(tableId);
        }

        /// <summary>
        ///     Получить актуальный список столов, упорядоченный по номеру стола
        /// </summary>
        public Table[] GetCurrentTables(bool withRefresh = false)
        {
            if (withRefresh)
            {
                _currentTables.Clear();
                foreach (var table in PrefContext.Tables)
                {
                    _currentTables.Add(table.Id, table);
                }
            }
            return _currentTables.Values.OrderBy(t => t.Number).ToArray();
        }

        public bool AddPlayerToTable(Player player, Guid tableId)
        {
            try
            {
                //смотрим, а не сидит ли уже игрок где-нибудь
                if (PlayerIsSeated(player.Id) == true)
                    throw new Exception("Player joined the table already.");
                var table = _currentTables[tableId];
                Player dbPlayer = PlayerManager.GetCreatePlayer(player.Id, player.Name);

                if (table.AddPlayer(dbPlayer))
                {
                    PrefContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                throw new Exception("Player adding error: " + ex.Message);
            }
        }

        /// <summary>
        ///     Сгенерить уникальный номер стола
        /// </summary>
        private int GetFreeNumber()
        {
            //todo
            if (_currentTables.Count > 0)
                return _currentTables.Values.Max(t => t.Number) + 1;
            else
                return 1;
            //var rnd = new Random();
            //return rnd.Next(1, 10000);
        }

        private bool PlayerIsSeated(string playerId)
        {
            //проверим, а нет ли уже стола, где этот игрок присутствует
            Table existing = _currentTables.Values.FirstOrDefault(t => t.HasPlayer(playerId));
            if (existing != null)
                return true;
            else
                return false;
        }
    }
}
