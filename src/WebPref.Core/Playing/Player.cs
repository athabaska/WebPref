using System;
using System.Collections.Generic;
using System.Text;
using WebPref.Core.Lobby;

namespace WebPref.Core.Playing
{
    public class Player
    {
        /// <summary>
        /// Класс представляет игрока, в принципе, особых свойств ему не надо, id пользователя и имя будет получать из web-части, должна накапливаться статистика где-то
        /// 
        /// </summary>
        public string Id { get; set; }
        public string Name { get; set; }        
        public List<TablePlayers> Tables { get; set; }


        public Player()
        {

        }

        public Player(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public override bool Equals(object obj)
        {
            Player player = obj as Player;
            if (player == null)
                return false;

            return (this.Id == player.Id && this.Name == player.Name);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public override string ToString()
        {
            return $"Игрок {Name}";
        }
    }
}