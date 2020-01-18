using System;
using System.Collections.Generic;

namespace WebPref.Core.Playing
{
    /// <summary>
    ///     Игра 
    /// </summary>
    internal class Game
    {
        public GameState State { get; private set; }
        
        /// <summary>
        /// Надо посмотреть, как обеспечивать постоянный порядок следования игроков
        /// </summary>
        public List<Player> Players { get; }
        
        /// <summary>
        /// Тут тоже желательно подумать, по идее последовательность розыгрышей не должна изменяться
        /// Может быть, надо скрыть список и показать только методы для запуска и просмотра
        /// </summary>
        public List<Deal> Deals { get; }


    }

}
