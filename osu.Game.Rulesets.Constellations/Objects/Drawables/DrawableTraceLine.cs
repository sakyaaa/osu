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

        protected override DrawableHitObject CreateNestedHitObject(HitObject hitObject)
        {
            if (hitObject is TraceTick tick)
                return new DrawableTraceTick(tick);

            return base.CreateNestedHitObject(hitObject);
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Origin = Anchor.Centre;

            var traceLine = (TraceLine)HitObject;

            // Получаем точки из SliderPath через ControlPoints
            var pathPoints = new List<Vector2>();
            foreach (var controlPoint in traceLine.Path.ControlPoints)
            {
                pathPoints.Add(traceLine.Position + controlPoint.Position);
            }

            AddRangeInternal(new Drawable[]
            {
                new osu.Framework.Graphics.Lines.Path
                {
                    PathRadius = 3,
                    Colour = Color4.White,
                    Vertices = pathPoints.ToArray(),
                    RelativeSizeAxes = Axes.None,
                },
                TraceLineInputManager = new TraceLineInputManager(this),
            });
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
