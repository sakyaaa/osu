// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Scoring;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Constellations.Objects.Drawables
{
    /// <summary>
    /// A small tick along a <see cref="TraceLine"/> used for continuous scoring.
    /// </summary>
    public partial class DrawableTraceTick : DrawableConstellationsHitObject
    {
        public DrawableTraceTick()
            : this(null)
        {
        }

        public DrawableTraceTick(TraceTick? h = null)
            : base(h)
        {
            Size = new Vector2(8);
            Origin = Anchor.Centre;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            AddInternal(new Circle
            {
                RelativeSizeAxes = Axes.Both,
                Colour = Color4.White,
            });
        }

        protected override void CheckForResult(bool userTriggered, double timeOffset)
        {
            if (timeOffset >= 0)
                ApplyResult(HitResult.Perfect);
        }

        protected override void UpdateHitStateTransforms(ArmedState state)
        {
            switch (state)
            {
                case ArmedState.Hit:
                    this.FadeOut(100);
                    break;

                case ArmedState.Miss:
                    this.FadeColour(Color4.Red, 100);
                    this.FadeOut(100);
                    break;
            }

            Expire();
        }
    }
}
