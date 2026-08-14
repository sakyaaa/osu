// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;
using osu.Game.Beatmaps;
using osu.Game.Beatmaps.ControlPoints;
using osu.Game.Rulesets.Constellations.Judgements;
using osu.Game.Rulesets.Judgements;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Objects.Types;
using osu.Game.Rulesets.Scoring;
using osuTK;

namespace osu.Game.Rulesets.Constellations.Objects
{
    /// <summary>
    /// A line the cursor must trace along — analogous to a slider.
    /// </summary>
    public class TraceLine : ConstellationsHitObject, IHasPath, IHasDuration
    {
        /// <summary>
        /// The radius within which the cursor must be to keep tracking the line.
        /// </summary>
        public const float FOLLOW_RADIUS = 48;

        /// <summary>
        /// The spacing between generated <see cref="TraceTick"/>s.
        /// </summary>
        public const double TICK_SPACING = 32;

        public double EndTime => StartTime + Duration;

        public double Duration { get; set; }

        [JsonIgnore]
        public double Distance => Path.Distance;

        private readonly SliderPath path = new SliderPath();

        public SliderPath Path
        {
            get => path;
            set
            {
                path.ControlPoints.Clear();
                path.ControlPoints.AddRange(value.ControlPoints.Select(c => new PathControlPoint(c.Position, c.Type)));
                path.ExpectedDistance.Value = value.ExpectedDistance.Value;
            }
        }

        public Vector2 PositionAt(double progress) => Position + Path.PositionAt(progress);

        public Vector2 EndPosition => Position + Path.PositionAt(1);

        public override Judgement CreateJudgement() => new TraceTickJudgement();

        protected override void CreateNestedHitObjects(CancellationToken cancellationToken)
        {
            base.CreateNestedHitObjects(cancellationToken);

            int tickCount = (int)Math.Floor(Path.Distance / TICK_SPACING);

            for (int i = 1; i <= tickCount; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                double progress = (double)i / tickCount;

                AddNested(new TraceTick
                {
                    StartTime = StartTime + Duration * progress,
                    Position = PositionAt(progress),
                    PathProgress = progress,
                });
            }
        }
    }
}