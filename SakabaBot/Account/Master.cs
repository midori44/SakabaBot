using Mastonet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakabaBot
{
    class Master: Account
    {
        public Master()
        {
            Email = Constant.MasterEmail;
            Password = Constant.Password;
            Name = "master";
            LifePoint = 100;
        }

    }
}
