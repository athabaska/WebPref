using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPref.Core.Lobby;

namespace WebPref.Web.Models
{
    public class TableModel
    {
        public TableModel(Table sourceTable)
        {
            this.Id = sourceTable.Id.ToString("N");
            this.Name = sourceTable.Number.ToString();
            this.State = TableState.Waiting; //TODO - подумать, нужно ли и описать

            this.Players = new List<PlayerModel>();
            foreach(var player in sourceTable.GetPlayers())
            {
                this.Players.Add(new PlayerModel
                {
                    Id = player.Id,
                    Name = player.Name
                });
            }
        }
        public string Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public TableState State { get; set; } 
        public int PlayerCount { get; set; }
        public List<PlayerModel> Players { get; set; }
        
    }

    public enum TableState
    {
        Waiting,
        Playing,
        Paused,
        Closed            
    }
}
