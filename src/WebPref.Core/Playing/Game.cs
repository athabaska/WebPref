using System;
using System.Collections.Generic;
using System.Linq;

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

        private Round _currentRound;


        //public IEnumerator<Player> PlayersEnumerator => Players.GetEnumerator();

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
        public IList<Round> Rounds { get; }

        public Game(IList<Player> players, GameSettings settings)
        {
            Players = players;
            Settings = settings;
            Rounds = new List<Round>();
        }

        /// <summary>
        ///     Получить игрока по индексу
        /// </summary>        
        //public Player GetPlayer(int i)
        //{
        //    //todo проверку индекса
        //    return Players[i];
        //}

        public Round StartNextRound()
        {
            //раздать карты 
            _currentRound = new Round(Rounds.Count);
            Rounds.Add(_currentRound);
            return _currentRound;
        }

        /// <summary>
        ///     Обработать ход
        /// </summary>
        public bool Process(Move move)
        {
            //todo валидация хода

            _currentRound.Observe(move);
            foreach (var p in Players)
            {
                p.Observe(move);
            }
            return true;
        }

    }
}