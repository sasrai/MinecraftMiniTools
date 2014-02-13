using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace MinecraftBorderless
{
    public class MinecraftControl
    {
        List<WindowInfo> targetList = new List<WindowInfo>();

        /// <summary>
        /// マイクラと思しきウィンドウ一覧の情報
        /// </summary>
        public WindowInfo[] MinecraftWindowList
        {
            get
            {
                return targetList.ToArray();
            }
        }
        public bool IsAttached {
            get
            {
                return (null != this.AttachedWindowInfo);
            }
        }
        public WindowInfo AttachedWindowInfo { get; protected set; }

        /// <summary>
        /// ウィンドウ情報クラス
        /// </summary>
        public class WindowInfo
        {
            /// <summary>
            /// ウィンドウハンドル
            /// </summary>
            public IntPtr WindowHandle { get; protected set; }
            /// <summary>
            /// ウィンドウを保持してるスレッドのプロセス情報
            /// </summary>
            public Process OwnerThread { get; protected set; }
            /// <summary>
            /// ウィンドウタイトル
            /// </summary>
            public string WindowTitle { get; protected set; }
            /// <summary>
            /// ウィンドウの位置
            /// </summary>
            public System.Drawing.Rectangle GetWindowPos()
            {
                NativeAPIs.Struct.RECT rect;
                NativeAPIs.GetWindowRect(this.WindowHandle, out rect);

                return new System.Drawing.Rectangle(rect.left, rect.top,
                    rect.right - rect.left, rect.bottom - rect.top);

            }
            /// <summary>
            /// コンストラクたん
            /// </summary>
            /// <param name="hWnd">ウィンドウハンドル</param>
            /// <param name="proc">オーナープロセス情報</param>
            /// <param name="wndTitle">ウィンドウタイトル</param>
            public WindowInfo(IntPtr hWnd, Process proc, string wndTitle)
            {
                this.WindowHandle = hWnd;
                this.OwnerThread = proc;
                this.WindowTitle = wndTitle;
            }

            /// <summary>
            /// ウィンドウ位置の移動
            /// </summary>
            /// <param name="pos">移動先の位置</param>
            public void SetWindowPos(System.Drawing.Rectangle pos)
            {
                this.SetWindowPos(pos.X, pos.Y, pos.Width, pos.Height);
            }
            /// <summary>
            /// ウィンドウ位置の移動
            /// </summary>
            /// <param name="x">移動先のX座標</param>
            /// <param name="y">移動先のY座標</param>
            /// <param name="width">移動後のウィンドウの横幅</param>
            /// <param name="height">移動後のウィンドウの縦幅</param>
            public void SetWindowPos(int x, int y, int width, int height)
            {
                // 位置とサイズのみ変更
                NativeAPIs.SetWindowPos(this.WindowHandle,
                    IntPtr.Zero,
                    x, y, width, height,
                    (uint)(NativeAPIs.Enum.SetWindowPos.SWP_NOOWNERZORDER 
                        | NativeAPIs.Enum.SetWindowPos.SWP_NOACTIVATE));
            }
            /// <summary>
            /// ウィンドウのZオーダー変更
            /// </summary>
            /// <param name="hWndInsertAfter">SetWindowPosの第2引数と同じ</param>
            public void SetWindowZOrder(IntPtr hWndInsertAfter)
            {
                // Zオーダーのみ変更
                NativeAPIs.SetWindowPos(this.WindowHandle,
                    hWndInsertAfter,
                    -1, -1, -1, -1,
                    (uint)(NativeAPIs.Enum.SetWindowPos.SWP_NOMOVE
                        | NativeAPIs.Enum.SetWindowPos.SWP_NOSIZE 
                        | NativeAPIs.Enum.SetWindowPos.SWP_NOACTIVATE));
            }

            /// <summary>
            /// BorderStyleはSizableとNoneにのみ対応
            /// </summary>
            /// <param name="borderStyle"></param>
            public void SetWindowBorderStyle(System.Windows.Forms.FormBorderStyle borderStyle)
            {
                Rectangle windowPos = this.GetWindowPos();
                Rectangle windowNewPos = new Rectangle();

                switch (borderStyle)
                {
                    // 枠を消す
                    case System.Windows.Forms.FormBorderStyle.None:
                        NativeAPIs.SetWindowLongPtr(this.WindowHandle, (int)NativeAPIs.Enum.WindowLong.GWL_STYLE,
                            new UIntPtr((uint)(NativeAPIs.Enum.WindowStyles.WS_POPUP
                                | NativeAPIs.Enum.WindowStyles.WS_CLIPCHILDREN)));
                        windowNewPos = windowPos;
                        windowNewPos.X += SystemInformation.FrameBorderSize.Width;
                        windowNewPos.Y += SystemInformation.CaptionHeight + SystemInformation.FrameBorderSize.Height;
                        windowNewPos.Width -= SystemInformation.FrameBorderSize.Width * 2;
                        windowNewPos.Height -= SystemInformation.CaptionHeight + SystemInformation.FrameBorderSize.Height * 2;
                        break;
                    // 通常のサイズ変更可能な枠をつける
                    case System.Windows.Forms.FormBorderStyle.Sizable:
                        NativeAPIs.SetWindowLongPtr(this.WindowHandle, (int)NativeAPIs.Enum.WindowLong.GWL_STYLE,
                            new UIntPtr((uint)(NativeAPIs.Enum.WindowStyles.WS_OVERLAPPEDWINDOW 
                                | NativeAPIs.Enum.WindowStyles.WS_CLIPCHILDREN)));
                        windowNewPos = windowPos;
                        windowNewPos.X -= SystemInformation.FrameBorderSize.Width;
                        windowNewPos.Y -= SystemInformation.CaptionHeight + SystemInformation.FrameBorderSize.Height;
                        windowNewPos.Width += SystemInformation.FrameBorderSize.Width * 2;
                        windowNewPos.Height += SystemInformation.CaptionHeight + SystemInformation.FrameBorderSize.Height * 2;
                        break;
                    // Sizable化する
                    default:
                        this.SetWindowBorderStyle(System.Windows.Forms.FormBorderStyle.Sizable);
                        return;
                }

                // ウィンドウに枠の変更を適用させる
                NativeAPIs.SetWindowPos(this.WindowHandle,
                    IntPtr.Zero,
                    windowNewPos.X, windowNewPos.Y, windowNewPos.Width, windowNewPos.Height,
                    (uint)(NativeAPIs.Enum.SetWindowPos.SWP_NOOWNERZORDER
                        | NativeAPIs.Enum.SetWindowPos.SWP_NOACTIVATE
                        | NativeAPIs.Enum.SetWindowPos.SWP_SHOWWINDOW
                        | NativeAPIs.Enum.SetWindowPos.SWP_FRAMECHANGED));

                // 少し待機
                System.Threading.Thread.Sleep(100);
            }

            /// <summary>
            /// ウィンドウの枠の状態を取得
            /// Sizable状態のみ対応。他の状態の場合はFormBorderStyle.Noneが返る。
            /// </summary>
            /// <returns></returns>
            public System.Windows.Forms.FormBorderStyle GetWindowBorderStyle()
            {
                UIntPtr styleData = NativeAPIs.GetWindowLongPtr(this.WindowHandle, (int)NativeAPIs.Enum.WindowLong.GWL_STYLE);

                // オーバーラップウィンドウのフラグが全部立ってるかどうか
                if ((uint)(styleData.ToUInt32() & (uint)NativeAPIs.Enum.WindowStyles.WS_OVERLAPPEDWINDOW)
                    == (uint)NativeAPIs.Enum.WindowStyles.WS_OVERLAPPEDWINDOW)
                    return FormBorderStyle.Sizable;
                else
                    return FormBorderStyle.None;
            }

            public bool IsWindow()
            {
                return NativeAPIs.IsWindow(this.WindowHandle);
            }

            public bool IsTopMostWindow()
            {
                UIntPtr exstyle = NativeAPIs.GetWindowLongPtr(this.WindowHandle, (int)NativeAPIs.Enum.WindowLong.GWL_EXSTYLE);
                return (exstyle.ToUInt32() & (uint)NativeAPIs.Enum.WindowStylesEx.WS_EX_TOPMOST) == (uint)NativeAPIs.Enum.WindowStylesEx.WS_EX_TOPMOST;
            }

            /// <summary>
            /// 保持しているウィンドウの情報をstring化
            /// </summary>
            /// <returns>「ウィンドウタイトル[ハンドル(16進数)]」の形式</returns>
            public override string ToString()
            {
                return this.WindowTitle + "[0x" + this.WindowHandle.ToString("X") + "]";
            }
        }

        /// <summary>
        /// ウィンドウリスト更新終了イベントデリゲート
        /// </summary>
        /// <param name="windowList">ウィンドウリスト</param>
        public delegate void WindowListUpdatedEventHandler(WindowInfo[] windowList);
        /// <summary>
        /// ウィンドウリスト更新終了イベント
        /// </summary>
        public event WindowListUpdatedEventHandler WindowListUpdated;

        public string SeeMinecraftChildWindow(IntPtr hWndParent, string targetWindowClass = "LWJGL")
        {
            string childWindowTitle = null;

            // 子ウィンドウクラス内にtargetWindowClassと同名のウィンドウクラスがあるかどうか
            NativeAPIs.EnumChildWindows(hWndParent, new NativeAPIs.Delegate.EnumWindowsDelegate(
                delegate(IntPtr hWnd, int lParam)
                {
                    StringBuilder wndClassBuffer = new StringBuilder(0x1024);
                    if (0 != NativeAPIs.GetClassName(hWnd, wndClassBuffer, wndClassBuffer.Capacity))
                    {
                        if (targetWindowClass.ToUpper() == wndClassBuffer.ToString().ToUpper())
                        {
                            StringBuilder titleBuffer = new StringBuilder(0x1024);
                            if (NativeAPIs.GetWindowText(hWnd, titleBuffer, titleBuffer.Capacity))
                                childWindowTitle = titleBuffer.ToString();
                        }
                    }
                    return 1;
                }), 0);

            // 「Minecraft Minecraft ***」の場合に最初のMinecraftを削除する
            if (null != childWindowTitle && childWindowTitle.StartsWith("Minecraft Minecraft"))
            {
                childWindowTitle = childWindowTitle.Substring(10);
            }

            return childWindowTitle;
        }

        /// <summary>
        /// マインクラフトのウィンドウを探索
        /// </summary>
        /// <param name="targetWindowClass">ウィンドウクラス名の指定</param>
        /// <returns>発見数</returns>
        public int ScanMinecraftWindow(string targetWindowClass="LWJGL", params string[] targetParentWindowClasses)
        {
            this.targetList.Clear();

            if (targetParentWindowClasses.Length == 0)
            {
                targetParentWindowClasses = new string[1] { "SunAwtFrame" };
            }

            // 探索の成功率をあげるためターゲットクラス名を総て大文字化しておく
            {
                string[] pwcTemp = targetParentWindowClasses;
                targetParentWindowClasses = new string[pwcTemp.Length];

                for (int i = 0; i < pwcTemp.Length; i++)
                    targetParentWindowClasses[i] = pwcTemp[i].ToUpper();
            }


            NativeAPIs.EnumWindows(new NativeAPIs.Delegate.EnumWindowsDelegate(
                delegate(IntPtr hWnd, int lParam)
                {
                    // ウィンドウタイトル取得バッファ
                    StringBuilder titleBuffer = new StringBuilder(0x1024);

                    {
                        string title = null;
                        if (NativeAPIs.GetWindowText(hWnd, titleBuffer, titleBuffer.Capacity))
                        {
                            title = titleBuffer.ToString();
                        }

                        StringBuilder wndClassBuffer = new StringBuilder(0x1024);
                        if (0 != NativeAPIs.GetClassName(hWnd, wndClassBuffer, wndClassBuffer.Capacity))
                        {
                            //System.Diagnostics.Debug.WriteLine("WindowClass => " + wndClassBuffer.ToString() + ", Caption => " + ((null != title)?title: "(null)"));
                        }
                    }

                    // 表示されてるウィンドウのタイトルを取得しマイクラ(LWJGL)のみターゲットリストへ登録
                    if (NativeAPIs.IsWindowVisible(hWnd) != 0
                        && NativeAPIs.GetWindowText(hWnd, titleBuffer, titleBuffer.Capacity))
                    {
                        string wndTitle = titleBuffer.ToString();
                        int pid;
                        NativeAPIs.GetWindowThreadProcessId(hWnd, out pid);
                        Process proc = Process.GetProcessById(pid);

                        StringBuilder wndClassBuffer = new StringBuilder(0x1024);
                        if (0 != NativeAPIs.GetClassName(hWnd, wndClassBuffer, wndClassBuffer.Capacity))
                        {
                            // System.Diagnostics.Debug.WriteLine("WindowClass => " + wndClassBuffer.ToString() + ", Caption => " + titleBuffer.ToString());

                            // ターゲットのウィンドウクラスを発見した場合
                            if (targetWindowClass.ToUpper() == wndClassBuffer.ToString().ToUpper())
                            {
                                if (this.IsAttached && hWnd == this.AttachedWindowInfo.WindowHandle)
                                    this.targetList.Add(this.AttachedWindowInfo);
                                else
                                    this.targetList.Add(new WindowInfo(hWnd, proc, wndTitle));

                            // 子ウィンドウ探索ターゲットに合致し、ターゲットが含まれていた場合
                            } else if (targetParentWindowClasses.Contains(wndClassBuffer.ToString().ToUpper()))
                            {
                                string childWindowTitle = SeeMinecraftChildWindow(hWnd, targetWindowClass);
                                if (null != childWindowTitle)
                                {
                                    if (this.IsAttached && hWnd == this.AttachedWindowInfo.WindowHandle)
                                        this.targetList.Add(this.AttachedWindowInfo);
                                    else
                                        this.targetList.Add(new WindowInfo(hWnd, proc, (string.Empty != childWindowTitle)?childWindowTitle:wndTitle));
                                }
                            }
                        }
                    }
                    return 1;
                }), 0);

            // ウィンドウリスト更新完了イベント
            WindowListUpdated(this.MinecraftWindowList);

            return targetList.Count;
        }

        /// <summary>
        /// ウィンドウにアタッチする
        /// </summary>
        /// <param name="info">アタッチするウィンドウのWindowInfo情報</param>
        /// <returns>失敗したらfalse</returns>
        public bool AttachMinecraftWindow(WindowInfo info)
        {
            if (null == info || IntPtr.Zero == info.WindowHandle)
                return false;

            // アタッチ済ならデタッチする
            if (this.IsAttached)
                this.DetachMinecraftWindow();

            // ウィンドウハンドルが生きているかどうか
            if (!info.IsWindow())
            {
                this.CheckWindowList();
                return false;
            }
            this.AttachedWindowInfo = info;

            return true;
        }

        public void CheckWindowList()
        {
            List<WindowInfo> removeList = new List<WindowInfo>();
            IEnumerator<WindowInfo> i = targetList.GetEnumerator();
            while (i.MoveNext())
            {
                if (!i.Current.IsWindow())
                {
                    removeList.Add(i.Current);
                }
            }

            // 存在しないウィンドウ情報は削除
            if (removeList.Count > 0)
            {
                i.Dispose();
                i = removeList.GetEnumerator();
                while (i.MoveNext())
                    targetList.Remove(i.Current);

                // ウィンドウリスト更新イベント
                this.WindowListUpdated(this.MinecraftWindowList);
            }
        }
        
        /// <summary>
        /// ウィンドウにアタッチする
        /// </summary>
        /// <param name="num">ウィンドウリストの番号</param>
        /// <returns>失敗したらfalse</returns>
        public bool AttachMinecraftWindow(int num)
        {
            return AttachMinecraftWindow(targetList[num]);
        }

        /// <summary>
        /// デタッチする
        /// </summary>
        public void DetachMinecraftWindow()
        {
            this.AttachedWindowInfo = null;
        }
    }
}
