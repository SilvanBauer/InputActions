using System;

namespace InputActions {
    public class KeyboardHookAction {
        public string Key { get; set; }

        public Action<int> Action { get; set; }

        public KeyboardEvent KeyboardEvent { get; set; }
    }
}
