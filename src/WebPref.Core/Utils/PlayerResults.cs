#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace WebPref.Core.Utils
{
    /// <summary> Пуля, гора и висты игрока в одной игре </summary>
    public class PlayerResults
    {
        #region Члены

        private volatile int _gains;
        private volatile int _penalties;
        private readonly IDictionary<string, int> _whists;

        #endregion

        #region Свойства

        /// <summary> Уникальный ID игрока </summary>
        public string PlayerId { get; private set; }

        #endregion

        #region Конструктор

        public PlayerResults(string playerId, string otherPlayerId1, string otherPlayerId2)
            : this(playerId)
        {
            _whists[otherPlayerId1] = 0;
            _whists[otherPlayerId2] = 0;
        }

        public PlayerResults(string playerId, string otherPlayerId1, string otherPlayerId2, string otherPlayerId3)
            : this(playerId)
        {
            _whists[otherPlayerId1] = 0;
            _whists[otherPlayerId2] = 0;
            _whists[otherPlayerId3] = 0;
        }

        private PlayerResults(string playerId)
        {
            PlayerId = playerId;
            _gains = 0;
            _penalties = 0;
            _whists = new Dictionary<string, int>();
        }

        #endregion

        #region Методы

        /// <summary> Записать в пулю </summary>
        public void AddGains(int value)
        {
            _gains += value;
        }

        /// <summary> Записать в гору </summary>
        public void AddPenalties(int value)
        {
            _penalties += value;
        }

        /// <summary> Записать висты </summary>
        public void AddWhists(string otherPlayerId, int value)
        {
            lock (_whists)
            {
                _whists[otherPlayerId] = GetWhistsPrivate(otherPlayerId) + value;
            }
        }

        /// <summary> Записать висты на всех </summary>
        public void AddWhists(int value)
        {
            lock (_whists)
            {
                foreach (var id in _whists.Keys.ToArray())
                {
                    _whists[id] += value;
                }
            }
        }

        /// <summary> Получить пулю </summary>
        /// <returns> </returns>
        public int GetGains()
        {
            return _gains;
        }

        /// <summary> Получить гору </summary>
        /// <returns> </returns>
        public int GetPenalties()
        {
            return _penalties;
        }

        /// <summary> Получить висты </summary>
        public int GetWhists(string otherPlayerId)
        {
            lock (_whists)
            {
                var w = GetWhistsPrivate(otherPlayerId);
                return w;
            }
        }

        private int GetWhistsPrivate(string otherPlayerId)
        {
            int w;
            if (!_whists.TryGetValue(otherPlayerId, out w))
            {
                throw new ArgumentException("Cant whist on " + otherPlayerId);
            }
            return w;
        }

        public override string ToString()
        {
            lock (_whists)
            {
                var sb = new StringBuilder(string.Format("{0} {1} {2}", PlayerId, _gains, _penalties));
                foreach (var w in _whists)
                {
                    sb.Append(string.Format(" ->{0}:{1}", w.Key, w.Value));
                }

                return sb.ToString();
            }
        }

        #endregion
    }
}