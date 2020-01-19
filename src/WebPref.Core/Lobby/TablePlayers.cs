using System;
using System.Collections.Generic;
using System.Text;
using WebPref.Core.Playing;

namespace WebPref.Core.Lobby
{
    public class TablePlayers
    {
        public string PlayerId { get; set; }
        public Player Player { get; set; }
        public Guid TableId { get; set; }
        public Table Table { get; set; }
    }
}
