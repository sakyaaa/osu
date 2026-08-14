// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Game.Rulesets.Constellations.Judgements;
using osu.Game.Rulesets.Judgements;
using osu.Game.Rulesets.Scoring;

namespace osu.Game.Rulesets.Constellations.Objects
{
    /// <summary>
    /// A nested hit object along a <see cref="TraceLine"/> used for continuous scoring.
    /// </summary>
    public class TraceTick : ConstellationsHitObject
    {
        /// <summary>
        /// The progress along the parent <see cref="TraceLine"/>'s path at which this tick sits.
        /// </summary>
        public double PathProgress { get; set; }

        public override Judgement CreateJudgement() => new TraceTickJudgement();
    }
}