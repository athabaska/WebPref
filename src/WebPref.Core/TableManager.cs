using System;
using System.Collections.Generic;
using System.Text;

namespace WebPref.Core
{
    public class TableManager
    {
        private Dictionary<string, Table> currentTables;

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
