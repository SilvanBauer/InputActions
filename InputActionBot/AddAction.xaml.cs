using InputActions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace InputActionBot {
    /// <summary>
    /// Interaction logic for AddAction.xaml
    /// </summary>
    public partial class AddAction : Window, INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        public int SelectedActionTypeIndex { get; set; }

        public List<string> ActionTypes { get; set; }

        public string ActionValue { get; set; }

        private string _mousePosition;
        public string MousePosition {
            get => _mousePosition;
            set {
                _mousePosition = value;
                PropertyHasChanged();
            }
        }

        public string Times { get; set; }

        public AddAction() {
            InitializeComponent();

            KeyboardActions.AddKeyboardHookAction(new KeyboardHookAction() {
                Key = "AddActionKeyboardHook",
                Action = KeyboardHook,
                KeyboardEvent = KeyboardEvent.KeyDown
            });

            DataContext = this;
            ActionTypes = new List<string>() {
                "Move Mouse",
                "Left Down",
                "Left Up",
                "Left Click",
                "Right Down",
                "Right Up",
                "Right Click",
                "Wait"
            };
        }

        private void KeyboardHook(int keyCode) {
            if (keyCode == (int)KeyboardKey.P) {
                var position = MouseActions.GetMousePosition();

                MousePosition = $"{position.X}/{position.Y}";
            }
        }

        private void Ok(object sender, RoutedEventArgs e) {
            if (!Int32.TryParse(Times, out var times)) {
                MessageBox.Show("Times is not a number.", "TImes not a number", MessageBoxButton.OK, MessageBoxImage.Error);
            } else {
                var actionType = ActionTypes[SelectedActionTypeIndex];
                var actionTypeId = 0;

                switch(actionType) {
                    case "Move Mouse":
                        actionTypeId = 1;
                        break;
                    case "Left Down":
                        actionTypeId = 2;
                        break;
                    case "Left Up":
                        actionTypeId = 3;
                        break;
                    case "Left Click":
                        actionTypeId = 4;
                        break;
                    case "Right Down":
                        actionTypeId = 5;
                        break;
                    case "Right Up":
                        actionTypeId = 6;
                        break;
                    case "Right Click":
                        actionTypeId = 7;
                        break;
                    case "Wait":
                        actionTypeId = 8;
                        break;
                }

                if (actionTypeId == 1 && MousePosition == "") {
                    MessageBox.Show("You need to set mouse position with P.", "Mouse position not set", MessageBoxButton.OK, MessageBoxImage.Error);
                } else if (actionTypeId == 8 && !Int32.TryParse(ActionValue, out var value)) {
                    MessageBox.Show("Value is not a number.", "Value not a number", MessageBoxButton.OK, MessageBoxImage.Error);
                } else {

                }
            }
        }

        private void WindowClosing(object sender, CancelEventArgs e) => KeyboardActions.RemoveKeyboardHookAction("AddActionKeyboardHook");

        private void PropertyHasChanged([CallerMemberName] string name = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
