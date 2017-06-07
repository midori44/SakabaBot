using Mastonet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            if (timeLabel.Text == "Off")
            {
                if (timeLeft == 0)
                {
                    timeLeft = randomizer.Next(min, max);
                    timeLabel.Text = timeLeft.ToString();
                }
                BattleTimer.Interval = 1000;
                BattleTimer.Start();
            }
            else
            {
                BattleTimer.Stop();
                timeLabel.Text = "Off";
            }
            
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
            if (clockLabel.Text == "Off")
            {
                ClockTimer.Interval = 1000;
                ClockTimer.Start();
                clockLabel.Text = "On";
            }
            else
            {
                ClockTimer.Stop();
                clockLabel.Text = "Off";
            }
        }
        private async void ReportButton_Click(object sender, EventArgs e)
        {
            var master = new Master();
            await master.InitializeAsync();

            var option = new ArrayOptions();
            var timeline = master.MastodonClient.GetPublicTimeline();

            var stream = master.MastodonClient.GetPublicStreaming();
            stream.OnUpdate += (_sender, _e) =>
            {
                var status = _e.Status;
                var user = status.Account;
            };

            await stream.Start();
        }







        private async void BattleTimer_Tick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                timeLeft = timeLeft - 1;
                timeLabel.Text = timeLeft.ToString();
            }
            else
            {
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
        private void ReportTimer_Tick(object sender, EventArgs e)
        {

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
            await clock.MastodonClient.PostStatus(dingdong, Visibility.Public);
        }

        private async Task ReportRun()
        {
            var record = new Record();
            await record.InitializeAsync();

        }


    }
}
