// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.

using NUnit.Framework;
using osu.Game.Rulesets.Constellations.Objects;
using osuTK;

namespace osu.Game.Rulesets.Constellations.Tests.Objects
{
    [TestFixture]
    public class DotTest
    {
        [Test]
        public void TestDotCreation()
        {
            var dot = new Dot
            {
                StartTime = 1000,
                Position = new Vector2(256, 192)
            };

            Assert.AreEqual(1000, dot.StartTime);
            Assert.AreEqual(new Vector2(256, 192), dot.Position);
            Assert.AreEqual(64, dot.HitRadius); // From ConstellationsHitObject
        }

        [Test]
        public void TestDotJudgement()
        {
            var dot = new Dot();
            var judgement = dot.CreateJudgement();

            Assert.IsNotNull(judgement);
            Assert.IsInstanceOf<DotJudgement>(judgement);
        }
    }
}
