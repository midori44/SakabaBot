namespace SakabaBot
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.BattleStartButton = new System.Windows.Forms.Button();
            this.BattleTimer = new System.Windows.Forms.Timer(this.components);
            this.timeLabel = new System.Windows.Forms.Label();
            this.BattleForceButton = new System.Windows.Forms.Button();
            this.ClockButton = new System.Windows.Forms.Button();
            this.clockLabel = new System.Windows.Forms.Label();
            this.ClockTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // BattleStartButton
            // 
            this.BattleStartButton.Location = new System.Drawing.Point(12, 12);
            this.BattleStartButton.Name = "BattleStartButton";
            this.BattleStartButton.Size = new System.Drawing.Size(75, 23);
            this.BattleStartButton.TabIndex = 0;
            this.BattleStartButton.Text = "Battle";
            this.BattleStartButton.UseVisualStyleBackColor = true;
            this.BattleStartButton.Click += new System.EventHandler(this.BattleStartButton_Click);
            // 
            // BattleTimer
            // 
            this.BattleTimer.Tick += new System.EventHandler(this.BattleTimer_Tick);
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Location = new System.Drawing.Point(93, 17);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(21, 12);
            this.timeLabel.TabIndex = 1;
            this.timeLabel.Text = "Off";
            // 
            // BattleForceButton
            // 
            this.BattleForceButton.Location = new System.Drawing.Point(12, 41);
            this.BattleForceButton.Name = "BattleForceButton";
            this.BattleForceButton.Size = new System.Drawing.Size(75, 23);
            this.BattleForceButton.TabIndex = 4;
            this.BattleForceButton.Text = "Force";
            this.BattleForceButton.UseVisualStyleBackColor = true;
            this.BattleForceButton.Click += new System.EventHandler(this.BattleForceButton_Click);
            // 
            // ClockButton
            // 
            this.ClockButton.Location = new System.Drawing.Point(12, 90);
            this.ClockButton.Name = "ClockButton";
            this.ClockButton.Size = new System.Drawing.Size(75, 23);
            this.ClockButton.TabIndex = 5;
            this.ClockButton.Text = "Clock";
            this.ClockButton.UseVisualStyleBackColor = true;
            this.ClockButton.Click += new System.EventHandler(this.ClockButton_Click);
            // 
            // clockLabel
            // 
            this.clockLabel.AutoSize = true;
            this.clockLabel.Location = new System.Drawing.Point(93, 95);
            this.clockLabel.Name = "clockLabel";
            this.clockLabel.Size = new System.Drawing.Size(21, 12);
            this.clockLabel.TabIndex = 6;
            this.clockLabel.Text = "Off";
            // 
            // ClockTimer
            // 
            this.ClockTimer.Tick += new System.EventHandler(this.ClockTimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.clockLabel);
            this.Controls.Add(this.ClockButton);
            this.Controls.Add(this.BattleForceButton);
            this.Controls.Add(this.timeLabel);
            this.Controls.Add(this.BattleStartButton);
            this.Name = "MainForm";
            this.Text = "SakabaBot";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BattleStartButton;
        private System.Windows.Forms.Timer BattleTimer;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.Button BattleForceButton;
        private System.Windows.Forms.Button ClockButton;
        private System.Windows.Forms.Label clockLabel;
        private System.Windows.Forms.Timer ClockTimer;
    }
}

