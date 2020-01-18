using System;
using System.Collections.Generic;
using WebPref.Core.Playing;

namespace WebPref.Core.Lobby
{
    /// <summary>
    ///     Стол с игроками
    /// </summary>
    public class Table
    {
        public Table()
        {
            this.Id = Guid.NewGuid().ToString("N");
            Players = new List<Player>();
        }

        public string Id { get; private set; }

        public List<Player> Players { get; set; }
    }
}
