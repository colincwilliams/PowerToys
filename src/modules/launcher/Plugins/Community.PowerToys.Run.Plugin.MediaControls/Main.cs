// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Community.PowerToys.Run.Plugin.MediaControls
{
    using ManagedCommon;
    using Wox.Plugin;

    public class Main : IPlugin
    {
        private static readonly IReadOnlyList<MediaControlResult> Results = new List<MediaControlResult>
        {
            MediaControlResults.Mute,
            MediaControlResults.NextTrack,
            MediaControlResults.Pause,
            MediaControlResults.Play,
            MediaControlResults.PreviousTrack,
            MediaControlResults.Stop,
            MediaControlResults.VolumeDown,
            MediaControlResults.VolumeUp,
        };

        private Theme _currentTheme = Theme.Dark;

        public string Name => "Media Controls";

        public string Description => "Adds media controls to PowerToys run, such as Play, Pause, Next, and more.";

        public void Init(PluginInitContext context)
        {
            ThemeChanged(Theme.Dark, Theme.Dark);
            context.API.ThemeChanged += ThemeChanged;
        }

        public List<Result> Query(Query query)
        {
            return Query(query.Search);
        }

        public List<Result> Query(string search)
        {
            var results = new List<Result>();

            return Results
                .Select(r => r.GetResult(search, _currentTheme))
                .Where(r => r.Score != MediaControlResult.NoMatchScore)
                .ToList();
        }

        private void ThemeChanged(Theme oldTheme, Theme newTheme)
        {
            _currentTheme = newTheme;
        }
    }
}
