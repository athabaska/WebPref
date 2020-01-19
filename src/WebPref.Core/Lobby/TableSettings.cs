using WebPref.Core.Playing;

namespace WebPref.Core.Lobby
{
    /// <summary>
    ///     Настройки стола
    /// </summary>
    public class TableSettings
    {
        public TableSettings(PlayersCountEnum playersCount, GameTypeEnum gameType, bool isPrivate)
        {
            this.PlayersCount = playersCount;
            this.GameType = gameType;
            this.IsPrivate = isPrivate;
        }
        /// <summary>
        ///     Создатель стола выбирает, кто может присоединиться
        /// </summary>
        public bool IsPrivate { get; private set; }

        /// <summary>
        ///     Количество игроков
        /// </summary>
        public PlayersCountEnum PlayersCount { get; private set; }

        /// <summary>
        ///     Тип игры
        /// </summary>
        public GameTypeEnum GameType { get; private set; }
    }
}