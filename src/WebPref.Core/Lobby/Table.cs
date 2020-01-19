using System;
using System.Collections.Generic;
using System.Linq;
using WebPref.Core.Playing;

namespace WebPref.Core.Lobby
{
    /// <summary>
    ///     Стол с игроками
    /// </summary>
    public class Table
    {
        private readonly TableSettings _settings;


        public List<TablePlayers> TablePlayers { get; set; }        

        /// <summary>
        ///     Уникальный ID
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        ///     Номер стола (для отражения на ГУИ)
        /// </summary>
        public int Number { get; private set; }

        public Table()
        {
            TablePlayers = new List<TablePlayers>();
        }

        /// <summary>
        ///     Текущая игра
        /// </summary>
        public Game CurrentGame { get; private set; }

        public Table(Player owner, TableSettings settings, int number)
        {
            Id = Guid.NewGuid();
            _settings = settings;
            Number = number;
            TablePlayers = new List<TablePlayers>();
            TablePlayers tablePlayers = new TablePlayers();
            tablePlayers.PlayerId = owner.Id;
            tablePlayers.Player = owner;
            tablePlayers.TableId = Id;
            tablePlayers.Table = this;
            TablePlayers.Add(tablePlayers);
        }

        /// <summary>
        ///     Получить список игроков
        /// </summary>
        public IList<Player> GetPlayers()
        {
            return TablePlayers.Select(tp => tp.Player).ToArray();            
        }

        public bool AddPlayer(Player player)
        {            
            if (this.TablePlayers.Count == (int)this._settings.PlayersCount)
                return false;
            if (this.TablePlayers.Find(tp => tp.Player == player) != null)
                return false;
                        
            this.TablePlayers.Add(new TablePlayers() { PlayerId = player.Id, Player = player, TableId = Id, Table = this });
            return true;
        }

        /// <summary>
        ///     Начать игру
        /// </summary>
        /// <returns>Получилось</returns>
        public bool StartGame()
        {
            //todo проверки на возможность начала игры
            CurrentGame = new Game(GetPlayers(), new GameSettings(_settings.PlayersCount, _settings.GameType));
            return true;
        }
    }
}
