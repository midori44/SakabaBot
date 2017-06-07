using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakabaBot
{
    class Rat : Account
    {
        public Rat()
        {
            Email = Constant.RatEmail;
            Password = Constant.Password;
            Name = "大ネズミ";
            LifePoint = 1;
            Roar = "ｷｲｲｨｨ";
            Dead = "ｷｰ...";
        }
    }
}
