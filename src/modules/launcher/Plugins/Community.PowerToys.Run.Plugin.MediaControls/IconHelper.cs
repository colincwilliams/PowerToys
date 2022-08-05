// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Community.PowerToys.Run.Plugin.MediaControls
{
    using ManagedCommon;

    // Enum names must match image filenames when lowercased.
    internal enum IconType
    {
        MediaControls, // https://flaticons.net/customize.php?dir=Mobile%20Application&icon=Media-Player.png
        Mute, // https://flaticons.net/customize.php?dir=Science%20and%20Technology&icon=Volume-Mute.png
        NextTrack, // https://flaticons.net/customize.php?dir=Application&icon=Media%20Forward.png
        Play, // https://flaticons.net/customize.php?dir=Mobile%20Application&icon=Media-Play.png
        Pause, // https://flaticons.net/customize.php?dir=Application&icon=Media-Pause.png
        PreviousTrack, // https://flaticons.net/customize.php?dir=Application&icon=Media%20Backward.png
        Stop, // https://flaticons.net/customize.php?dir=Application&icon=Stop%20Media.png
        Volume, // https://flaticons.net/customize.php?dir=Mobile%20Application&icon=Volume.png
    }

    internal static class IconHelper
    {
        public static string GetIcon(IconType iconType, Theme theme)
        {
            var themeString = theme switch
            {
                Theme.Light or
                Theme.HighContrastWhite => "light",

                _ => "dark"
            };

            var iconTypeString = iconType.ToString().ToLowerInvariant();

            return $"Images\\{iconTypeString}.{themeString}.png";
        }
    }
}
