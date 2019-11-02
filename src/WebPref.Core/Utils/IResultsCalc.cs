namespace WebPref.Core.Utils
{
    /// <summary> Результаты одной игры </summary>
    public interface IResultsCalc
    {
        #region Методы

        /// <summary> Записать в пулю </summary>
        /// <param name="playerId"> Уникальный ID игрока </param>
        /// <param name="value"> Сколько писать </param>
        void Gain(string playerId, int value);

        /// <summary> Записать в гору </summary>
        /// <param name="playerId"> Уникальный ID игрока </param>
        /// <param name="value"> Сколько писать </param>
        void Penalty(string playerId, int value);

        /// <summary> Записать висты </summary>
        /// <param name="playerId"> Уникальный ID игрока </param>
        /// <param name="targetId"> Уникальный ID игрока, на которого пишутся висты </param>
        /// <param name="value"> Сколько писать </param>
        void Whist(string playerId, string targetId, int value);

        /// <summary> Окончательный расчет </summary>
        /// <returns> Результаты </returns>
        PlayerResults[] Calculate();

        #endregion
    }
}