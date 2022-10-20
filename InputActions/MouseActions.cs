using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace InputActions {
    public static class MouseActions {
        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

        private static readonly LowLevelMouseProc _mouseProc = MouseProc;
        private static readonly Dictionary<string, MouseHookAction> _mouseHookActions = new Dictionary<string, MouseHookAction>();

        private static IntPtr _mouseHook = IntPtr.Zero;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetCursorPos(ref Win32Point pt);

        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc callback, IntPtr hInstance, uint threadId);

        [DllImport("user32.dll")]
        private static extern bool UnhookWindowsHookEx(IntPtr hInstance);

        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr idHook, int nCode, int wParam, IntPtr lParam);

        [DllImport("kernel32.dll")]
        private static extern IntPtr LoadLibrary(string lpFileName);

        public static bool SetMousePosition(int x, int y) => SetCursorPos(x, y);

        public static Win32Point GetMousePosition() {
            var w32Mouse = new Win32Point();

            GetCursorPos(ref w32Mouse);

            return w32Mouse;
        }
        
        public static void RightClick(int x, int y) {
            mouse_event((Int32) MouseEventFlags.RightDown, x, y, 0, 0);
            mouse_event((Int32) MouseEventFlags.RightUp, x, y, 0, 0);
        }

        public static void MiddleClick(int x, int y) {
            mouse_event((Int32) MouseEventFlags.MiddleDown, x, y, 0, 0);
            mouse_event((Int32) MouseEventFlags.MiddleUp, x, y, 0, 0);
        }

        public static void LeftClick(int x, int y) {
            mouse_event((Int32) MouseEventFlags.LeftDown, x, y, 0, 0);
            mouse_event((Int32) MouseEventFlags.LeftUp, x, y, 0, 0);
        }

        public static void RightDown(int x, int y) => mouse_event((Int32) MouseEventFlags.RightDown, x, y, 0, 0);

        public static void RightUp(int x, int y) => mouse_event((Int32) MouseEventFlags.RightUp, x, y, 0, 0);

        public static void MiddleDown(int x, int y) => mouse_event((Int32) MouseEventFlags.MiddleDown, x, y, 0, 0);

        public static void MiddleUp(int x, int y) => mouse_event((Int32) MouseEventFlags.MiddleUp, x, y, 0, 0);

        public static void LeftDown(int x, int y) => mouse_event((Int32) MouseEventFlags.LeftDown, x, y, 0, 0);

        public static void LeftUp(int x, int y) => mouse_event((Int32) MouseEventFlags.LeftUp, x, y, 0, 0);

        public static void ScrollVerticalWheel(int x, int y, int amount) => mouse_event((Int32) MouseEventFlags.VWeel, x, y, amount, 0);

        public static void ScrollHorizonalWheel(int x, int y, int amount) => mouse_event((Int32) MouseEventFlags.HWeel, x, y, amount, 0);

        public static void AddMouseHookAction(MouseHookAction mouseHookAction) {
            _mouseHookActions.Add(mouseHookAction.Key, mouseHookAction);

            if (_mouseHook == IntPtr.Zero) {
                IntPtr hInstance = LoadLibrary("User32");

                _mouseHook = SetWindowsHookEx(14, _mouseProc, hInstance, 0); // 14 = WH_MOUSE_LL
            }
        }

        public static void RemoveMouseHookAction(string key) {
            if (_mouseHookActions.ContainsKey(key)) {
                if (_mouseHookActions.Count == 1) {
                    UnhookMouseHook();
                } else {
                    _mouseHookActions.Remove(key);
                }
            }
        }

        public static void UnhookMouseHook() {
            if (_mouseHook != IntPtr.Zero) {
                UnhookWindowsHookEx(_mouseHook);

                _mouseHookActions.Clear();
                _mouseHook = IntPtr.Zero;
            }
        }

        private static IntPtr MouseProc(int code, IntPtr wParam, IntPtr lParam) {
            if (code >= 0) {
                foreach (var mouseHookAction in _mouseHookActions) {
                    mouseHookAction.Value.Action((int)wParam);
                }
            }

            return CallNextHookEx(_mouseHook, code, (int)wParam, lParam);
        }
    }
}
