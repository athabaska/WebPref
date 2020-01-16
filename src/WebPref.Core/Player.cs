using System;
using System.Collections.Generic;
using System.Text;

namespace WebPref.Core
{
    
    /// <summary>
    /// Класс представляет игрока, в принципе, особых свойств ему не надо, id пользователя и имя будет получать из web-части, должна накапливаться статистика где-то
    /// 
    /// </summary>
    public class Player
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
