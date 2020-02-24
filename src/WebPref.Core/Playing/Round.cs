#region usings

using System.Collections.Generic;

#endregion

namespace WebPref.Core.Playing
{
    /// <summary>
    ///     Круг
    ///     3(4) хода игроков до взятки
    /// </summary>
    public sealed class Round
    {
        #region Свойства

        /// <summary>
        ///     Ходы
        /// </summary>
        public IList<Move> Moves { get; }

        #endregion

        #region Конструктор

        /// <summary>
        ///     Конструктор
        /// </summary>
        public Round()
        {
            Moves = new List<Move>();
        }

        #endregion

        #region Методы

        /// <summary>
        ///     Обработать ход
        /// </summary>
        public void Add(Move move)
        {
            Moves.Add(move);
        }        

        #endregion
    }
}