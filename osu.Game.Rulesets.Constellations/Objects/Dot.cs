// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Game.Rulesets.Constellations.Judgements;
using osu.Game.Rulesets.Judgements;
using osuTK;

namespace osu.Game.Rulesets.Constellations.Objects
{
    public class Dot : ConstellationsHitObject
    {
        public Dot()
        {
        }

                public Dot(double x, double y)
        {
            Position = new Vector2((float)x, (float)y);
        }


        public override Judgement CreateJudgement() => new DotJudgement();
    }
}