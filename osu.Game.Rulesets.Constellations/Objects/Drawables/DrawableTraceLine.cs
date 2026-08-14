// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Lines;
using osuTK;
using osuTK.Graphics;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Objects.Types;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Scoring;

namespace osu.Game.Rulesets.Constellations.Objects.Drawables
{
    /// <summary>
    /// A drawable representation of a <see cref="TraceLine"/> — the cursor must trace along its path.
    /// </summary>
    public partial class DrawableTraceLine : DrawableConstellationsHitObject
    {
        public TraceLineInputManager TraceLineInputManager { get; private set; } = null!;

        public DrawableTraceLine()
            : this(null)
        {
        }

        public DrawableTraceLine(TraceLine? h = null)
            : base(h)
        {
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Origin = Anchor.Centre;

            AddRangeInternal(new Drawable[]
            {
                new osu.Framework.Graphics.Lines.Path
                {
                    PathRadius = 3,
                    Colour = Color4.White,
                    // Generate path from the slider path
                    RelativeSizeAxes = Axes.Both,
                },
                TraceLineInputManager = new TraceLineInputManager(this),
            });
        }

        /// <summary>
        /// Generates points along the slider path using PositionAt.
        /// </summary>
        private List<Vector2> GeneratePathPoints(SliderPath path, int pointCount)
        {
            var points = new List<Vector2>();

            for (int i = 0; i <= pointCount; i++)
            {
                double progress = (double)i / pointCount;
                points.Add(path.PositionAt(progress));
            }

            return points;
        }

        protected override void Update()
        {
            base.Update();

            // judge nested ticks based on tracking state.
            foreach (var tick in NestedHitObjects.OfType<DrawableTraceTick>())
            {
                if (tick.Judged)
                    continue;

                if (tick.HitObject.StartTime > Time.Current)
                    break;

                if (TraceLineInputManager.Tracking)
                    tick.HitForcefully();
                else
                    tick.MissForcefully();
            }
        }

        protected override void CheckForResult(bool userTriggered, double timeOffset)
        {
            // The TraceLine itself is judged by its nested ticks; no direct judgement here.
            if (timeOffset >= ((TraceLine)HitObject).Duration)
                ApplyResult(HitResult.Perfect);
        }

        protected override void UpdateHitStateTransforms(ArmedState state)
        {
            switch (state)
            {
                case ArmedState.Hit:
                    this.FadeOut(200);
                    break;

                case ArmedState.Miss:
                    this.FadeColour(Color4.Red, 200);
                    this.FadeOut(200);
                    break;
            }

            Expire();
        }
    }
}