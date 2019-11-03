#region Usings

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#endregion

namespace WebPref.Core.Utils
{
    /// <summary> Расчетчик результатов игры </summary>
    public class ResultsCalc : IResultsCalc
    {
        #region Константы

        private const int PenaltyCost = 3;

        #endregion

        #region Члены

        private readonly IDictionary<string, PlayerResults> _players;
        private int _playersCount;

        #endregion

        #region Конструктор

        public ResultsCalc()
        {
            _players = new Dictionary<string, PlayerResults>();

        }

        #endregion

        #region Методы

        public bool Init(IList<string> playerIds)
        {
            var hash = new HashSet<string>(playerIds);
            //уникальность ID
            if (hash.Count != playerIds.Count)
                return false;
            if (playerIds.Count == 3)
            {
                _players[playerIds[0]] = new PlayerResults(playerIds[0]);
                _players[playerIds[1]] = new PlayerResults(playerIds[1]);
                _players[playerIds[2]] = new PlayerResults(playerIds[2]);
                _playersCount = 3;
                return true;
            }
            else 
            if (playerIds.Count == 4)
            {
                _players[playerIds[0]] = new PlayerResults(playerIds[0]);
                _players[playerIds[1]] = new PlayerResults(playerIds[1]);
                _players[playerIds[2]] = new PlayerResults(playerIds[2]);
                _players[playerIds[3]] = new PlayerResults(playerIds[3]);
                _playersCount = 4;
                return true;
            }
            return false;
        }

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
            GetPlayer(targetId);
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

        public IList<PlayerResults> Calculate()
        {
            //создать клоны объектов
            var players = new List<PlayerResults>();
            foreach (var p in _players.Values)
            {
                players.Add((PlayerResults)p.Clone());
            }

            //гора минус пуля, минимальная гора
            var minPenalty = int.MaxValue;
            foreach (var currPlayer in players)
            {
                currPlayer.AddPenalties(-currPlayer.GetGains());
                minPenalty = Math.Min(minPenalty, currPlayer.GetPenalties());
            }

            //амнистия
            foreach (var currPlayer in players)
            {
                currPlayer.AddPenalties(-minPenalty);
                Trace.WriteLine(string.Format("Амнистия {0} {1}", currPlayer.PlayerId, currPlayer.GetPenalties()));
            }

            //окончательный расчет
            foreach (var currPlayer in players)
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
                foreach (var otherPlayer in players)
                {
                    if (otherPlayer.PlayerId != currPlayer.PlayerId)
                        otherPlayer.AddWhists(currPlayer.PlayerId, p/_playersCount);
                }                
            }

            //сократить висты
            foreach (var currPlayer in players)
            {
                foreach (var otherPlayer in players)
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

            return players;
        }

        #endregion
    }
}