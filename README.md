# InputActions
A small class library which provides classes with methods to capture user inputs with hooks and to simulate user actions.
This is made possible by using the user32.dll.

This library was created to provide an easy to use solution for capturing and making user inputs with the keyboard or the mouse.
Additionally the Input Actions library also provides support for capturing xbox controller inputs.

## Classes and Methods
### MouseActions
MouseActions is the static class providing input and output methods for mouse actions.
- bool SetMousePosition(int x, int y)
- Win32Point GetMousePosition()
- void RightClick(int x, int y)
- void RightDown(int x, int y)
- void RightUp(int x, int y)
- void MiddleClick(int x, int y)
- void MiddleDown(int x, int y)
- void MiddleUp(int x, int y)
- void LeftClick(int x, int y)
- void LeftDown(int x, int y)
- void LeftUp(int x, int y)
- void ScrollVerticalWheel(int x, int y, int amount)
- void ScrollHorizonalWheel(int x, int y, int amount)
- void AddMouseHookAction(MouseHookAction mouseHookAction)
- void RemoveMouseHookAction(string key)
- void UnhookMouseHook()

### KeyboardActions
KeyboardActions is the static class providing hook methods for keyboard actions.
- void AddKeyboardHookAction(KeyboardHookAction keyboardHookAction)
- void RemoveKeyboardHookAction(string key)
- void UnhookKeyboardHook()
