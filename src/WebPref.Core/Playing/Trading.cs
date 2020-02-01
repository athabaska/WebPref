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

        private readonly ISet<Player> passed;

        private IDictionary<Player, Bid> lastBids;

        private ContractEnum minContract;
        
        /// <summary>
        ///     Торги окончены
        /// </summary>
        public bool IsFinished { get; private set; }

        /// <summary>
        ///     Высший текущий бид
        /// </summary>
        public Bid Highest { get; private set; }

        /// <summary>
        ///     Конструктор
        /// </summary>
        /// <param name="players">Игроки</param>

        public Trading(IList<Player> players)
        {
            IsFinished = false;
            lastBids = new Dictionary<Player, Bid>();
            passed = new HashSet<Player>();
            this.players = players;            
        }

        /// <summary>
        ///     Начать торги
        /// </summary>
        /// <param name="first">Первое слово</param>
        /// <param name="minContract">Минимальная игра</param>
        public void Start(Player first, ContractEnum minContract)
        {
            lastBids.Clear();
            passed.Clear();
            Console.WriteLine($"Первый кричит {first}, мин игра {minContract.GetDescription()}");
            this.currentPlayer = first;
            this.minContract = minContract;
        }

        /// <summary>
        ///     Получить ставку
        /// </summary>
        public bool CheckBid(Bid bid)
        {
            Console.WriteLine(bid);
            //валидации
            if (passed.Contains(bid.Player))
            {
                Console.WriteLine("Игрок уже пасанул ранее");
                return false;
            }
            if (bid.Player != currentPlayer)
            {
                Console.WriteLine("Ставка не в очередь");
                return false;
            }
            if (bid.BidType == BidTypeEnum.Pass)
            {
                lastBids[bid.Player] = bid;
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

            if (bid.Contract == ContractEnum.Miser)
            {
                //todo кабальный
            }

            Bid lastBid;
            if (lastBids.TryGetValue(bid.Player, out lastBid) && !bid.IsHigherThan(lastBid))
            {
                Console.WriteLine($"Ставку можно только повышать");
                return false;
            }

            if (bid.Contract < minContract)
            {
                Console.WriteLine($"Минимальная игра {minContract}");
                return false;
            }

            if (Highest != null && !bid.IsHigherThan(Highest))
            {
                Console.WriteLine($"Надо ставить выше текущей {Highest}");
                return false;
            }

            lastBids[bid.Player] = bid;
            Highest = bid;
            MoveCurrent();
            return true;
        }

        private void Finish(Bid winner)
        {
            Console.WriteLine("Победил " + Highest);
            IsFinished = true;
            TradingFinished?.Invoke(this, winner);            
        }

        private void AllPassed()
        {
            Console.WriteLine("Все пасанули");
            IsFinished = true;
            TradingFinished?.Invoke(this, null);
        }

        private void MoveCurrent()
        {
            var cur = players.IndexOf(currentPlayer);
            for (var i = cur + 1; i < players.Count; i++)
            {
                var p = players[i];
                if (!passed.Contains(p))
                {
                    currentPlayer = p;
                    Console.WriteLine($"Следующий голос {p}");
                    return;
                }
            }

            for (var i = 0; i < cur; i++)
            {
                var p = players[i];
                if (!passed.Contains(p))
                {
                    currentPlayer = p;
                    Console.WriteLine($"Следующий голос {p}");
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
