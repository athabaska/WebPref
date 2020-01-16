using System;
using System.Collections.Generic;
using System.Text;

namespace WebPref.Core
{
    /// <summary>
    ///     Партия, 
    /// </summary>
    internal class Party
    {
        public PartyState State { get; private set; }
        /// <summary>
        /// Надо посмотреть, как обеспечивать постоянный порядок следования игроков
        /// </summary>
        public List<Player> Players { get; }
        /// <summary>
        /// Тут тоже желательно подумать, по идее последовательность розыгрышей не должна изменяться
        /// Может быть, надо скрыть список и показать только методы для запуска и просмотра        /// 
        /// </summary>
        public List<Game> Games { get; }


    }

    internal enum PartyState
    {
        Configuring,
        Bidding,
        Playing,
        Ended,
        Defered
    }
}
