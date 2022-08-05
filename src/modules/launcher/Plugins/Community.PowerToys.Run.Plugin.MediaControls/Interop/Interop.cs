// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Community.PowerToys.Run.Plugin.MediaControls
{
    using System;
    using System.Runtime.InteropServices;

    internal class Interop
    {
        public static void SendKey(VirtualKeyShort virtualKey)
        {
            var input = new INPUT
            {
                type = InputType.INPUT_KEYBOARD,
                U = new InputUnion
                {
                    ki = new KEYBDINPUT
                    {
                        wVk = virtualKey,
                        wScan = 0,
                        dwFlags = 0,
                        time = 0,
                        dwExtraInfo = (UIntPtr)0,
                    },
                },
            };

            var inputs = new INPUT[] { input };

            var insertions = SendInputInterop.SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(input));
            if (insertions == 0)
            {
                throw new InvalidOperationException($"Community.PowerToys.Run.Plugin.MediaControls: {nameof(Interop)}: {nameof(SendKey)}: Failed to send key: {virtualKey}");
            }
        }
    }
}
