namespace WebPref.Core.Playing
{
    /// <summary>
    ///     Ход игрока
    /// </summary>
    public sealed class Move
    {
        #region Свойства

        /// <summary>
        ///     Игрок
        /// </summary>
        public string PlayerId { get; private set; }

        /// <summary>
        ///     Карта
        /// </summary>
        public Card Card { get; private set; }

        #endregion

        #region Конструктор

        /// <summary>
        ///     Конструктор
        /// </summary>
        public Move(string playerId, Card card)
        {
            PlayerId = playerId;
            Card = card;
        }

        #endregion
    }
}