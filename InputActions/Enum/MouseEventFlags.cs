﻿using System;

namespace InputActions {
    [Flags]
    public enum MouseEventFlags {
        LeftDown = 0x00000002,
        LeftUp = 0x00000004,
        MiddleDown = 0x00000020,
        MiddleUp = 0x00000040,
        Move = 0x00000001,
        Absolute = 0x00008000,
        RightDown = 0x00000008,
        RightUp = 0x00000010,
        VWeel = 0x00000800,
        HWeel = 0x00001000
    }
}
