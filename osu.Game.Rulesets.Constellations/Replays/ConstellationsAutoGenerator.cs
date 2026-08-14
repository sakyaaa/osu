// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Game.Beatmaps;
using osu.Game.Rulesets.Constellations.Objects;
using osu.Game.Rulesets.Replays;

namespace osu.Game.Rulesets.Constellations.Replays
{
    public class ConstellationsAutoGenerator : AutoGenerator<ConstellationsReplayFrame>
    {
        public new Beatmap<ConstellationsHitObject> Beatmap => (Beatmap<ConstellationsHitObject>)base.Beatmap;

        public ConstellationsAutoGenerator(IBeatmap beatmap)
            : base(beatmap)
        {
        }

        protected override void GenerateFrames()
        {
            Frames.Add(new ConstellationsReplayFrame());

            foreach (ConstellationsHitObject hitObject in Beatmap.HitObjects)
            {
                Frames.Add(new ConstellationsReplayFrame
                {
                    Time = hitObject.StartTime,
                    Position = hitObject.Position,
                    Actions = { ConstellationsAction.LeftButton },
                });

                Frames.Add(new ConstellationsReplayFrame
                {
                    Time = hitObject.StartTime + 50,
                    Position = hitObject.Position,
                });
            }
        }
    }
}