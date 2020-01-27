using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using WebPref.Core.Utils;

namespace WebPref.Core.Playing
{
    /// <summary>
    ///     Торговля
    /// </summary>
    public class Trading
    {
        private readonly IList<Player> players;

        private Player currentPlayer;

        private readonly HashSet<Player> passed;

        private ContractEnum minContract;

        /// <summary>
        ///     Высший текущий бид
        /// </summary>
        public Bid Highest { get; private set; }

        /// <summary>
        ///     Конструктор
        /// </summary>
        /// <param name="players">Игроки</param>
        /// <param name="first">Первое слово</param>
        /// <param name="minContract">Минимальная игра</param>
        public Trading(IList<Player> players, Player first, ContractEnum minContract)
        {
            Trace.WriteLine($"Первый ходит № {first}, мин игра {minContract.GetDescription()}");

            passed = new HashSet<Player>();
            this.players = players;
            this.currentPlayer = first;
            this.minContract = minContract;
        }

        /// <summary>
        ///     Получить ставку
        /// </summary>
        public bool CheckBid(Bid bid)
        {
            Trace.WriteLine(bid);
            //валидации
            if (passed.Contains(bid.Player))
            {
                Trace.WriteLine("Игрок уже пасанул ранее");
                return false;
            }
            if (bid.Player != currentPlayer)
            {
                Trace.WriteLine("Ставка не в очередь");
                return false;
            }
            if (bid.BidType == BidTypeEnum.Pass)
            {
                passed.Add(bid.Player);
                if (passed.Count == players.Count)
                {
                    AllPassed();
                }
                else
                {
                    if (Highest != null && passed.Count == players.Count - 1)
                        Finish(Highest);
                    else
                        MoveCurrent();
                }
                return true;
            }

            if (bid.Contract <= minContract)
            {
                Trace.WriteLine($"Минимальная игра {minContract}");
                return false;
            }

            if (Highest == null || bid.IsHigherThan(Highest))
            {
                Highest = bid;
                MoveCurrent();
                return true;
            }

            return false;
        }

        
        private void Finish(Bid winner)
        {
            Trace.WriteLine("Победил " + Highest);
            TradingFinished?.Invoke(this, winner);
        }

        private void AllPassed()
        {
            Trace.WriteLine("Все пасанули");
            TradingFinished?.Invoke(this, null);
        }

        private void MoveCurrent()
        {
            var cur = players.IndexOf(currentPlayer);
            for (var i = cur; i <= players.Count; i++)
            {
                if (!passed.Contains(players[i]))
                {
                    Trace.WriteLine($"Следующий голос {players[i]}");
                    return;
                }
            }

            for (var i = 0; i < cur; i++)
            {
                if (!passed.Contains(players[i]))
                {
                    Trace.WriteLine($"Следующий голос {players[i]}");
                    return;
                }
            }
        }

        /// <summary>
        ///     Закончены торги
        /// </summary>
        public event EventHandler<Bid> TradingFinished;
    }
}
