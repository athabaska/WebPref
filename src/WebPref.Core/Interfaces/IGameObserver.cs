
using WebPref.Core.Playing;

namespace WebPref.Core.Interfaces
{
    /// <summary>
    ///     Наблюдатель за ходом игры
    /// </summary>
    public interface IGameObserver
    {
        /// <summary>
        ///     Обработать ход
        /// </summary>
        void Observe(Move move);
    }
}