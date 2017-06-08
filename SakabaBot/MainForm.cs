﻿using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mastonet;

namespace SakabaBot
{
    public partial class MainForm : Form
    {
        static int min = 14400; // 4時間
        static int max = 21600; // 6時間

        Random randomizer = new Random();
        int timeLeft = 0;
        int clockHour = -1;

        public MainForm()
        {
            InitializeComponent();

#if DEBUG
            this.Text += " [DEBUG]";
#endif
        }

        private void BattleStartButton_Click(object sender, EventArgs e)
        {
            if (timeLabel.Text != "Off")
            {
                BattleTimer.Stop();
                timeLabel.Text = "Off";
                return;
            }

            if (timeLeft == 0)
            {
                timeLeft = randomizer.Next(min, max);
                timeLabel.Text = timeLeft.ToString();
            }
            BattleTimer.Interval = 1000;
            BattleTimer.Start();
            
        }
        private void BattleForceButton_Click(object sender, EventArgs e)
        {
            timeLeft = 0;
            timeLabel.Text = timeLeft.ToString();
            BattleTimer.Interval = 1000;
            BattleTimer.Start();
        }
        private void ClockButton_Click(object sender, EventArgs e)
        {
            if (clockLabel.Text != "Off")
            {
                ClockTimer.Stop();
                clockLabel.Text = "Off";
                return;
            }

            ClockTimer.Interval = 1000;
            ClockTimer.Start();
            clockLabel.Text = "On";
        }





        private async void BattleTimer_Tick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                timeLeft = timeLeft - 1;
                timeLabel.Text = timeLeft.ToString();
                return;
            }

            BattleTimer.Stop();
            int random = randomizer.Next(10);
            switch (random)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                    timeLabel.Text = "zombie";
                    await ZombieRun();
                    break;
                case 5:
                case 6:
                case 7:
                    timeLabel.Text = "rat";
                    await RatRun();
                    break;
                case 8:
                case 9:
                    timeLabel.Text = "skeleton";
                    await SkeletonRun();
                    break;
            }

            timeLeft = randomizer.Next(min, max);
            BattleTimer.Start();
        }
        private async void ClockTimer_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.UtcNow;
            if (now.Hour != clockHour && now.Minute == 0)
            {
                ClockTimer.Stop();
                clockLabel.Text = "dingdong";
                clockHour = now.Hour;
                await ClockRun();

                clockLabel.Text = "On";
                ClockTimer.Start();
            }
        }





        private async Task ZombieRun()
        {
            var zombie = new Zombie();
            await zombie.InitializeAsync();

            var battle = new Battle(zombie);
            await battle.Start();
        }
        private async Task SkeletonRun()
        {
            var skeleton = new Skeleton();
            await skeleton.InitializeAsync();

            var battle = new Battle(skeleton);
            await battle.Start();
        }
        private async Task RatRun()
        {
            var rat = new Rat();
            await rat.InitializeAsync();

            var battle = new Battle(rat);
            await battle.Start();
        }
        private async Task ClockRun()
        {
            var clock = new Clock();
            await clock.InitializeAsync();

            int hour = DateTime.UtcNow.AddHours(9).Hour;
            string dingdong = (randomizer.Next(10) > 1) ? $"{clock.Roar} ({hour}:00)" : clock.Dead;
            var postStatus = await clock.MastodonClient.PostStatus(dingdong, Visibility.Public);

            var statuses = await clock.MastodonClient.GetAccountStatuses(postStatus.Account.Id, postStatus.Id);
            foreach (var status in statuses)
            {
                await clock.MastodonClient.DeleteStatus(status.Id);
            }
        }

    }
}
