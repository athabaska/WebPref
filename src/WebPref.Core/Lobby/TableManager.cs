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

        public TableManager()
        {
            _currentTables = new Dictionary<Guid, Table>();
        }

        /// <summary>
        ///     Создать стол
        /// </summary>
        /// <param name="owner">Создатель стола</param>
        /// <param name="settings">Настройки стола</param>
        public Table CreateTable(Player owner, TableSettings settings)
        {            
            var table = new Table(owner, settings, GetFreeNumber());
            _currentTables[table.Id] = table;
            return table;
        }

        /// <summary>
        ///     Удалить стол
        /// </summary>
        /// <param name="tableId">Уникальный ID стола</param>
        public void RemoveTable(Guid tableId)
        {
            //todo проверка что стол закрыт и можно вообще удалять?
            _currentTables.Remove(tableId);
        }

        /// <summary>
        ///     Получить актуальный список столов, упорядоченный по номеру стола
        /// </summary>
        public Table[] GetCurrentTables()
        {
            return _currentTables.Values.OrderBy(t => t.Number).ToArray();
        }

        /// <summary>
        ///     Сгенерить уникальный номер стола
        /// </summary>
        private int GetFreeNumber()
        {
            //todo
            var rnd = new Random();
            return rnd.Next(1, 10000);
        }
    }
}
