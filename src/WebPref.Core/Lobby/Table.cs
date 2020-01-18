using System;
using System.Collections.Generic;
using System.Linq;
using WebPref.Core.Playing;

namespace WebPref.Core.Lobby
{
    /// <summary>
    ///     Стол с игроками
    /// </summary>
    public class Table
    {
        private readonly TableSettings _settings;

        private readonly IList<Player> _players;

        /// <summary>
        ///     Уникальный ID
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        ///     Номер стола (для отражения на ГУИ)
        /// </summary>
        public int Number { get; private set; }

        public Table(Player owner, TableSettings settings, int number)
        {
            Id = Guid.NewGuid();
            _settings = settings;
            Number = number;
            _players = new List<Player>();
        }

        /// <summary>
        ///     Получить список игроков
        /// </summary>
        public IList<Player> GetPlayers()
        {
            return _players.ToArray();
        }
    }
}
