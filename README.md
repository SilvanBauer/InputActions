# InputActions
A small class library which provides classes with methods to capture user inputs with hooks and to simulate user actions.
This is made possible by using the user32.dll.

This library was created to provide an easy to use solution for capturing and making user inputs with the keyboard or the mouse.

## Classes and Methods
### MouseActions
`MouseActions` is the static class providing user input and output methods for mouse actions.
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
`KeyboardActions` is the static class providing user input and output methods for keyboard actions.
- KeyState GetKey(KeyboardKey key)
- void KeyPress(KeyboardKey key)
- void KeyUp(KeyboardKey key)
- void KeyDown(KeyboardKey key)
- void AddKeyboardHookAction(KeyboardHookAction keyboardHookAction)
- void RemoveKeyboardHookAction(string key)
- void UnhookKeyboardHook()

### Hooks
When a add hook method is used a windows hook is created and the hook action is added. With the add command the same hook will have another action registered instead of creating another hook. With the remove method a registered hook action can be removed. If the last one was removed then the hook will be unhooked. To unhook all hooks at once there is the unhook methods.

```cs
KeyboardActions.AddKeyboardHookAction(new KeyboardHookAction() {
    Key = "Test",
    Action = TestKeyboardHookAction,
    KeyboardEvent = KeyboardEvent.KeyDown
});
MouseActions.AddMouseHookAction(new MouseHookAction() {
    Key = "Test",
    Action = TestMouseHookAction
});
```

```cs
private void TestKeyboardHookAction(int keyCode) {
    var keyName = Enum.GetName(typeof(KeyboardKey), keyCode);
    var keyState = Enum.GetName(typeof(KeyState), KeyboardActions.GetKey(KeyboardKey.LShift));

    Trace.WriteLine(keyName != null ? keyName : keyCode.ToString());
    Trace.WriteLine(keyState);
}
```

```cs
private void TestMouseHookAction(int mouseAction) {
    var mouseActionName = Enum.GetName(typeof(MouseEvent), mouseAction);
    Trace.WriteLine(mouseActionName);

    KeyboardActions.KeyDown(KeyboardKey.LShift);
    KeyboardActions.KeyPress(KeyboardKey.A);
    KeyboardActions.KeyPress(KeyboardKey.B);
    KeyboardActions.KeyPress(KeyboardKey.C);
    KeyboardActions.KeyUp(KeyboardKey.LShift);
}
```

## Links
This class library is available on [NuGet](https://www.nuget.org/packages/SilvanBauer.InputActions).
