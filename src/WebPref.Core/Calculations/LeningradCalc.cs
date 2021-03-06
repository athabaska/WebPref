﻿#region Usings

using System;
using System.Collections.Generic;
using WebPref.Core.Playing;

#endregion

namespace WebPref.Core.Calculations
{
    /// <summary> Расчетчик результатов игры </summary>
    public class LeningradCalc : IResultsCalc
    {
        #region Константы

        private const int PenaltyCost = 3;

        #endregion

        #region Члены

        /// <summary>
        ///     Список игроков
        /// </summary>
        private readonly IDictionary<string, PlayerResults> _players;
        
        /// <summary>
        ///     Цена успешной игры
        /// </summary>
        private readonly IDictionary<ContractEnum, int> _successPrice;

        /// <summary>
        ///     Цена несыгранной игры
        /// </summary>
        private readonly IDictionary<ContractEnum, int> _failPrice;

        /// <summary>
        ///     Цена успешного виста
        /// </summary>
        private readonly IDictionary<ContractEnum, int> _successWhistPrice;

        /// <summary>
        ///     Цена несыгранного виста
        /// </summary>
        private readonly IDictionary<ContractEnum, int> _failWhistPrice;

        private int _playersCount;

        #endregion

        #region Конструктор

        public LeningradCalc()
        {
            _players = new Dictionary<string, PlayerResults>();
            _successPrice = new Dictionary<ContractEnum, int>();
            _failPrice = new Dictionary<ContractEnum, int>();
            _successWhistPrice = new Dictionary<ContractEnum, int>();
            _failWhistPrice = new Dictionary<ContractEnum, int>();

            _successPrice[ContractEnum.Six] = 2;
            _failPrice[ContractEnum.Six] = 4;
            _successWhistPrice[ContractEnum.Six] = 4;
            _failWhistPrice[ContractEnum.Six] = 2;

            _successPrice[ContractEnum.Seven] = 4;
            _failPrice[ContractEnum.Seven] = 8;
            _successWhistPrice[ContractEnum.Seven] = 8;
            _failWhistPrice[ContractEnum.Seven] = 4;

            _successPrice[ContractEnum.Eight] = 6;
            _failPrice[ContractEnum.Eight] = 12;
            _successWhistPrice[ContractEnum.Eight] = 6;
            _failWhistPrice[ContractEnum.Eight] = 12;

            _successPrice[ContractEnum.Nine] = 8;
            _failPrice[ContractEnum.Nine] = 16;
            _successWhistPrice[ContractEnum.Nine] = 16;
            _failWhistPrice[ContractEnum.Nine] = 8;

            _successPrice[ContractEnum.Ten] = 10;
            _failPrice[ContractEnum.Ten] = 20;
            _successWhistPrice[ContractEnum.Ten] = 20;
            _failWhistPrice[ContractEnum.Ten] = 10;

            _successPrice[ContractEnum.Miser] = 10;
            _failPrice[ContractEnum.Miser] = 20;
            _successWhistPrice[ContractEnum.Miser] = 0;
            _failWhistPrice[ContractEnum.Miser] = 0;
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

        public void GameSuccess(string playerId, ContractEnum game)
        {
            var player = GetPlayer(playerId);
            GetPlayer(playerId);
            player.AddBullet(_successPrice[game]);
        }

        public void WhistSuccess(string playerId, string targetId, ContractEnum game, int tricks)
        {
            var player = GetPlayer(playerId);
            GetPlayer(targetId);
            player.AddWhists(targetId, _successWhistPrice[game]);
        }

        public void GameFail(string playerId, ContractEnum game, int tricks)
        {
            var player = GetPlayer(playerId);
            GetPlayer(playerId);
            player.AddMountain(_failPrice[game]);
        }

        public void WhistFail(string playerId, ContractEnum game, int tricks)
        {
            var player = GetPlayer(playerId);
            GetPlayer(playerId);
            player.AddMountain(_failWhistPrice[game]);
        }

        public IList<PlayerResults> Calculate()
        {
            throw new NotImplementedException();
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

        //public IList<PlayerResults> Calculate()
        //{
        //    //создать клоны объектов
        //    var players = new List<PlayerResults>();
        //    foreach (var p in _players.Values)
        //    {
        //        players.Add((PlayerResults)p.Clone());
        //    }

        //    //гора минус пуля, минимальная гора
        //    var minPenalty = int.MaxValue;
        //    foreach (var currPlayer in players)
        //    {
        //        currPlayer.AddPenalties(-currPlayer.GetGains());
        //        minPenalty = Math.Min(minPenalty, currPlayer.GetPenalties());
        //    }

        //    //амнистия
        //    foreach (var currPlayer in players)
        //    {
        //        currPlayer.AddPenalties(-minPenalty);
        //        Trace.WriteLine(string.Format("Амнистия {0} {1}", currPlayer.PlayerId, currPlayer.GetPenalties()));
        //    }

        //    //окончательный расчет
        //    foreach (var currPlayer in players)
        //    {
        //        var p = currPlayer.GetPenalties();
        //        if (p == 0)
        //            continue;

        //        //гора должна быть кратна количеству игроков
        //        var incr = 0;
        //        int rem;
        //        Math.DivRem(p, _playersCount, out rem);
        //        while (rem != 0)
        //        {
        //            incr++;
        //            Math.DivRem(p + incr, PenaltyCost, out rem);
        //        }
        //        p = (p + incr)*10;
        //        currPlayer.AddWhists(incr*PenaltyCost);

        //        //записать остальным висты за гору
        //        foreach (var otherPlayer in players)
        //        {
        //            if (otherPlayer.PlayerId != currPlayer.PlayerId)
        //                otherPlayer.AddWhists(currPlayer.PlayerId, p/_playersCount);
        //        }                
        //    }

        //    //сократить висты
        //    foreach (var currPlayer in players)
        //    {
        //        foreach (var otherPlayer in players)
        //        {
        //            if (currPlayer.PlayerId == otherPlayer.PlayerId)
        //                continue;

        //            var w1 = currPlayer.GetWhists(otherPlayer.PlayerId);
        //            var w2 = otherPlayer.GetWhists(currPlayer.PlayerId);
        //            var min = Math.Min(w1, w2);
        //            currPlayer.AddWhists(otherPlayer.PlayerId, -min);
        //            otherPlayer.AddWhists(currPlayer.PlayerId, -min);
        //        }
        //    }

        //    return players;
        //}

        #endregion
    }
}