/*
 * Author: oparinig
 * Date of creation: 2/20/2018 3:57:00 PM
 * Comments: Карта
 */

#region usings

using WebPref.Core.Utils;

#endregion

namespace WebPref.Core.Playing
{
    /// <summary>
    ///     Карта
    /// </summary>
    public sealed class Card
    {
        #region Свойства

        /// <summary>
        ///     Масть
        /// </summary>
        public SuitEnum Suit { get; }

        /// <summary>
        ///     Достоинство
        /// </summary>
        public RankEnum Rank { get; }

        #endregion

        #region Конструктор

        /// <summary>
        ///     Конструктор
        /// </summary>
        public Card(SuitEnum s, RankEnum r)
        {
            Suit = s;
            Rank = r;
        }

        #endregion

        #region Методы

        public override bool Equals(object obj)
        {
            var c = obj as Card;
            if (c == null)
                return false;
            return Suit == c.Suit && Rank == c.Rank;
        }

        public override int GetHashCode()
        {
            return (int)Rank * 10 + (int)Suit;
        }

        public override string ToString()
        {
            return Rank.GetDescription() + Suit.GetDescription();
        }

        #endregion
    }
}