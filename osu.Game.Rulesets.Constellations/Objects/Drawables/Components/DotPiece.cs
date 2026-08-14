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
    /// The visual dot itself — a filled circle with a subtle outline.
    /// </summary>
    public partial class DotPiece : CompositeDrawable
    {
        public DotPiece()
        {
            Size = new Vector2(ConstellationsHitObject.DOT_RADIUS * 2);
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;

            InternalChildren = new Drawable[]
            {
                new Circle
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Colour4.White,
                },
            };
        }
    }
}