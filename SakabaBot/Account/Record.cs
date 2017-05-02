﻿using Mastonet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SakabaBot
{
    class Record : Account
    {
        public Record()
        {
            Email = Constant.RecordEmail;
            Password = Constant.Password;
            Name = "冒険の記録";
            LifePoint = 999;
        }

        public async Task PostResultAsync(string name, IEnumerable<BattleResult> results)
        {
            if (!results.Any()) { return; }

            int num = results.Select(x => x.Name).Distinct().Count();
            string users = string.Join(",", results.Select(x => x.Name).Distinct());
            string lastUser = results.Last().Name;
            string lastContent = Regex.Replace(results.Last().Content, "<span.*</span>", "");
            lastContent = Regex.Replace(lastContent, "<.*?>", "").Trim();

            string result = new StringBuilder()
                .AppendLine($"【{name} を倒した！】")
                .AppendLine($"参加人数: {num}人 ({users})")
                .AppendLine($"最後の一撃: @{lastUser} 「{lastContent}」")
                .ToString();

            await MastodonClient.PostStatus(result, Visibility.Public);
        }
    }
}
