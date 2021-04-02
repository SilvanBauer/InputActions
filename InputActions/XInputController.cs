using SharpDX.XInput;
using System;
using System.Windows;

namespace InputActions {
    public class XInputController {
        private Controller _controller;

        private int _deadband = 2500;

        public BatteryInformation BatteryInfo { get; set; }

        public bool Connected { get; set; }
        public bool ADown { get; set; }
        public bool BDown { get; set; }
        public bool XDown { get; set; }
        public bool YDown { get; set; }
        public bool UpDown { get; set; }
        public bool RightDown { get; set; }
        public bool DownDown { get; set; }
        public bool LeftDown { get; set; }
        public bool StartDown { get; set; }
        public bool BackDown { get; set; }
        public bool RightThumbDown { get; set; }
        public bool LeftThumbDown { get; set; }
        public bool RightShoulderDown { get; set; }
        public bool LeftShoulderDown { get; set; }

        public byte RightTrigger { get; set; }
        public byte LeftTrigger { get; set; }

        public Point LeftThumb { get; set; }
        public Point RightThumb { get; set; }

        public XInputController() {
            _controller = new Controller(UserIndex.One);
        }

        public void Update() {
            Connected = _controller.IsConnected;

            if (Connected) {
                BatteryInfo = _controller.GetBatteryInformation(BatteryDeviceType.Gamepad);

                var gamepad = _controller.GetState().Gamepad;

                var leftThumb = new Point(0, 0);
                leftThumb.X = (Math.Abs((float) gamepad.LeftThumbX) < _deadband) ? 0 : (float) gamepad.LeftThumbX / short.MinValue * -100;
                leftThumb.Y = (Math.Abs((float) gamepad.LeftThumbY) < _deadband) ? 0 : (float) gamepad.LeftThumbY / short.MaxValue * -100;
                LeftThumb = leftThumb;

                var rightThumb = new Point(0, 0);
                rightThumb.X = (Math.Abs((float) gamepad.RightThumbX) < _deadband) ? 0 : (float) gamepad.RightThumbX / short.MinValue * -100;
                rightThumb.Y = (Math.Abs((float) gamepad.RightThumbY) < _deadband) ? 0 : (float) gamepad.RightThumbY / short.MinValue * -100;
                RightThumb = rightThumb;

                ADown = gamepad.Buttons == GamepadButtonFlags.A;
                BDown = gamepad.Buttons == GamepadButtonFlags.B;
                XDown = gamepad.Buttons == GamepadButtonFlags.X;
                YDown = gamepad.Buttons == GamepadButtonFlags.Y;
                UpDown = gamepad.Buttons == GamepadButtonFlags.DPadUp;
                RightDown = gamepad.Buttons == GamepadButtonFlags.DPadRight;
                DownDown = gamepad.Buttons == GamepadButtonFlags.DPadDown;
                LeftDown = gamepad.Buttons == GamepadButtonFlags.DPadLeft;
                StartDown = gamepad.Buttons == GamepadButtonFlags.Start;
                BackDown = gamepad.Buttons == GamepadButtonFlags.Back;
                RightThumbDown = gamepad.Buttons == GamepadButtonFlags.RightThumb;
                LeftThumbDown = gamepad.Buttons == GamepadButtonFlags.LeftThumb;
                RightShoulderDown = gamepad.Buttons == GamepadButtonFlags.RightShoulder;
                LeftShoulderDown = gamepad.Buttons == GamepadButtonFlags.LeftShoulder;

                RightTrigger = gamepad.RightTrigger;
                LeftTrigger = gamepad.LeftTrigger;
            }
        }
    }
}
