﻿#region usings

using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace WebPref.Core.Playing
{
    /// <summary>
    ///     Рука
    /// </summary>
    public sealed class Hand
    {
        private readonly HashSet<Card> currentCards;

        /// <summary>
        ///     Игрок
        /// </summary>
        public Player Owner { get; private set; }

        /// <summary>
        ///     Карты на руке сейчас
        /// </summary>
        public IList<Card> CurrentCards => currentCards.ToArray();

        /// <summary>
        ///     Конструктор
        /// </summary>
        public Hand(Player owner, IList<Card> cards)
        {
            currentCards = new HashSet<Card>(cards);
            Owner = owner;
        }

        public Player Holder { get; private set; }

        /// <summary>
        ///     Взять карту
        /// </summary>
        public void AddCard(Card c)
        {
            currentCards.Add(c);
        }

        /// <summary>
        ///     Сходить картой
        /// </summary>
        public void RemoveCard(Card c)
        {
            currentCards.Remove(c);
        }

        /// <summary>
        ///     Удалить все карты
        /// </summary>
        public void Clear()
        {
            currentCards.Clear();
        }

        /// <summary>
        ///     Сменить держателя руки
        /// </summary>
        public void UpdateOwner (Player owner)
        {
            Owner = owner;
        }

        /// <summary>
        ///     Содержит карту
        /// </summary>
        public bool Contains(Card c)
        {
            return currentCards.Contains(c);
        }

        public override string ToString()
        {
            var list = currentCards.OrderBy(c => c.Suit).ThenBy(c => c.Rank).ToList();
            
            var sb = new StringBuilder();
            for (var i = 0; i < list.Count; i++)
            {
                var c = list[i];
                if (i > 0 && list[i - 1].Suit != c.Suit)
                    sb.Append(" |");
                sb.Append(c);
                sb.Append("|");
            }

            return sb.ToString();
        }
    }
}