using System.Collections.Generic;
using WebPref.Core.Playing;

namespace WebPref.Core.Calculations
{
    /// <summary> Расчетчик результатов игры </summary>
    public interface IResultsCalc
    {
        #region Методы
        
        /// <summary>
        ///     Инициализировать расчетчик
        /// </summary>
        /// <param name="playerIds">Уникальные ID игроков</param>
        /// <returns>Инициализация успешна</returns>
        bool Init(IList<string> playerIds);

        /// <summary>
        ///     Игрок сыграл игру
        /// </summary>
        /// <param name="playerId"> Уникальный ID игрока </param>
        /// <param name="game"> Сыгранная игра </param>
        void GameSuccess(string playerId, GameEnum game);

        /// <summary>
        ///     Игрок несыграл игру
        /// </summary>
        /// <param name="playerId"> Уникальный ID игрока </param>
        /// <param name="game"> Сыгранная игра </param>
        /// <param name="tricks"> Сколько взяток недобрано </param>
        void GameFail(string playerId, GameEnum game, int tricks);

        /// <summary> Записать висты за игру </summary>
        /// <param name="playerId"> Уникальный ID игрока </param>
        /// <param name="targetId"> Уникальный ID игрока, на которого пишутся висты </param>
        /// <param name="game"> Сыгранная игра </param>
        /// <param name="tricks"> Сколько взяток писать </param>
        void WhistSuccess(string playerId, string targetId, GameEnum game, int tricks);

        /// <summary> Записать висты за игру </summary>
        /// <param name="playerId"> Уникальный ID игрока </param>
        /// <param name="game"> Сыгранная игра </param>
        /// <param name="tricks"> Сколько взяток недобрано </param>
        void WhistFail(string playerId, GameEnum game, int tricks);

        /// <summary> Окончательный расчет </summary>
        /// <returns> Результаты </returns>
        IList<PlayerResults> Calculate();

        #endregion
    }
}