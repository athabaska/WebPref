/*
 * Author: oparinig
 * Date of creation: 2/20/2018 4:03:17 PM
 * Comments: Енумы
 */

using System.ComponentModel;

namespace WebPref.Core.Playing
{
    /// <summary>
    ///     Масть
    /// </summary>
    public enum SuitEnum
    {
        [Description("\u2660")]
        Spades = 1,
        [Description("\u2663")]
        Clubs = 2,
        [Description("\u2666")]
        Diamonds = 3,
        [Description("\u2665")]
        Hearts = 4,
        [Description("БК")]
        None = 5
    }

    /// <summary>
    ///     Достоинство
    /// </summary>
    public enum RankEnum
    {
        [Description("7")]
        Seven = 7,
        [Description("8")]
        Eight = 8,
        [Description("9")]
        Nine = 9,
        [Description("10")]
        Ten = 10,
        [Description("В")]
        Jack = 20,
        [Description("Д")]
        Queen = 21,
        [Description("К")]
        King = 22,
        [Description("Т")]
        Ace = 30
    }

    /// <summary>
    ///     Заказанная игра
    /// </summary>
    public enum ContractEnum
    {
        [Description("6")]
        Six = 6,
        [Description("7")]
        Seven = 7,
        [Description("8")]
        Eight = 8,
        [Description("9")]
        Nine = 10,
        [Description("10")]
        Ten = 11,
        [Description("Мизер")]
        Miser = 9
    }

    /// <summary>
    ///     Игра по количеству игроков
    /// </summary>
    public enum PlayersCountEnum
    {
        Three = 3,
        Four = 4
    }

    /// <summary>
    ///     Тип игры
    /// </summary>
    public enum GameTypeEnum
    {
        Leningrad = 0,
        Rostov = 1,
        Sochi = 2,
        Hybrid = 3
    }

    /// <summary>
    ///     Заявка игра/пас
    /// </summary>
    public enum BidTypeEnum
    {
        [Description("Пас")]
        Pass,

        [Description("Игра")]
        Play
    }

    /// <summary>
    ///     Заявка вист/пас
    /// </summary>
    public enum DefendTypeEnum
    {
        [Description("Вист")]
        Whist,

        [Description("Пас")]
        Pass,
    }

    public enum GameState
    {
        Configuring = 0,
        Bidding = 1,
        Playing = 2,
        Ended = 3,
        Defered = 4
    }
}