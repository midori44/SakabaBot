using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Mastonet;
using Mastonet.Entities;

namespace SakabaBot
{
    class Cards : Account
    {
        private CardsSet cardsSet;
        private TimelineStreaming UserStreaming;
        public Cards()
        {
            Email = Constant.CardsEmail;
            Password = Constant.Password;
            Name = "cards";

            cardsSet = new CardsSet();
        }

        public async Task Start()
        {
            UserStreaming = MastodonClient.GetUserStreaming();
            UserStreaming.OnNotification += async (sender, e) =>
            {
                var status = e.Notification.Status;
                if (status == null || !status.Content.Contains($"@<span>{Name}</span>")) { return; }

                if (status.Content.Contains("シャッフル"))
                {
                    cardsSet.Reset();
                    await MastodonClient.PostStatus($"シャッフルしました", Visibility.Public);
                    return;
                }

                int num = 1;
                var match = Regex.Match(status.Content, "[0-9０-９]+");
                if (match.Success)
                {
                    string value = match.Value
                        .Replace("０", "0")
                        .Replace("１", "1")
                        .Replace("２", "2")
                        .Replace("３", "3")
                        .Replace("４", "4")
                        .Replace("５", "5")
                        .Replace("６", "6")
                        .Replace("７", "7")
                        .Replace("８", "8")
                        .Replace("９", "9");
                    num = int.Parse(match.Value);
                    if (num < 1) num = 1;
                    if (num > 5) num = 5;
                }

                var builder = new StringBuilder();
                for (int i = 0; i < num; i++)
                {
                    if (cardsSet.Empty)
                    {
                        cardsSet.Reset();
                        builder.AppendLine("シャッフルしました");
                    }

                    string card = cardsSet.Get();
                    builder.AppendLine(card);
                }

                string post = builder.ToString();
                await MastodonClient.PostStatus($"@{status.Account.AccountName} {post}", Visibility.Public);
            };

            await UserStreaming.Start();
        }

        public void Stop()
        {
            if (UserStreaming != null)
            {
                UserStreaming.Stop();
            }
        }
    }

    class CardsSet
    {
        private string[] marks = { "spades", "clubs", "hearts", "diamonds" };
        private IList<string> list;
        
        public CardsSet()
        {
            list = new List<string>();
        }

        public bool Empty => (list.Count == 0);
        public void Reset()
        {
            list = new List<string>();
            foreach (string mark in marks)
            {
                list.Add($":{mark}: A");
                for (int i = 2; i <= 10; i++)
                {
                    list.Add($":{mark}: {i}");
                }
                list.Add($":{mark}: J");
                list.Add($":{mark}: Q");
                list.Add($":{mark}: K");
            }
            list.Add($":black_joker:");
        }
        public string Get()
        {
            if (Empty) { return ""; };

            var random = new Random();
            int index = random.Next(list.Count);

            string card = list[index];
            list.RemoveAt(index);

            return card;
        }

    }
}
