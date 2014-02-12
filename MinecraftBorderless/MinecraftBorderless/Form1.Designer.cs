namespace MinecraftBorderless
{
    partial class MinecraftBorderlessMainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
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
            this.searchButton = new System.Windows.Forms.Button();
            this.windowList = new System.Windows.Forms.ListBox();
            this.attachUI = new System.Windows.Forms.GroupBox();
            this.attachUIPanel = new System.Windows.Forms.Panel();
            this.configLoadButton = new System.Windows.Forms.Button();
            this.configSaveButton = new System.Windows.Forms.Button();
            this.checkBoxFrameVisible = new System.Windows.Forms.CheckBox();
            this.checkBoxTopMost = new System.Windows.Forms.CheckBox();
            this.windowMoveButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.wndPosX = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.wndPosY = new System.Windows.Forms.TextBox();
            this.wndSizeH = new System.Windows.Forms.TextBox();
            this.wndSizeW = new System.Windows.Forms.TextBox();
            this.attachButton = new System.Windows.Forms.Button();
            this.mcSearchBgWorker = new System.ComponentModel.BackgroundWorker();
            this.attachCheckTimer = new System.Windows.Forms.Timer(this.components);
            this.attachUI.SuspendLayout();
            this.attachUIPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(12, 12);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(139, 23);
            this.searchButton.TabIndex = 0;
            this.searchButton.Text = "ウィンドウ探索";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // windowList
            // 
            this.windowList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.windowList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.windowList.FormattingEnabled = true;
            this.windowList.ItemHeight = 12;
            this.windowList.Location = new System.Drawing.Point(12, 41);
            this.windowList.Name = "windowList";
            this.windowList.Size = new System.Drawing.Size(139, 136);
            this.windowList.TabIndex = 1;
            this.windowList.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.windowList_DrawItem);
            // 
            // attachUI
            // 
            this.attachUI.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.attachUI.Controls.Add(this.attachUIPanel);
            this.attachUI.Controls.Add(this.attachButton);
            this.attachUI.Location = new System.Drawing.Point(157, 12);
            this.attachUI.Name = "attachUI";
            this.attachUI.Size = new System.Drawing.Size(187, 167);
            this.attachUI.TabIndex = 2;
            this.attachUI.TabStop = false;
            this.attachUI.Text = "AttachInfo";
            // 
            // attachUIPanel
            // 
            this.attachUIPanel.Controls.Add(this.configLoadButton);
            this.attachUIPanel.Controls.Add(this.configSaveButton);
            this.attachUIPanel.Controls.Add(this.checkBoxFrameVisible);
            this.attachUIPanel.Controls.Add(this.checkBoxTopMost);
            this.attachUIPanel.Controls.Add(this.windowMoveButton);
            this.attachUIPanel.Controls.Add(this.button2);
            this.attachUIPanel.Controls.Add(this.label1);
            this.attachUIPanel.Controls.Add(this.wndPosX);
            this.attachUIPanel.Controls.Add(this.button1);
            this.attachUIPanel.Controls.Add(this.wndPosY);
            this.attachUIPanel.Controls.Add(this.wndSizeH);
            this.attachUIPanel.Controls.Add(this.wndSizeW);
            this.attachUIPanel.Location = new System.Drawing.Point(6, 44);
            this.attachUIPanel.Name = "attachUIPanel";
            this.attachUIPanel.Size = new System.Drawing.Size(175, 117);
            this.attachUIPanel.TabIndex = 8;
            this.attachUIPanel.Visible = false;
            // 
            // configLoadButton
            // 
            this.configLoadButton.Location = new System.Drawing.Point(95, 90);
            this.configLoadButton.Name = "configLoadButton";
            this.configLoadButton.Size = new System.Drawing.Size(75, 23);
            this.configLoadButton.TabIndex = 13;
            this.configLoadButton.Text = "設定読込";
            this.configLoadButton.UseVisualStyleBackColor = true;
            this.configLoadButton.Click += new System.EventHandler(this.configLoadButton_Click);
            // 
            // configSaveButton
            // 
            this.configSaveButton.Location = new System.Drawing.Point(95, 65);
            this.configSaveButton.Name = "configSaveButton";
            this.configSaveButton.Size = new System.Drawing.Size(75, 23);
            this.configSaveButton.TabIndex = 12;
            this.configSaveButton.Text = "設定保存";
            this.configSaveButton.UseVisualStyleBackColor = true;
            this.configSaveButton.Click += new System.EventHandler(this.configSaveButton_Click);
            // 
            // checkBoxFrameVisible
            // 
            this.checkBoxFrameVisible.AutoSize = true;
            this.checkBoxFrameVisible.Location = new System.Drawing.Point(95, 37);
            this.checkBoxFrameVisible.Name = "checkBoxFrameVisible";
            this.checkBoxFrameVisible.Size = new System.Drawing.Size(69, 16);
            this.checkBoxFrameVisible.TabIndex = 11;
            this.checkBoxFrameVisible.Text = "枠を表示";
            this.checkBoxFrameVisible.UseVisualStyleBackColor = true;
            this.checkBoxFrameVisible.CheckedChanged += new System.EventHandler(this.checkBoxFrameVisible_CheckedChanged);
            // 
            // checkBoxTopMost
            // 
            this.checkBoxTopMost.AutoSize = true;
            this.checkBoxTopMost.Location = new System.Drawing.Point(95, 15);
            this.checkBoxTopMost.Name = "checkBoxTopMost";
            this.checkBoxTopMost.Size = new System.Drawing.Size(72, 16);
            this.checkBoxTopMost.TabIndex = 10;
            this.checkBoxTopMost.Text = "最前面化";
            this.checkBoxTopMost.UseVisualStyleBackColor = true;
            this.checkBoxTopMost.CheckedChanged += new System.EventHandler(this.checkBoxTopMost_CheckedChanged);
            // 
            // windowMoveButton
            // 
            this.windowMoveButton.Location = new System.Drawing.Point(5, 90);
            this.windowMoveButton.Name = "windowMoveButton";
            this.windowMoveButton.Size = new System.Drawing.Size(84, 23);
            this.windowMoveButton.TabIndex = 9;
            this.windowMoveButton.Text = "ウィンドウ移動";
            this.windowMoveButton.UseVisualStyleBackColor = true;
            this.windowMoveButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.windowMoveButton_MouseDown);
            this.windowMoveButton.MouseMove += new System.Windows.Forms.MouseEventHandler(this.windowMoveButton_MouseMove);
            this.windowMoveButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.windowMoveButton_MouseUp);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(48, 65);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(39, 19);
            this.button2.TabIndex = 8;
            this.button2.Text = "適用";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "WindowPosition";
            // 
            // wndPosX
            // 
            this.wndPosX.Location = new System.Drawing.Point(5, 15);
            this.wndPosX.Name = "wndPosX";
            this.wndPosX.Size = new System.Drawing.Size(39, 19);
            this.wndPosX.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(5, 65);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(39, 19);
            this.button1.TabIndex = 6;
            this.button1.Text = "更新";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // wndPosY
            // 
            this.wndPosY.Location = new System.Drawing.Point(50, 15);
            this.wndPosY.Name = "wndPosY";
            this.wndPosY.Size = new System.Drawing.Size(39, 19);
            this.wndPosY.TabIndex = 2;
            // 
            // wndSizeH
            // 
            this.wndSizeH.Location = new System.Drawing.Point(50, 40);
            this.wndSizeH.Name = "wndSizeH";
            this.wndSizeH.Size = new System.Drawing.Size(39, 19);
            this.wndSizeH.TabIndex = 5;
            // 
            // wndSizeW
            // 
            this.wndSizeW.Location = new System.Drawing.Point(5, 40);
            this.wndSizeW.Name = "wndSizeW";
            this.wndSizeW.Size = new System.Drawing.Size(39, 19);
            this.wndSizeW.TabIndex = 4;
            // 
            // attachButton
            // 
            this.attachButton.Location = new System.Drawing.Point(6, 15);
            this.attachButton.Name = "attachButton";
            this.attachButton.Size = new System.Drawing.Size(96, 23);
            this.attachButton.TabIndex = 3;
            this.attachButton.Text = "アタッチ";
            this.attachButton.UseVisualStyleBackColor = true;
            this.attachButton.Click += new System.EventHandler(this.attachButton_Click);
            // 
            // mcSearchBgWorker
            // 
            this.mcSearchBgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.mcSearchBgWorker_DoWork);
            this.mcSearchBgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.mcSearchBgWorker_RunWorkerCompleted);
            // 
            // attachCheckTimer
            // 
            this.attachCheckTimer.Tick += new System.EventHandler(this.attachCheckTimer_Tick);
            // 
            // MinecraftBorderlessMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(356, 190);
            this.Controls.Add(this.attachUI);
            this.Controls.Add(this.windowList);
            this.Controls.Add(this.searchButton);
            this.MinimumSize = new System.Drawing.Size(356, 192);
            this.Name = "MinecraftBorderlessMainForm";
            this.Text = "Minecraft 窓枠消し君";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.attachUI.ResumeLayout(false);
            this.attachUIPanel.ResumeLayout(false);
            this.attachUIPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.ListBox windowList;
        private System.Windows.Forms.GroupBox attachUI;
        private System.ComponentModel.BackgroundWorker mcSearchBgWorker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel attachUIPanel;
        private System.Windows.Forms.TextBox wndPosX;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox wndPosY;
        private System.Windows.Forms.TextBox wndSizeH;
        private System.Windows.Forms.TextBox wndSizeW;
        private System.Windows.Forms.Button attachButton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button windowMoveButton;
        private System.Windows.Forms.Timer attachCheckTimer;
        private System.Windows.Forms.CheckBox checkBoxTopMost;
        private System.Windows.Forms.CheckBox checkBoxFrameVisible;
        private System.Windows.Forms.Button configLoadButton;
        private System.Windows.Forms.Button configSaveButton;
    }
}

