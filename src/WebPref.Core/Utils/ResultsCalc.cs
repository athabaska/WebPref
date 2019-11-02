#region Usings

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#endregion

namespace WebPref.Core.Utils
{
    /// <summary> Результаты одной игры на 3х игроков </summary>
    public class ResultsCalc : IResultsCalc
    {
        #region Константы

        private const int PenaltyCost = 3;

        #endregion

        #region Члены

        private readonly IDictionary<string, PlayerResults> _players;
        private readonly int _playersCount;

        #endregion

        #region Конструктор

        public ResultsCalc(string p1, string p2, string p3)
        {
            _players = new Dictionary<string, PlayerResults>();
            _players[p1] = new PlayerResults(p1, p2, p3);
            _players[p2] = new PlayerResults(p2, p1, p3);
            _players[p3] = new PlayerResults(p3, p1, p2);
            _playersCount = 3;
        }

        public ResultsCalc(string p1, string p2, string p3, string p4)
        {
            _players = new Dictionary<string, PlayerResults>();
            _players[p1] = new PlayerResults(p1, p2, p3, p4);
            _players[p2] = new PlayerResults(p2, p1, p3, p4);
            _players[p3] = new PlayerResults(p3, p1, p2, p4);
            _players[p3] = new PlayerResults(p4, p1, p2, p3);
            _playersCount = 4;
        }

        #endregion

        #region Методы

        public void Gain(string playerId, int value)
        {
            var player = GetPlayer(playerId);
            player.AddGains(value);
        }

        public void Penalty(string playerId, int value)
        {
            var player = GetPlayer(playerId);
            player.AddPenalties(value);
        }

        public void Whist(string playerId, string targetId, int value)
        {
            var player = GetPlayer(playerId);
            player.AddWhists(targetId, value);
        }

        private PlayerResults GetPlayer(string playerId)
        {
            PlayerResults p;
            if (!_players.TryGetValue(playerId, out p))
            {
                throw new ArgumentException("No player " + playerId);
            }
            return p;
        }

        public PlayerResults[] Calculate()
        {
            //гора минус пуля, минимальная гора
            var minPenalty = int.MaxValue;
            foreach (var currPlayer in _players.Values)
            {
                currPlayer.AddPenalties(-currPlayer.GetGains());
                minPenalty = Math.Min(minPenalty, currPlayer.GetPenalties());
            }

            //амнистия
            foreach (var currPlayer in _players.Values)
            {
                currPlayer.AddPenalties(-minPenalty);
                Trace.WriteLine(string.Format("Амнистия {0} {1}", currPlayer.PlayerId, currPlayer.GetPenalties()));
            }

            //окончательный расчет
            foreach (var currPlayer in _players.Values)
            {
                var p = currPlayer.GetPenalties();
                if (p == 0)
                    continue;

                //гора должна быть кратна количеству игроков
                var incr = 0;
                int rem;
                Math.DivRem(p, _playersCount, out rem);
                while (rem != 0)
                {
                    incr++;
                    Math.DivRem(p + incr, PenaltyCost, out rem);
                }
                p = (p + incr)*10;
                currPlayer.AddWhists(incr*PenaltyCost);

                //записать остальным висты за гору
                foreach (var otherPlayer in _players.Values)
                {
                    if (otherPlayer.PlayerId != currPlayer.PlayerId)
                        otherPlayer.AddWhists(currPlayer.PlayerId, p/_playersCount);
                }                
            }

            //сократить висты
            foreach (var currPlayer in _players.Values)
            {
                foreach (var otherPlayer in _players.Values)
                {
                    if (currPlayer.PlayerId == otherPlayer.PlayerId)
                        continue;

                    var w1 = currPlayer.GetWhists(otherPlayer.PlayerId);
                    var w2 = otherPlayer.GetWhists(currPlayer.PlayerId);
                    var min = Math.Min(w1, w2);
                    currPlayer.AddWhists(otherPlayer.PlayerId, -min);
                    otherPlayer.AddWhists(currPlayer.PlayerId, -min);
                }
            }

            return _players.Values.ToArray();
        }

        #endregion
    }
}