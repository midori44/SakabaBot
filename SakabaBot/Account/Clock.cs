using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakabaBot
{
    class Clock : Account
    {
        public Clock()
        {
            Email = Constant.ClockEmail;
            Password = Constant.Password;
            Name = "clock";
            Roar = "ゴーンゴーン";
            Dead = "...";
        }
    }
}
