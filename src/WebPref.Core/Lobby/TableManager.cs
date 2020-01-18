using System.Collections.Generic;
using WebPref.Core.Playing;

namespace WebPref.Core.Lobby
{
    /// <summary>
    ///     Диспетчер столов
    /// </summary>
    public class TableManager
    {
        private readonly Dictionary<string, Table> currentTables;

        public TableManager()
        {
            this.currentTables = new Dictionary<string, Table>();
        }

        public Table CreateTable(List<Player> players)
        {
            
            return new Table();
        }
    }
}
