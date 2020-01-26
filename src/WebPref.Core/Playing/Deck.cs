
using System;
using System.Collections.Generic;
using System.Linq;


namespace WebPref.Core.Playing
{
    /// <summary>
    ///     Колода
    /// </summary>
    public sealed class Deck
    {
        private HashSet<Card> cards;

        /// <summary>
        ///     Количество карт в колоде
        /// </summary>
        public int Count => cards.Count;

        /// <summary>
        ///     Доступные карты
        /// </summary>
        public IList<Card> AvailableCars => cards.ToList();

        public Deck()
        {
            cards = new HashSet<Card>();

            var suits = Utils.Utils.Suits;
            var ranks = (RankEnum[])Enum.GetValues(typeof(RankEnum));

            foreach (var s in suits)
            {
                foreach (var r in ranks)
                {
                    cards.Add(new Card(s, r));
                }
            }
        }

        /// <summary>
        ///     Очистить колоду
        /// </summary>
        public void Clear()
        {
            cards.Clear();
        }

        /// <summary>
        ///     Перемешать
        /// </summary>
        public void Shuffle()
        {
            var cards = this.cards.ToList();

            var rnd = new Random();
            for (var i = 0; i < 10; i++)
            {
                for (var j = 0; j < cards.Count; j++)
                {
                    var newPosition = rnd.Next(0, cards.Count);
                    var c = cards[j];
                    cards.RemoveAt(j);
                    cards.Insert(newPosition, c);
                }
            }

            this.cards = new HashSet<Card>(cards);
        }

        /// <summary>
        ///     Вытащить первую карту
        /// </summary>
        public Card TakeFirst()
        {
            if (cards.Count == 0)
                throw new IndexOutOfRangeException("Нет больше карт в колоде");

            var c = cards.First();
            cards.Remove(c);
            return c;
        }

        /// <summary>
        ///     Добавить карту
        /// </summary>
        public void AddCard(Card card)
        {
            cards.Add(card);
        }

    }
}