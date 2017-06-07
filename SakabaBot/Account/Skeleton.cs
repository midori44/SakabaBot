using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakabaBot
{
    class Skeleton : Account
    {
        public Skeleton()
        {
            Email = Constant.SkeletonEmail;
            Password = Constant.Password;
            Name = "スケルトン";
            LifePoint = 3;
            Roar = "ｶｶｶｶｶ";
            Dead = "ｶﾗｶﾗ...";
        }
    }
}
