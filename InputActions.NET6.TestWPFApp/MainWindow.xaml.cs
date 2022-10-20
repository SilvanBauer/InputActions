using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace InputActions.NET6.TestWPFApp {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            KeyboardActions.AddKeyboardHookAction(new KeyboardHookAction() {
                Key = "Test",
                Action = TestKeyboardHookAction,
                KeyboardEvent = KeyboardEvent.KeyDown
            });
            MouseActions.AddMouseHookAction(new MouseHookAction() {
                Key = "Test",
                Action = TestMouseHookAction
            });
        }

        private void TestKeyboardHookAction(int keyCode) {
            var keyName = Enum.GetName(typeof(KeyboardKey), keyCode);

            Trace.WriteLine($"{(keyName != null ? keyName : keyCode.ToString())} / A still pressed: {Keyboard.IsKeyDown(Key.A)} / LShift pressed: {Keyboard.IsKeyDown(Key.LeftShift)}");
        }

        private void TestMouseHookAction(int mouseAction) {
            var mouseActionName = Enum.GetName(typeof(MouseEvent), mouseAction);

            Trace.WriteLine(mouseActionName);
        }

        private void Window_Closing(object sender, CancelEventArgs e) {
            KeyboardActions.UnhookKeyboardHook();
            MouseActions.UnhookMouseHook();
        }
    }
}
