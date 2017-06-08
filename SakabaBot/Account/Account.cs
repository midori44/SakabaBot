using System.Threading.Tasks;
using Mastonet;

namespace SakabaBot
{
    class Account
    {
        protected string Email { get; set; }
        protected string Password { get; set; }
        public MastodonClient MastodonClient { get; private set; }
        public int LifePoint { get; set; }
        public string Roar { get; set; }
        public string Dead { get; set; }
        public string Name { get; set; }

        public async Task InitializeAsync()
        {
            var authenticationClient = new AuthenticationClient(Constant.Instance);
            var registration = await authenticationClient.CreateApp("SakabaBot", Scope.Read | Scope.Write | Scope.Follow);
            var auth = await authenticationClient.ConnectWithPassword(Email, Password);

            MastodonClient = new MastodonClient(registration, auth);
        }

        
    }
}
