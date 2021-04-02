using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace InputActions {
    public class KeyboardActions {
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        private static readonly LowLevelKeyboardProc _keyboardProc = KeyboardProc;
        private static readonly Dictionary<string, KeyboardHookAction> _keyboardHookActions = new Dictionary<string, KeyboardHookAction>();

        private static IntPtr _keyboardHook = IntPtr.Zero;

        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc callback, IntPtr hInstance, uint threadId);

        [DllImport("user32.dll")]
        private static extern bool UnhookWindowsHookEx(IntPtr hInstance);

        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr idHook, int nCode, int wParam, IntPtr lParam);

        [DllImport("kernel32.dll")]
        private static extern IntPtr LoadLibrary(string lpFileName);

        public static void AddKeyboardHookAction(KeyboardHookAction keyboardHookAction) {
            _keyboardHookActions.Add(keyboardHookAction.Key, keyboardHookAction);

            if (_keyboardHook == IntPtr.Zero) {
                IntPtr hInstance = LoadLibrary("User32");

                _keyboardHook = SetWindowsHookEx(13, _keyboardProc, hInstance, 0); // 13 = WH_KEYBOARD_LL
            }
        }

        public static void RemoveKeyboardHookAction(string key) {
            if (_keyboardHookActions.ContainsKey(key)) {
                if (_keyboardHookActions.Count == 1) {
                    UnhookKeyboardHook();
                } else {
                    _keyboardHookActions.Remove(key);
                }
            }
        }

        public static void UnhookKeyboardHook() {
            if (_keyboardHook != IntPtr.Zero) {
                UnhookWindowsHookEx(_keyboardHook);

                _keyboardHookActions.Clear();
                _keyboardHook = IntPtr.Zero;
            }
        }

        private static IntPtr KeyboardProc(int code, IntPtr wParam, IntPtr lParam) {
            if (code >= 0) {
                var vkCode = Marshal.ReadInt32(lParam);

                foreach (var keyboardHookAction in _keyboardHookActions) {
                    if (wParam == (IntPtr)keyboardHookAction.Value.KeyboardEvent) {
                        keyboardHookAction.Value.Action(vkCode);
                    }
                }
            }

            return CallNextHookEx(_keyboardHook, code, (int)wParam, lParam);
        }
    }
}
