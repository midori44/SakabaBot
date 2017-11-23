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
            Rank = 1;
            Roar = "ｳｱｱｧｧ";
            Dead = "ｵｵｫ...";
            Actions = new string[] {
                "ウロウロしている",
                "叫んでいる",
                "じっとしている",
                "呻いている",
                "扉を叩いている",
            };
        }
    }
}
