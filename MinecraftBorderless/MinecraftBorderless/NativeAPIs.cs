using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MinecraftBorderless
{
    public static class NativeAPIs
    {
        // デリゲート
        public static class Delegate
        {
            public delegate int EnumWindowsDelegate(IntPtr hWnd, int lParam);
        }

        // 構造体
        public static class Struct
        {
            [StructLayout(LayoutKind.Sequential, Pack = 4)]
            public struct RECT
            {
                public int left;
                public int top;
                public int right;
                public int bottom;
            }
        }

        // 列挙体
        public static class Enum
        {
            public enum WindowLong : int
            {
                GWL_WNDPROC = -4,
                GWL_HINSTANCE = -6,
                GWL_HWNDPARENT = -8,
                GWL_STYLE = -16,
                GWL_EXSTYLE = -20,
                GWL_USERDATA = -21,
                GWL_ID = -12,
                DWLP_USER = 0x8,
                DWLP_MSGRESULT = 0x0,
                DWLP_DLGPROC = 0x4
            }

            [Flags()]
            public enum SetWindowPos : uint
            {
                /// <summary>If the calling thread and the thread that owns the window are attached to different input queues, 
                /// the system posts the request to the thread that owns the window. This prevents the calling thread from 
                /// blocking its execution while other threads process the request.</summary>
                /// <remarks>SWP_ASYNCWINDOWPOS</remarks>
                SWP_ASYNCWINDOWPOS = 0x4000,
                /// <summary>Prevents generation of the WM_SYNCPAINT message.</summary>
                /// <remarks>SWP_DEFERERASE</remarks>
                SWP_DEFERERASE = 0x2000,
                /// <summary>Draws a frame (defined in the window's class description) around the window.</summary>
                /// <remarks>SWP_DRAWFRAME</remarks>
                SWP_DRAWFRAME = 0x0020,
                /// <summary>Applies new frame styles set using the SetWindowLong function. Sends a WM_NCCALCSIZE message to 
                /// the window, even if the window's size is not being changed. If this flag is not specified, WM_NCCALCSIZE 
                /// is sent only when the window's size is being changed.</summary>
                /// <remarks>SWP_FRAMECHANGED</remarks>
                SWP_FRAMECHANGED = 0x0020,
                /// <summary>Hides the window.</summary>
                /// <remarks>SWP_HIDEWINDOW</remarks>
                SWP_HIDEWINDOW = 0x0080,
                /// <summary>Does not activate the window. If this flag is not set, the window is activated and moved to the 
                /// top of either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter 
                /// parameter).</summary>
                /// <remarks>SWP_NOACTIVATE</remarks>
                SWP_NOACTIVATE = 0x0010,
                /// <summary>Discards the entire contents of the client area. If this flag is not specified, the valid 
                /// contents of the client area are saved and copied back into the client area after the window is sized or 
                /// repositioned.</summary>
                /// <remarks>SWP_NOCOPYBITS</remarks>
                SWP_NOCOPYBITS = 0x0100,
                /// <summary>Retains the current position (ignores X and Y parameters).</summary>
                /// <remarks>SWP_NOMOVE</remarks>
                SWP_NOMOVE = 0x0002,
                /// <summary>Does not change the owner window's position in the Z order.</summary>
                /// <remarks>SWP_NOOWNERZORDER</remarks>
                SWP_NOOWNERZORDER = 0x0200,
                /// <summary>Does not redraw changes. If this flag is set, no repainting of any kind occurs. This applies to 
                /// the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent 
                /// window uncovered as a result of the window being moved. When this flag is set, the application must 
                /// explicitly invalidate or redraw any parts of the window and parent window that need redrawing.</summary>
                /// <remarks>SWP_NOREDRAW</remarks>
                SWP_NOREDRAW = 0x0008,
                /// <summary>Same as the SWP_NOOWNERZORDER flag.</summary>
                /// <remarks>SWP_NOREPOSITION</remarks>
                SWP_NOREPOSITION = 0x0200,
                /// <summary>Prevents the window from receiving the WM_WINDOWPOSCHANGING message.</summary>
                /// <remarks>SWP_NOSENDCHANGING</remarks>
                SWP_NOSENDCHANGING = 0x0400,
                /// <summary>Retains the current size (ignores the cx and cy parameters).</summary>
                /// <remarks>SWP_NOSIZE</remarks>
                SWP_NOSIZE = 0x0001,
                /// <summary>Retains the current Z order (ignores the hWndInsertAfter parameter).</summary>
                /// <remarks>SWP_NOZORDER</remarks>
                SWP_NOZORDER = 0x0004,
                /// <summary>Displays the window.</summary>
                /// <remarks>SWP_SHOWWINDOW</remarks>
                SWP_SHOWWINDOW = 0x0040
            }

            /// <summary>
            /// Window Styles.
            /// The following styles can be specified wherever a window style is required. After the control has been created, these styles cannot be modified, except as noted.
            /// </summary>
            [Flags()]
            public enum WindowStyles : uint
            {
                /// <summary>The window has a thin-line border.</summary>
                WS_BORDER = 0x00800000,

                /// <summary>The window has a title bar (includes the WS_BORDER style).</summary>
                WS_CAPTION = 0x00c00000,

                /// <summary>The window is a child window. A window with this style cannot have a menu bar. This style cannot be used with the WS_POPUP style.</summary>
                WS_CHILD = 0x40000000,

                /// <summary>Excludes the area occupied by child windows when drawing occurs within the parent window. This style is used when creating the parent window.</summary>
                WS_CLIPCHILDREN = 0x02000000,

                /// <summary>
                /// Clips child windows relative to each other; that is, when a particular child window receives a WM_PAINT message, the WS_CLIPSIBLINGS style clips all other overlapping child windows out of the region of the child window to be updated.
                /// If WS_CLIPSIBLINGS is not specified and child windows overlap, it is possible, when drawing within the client area of a child window, to draw within the client area of a neighboring child window.
                /// </summary>
                WS_CLIPSIBLINGS = 0x04000000,

                /// <summary>The window is initially disabled. A disabled window cannot receive input from the user. To change this after a window has been created, use the EnableWindow function.</summary>
                WS_DISABLED = 0x08000000,

                /// <summary>The window has a border of a style typically used with dialog boxes. A window with this style cannot have a title bar.</summary>
                WS_DLGFRAME = 0x00400000,

                /// <summary>
                /// The window is the first control of a group of controls. The group consists of this first control and all controls defined after it, up to the next control with the WS_GROUP style.
                /// The first control in each group usually has the WS_TABSTOP style so that the user can move from group to group. The user can subsequently change the keyboard focus from one control in the group to the next control in the group by using the direction keys.
                /// You can turn this style on and off to change dialog box navigation. To change this style after a window has been created, use the SetWindowLong function.
                /// </summary>
                WS_GROUP = 0x00020000,

                /// <summary>The window has a horizontal scroll bar.</summary>
                WS_HSCROLL = 0x00100000,

                /// <summary>The window is initially maximized.</summary> 
                WS_MAXIMIZE = 0x01000000,

                /// <summary>The window has a maximize button. Cannot be combined with the WS_EX_CONTEXTHELP style. The WS_SYSMENU style must also be specified.</summary> 
                WS_MAXIMIZEBOX = 0x00010000,

                /// <summary>The window is initially minimized.</summary>
                WS_MINIMIZE = 0x20000000,

                /// <summary>The window has a minimize button. Cannot be combined with the WS_EX_CONTEXTHELP style. The WS_SYSMENU style must also be specified.</summary>
                WS_MINIMIZEBOX = 0x00020000,

                /// <summary>The window is an overlapped window. An overlapped window has a title bar and a border.</summary>
                WS_OVERLAPPED = 0x00000000,

                /// <summary>The window is an overlapped window.</summary>
                WS_OVERLAPPEDWINDOW = WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_SIZEFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX,

                /// <summary>The window is a pop-up window. This style cannot be used with the WS_CHILD style.</summary>
                WS_POPUP = 0x80000000u,

                /// <summary>The window is a pop-up window. The WS_CAPTION and WS_POPUPWINDOW styles must be combined to make the window menu visible.</summary>
                WS_POPUPWINDOW = WS_POPUP | WS_BORDER | WS_SYSMENU,

                /// <summary>The window has a sizing border.</summary>
                WS_SIZEFRAME = 0x00040000,

                /// <summary>The window has a window menu on its title bar. The WS_CAPTION style must also be specified.</summary>
                WS_SYSMENU = 0x00080000,

                /// <summary>
                /// The window is a control that can receive the keyboard focus when the user presses the TAB key.
                /// Pressing the TAB key changes the keyboard focus to the next control with the WS_TABSTOP style.  
                /// You can turn this style on and off to change dialog box navigation. To change this style after a window has been created, use the SetWindowLong function.
                /// For user-created windows and modeless dialogs to work with tab stops, alter the message loop to call the IsDialogMessage function.
                /// </summary>
                WS_TABSTOP = 0x00010000,

                /// <summary>The window is initially visible. This style can be turned on and off by using the ShowWindow or SetWindowPos function.</summary>
                WS_VISIBLE = 0x10000000,

                /// <summary>The window has a vertical scroll bar.</summary>
                WS_VSCROLL = 0x00200000
            }

            [Flags]
            public enum WindowStylesEx : uint
            {
                /// <summary>
                /// Specifies that a window created with this style accepts drag-drop files.
                /// </summary>
                WS_EX_ACCEPTFILES = 0x00000010,
                /// <summary>
                /// Forces a top-level window onto the taskbar when the window is visible.
                /// </summary>
                WS_EX_APPWINDOW = 0x00040000,
                /// <summary>
                /// Specifies that a window has a border with a sunken edge.
                /// </summary>
                WS_EX_CLIENTEDGE = 0x00000200,
                /// <summary>
                /// Windows XP: Paints all descendants of a window in bottom-to-top painting order using double-buffering. For more information, see Remarks. This cannot be used if the window has a class style of either CS_OWNDC or CS_CLASSDC. 
                /// </summary>
                WS_EX_COMPOSITED = 0x02000000,
                /// <summary>
                /// Includes a question mark in the title bar of the window. When the user clicks the question mark, the cursor changes to a question mark with a pointer. If the user then clicks a child window, the child receives a WM_HELP message. The child window should pass the message to the parent window procedure, which should call the WinHelp function using the HELP_WM_HELP command. The Help application displays a pop-up window that typically contains help for the child window.
                /// WS_EX_CONTEXTHELP cannot be used with the WS_MAXIMIZEBOX or WS_MINIMIZEBOX styles.
                /// </summary>
                WS_EX_CONTEXTHELP = 0x00000400,
                /// <summary>
                /// The window itself contains child windows that should take part in dialog box navigation. If this style is specified, the dialog manager recurses into children of this window when performing navigation operations such as handling the TAB key, an arrow key, or a keyboard mnemonic.
                /// </summary>
                WS_EX_CONTROLPARENT = 0x00010000,
                /// <summary>
                /// Creates a window that has a double border; the window can, optionally, be created with a title bar by specifying the WS_CAPTION style in the dwStyle parameter.
                /// </summary>
                WS_EX_DLGMODALFRAME = 0x00000001,
                /// <summary>
                /// Windows 2000/XP: Creates a layered window. Note that this cannot be used for child windows. Also, this cannot be used if the window has a class style of either CS_OWNDC or CS_CLASSDC. 
                /// </summary>
                WS_EX_LAYERED = 0x00080000,
                /// <summary>
                /// Arabic and Hebrew versions of Windows 98/Me, Windows 2000/XP: Creates a window whose horizontal origin is on the right edge. Increasing horizontal values advance to the left. 
                /// </summary>
                WS_EX_LAYOUTRTL = 0x00400000,
                /// <summary>
                /// Creates a window that has generic left-aligned properties. This is the default.
                /// </summary>
                WS_EX_LEFT = 0x00000000,
                /// <summary>
                /// If the shell language is Hebrew, Arabic, or another language that supports reading order alignment, the vertical scroll bar (if present) is to the left of the client area. For other languages, the style is ignored.
                /// </summary>
                WS_EX_LEFTSCROLLBAR = 0x00004000,
                /// <summary>
                /// The window text is displayed using left-to-right reading-order properties. This is the default.
                /// </summary>
                WS_EX_LTRREADING = 0x00000000,
                /// <summary>
                /// Creates a multiple-document interface (MDI) child window.
                /// </summary>
                WS_EX_MDICHILD = 0x00000040,
                /// <summary>
                /// Windows 2000/XP: A top-level window created with this style does not become the foreground window when the user clicks it. The system does not bring this window to the foreground when the user minimizes or closes the foreground window. 
                /// To activate the window, use the SetActiveWindow or SetForegroundWindow function.
                /// The window does not appear on the taskbar by default. To force the window to appear on the taskbar, use the WS_EX_APPWINDOW style.
                /// </summary>
                WS_EX_NOACTIVATE = 0x08000000,
                /// <summary>
                /// Windows 2000/XP: A window created with this style does not pass its window layout to its child windows.
                /// </summary>
                WS_EX_NOINHERITLAYOUT = 0x00100000,
                /// <summary>
                /// Specifies that a child window created with this style does not send the WM_PARENTNOTIFY message to its parent window when it is created or destroyed.
                /// </summary>
                WS_EX_NOPARENTNOTIFY = 0x00000004,
                /// <summary>
                /// Combines the WS_EX_CLIENTEDGE and WS_EX_WINDOWEDGE styles.
                /// </summary>
                WS_EX_OVERLAPPEDWINDOW = WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE,
                /// <summary>
                /// Combines the WS_EX_WINDOWEDGE, WS_EX_TOOLWINDOW, and WS_EX_TOPMOST styles.
                /// </summary>
                WS_EX_PALETTEWINDOW = WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST,
                /// <summary>
                /// The window has generic "right-aligned" properties. This depends on the window class. This style has an effect only if the shell language is Hebrew, Arabic, or another language that supports reading-order alignment; otherwise, the style is ignored.
                /// Using the WS_EX_RIGHT style for static or edit controls has the same effect as using the SS_RIGHT or ES_RIGHT style, respectively. Using this style with button controls has the same effect as using BS_RIGHT and BS_RIGHTBUTTON styles.
                /// </summary>
                WS_EX_RIGHT = 0x00001000,
                /// <summary>
                /// Vertical scroll bar (if present) is to the right of the client area. This is the default.
                /// </summary>
                WS_EX_RIGHTSCROLLBAR = 0x00000000,
                /// <summary>
                /// If the shell language is Hebrew, Arabic, or another language that supports reading-order alignment, the window text is displayed using right-to-left reading-order properties. For other languages, the style is ignored.
                /// </summary>
                WS_EX_RTLREADING = 0x00002000,
                /// <summary>
                /// Creates a window with a three-dimensional border style intended to be used for items that do not accept user input.
                /// </summary>
                WS_EX_STATICEDGE = 0x00020000,
                /// <summary>
                /// Creates a tool window; that is, a window intended to be used as a floating toolbar. A tool window has a title bar that is shorter than a normal title bar, and the window title is drawn using a smaller font. A tool window does not appear in the taskbar or in the dialog that appears when the user presses ALT+TAB. If a tool window has a system menu, its icon is not displayed on the title bar. However, you can display the system menu by right-clicking or by typing ALT+SPACE. 
                /// </summary>
                WS_EX_TOOLWINDOW = 0x00000080,
                /// <summary>
                /// Specifies that a window created with this style should be placed above all non-topmost windows and should stay above them, even when the window is deactivated. To add or remove this style, use the SetWindowPos function.
                /// </summary>
                WS_EX_TOPMOST = 0x00000008,
                /// <summary>
                /// Specifies that a window created with this style should not be painted until siblings beneath the window (that were created by the same thread) have been painted. The window appears transparent because the bits of underlying sibling windows have already been painted.
                /// To achieve transparency without these restrictions, use the SetWindowRgn function.
                /// </summary>
                WS_EX_TRANSPARENT = 0x00000020,
                /// <summary>
                /// Specifies that a window has a border with a raised edge.
                /// </summary>
                WS_EX_WINDOWEDGE = 0x00000100
            }

            public enum ShowWindow : int
            {
                SW_HIDE = 0,
                SW_SHOWNORMAL = 1,
                SW_NORMAL = 1,
                SW_SHOWMINIMIZED = 2,
                SW_SHOWMAXIMIZED = 3,
                SW_MAXIMIZE = 3,
                SW_SHOWNOACTIVATE = 4,
                SW_SHOW = 5,
                SW_MINIMIZE = 6,
                SW_SHOWMINNOACTIVE = 7,
                SW_SHOWNA = 8,
                SW_RESTORE = 9,
                SW_SHOWDEFAULT = 10,
                SW_FORCEMINIMIZE = 11,
                SW_MAX = 11
            }

            public enum HWND : int {
                HWND_NOTOPMOST = -2,
                HWND_TOPMOST = -1,
                HWND_TOP = 0,
                HWND_BOTTOM = 1
            }
        }

        // Win32APIインポート
        [DllImport("USER32.dll")]
        public static extern IntPtr FindWindowEx(
            IntPtr hWndParent, IntPtr hWndChildAfter,
            string strClass, string strWindows);

        [DllImport("USER32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowText(
            IntPtr hWnd, StringBuilder lpString,
            int nMaxCount);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumWindows(
            Delegate.EnumWindowsDelegate lpEnumFunc,
            int lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumChildWindows(
            IntPtr hWndParent,
            Delegate.EnumWindowsDelegate lpEnumFunc,
            int lParam);

        [DllImport("user32.dll")]
        public static extern int IsWindowVisible(
            IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern uint GetWindowThreadProcessId(
            IntPtr hWnd, out int lpdwProcessId);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(
            IntPtr hWnd, out Struct.RECT lpRect);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetClassName(
            IntPtr hWnd, StringBuilder lpClassName,
            int nMaxCount);


        [DllImport("User32.Dll")]
        public static extern int ShowWindow(
            IntPtr hWnd, int nCmdShow);

        /// <summary>
        /// SetWindowLongPtrの32/64bitセレクタ内蔵品
        /// </summary>
        /// <param name="hWnd">ウィンドウのハンドル</param>
        /// <param name="nIndex">変更する値のオフセット</param>
        /// <param name="dwNewLong">新しい値(NativeAPIs.WindowLongFlagsを使用)</param>
        /// <returns></returns>
        public static UIntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, UIntPtr dwNewLong)
        {
            if (IntPtr.Size == 8)
                return SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
            else
                return new UIntPtr(SetWindowLongPtr32(hWnd, nIndex, dwNewLong.ToUInt32()));
        }
        [DllImport("User32.Dll", EntryPoint = "SetWindowLong")]
        private static extern uint SetWindowLongPtr32(
            IntPtr hWnd, int nIndex,
            uint dwNewLong);
        [DllImport("User32.Dll", EntryPoint = "SetWindowLongPtr")]
        private static extern UIntPtr SetWindowLongPtr64(
            IntPtr hWnd, int nIndex,
            UIntPtr dwNewLong);

        /// <summary>
        /// GetWindowLongPtrの32/64bitセレクタ内蔵品
        /// </summary>
        /// <param name="hWnd">ウィンドウのハンドル</param>
        /// <param name="nIndex">取得する値のオフセット</param>
        /// <returns></returns>
        public static UIntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex)
        {
            if (IntPtr.Size == 8)
                return GetWindowLongPtr64(hWnd, nIndex);
            else
                return GetWindowLongPtr32(hWnd, nIndex);
        }
        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        private static extern UIntPtr GetWindowLongPtr32(
            IntPtr hWnd, int nIndex);
        [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr")]
        private static extern UIntPtr GetWindowLongPtr64(
            IntPtr hWnd, int nIndex);


        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(
            IntPtr hWnd, IntPtr hWndInsertAfter,
            int x, int y,
            int cx, int cy,
            uint uFlags);

        [DllImport("user32.dll")]
        public static extern IntPtr SetCapture(
            IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindow(
            IntPtr hWnd);
    }
}
