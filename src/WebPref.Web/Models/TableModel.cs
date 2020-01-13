using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPref.Web.Models
{
    public class TableModel
    {
        public string Name { get; set; }
        public TableState State { get; set; } 
        public int PlayerCount { get; set; }
    }

    public enum TableState
    {
        Waiting,
        Playing,
        Paused,
        Closed            
    }
}
