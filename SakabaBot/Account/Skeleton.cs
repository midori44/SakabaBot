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
            LifePoint = 2;
            Rank = 2;
            Roar = "ｶｶｶｶｶ";
            Dead = "ｶﾗｶﾗ...";
            Actions = new string[] {
                "葡萄酒を飲んでいる",
                "酒瓶をなぎ倒している",
                "剣を振り回している",
                "陽気に歌っている",
                "辺りを見回している",
            };

        }
    }
}
