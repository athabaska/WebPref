
#region usings

using System;
using WebPref.Core.Utils;

#endregion

namespace WebPref.Core.Playing
{
    /// <summary>
    ///     Ставка в торгах
    /// </summary>
    public sealed class Bid
    {
        /// <summary>
        ///     Игрок
        /// </summary>
        public Player Player { get; private set; }

        /// <summary>
        ///     Тип ставки
        /// </summary>
        public BidTypeEnum BidType { get; private set; }

        /// <summary>
        ///     Масть
        /// </summary>
        public SuitEnum Suit { get; private set; }

        /// <summary>
        ///     Игра
        /// </summary>
        public ContractEnum Contract { get; private set; }

        /// <summary>
        ///     Конструктор
        /// </summary>
        private Bid(Player player, BidTypeEnum bidType)
        {
            Player = player;
            BidType = bidType;
        }

        /// <summary>
        ///     Конструктор
        /// </summary>
        public Bid(Player player, BidTypeEnum bidType, ContractEnum contract, SuitEnum suit) : this(player, bidType)
        {
            if (bidType == BidTypeEnum.Pass)
                return;

            if (contract == ContractEnum.Miser && suit != SuitEnum.None)
                throw new ArgumentException($"Мизер с козырем {Suit}");            
            Suit = suit;
            Contract = contract;
        }

        /// <summary>
        ///     Пас
        /// </summary>
        public static Bid PassBid(Player player)
        {
            return new Bid(player, BidTypeEnum.Pass);
        }

        /// <summary>
        ///     Перебивает другую ставку
        /// </summary>
        public bool IsHigherThan(Bid other)
        {
            if (other.BidType == BidTypeEnum.Pass)
                return true;
            if (other.Contract > Contract)
                return false;
            if (other.Contract == Contract && other.Suit >= Suit)
                return false;
            return true;
        }

        public override string ToString()
        {
            if (BidType == BidTypeEnum.Pass)
                return $"{Player.Name} {BidType.GetDescription()}";
            else
            {
                if (Contract == ContractEnum.Miser)
                    return $"{Player.Name} {Contract.GetDescription()}";
                else
                    return $"{Player.Name} {Contract.GetDescription()} {Suit.GetDescription()} ";
            }
        }

        public override bool Equals(object obj)
        {
            Bid other = obj as Bid;
            if (other == null)
                return false;

            return other.Player == Player && other.BidType == BidType && other.Contract == Contract && other.Suit == Suit;
        }
    }
}