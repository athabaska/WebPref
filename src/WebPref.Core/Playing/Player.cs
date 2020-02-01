using System;
using System.Collections.Generic;
using System.Text;
using WebPref.Core.Interfaces;
using WebPref.Core.Lobby;

namespace WebPref.Core.Playing
{
    
    /// <summary>
    /// Класс представляет игрока, в принципе, особых свойств ему не надо, id пользователя и имя будет получать из web-части, должна накапливаться статистика где-то
    public class Player
    {/// 
     /// </summary>
        public string Id { get; set; }
        public string Name { get; set; }        

        public List<TablePlayers> Tables { get; set; }



        public override bool Equals(object obj)
        {
            Player player = obj as Player;
            if (player == null)
                return false;

            return (this.Id == player.Id && this.Name == player.Name);
        }
    }
}
