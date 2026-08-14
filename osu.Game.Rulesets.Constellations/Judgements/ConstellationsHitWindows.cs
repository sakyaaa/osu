// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Game.Rulesets.Scoring;

namespace osu.Game.Rulesets.Constellations.Judgements
{
    /// <summary>
    /// Hit windows for Constellations — Perfect / Great / Good / Miss, analogous to 300/100/50/miss.
    /// </summary>
    public class ConstellationsHitWindows : HitWindows
    {
        public override bool IsHitResultAllowed(HitResult result)
        {
            switch (result)
            {
                case HitResult.Perfect:
                case HitResult.Great:
                case HitResult.Good:
                case HitResult.Miss:
                    return true;
            }

            return false;
        }

        public override void SetDifficulty(double difficulty)
        {
        }

        public override double WindowFor(HitResult result)
        {
            switch (result)
            {
                case HitResult.Perfect:
                    return 80;

                case HitResult.Great:
                    return 140;

                case HitResult.Good:
                    return 200;

                case HitResult.Miss:
                    return 400;

                default:
                    return 0;
            }
        }
    }
}