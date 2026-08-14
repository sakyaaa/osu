// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Game.Rulesets.UI;
using osuTK;

namespace osu.Game.Rulesets.Constellations.UI
{
    /// <summary>
    /// A hollow ring cursor with an optional trail.
    /// </summary>
    public partial class ConstellationsCursorContainer : GameplayCursorContainer
    {
        private const float ring_size = 48;
        private const float trail_size = 24;

        protected override Drawable CreateCursor() => new ConstellationsCursor();

        public partial class ConstellationsCursor : CompositeDrawable
        {
            public ConstellationsCursor()
            {
                Size = new Vector2(ring_size);
                Origin = Anchor.Centre;

                InternalChildren = new Drawable[]
                {
                    new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Colour = Colour4.White,
                        Alpha = 0.2f,
                    },
                    new Circle
                    {
                        Size = new Vector2(trail_size),
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Alpha = 0.3f,
                        Colour = Colour4.White,
                    },
                };
            }
        }
    }
}