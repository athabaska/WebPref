/*
 * Author: oparinig
 * Date of creation: 2/20/2018 4:28:12 PM
 * Comments: Утилиты
 */

#region usings

#endregion

using System;
using System.ComponentModel;
using System.Linq;

namespace WebPref.Core.Utils
{
    /// <summary>
    ///     Утилиты
    /// </summary>
    public static class Utils
    {
        #region Методы

        /// <summary>
        ///     
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum e)
        {
            var ca = e.GetType().GetField(e.ToString("F")).GetCustomAttributes(false);
            if (ca.Length == 0)
                return e.ToString("F");
            var da = ca.OfType<DescriptionAttribute>().FirstOrDefault();
            return da == null ? string.Empty : da.Description;
        }

        #endregion
    }
}