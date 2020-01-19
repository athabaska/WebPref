using System;
using System.Collections.Generic;

namespace WebPref.Core.Playing
{
    /// <summary>
    ///     Игра 
    /// </summary>
    public class Game
    {
        /// <summary>
        ///     Надо посмотреть, как обеспечивать постоянный порядок следования игроков
        ///     Можно вытащить итератор и метод для получения игрока по индексу
        /// </summary>
        private IList<Player> Players { get; }


        public IEnumerator<Player> PlayersEnumerator => Players.GetEnumerator();

        /// <summary>
        ///     Параметы и конвенции
        /// </summary>
        public GameSettings Settings { get; private set; }

        /// <summary>
        ///     Состояние игры
        /// </summary>
        public GameState State { get; private set; }
        
                
        /// <summary>
        /// Тут тоже желательно подумать, по идее последовательность розыгрышей не должна изменяться
        /// Может быть, надо скрыть список и показать только методы для запуска и просмотра
        /// </summary>
        public IList<Deal> Deals { get; }

        public Game(IList<Player> players, GameSettings settings)
        {
            Players = players;
            Settings = settings;            
        }

        /// <summary>
        ///     Получить игрока по индексу
        /// </summary>        
        public Player GetPlayer(int i)
        {
            //todo проверку индекса
            return Players[i];
        }
        
    }

}
