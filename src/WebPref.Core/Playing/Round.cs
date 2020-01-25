#region usings

using System.Collections.Generic;
using WebPref.Core.Interfaces;

#endregion

namespace WebPref.Core.Playing
{
    /// <summary>
    ///     Розыгрыш 
    /// </summary>
    public sealed class Round : IGameObserver
    {
        #region Свойства

        /// <summary>
        ///     Номер розыгрыша
        /// </summary>
        public int Number { get; }

        /// <summary>
        ///     Ходы
        /// </summary>
        public IList<Move> Moves { get; }

        #endregion

        #region Конструктор

        /// <summary>
        ///     Конструктор
        /// </summary>
        /// <param name="number">Номер хода</param>
        public Round(int number)
        {
            Number = number;
            Moves = new List<Move>();
        }

        #endregion

        #region Методы

        /// <summary>
        ///     Обработать ход
        /// </summary>
        public void Observe(Move move)
        {
            Moves.Add(move);
        }        

        #endregion
    }
}