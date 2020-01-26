#region usings

#endregion

using System;
using System.ComponentModel;
using System.Linq;
using WebPref.Core.Playing;

namespace WebPref.Core.Utils
{
    /// <summary>
    ///     Утилиты
    /// </summary>
    public static class Utils
    {
        public static SuitEnum[] Suits = { SuitEnum.Spades, SuitEnum.Clubs, SuitEnum.Diamonds, SuitEnum.Hearts, };

        public static string GetDescription(this Enum e)
        {
            var ca = e.GetType().GetField(e.ToString("F")).GetCustomAttributes(false);
            if (ca.Length == 0)
                return e.ToString("F");
            var da = ca.OfType<DescriptionAttribute>().FirstOrDefault();
            return da == null ? string.Empty : da.Description;
        }

    }
}