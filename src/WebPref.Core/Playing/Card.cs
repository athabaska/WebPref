/*
 * Author: oparinig
 * Date of creation: 2/20/2018 3:57:00 PM
 * Comments: Карта
 */

#region usings

using System.ComponentModel.DataAnnotations.Schema;
using WebPref.Core.Utils;

#endregion

namespace WebPref.Core.Playing
{
    /// <summary>
    ///     Карта
    /// </summary>
    [NotMapped]
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

        #region Колода

        public static Card SevenOfSpades => new Card(SuitEnum.Spades, RankEnum.Seven);
        public static Card EightOfSpades => new Card(SuitEnum.Spades, RankEnum.Eight);
        public static Card NineOfSpades => new Card(SuitEnum.Spades, RankEnum.Nine);
        public static Card TenOfSpades => new Card(SuitEnum.Spades, RankEnum.Ten);
        public static Card JackOfSpades => new Card(SuitEnum.Spades, RankEnum.Jack);
        public static Card QuennOfSpades => new Card(SuitEnum.Spades, RankEnum.Queen);
        public static Card KingOfSpades => new Card(SuitEnum.Spades, RankEnum.King);
        public static Card AceOfSpades => new Card(SuitEnum.Spades, RankEnum.Ace);

        public static Card SevenOfClubs => new Card(SuitEnum.Clubs, RankEnum.Seven);
        public static Card EightOfClubs => new Card(SuitEnum.Clubs, RankEnum.Eight);
        public static Card NineOfClubs => new Card(SuitEnum.Clubs, RankEnum.Nine);
        public static Card TenOfClubs => new Card(SuitEnum.Clubs, RankEnum.Ten);
        public static Card JackOfClubs => new Card(SuitEnum.Clubs, RankEnum.Jack);
        public static Card QuennOfClubs => new Card(SuitEnum.Clubs, RankEnum.Queen);
        public static Card KingOfClubs => new Card(SuitEnum.Clubs, RankEnum.King);
        public static Card AceOfClubs => new Card(SuitEnum.Clubs, RankEnum.Ace);

        public static Card SevenOfDiamonds => new Card(SuitEnum.Diamonds, RankEnum.Seven);
        public static Card EightOfDiamonds => new Card(SuitEnum.Diamonds, RankEnum.Eight);
        public static Card NineOfDiamonds => new Card(SuitEnum.Diamonds, RankEnum.Nine);
        public static Card TenOfDiamonds => new Card(SuitEnum.Diamonds, RankEnum.Ten);
        public static Card JackOfDiamonds => new Card(SuitEnum.Diamonds, RankEnum.Jack);
        public static Card QuennOfDiamonds => new Card(SuitEnum.Diamonds, RankEnum.Queen);
        public static Card KingOfDiamonds => new Card(SuitEnum.Diamonds, RankEnum.King);
        public static Card AceOfDiamonds => new Card(SuitEnum.Diamonds, RankEnum.Ace);

        public static Card SevenOfHearts => new Card(SuitEnum.Hearts, RankEnum.Seven);
        public static Card EightOfHearts => new Card(SuitEnum.Hearts, RankEnum.Eight);
        public static Card NineOfHearts => new Card(SuitEnum.Hearts, RankEnum.Nine);
        public static Card TenOfHearts => new Card(SuitEnum.Hearts, RankEnum.Ten);
        public static Card JackOfHearts => new Card(SuitEnum.Hearts, RankEnum.Jack);
        public static Card QuennOfHearts => new Card(SuitEnum.Hearts, RankEnum.Queen);
        public static Card KingOfHearts => new Card(SuitEnum.Hearts, RankEnum.King);
        public static Card AceOfHearts => new Card(SuitEnum.Hearts, RankEnum.Ace);

        #endregion
    }
}