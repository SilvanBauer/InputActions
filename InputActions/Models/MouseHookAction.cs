using System;

namespace InputActions {
    public class MouseHookAction {
        public string Key { get; set; }

        public Action<int> Action { get; set; }
    }
}
