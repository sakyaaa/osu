// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Constellations.Objects;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Objects.Types;
using osuTK;

namespace osu.Game.Rulesets.Constellations.Beatmaps
{
    public class ConstellationsBeatmapConverter : BeatmapConverter<ConstellationsHitObject>
    {
        public ConstellationsBeatmapConverter(IBeatmap beatmap, Ruleset ruleset)
            : base(beatmap, ruleset)
        {
        }

        public override bool CanConvert() => Beatmap.HitObjects.Any(h => h is IHasPosition);

        protected override IEnumerable<ConstellationsHitObject> ConvertHitObject(HitObject original, IBeatmap beatmap, CancellationToken cancellationToken)
        {
            switch (original)
            {
                case IHasPathWithRepeats slider:
                    yield return new TraceLine
                    {
                        Samples = original.Samples,
                        StartTime = original.StartTime,
                        Position = (original as IHasPosition)?.Position ?? Vector2.Zero,
                        Duration = slider.Duration,
                        Path = slider.Path,
                    };
                    break;

                case IHasPosition position:
                    yield return createDot(original, position.Position);
                    break;

                default:
                    yield return createDot(original, Vector2.Zero);
                    break;
            }
        }

        private Dot createDot(HitObject original, Vector2 position)
        {
            // Heuristic for AccentDot: every 4th object in the beatmap becomes an accent dot.
            int index = Beatmap.HitObjects.ToList().IndexOf(original);

            if (index >= 0 && index % 4 == 3)
            {
                return new AccentDot
                {
                    Samples = original.Samples,
                    StartTime = original.StartTime,
                    Position = position,
                };
            }

            return new Dot
            {
                Samples = original.Samples,
                StartTime = original.StartTime,
                Position = position,
            };
        }
    }
}