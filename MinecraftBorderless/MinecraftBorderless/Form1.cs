using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MinecraftBorderless
{
    public partial class MinecraftBorderlessMainForm : Form
    {
        MinecraftControl mcControl = new MinecraftControl();
        bool IsAttachedWindowMoving { get; set; }
        CLIOption option = new CLIOption();

        bool RunAtOnceAutoAttach = false;
        bool RunAtOnceAutoLoad = false;
        bool RunAtOnceLoadingFile = false;
        bool AttachInitializing = false;

        public MinecraftBorderlessMainForm()
        {
            InitializeComponent();

            mcControl.WindowListUpdated += new MinecraftControl.WindowListUpdatedEventHandler(mcControl_WindowListUpdated);

            searchButton_Click(null, null);

            // ウィンドウの最小サイズを設定
            MinimumSize = new Size(MinimumSize.Width + SystemInformation.FrameBorderSize.Width * 2,
                MinimumSize.Height + SystemInformation.CaptionHeight + SystemInformation.FrameBorderSize.Height * 2);

            // 起動時自動ロード用フラグのセット
            if (option.Options.Keys.Contains("AutomaticLoad"))
            {
                this.RunAtOnceAutoLoad = true;
                this.RunAtOnceLoadingFile = true;
            }
            if (option.Options.Keys.Contains("RunAtStartup"))
                this.RunAtOnceAutoAttach = true;
        }

        void mcControl_WindowListUpdated(MinecraftControl.WindowInfo[] windowInfoList)
        {
            if (InvokeRequired)
            {
                Invoke(new MinecraftControl.WindowListUpdatedEventHandler(mcControl_WindowListUpdated), (object)windowInfoList);
                return;
            }

            // リストを初期化して登録し直す
            windowList.Items.Clear();
            windowList.Items.AddRange(windowInfoList);

            if (RunAtOnceAutoAttach && option.Options.Keys.Contains("RunAtStartup"))
            {
                this.RunAtOnceAutoAttach = false;
                if (windowList.Items.Count > 0)
                {
                    windowList.SelectedIndex = 0;
                    windowList.Refresh();
                    attachButton_Click(null, null);
                }
                else
                {
                    this.RunAtOnceAutoLoad = false;
                    this.RunAtOnceLoadingFile = false;
                }
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            windowList.Items.Clear();
            windowList.Items.Add("更新中...");

            // ウィンドウの探索には多少時間かかる為バックグラウンド処理する
            searchButton.Enabled = false;
            mcSearchBgWorker.RunWorkerAsync();
        }

        private void mcSearchBgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (option.Options.Keys.Contains("WindowClass"))
                mcControl.ScanMinecraftWindow(option.Options["WindowClass"].GetStringValue());
            else
                mcControl.ScanMinecraftWindow();
        }

        private void mcSearchBgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            searchButton.Enabled = true;
        }

        private void windowList_DrawItem(object sender, DrawItemEventArgs e)
        {
            ListBox lb = (ListBox)sender;
            Font font = null;

            if (e.Index > -1)
            {
                // アタッチされてるウィンドウの場合
                if (mcControl.IsAttached && mcControl.AttachedWindowInfo == lb.Items[e.Index])
                {
                    // 選択されて無ければ背景色を変更
                    if ((e.State & DrawItemState.Selected) != DrawItemState.Selected)
                        e.Graphics.FillRectangle(Brushes.LightPink, e.Bounds);
                    else
                        e.DrawBackground();

                    // 文字を太字にする
                    font = new Font(e.Font, FontStyle.Bold);
                } else
                    e.DrawBackground();

                Brush b = new SolidBrush(e.ForeColor);
                //描画する文字列の取得
                string txt = ((ListBox)sender).Items[e.Index].ToString();
                //文字列の描画
                e.Graphics.DrawString(txt, (font != null) ? font : e.Font, b, e.Bounds);
                b.Dispose();

                if (font != null)
                    font.Dispose();
            }

            // フォーカスの枠を表示
            e.DrawFocusRectangle();
        }

        private void attachButton_Click(object sender, EventArgs e)
        {
            if (mcControl.IsAttached)
            {
                // タイマー停止
                attachCheckTimer.Stop();

                // デタッチ処理
                mcControl.DetachMinecraftWindow();

                // デタッチの後処理
                attachUIPanel.Visible = false;

                // ボタンの更新
                attachButton.Text = "アタッチ";
            }
            else
            {
                // アタッチ処理
                if (!mcControl.AttachMinecraftWindow((MinecraftControl.WindowInfo)windowList.SelectedItem))
                    return;

                this.AttachInitializing = true;

                // アタッチ後の初期化
                AttachWindowInfoUpdate();
                IsAttachedWindowMoving = false;
                attachUIPanel.Visible = true;

                // ボタンの更新
                attachButton.Text = "切断";

                // 監視タイマー起動
                attachCheckTimer.Start();
                this.AttachInitializing = false;

                // 初回自動ロード設定時にはロード処理を行う
                if (RunAtOnceAutoLoad && option.Options.Keys.Contains("AutomaticLoad"))
                {
                    this.RunAtOnceAutoLoad = false;
                    configLoadButton_Click(null, null);
                }
            }

            // ウィンドウリスト再描画(ユーザー描画イベントを処理させる)
            windowList.Refresh();
        }

        private void AttachWindowInfoUpdate()
        {
            if (!mcControl.IsAttached)
                return;

            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(AttachWindowInfoUpdate));
                return;
            }

            // 現在座標の表示
            wndPosX.Text = mcControl.AttachedWindowInfo.GetWindowPos().X.ToString();
            wndPosY.Text = mcControl.AttachedWindowInfo.GetWindowPos().Y.ToString();
            wndSizeW.Text = mcControl.AttachedWindowInfo.GetWindowPos().Width.ToString();
            wndSizeH.Text = mcControl.AttachedWindowInfo.GetWindowPos().Height.ToString();
            wndPosX.Refresh();
            wndPosY.Refresh();
            wndSizeW.Refresh();
            wndSizeH.Refresh();

            // ウィンドウ枠の状態の取得
            // 不明な場合はSizable化しちゃう
            switch (mcControl.AttachedWindowInfo.GetWindowBorderStyle())
            {
                case System.Windows.Forms.FormBorderStyle.Sizable:
                    checkBoxFrameVisible.Checked = true;
                    break;
                case System.Windows.Forms.FormBorderStyle.None:
                    checkBoxFrameVisible.Checked = false;
                    break;
                default:
                    mcControl.AttachedWindowInfo.SetWindowBorderStyle(System.Windows.Forms.FormBorderStyle.Sizable);
                    break;
            }

            // ウィンドウの最全面化の取得
            if (mcControl.AttachedWindowInfo.IsTopMostWindow())
                checkBoxTopMost.Checked = true;
            else
                checkBoxTopMost.Checked = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AttachWindowInfoUpdate();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mcControl.IsAttached)
                mcControl.DetachMinecraftWindow();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (mcControl.IsAttached) {
                mcControl.AttachedWindowInfo.SetWindowPos(
                    int.Parse(wndPosX.Text), int.Parse(wndPosY.Text),
                    int.Parse(wndSizeW.Text), int.Parse(wndSizeH.Text));
            }
            AttachWindowInfoUpdate();
        }

        private void windowMoveButton_MouseDown(object sender, MouseEventArgs e)
        {
            // SetCaptureAPIでマウスキャプチャ開始
            AttachWindowInfoUpdate();
            NativeAPIs.SetCapture(windowMoveButton.Handle);
            this.IsAttachedWindowMoving = true;

            // マウスカーソルの位置をアタッチ中のウィンドウの中央へ移動
            Rectangle pos = mcControl.AttachedWindowInfo.GetWindowPos();
            Cursor.Position = new Point(pos.X + pos.Width / 2, pos.Y + pos.Height / 2);
            mcControl.AttachedWindowInfo.SetWindowZOrder(this.Handle);
        }

        private void windowMoveButton_MouseUp(object sender, MouseEventArgs e)
        {
            // マウス開放
            this.IsAttachedWindowMoving = false;
            NativeAPIs.ReleaseCapture();

            // マウスカーソルをボタンの中央へ移動
            Cursor.Position = windowMoveButton.PointToScreen(new Point(windowMoveButton.Size.Width / 2, windowMoveButton.Size.Height / 2));
        }

        private void windowMoveButton_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.IsAttachedWindowMoving)
            {
                if (mcControl.IsAttached)
                {
                    Rectangle wndPos = mcControl.AttachedWindowInfo.GetWindowPos();
                    Point mousePos = Cursor.Position;
                    Rectangle newPos = wndPos;

                    // 移動先の計算
                    newPos.X = mousePos.X - wndPos.Width / 2;
                    newPos.Y = mousePos.Y - wndPos.Height / 2;

                    // ウィンドウがはみ出る場合
                    if (mousePos.X - wndPos.Width / 2 < 0)
                        newPos.X = 0;
                    if (mousePos.X + wndPos.Width / 2 > SystemInformation.WorkingArea.Width)
                        newPos.X = SystemInformation.WorkingArea.Width - wndPos.Width;
                    if (mousePos.Y - wndPos.Height / 2 < 0)
                        newPos.Y = 0;
                    if (mousePos.Y + wndPos.Height / 2 > SystemInformation.WorkingArea.Height)
                        newPos.Y = SystemInformation.WorkingArea.Height - wndPos.Height;

                    // 現在座標を更新
                    wndPosX.Text = newPos.X.ToString();
                    wndPosY.Text = newPos.Y.ToString();
                    wndPosX.Refresh();
                    wndPosY.Refresh();

                    // ウィンドウを移動させる
                    mcControl.AttachedWindowInfo.SetWindowPos(newPos);
                }
            }
        }

        private void attachCheckTimer_Tick(object sender, EventArgs e)
        {
            if (!mcControl.IsAttached || !mcControl.AttachedWindowInfo.IsWindow())
            {
                mcControl.CheckWindowList();
                attachButton_Click(null, null);
                attachCheckTimer.Stop();
            }
        }

        private void checkBoxTopMost_CheckedChanged(object sender, EventArgs e)
        {
            if (this.AttachInitializing)
                return;

            if (checkBoxTopMost.Checked)
            {
                mcControl.AttachedWindowInfo.SetWindowZOrder(new IntPtr((int)NativeAPIs.Enum.HWND.HWND_TOPMOST));
            }
            else
            {
                mcControl.AttachedWindowInfo.SetWindowZOrder(this.Handle);
            }
        }

        private void checkBoxFrameVisible_CheckedChanged(object sender, EventArgs e)
        {
            if (this.AttachInitializing)
                return;

            checkBoxFrameVisible.Enabled = false;
            if (checkBoxFrameVisible.Checked)
            {
                mcControl.AttachedWindowInfo.SetWindowBorderStyle(System.Windows.Forms.FormBorderStyle.Sizable);
            }
            else
            {
                mcControl.AttachedWindowInfo.SetWindowBorderStyle(System.Windows.Forms.FormBorderStyle.None);
            }
            AttachWindowInfoUpdate();
            checkBoxFrameVisible.Enabled = true;
        }

        private void configSaveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "wcontrol.conf";
            sfd.Filter = "Config Files (*.conf)|*.conf|All Files (*.*)|*.*";
            DialogResult result = sfd.ShowDialog(this);

            if (result != System.Windows.Forms.DialogResult.OK)
                return;

            AttachWindowInfoUpdate();

            WindowContorolSetting wcs = new WindowContorolSetting();

            wcs.WindowOption.IsBorderVisible = checkBoxFrameVisible.Checked;
            wcs.WindowOption.IsTopMostWindow = checkBoxTopMost.Checked;

            wcs.WindowPosition = new Rectangle(
                int.Parse(wndPosX.Text), int.Parse(wndPosY.Text),
                int.Parse(wndSizeW.Text), int.Parse(wndSizeH.Text));

            wcs.Save(sfd.FileName);

            sfd.Dispose();
        }

        private void configLoadButton_Click(object sender, EventArgs e)
        {
            WindowContorolSetting wcs = new WindowContorolSetting();

            if (this.RunAtOnceLoadingFile)
            {
                this.RunAtOnceLoadingFile = false;
                if (option.Options.Keys.Contains("ConfigFile"))
                    wcs.Load(option.Options["ConfigFile"].GetStringValue());
                else
                    wcs.Load();
            }
            else
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.FileName = "wcontrol.conf";
                ofd.Filter = "Config Files (*.conf)|*.conf|All Files (*.*)|*.*";
                ofd.Multiselect = false;

                DialogResult result = ofd.ShowDialog();

                if (result != System.Windows.Forms.DialogResult.OK)
                    return;

                wcs.Load(ofd.FileName);
            }

            checkBoxFrameVisible.Checked = wcs.WindowOption.IsBorderVisible;
 
            if (mcControl.IsAttached)
                mcControl.AttachedWindowInfo.SetWindowPos(wcs.WindowPosition);

            checkBoxTopMost.Checked = wcs.WindowOption.IsTopMostWindow;
 
            AttachWindowInfoUpdate();
        }
    }
}
