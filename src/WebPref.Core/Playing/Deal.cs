using System;
using System.Collections.Generic;
using System.Text;
using WebPref.Core.Interfaces;

namespace WebPref.Core.Playing
{
    /// <summary>
    ///     Розыгрыш раздачи
    /// </summary>
    public class Deal : IGameObserver
    {
        private Round currentRound;

        /// <summary>
        ///     Номер раздачи
        /// </summary>
        public int Number { get; }

        /// <summary>
        ///     Круги
        /// </summary>
        public IList<Round> Rounds { get; }

        public Deal(int number)
        {
            Number = number;
            Rounds = new List<Round>();
            currentRound = new Round();
            Rounds.Add(currentRound);
        }

        public void Observe(Move move)
        {
            //todo обработать ход
            
            currentRound.Observe(move);

        }
    }
}
