using System;
using System.Collections.Generic;
using System.Text;

namespace WebPref.Core.Playing
{
    /// <summary>
    ///     Параметры и конвенции игры 
    /// </summary>
    public class GameSettings
    {
        /// <summary>
        ///     Количество игроков
        /// </summary>
        public PlayersCountEnum PlayersCount { get; private set; }

        /// <summary>
        ///     Тип игры
        /// </summary>
        public GameTypeEnum GameType { get; private set; }

        /// <summary>
        ///     Конструктор с неизменяемыми параметрами
        /// </summary>
        public GameSettings(PlayersCountEnum playersCount, GameTypeEnum gameType)
        {
            PlayersCount = playersCount;
            GameType = gameType;
        }

    }
}
