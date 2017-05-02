using Mastonet;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SakabaBot
{
    class Battle
    {
        static int limmitTime = 900_000;

        TimelineStreaming UserStreaming;
        MastodonClient MastodonClient;
        Account Account;
        List<BattleResult> Results;
        int AttackCount;
        bool IsRunning;

        public Battle(Account account)
        {
            Account = account;
            MastodonClient = account.MastodonClient;

            Task.Run(async () => {
                await Task.Delay(limmitTime);
                await End(false);
            });
        }

        public async Task Start()
        {
            Results = new List<BattleResult>();
            AttackCount = 0;
            IsRunning = true;

            var postStatus = await MastodonClient.PostStatus(Account.Roar, Visibility.Public);
            string accoutName = postStatus.Account.UserName;

            UserStreaming = MastodonClient.GetUserStreaming();
            UserStreaming.OnNotification += async (sender, e) =>
            {
                var status = e.Notification.Status;
                if (status == null || !status.Content.Contains($"@<span>{accoutName}</span>")) { return; }

                AttackCount++;
                if (AttackCount > Account.LifePoint) { return; }

                Results.Add(new BattleResult
                {
                    PostId = status.Id,
                    Name = status.Account.AccountName,
                    Content = status.Content
                });
                if (AttackCount == Account.LifePoint)
                {
                    await End(true);
                    return;
                }
                await MastodonClient.PostStatus($"@{status.Account.AccountName} {Account.Roar}", Visibility.Public, replyStatusId: status.Id);
            };

            await UserStreaming.Start();

            //PublicStreaming = MastodonClient.GetPublicStreaming();
            //PublicStreaming.OnUpdate += async (sender, e) =>
            //{
            //    if (DateTime.UtcNow > postStatus.CreatedAt.AddSeconds(10))
            //    {
            //        await End(false);
            //    }
            //};
            //await PublicStreaming.Start();
        }

        public async Task End(bool success)
        {
            if (IsRunning)
            {
                IsRunning = false;
                if (UserStreaming != null)
                {
                    UserStreaming.Stop();
                }

                if (success)
                {
                    await MastodonClient.PostStatus(Account.Dead, Visibility.Public);

                    var record = new Record();
                    await record.InitializeAsync();
                    await record.PostResultAsync(Account.Name, Results);
                }
                else
                {
                    await MastodonClient.PostStatus($"（{Account.Name}は去って行った...）", Visibility.Public);
                }

            }
        }
    }
}
