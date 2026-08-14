// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Game.Rulesets.Judgements;
using osu.Game.Rulesets.Scoring;

namespace osu.Game.Rulesets.Constellations.Judgements
{
    /// <summary>
    /// Judgement for a standard <see cref="Objects.Dot"/>.
    /// </summary>
    public class DotJudgement : Judgement
    {
        public override HitResult MaxResult => HitResult.Perfect;
    }

    /// <summary>
    /// Judgement for an <see cref="Objects.AccentDot"/> — worth more points.
    /// </summary>
    public class AccentDotJudgement : Judgement
    {
        public override HitResult MaxResult => HitResult.Perfect;
    }

    /// <summary>
    /// Judgement for a nested <see cref="Objects.TraceTick"/> — continuous scoring along a TraceLine.
    /// </summary>
    public class TraceTickJudgement : Judgement
    {
        public override HitResult MaxResult => HitResult.SmallTickHit;
    }
}