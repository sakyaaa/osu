// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Graphics;
using osu.Game.Rulesets.Constellations.Objects.Drawables.Components;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Constellations.Objects.Drawables
{
    /// <summary>
    /// A <see cref="DrawableDot"/> with a distinct accent colour and slightly larger visual.
    /// </summary>
    public partial class DrawableAccentDot : DrawableDot
    {
        public DrawableAccentDot()
            : this(null)
        {
        }

        public DrawableAccentDot(AccentDot? h = null)
            : base(h)
        {
        }

        protected override void UpdateInitialTransforms()
        {
            base.UpdateInitialTransforms();

            DotPiece.Colour = Color4.Gold;
            DotPiece.ScaleTo(1.3f);
        }
    }
}