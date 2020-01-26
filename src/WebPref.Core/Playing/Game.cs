using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using WebPref.Core.Interfaces;

namespace WebPref.Core.Playing
{
    /// <summary>
    ///     Игра 
    /// </summary>
    [NotMapped]
    public class Game : IGameObserver
    {
        private Deal currentDeal;

        private IList<Player> players;

        /// <summary>
        ///     Надо посмотреть, как обеспечивать постоянный порядок следования игроков
        ///     Можно вытащить итератор и метод для получения игрока по индексу
        /// </summary>
        private IList<Hand> Hands { get; }

        
        //public IEnumerator<Player> PlayersEnumerator => Players.GetEnumerator();

        /// <summary>
        ///     Параметы и конвенции
        /// </summary>
        public GameSettings Settings { get; private set; }

        /// <summary>
        ///     Состояние игры
        /// </summary>
        public GameState State { get; private set; }

        /// <summary>
        ///     Раздачи
        /// </summary>
        public IList<Deal> Deals { get; }

        public Game(IList<Player> players, GameSettings settings)
        {
            this.players = players;
            Settings = settings;
            Deals = new List<Deal>();
            Hands = new List<Hand>();            
        }

        /// <summary>
        ///     Получить игрока по индексу
        /// </summary>        
        //public Player GetPlayer(int i)
        //{
        //    //todo проверку индекса
        //    return Players[i];
        //}

        public Deal StartNewDeal()
        {
            Hands.Clear();

            //раздать карты 
            var deck = new Deck();
            deck.Shuffle();
            var p1 = new List<Card>();
            var p2 = new List<Card>();
            var p3 = new List<Card>();
            var buy = new List<Card>();
            
            while (deck.Count > 0)
            {
                p1.Add(deck.TakeFirst());
                p2.Add(deck.TakeFirst());
                p3.Add(deck.TakeFirst());
                if (buy.Count < 2)
                    buy.Add(deck.TakeFirst());                                
            }

            //todo тут всё переделать, и насчет прикупа придумать
            //для каждого игрока - новая рука
            foreach (var p in players)
            {
                Hands.Add(new Hand(p, p1));
                Hands.Add(new Hand(p, p2));
                Hands.Add(new Hand(p, p3));
            }
            
            currentDeal = new Deal(Deals.Count);
            Deals.Add(currentDeal);
            return currentDeal;
        }

        /// <summary>
        ///     Обработать ход
        /// </summary>
        public void Observe(Move move)
        {
            //todo валидация хода
            currentDeal.Observe(move);
        }
    }
}