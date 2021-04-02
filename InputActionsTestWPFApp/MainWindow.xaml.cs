using InputActions;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace InputActionsTestWPFApp {
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

            Console.WriteLine($"{(keyName != null ? keyName : keyCode.ToString())} / A is pressed: {Keyboard.IsKeyDown(Key.A)}");
        }

        private void TestMouseHookAction(int mouseAction) {
            var mouseActionName = Enum.GetName(typeof(MouseEvent), mouseAction);

            Console.WriteLine(mouseActionName);
        }

        private void Window_Closing(object sender, CancelEventArgs e) {
            KeyboardActions.UnhookKeyboardHook();
            MouseActions.UnhookMouseHook();
        }
    }
}
