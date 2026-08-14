// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Game.Rulesets.Constellations.Objects;
using osuTK;

namespace osu.Game.Rulesets.Constellations.Objects.Drawables.Components
{
    /// <summary>
    /// A brief flash shown when a dot is hit.
    /// </summary>
    public partial class HitExplosion : CompositeDrawable
    {
        public HitExplosion()
        {
            Size = new Vector2(ConstellationsHitObject.HIT_RADIUS * 2);
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;

            InternalChild = new Box
            {
                RelativeSizeAxes = Axes.Both,
                Colour = Colour4.White,
                Alpha = 0.5f,
            };
        }
    }
}