using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mastonet;

namespace SakabaBot
{
    class Battle
    {
        static int actionTime = 600_000; // 10分
        static int limmitTime = 1200_000; // 20分

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
                await Task.Delay(actionTime);
                if (IsRunning)
                {
                    await MastodonClient.PostStatus(Account.GetAction(), Visibility.Public);

                    await Task.Delay(limmitTime);
                    await End(false);
                }
            });
        }

        public async Task Start()
        {
            Results = new List<BattleResult>();
            AttackCount = 0;
            IsRunning = true;


            var postStatus = await MastodonClient.PostStatus(Account.Roar, Visibility.Public);
            string accoutName = postStatus.Account.UserName;
            int accountId = postStatus.Account.Id;

            // 過去トゥートの削除
            var statuses = await MastodonClient.GetAccountStatuses(accountId, postStatus.Id);
            foreach (var status in statuses)
            {
                await MastodonClient.DeleteStatus(status.Id);
            }

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
                await MastodonClient.PostStatus($"@{status.Account.AccountName} {Account.Roar}", Visibility.Public);
            };

            await UserStreaming.Start();
        }

        public async Task End(bool success)
        {
            if (!IsRunning) { return; }

            IsRunning = false;
            if (success)
            {
                await MastodonClient.PostStatus(Account.Dead, Visibility.Public);

                var record = new Record();
                await record.InitializeAsync();

                await record.PostResultAsync(Account, Results);
            }
            else
            {
                string option;
                int random = new Random().Next(5);
                switch (random)
                {
                    case 0:
                        option = "壁に穴を開けて";
                        break;
                    case 1:
                        option = "食料を奪って";
                        break;
                    default:
                        option = "";
                        break;
                }
                string message = $"（{Account.Name}は{option}去って行った...）";

                await MastodonClient.PostStatus(message, Visibility.Public);
            }

            if (UserStreaming != null)
            {
                UserStreaming.Stop();
            }
        }
    }
}
