using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebPref.Core.Playing
{
    /// <summary>
    ///     Розыгрыш раздачи
    /// </summary>
    [NotMapped]
    public class Deal
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
            //проверитЬ ,что ход круг закончен
            currentRound.Add(move);
        }
    }
}
