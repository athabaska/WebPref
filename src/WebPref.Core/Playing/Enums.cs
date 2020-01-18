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
    ///     Игра
    /// </summary>
    public enum GameEnum
    {
        [Description("6")]
        Six = 6,
        [Description("7")]
        Seven = 7,
        [Description("8")]
        Eight = 8,
        [Description("9")]
        Nine = 9,
        [Description("10")]
        Ten = 10,
        [Description("Miser")]
        Miser = 11
    }

    public enum GameState
    {
        Configuring,
        Bidding,
        Playing,
        Ended,
        Defered
    }
}