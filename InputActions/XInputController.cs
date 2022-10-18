using SharpDX.XInput;
using System;

namespace InputActions {
    public class XInputController {
        private Controller _controller;

        private int _deadband = 5000;

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

        public Win32Point LeftThumb { get; set; }
        public Win32Point RightThumb { get; set; }

        public XInputController() {
            _controller = new Controller(UserIndex.One);
        }

        public void Update() {
            Connected = _controller.IsConnected;

            if (Connected) {
                BatteryInfo = _controller.GetBatteryInformation(BatteryDeviceType.Gamepad);

                var gamepad = _controller.GetState().Gamepad;

                var leftThumb = new Win32Point();
                var leftThumbXInt = (int)gamepad.LeftThumbX;
                var leftThumbYInt = (int)gamepad.LeftThumbY;
                leftThumb.X = Math.Abs(leftThumbXInt) < _deadband ? 0 : (int)Math.Round((100.0 / short.MaxValue) * Math.Abs(leftThumbXInt) * (leftThumbXInt < 0 ? -1 : 1));
                leftThumb.Y = Math.Abs(leftThumbYInt) < _deadband ? 0 : (int)Math.Round((100.0 / short.MaxValue) * Math.Abs(leftThumbYInt) * (leftThumbYInt > 0 ? -1 : 1));
                LeftThumb = leftThumb;

                var rightThumb = new Win32Point();
                var rightThumbXInt = (int)gamepad.RightThumbX;
                var rightThumbYInt = (int)gamepad.RightThumbY;
                rightThumb.X = Math.Abs(rightThumbXInt) < _deadband ? 0 : (int)Math.Round((100.0 / short.MaxValue) * Math.Abs(rightThumbXInt) * (rightThumbXInt < 0 ? -1 : 1));
                rightThumb.Y = Math.Abs(rightThumbYInt) < _deadband ? 0 : (int)Math.Round((100.0 / short.MaxValue) * Math.Abs(rightThumbYInt) * (rightThumbYInt > 0 ? -1 : 1));
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
