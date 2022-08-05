// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Community.PowerToys.Run.Plugin.MediaControls
{
    using System;
    using ManagedCommon;
    using Wox.Plugin;

    internal static class MediaControlResults
    {
        public static MediaControlResult Mute => new ()
        {
            IconType = IconType.Mute,
            VirtualKey = VirtualKeyShort.VOLUME_MUTE,
            Title = "Mute",
            SubTitle = "Mute system volume",
        };

        public static MediaControlResult NextTrack => new ()
        {
            IconType = IconType.NextTrack,
            VirtualKey = VirtualKeyShort.MEDIA_NEXT_TRACK,
            Title = "Next Track",
            SubTitle = "Move to the next track",
        };

        public static MediaControlResult Pause => new ()
        {
            IconType = IconType.Pause,
            VirtualKey = VirtualKeyShort.MEDIA_PLAY_PAUSE,
            Title = "Pause",
            SubTitle = "Pause current media",
        };

        public static MediaControlResult Play => new ()
        {
            IconType = IconType.Play,
            VirtualKey = VirtualKeyShort.MEDIA_PLAY_PAUSE,
            Title = "Play",
            SubTitle = "Resume current media",
        };

        public static MediaControlResult PreviousTrack => new ()
        {
            IconType = IconType.PreviousTrack,
            VirtualKey = VirtualKeyShort.MEDIA_PREV_TRACK,
            Title = "Previous Track",
            SubTitle = "Move to the previous track",
        };

        public static MediaControlResult Stop => new ()
        {
            IconType = IconType.Stop,
            VirtualKey = VirtualKeyShort.MEDIA_STOP,
            Title = "Stop",
            SubTitle = "Stop current media playback",
        };

        public static MediaControlResult VolumeDown => new ()
        {
            IconType = IconType.Volume,
            VirtualKey = VirtualKeyShort.VOLUME_DOWN,
            Title = "Volume Down",
            SubTitle = "Reduce system volume",
        };

        public static MediaControlResult VolumeUp => new ()
        {
            IconType = IconType.Volume,
            VirtualKey = VirtualKeyShort.VOLUME_UP,
            Title = "Volume Up",
            SubTitle = "Increase System Volume",
        };
    }

    internal struct MediaControlResult
    {
        public const int HighScore = 10_000;
        public const int MediumScore = 5_000;
        public const int NoMatchScore = -1;

        public IconType IconType { get; set; }

        public string SubTitle { get; set; }

        public string Title { get; set; }

        public VirtualKeyShort VirtualKey { get; set; }

        public Result GetResult(string search, Theme theme)
        {
            // Take a copy of the virtual key for capturing in the Action
            var virtualKey = VirtualKey;

            return new Result
            {
                Title = Title,
                SubTitle = SubTitle,
                Score = GetScore(search),
                IcoPath = IconHelper.GetIcon(IconType, theme),
                Action = actionContext =>
                {
                    Interop.SendKey(virtualKey);
                    return false;
                },
            };
        }

        private int GetScore(string search)
        {
            var title = Title;

            if (title.StartsWith(search, StringComparison.CurrentCultureIgnoreCase))
            {
                // If query matches the start of the title, set highest score.
                return HighScore;
            }
            else if (title.Contains($" {search}", StringComparison.CurrentCultureIgnoreCase))
            {
                // If query matches second or next word of title, set medium score.
                return MediumScore;
            }
            else
            {
                // If no previous matches, consider the query a non-match.
                return NoMatchScore;
            }
        }
    }
}
