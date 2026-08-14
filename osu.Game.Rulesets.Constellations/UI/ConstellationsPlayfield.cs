// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Constellations.UI
{
    [Cached]
    public partial class ConstellationsPlayfield : Playfield
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            AddRangeInternal(new Drawable[]
            {
                new ConnectionLineRenderer(),
                HitObjectContainer,
            });
        }

        protected override GameplayCursorContainer CreateCursor() => new ConstellationsCursorContainer();
    }
}