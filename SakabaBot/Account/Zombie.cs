using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakabaBot
{
    class Zombie : Account
    {
        public Zombie()
        {
            Email = Constant.ZombieEmail;
            Password = Constant.Password;
            Name = "ゾンビ";
            LifePoint = 2;
            Roar = "ｳｱｱｧｧ";
            Dead = "ｵｵｫ...";
        }
    }
}
