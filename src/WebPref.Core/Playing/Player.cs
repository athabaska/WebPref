using System;
using System.Collections.Generic;
using System.Text;
using WebPref.Core.Interfaces;
using WebPref.Core.Lobby;

namespace WebPref.Core.Playing
{
    
    /// <summary>
    /// Класс представляет игрока, в принципе, особых свойств ему не надо, id пользователя и имя будет получать из web-части, должна накапливаться статистика где-то
    /// 
    /// </summary>
    public class Player : IGameObserver
    {
        private Hand currentHand;

        public string Id { get; set; }
        public string Name { get; set; }        

        public List<TablePlayers> Tables { get; set; }

        /// <summary>
        ///     Получить карты после раздачи
        /// </summary>
        public void AcceptCards(IList<Card> newCards)
        {
            currentHand = new Hand();
            currentHand.AddCards(newCards);
        }
                
        public void Observe(Move move)
        {
            //todo обработать ход

            
        }
    }
}
