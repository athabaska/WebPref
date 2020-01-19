using System;
using System.Collections.Generic;
using System.Text;

namespace WebPref.Core.Playing
{
    /// <summary>
    /// Игроки существуют не сами по себе, а являются производными от пользователей, в БД авторизации лезть пока не хочется, поэтому
    /// получается некоторое дублирование информации. 
    /// Предполагается, что игрок создается, когда пользователь впервые участвует в игре, а потом используется готовый.
    /// В дальнейшем будет накапливаться статистика, пока не определено, как точно будет выглядеть.
    /// Соответственно, данный класс используется для управления игроками там, где не виден прямо контекст данных.
    /// </summary>
    public class PlayerManager
    {
        private PrefContext PrefContext;
        internal PlayerManager(PrefContext prefContext)
        {
            PrefContext = prefContext;
        }

        public Player GetCreatePlayer(string id, string name)
        {
            Player player = PrefContext.Players.Find(new object[] { id });
            if (player == null)
            {
                player = new Player();
                player.Id = id;
                player.Name = name;
                PrefContext.Players.Add(player);
                PrefContext.SaveChanges();
            }
            return player;
        }

        public Player GetPlayer(string id)
        {
            return PrefContext.Players.Find(new object[] { id });
        }

        public bool DeletePlayer(string id)
        {
            try
            {
                Player player = PrefContext.Players.Find(new object[] { id });
                if (player != null)
                {
                    PrefContext.Players.Remove(player);
                    PrefContext.SaveChanges();               
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
