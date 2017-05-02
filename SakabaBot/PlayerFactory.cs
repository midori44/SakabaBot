using Mastonet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakabaBot
{
    class Player
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public MastodonClient MastodonClient { get; private set; }
        public int LifePoint { get; set; }
        public string Roar { get; set; }
        public string Dead { get; set; }
        public string Name { get; }

        public Player(string email, string password)
        {
            Email = email;
            Password = password;
        }
        public async Task InitializeAsync()
        {
            var authenticationClient = new AuthenticationClient(Constant.Instance);
            var registration = await authenticationClient.CreateApp("SakabaBot", Scope.Read | Scope.Write | Scope.Follow);
            var auth = await authenticationClient.ConnectWithPassword(Email, Password);

            MastodonClient = new MastodonClient(registration, auth);
        }
    }

    enum Avatar
    {
        Master,
        Record,
        Zombie,
    }

    static class PlayerFactory
    {
        public static async Task<Zombie> Zombie()
        {
            var zombie = new Zombie();
            await zombie.InitializeAsync();
            return zombie;
        }
    }
}
