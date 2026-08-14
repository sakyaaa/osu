// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.Linq;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Constellations.Objects;
using osu.Game.Rulesets.Difficulty;
using osu.Game.Rulesets.Difficulty.Preprocessing;
using osu.Game.Rulesets.Difficulty.Skills;
using osu.Game.Rulesets.Mods;
using osuTK;

namespace osu.Game.Rulesets.Constellations.Difficulty
{
    public class ConstellationsDifficultyCalculator : DifficultyCalculator
    {
        public ConstellationsDifficultyCalculator(IRulesetInfo ruleset, IWorkingBeatmap beatmap)
            : base(ruleset, beatmap)
        {
        }

        protected override DifficultyAttributes CreateDifficultyAttributes(IBeatmap beatmap, Mod[] mods, Skill[] skills)
        {
            double aimStrain = computeAimStrain(beatmap);
            double starRating = Math.Sqrt(aimStrain) * 1.5;

            return new DifficultyAttributes(mods, starRating);
        }

        private double computeAimStrain(IBeatmap beatmap)
        {
            var objects = beatmap.HitObjects.OfType<ConstellationsHitObject>().OrderBy(h => h.StartTime).ToList();

            if (objects.Count < 2)
                return 0;

            double totalDistance = 0;
            double totalTime = 0;

            for (int i = 1; i < objects.Count; i++)
            {
                var prev = objects[i - 1];
                var next = objects[i];

                totalDistance += Vector2.Distance(prev.Position, next.Position);
                totalTime += Math.Max(1, next.StartTime - prev.StartTime);
            }

            double density = objects.Count / Math.Max(1, totalTime / 1000.0);
            double aim = (totalDistance / Math.Max(1, totalTime)) * density;

            return aim;
        }

        protected override IEnumerable<DifficultyHitObject> CreateDifficultyHitObjects(IBeatmap beatmap, Mod[] mods) => Enumerable.Empty<DifficultyHitObject>();

        protected override Skill[] CreateSkills(IBeatmap beatmap, Mod[] mods) => Array.Empty<Skill>();
    }
}