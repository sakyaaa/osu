// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.

using NUnit.Framework;
using osu.Game.Rulesets.Constellations.Objects;
using osuTK;

namespace osu.Game.Rulesets.Constellations.Tests.Objects
{
    [TestFixture]
    public class AccentDotTest
    {
        [Test]
        public void TestAccentDotCreation()
        {
            var accentDot = new AccentDot
            {
                StartTime = 2000,
                Position = new Vector2(300, 250)
            };

            Assert.AreEqual(2000, accentDot.StartTime);
            Assert.AreEqual(new Vector2(300, 250), accentDot.Position);
            Assert.AreEqual(64, accentDot.HitRadius);
        }

        [Test]
        public void TestAccentDotJudgement()
        {
            var accentDot = new AccentDot();
            var judgement = accentDot.CreateJudgement();

            Assert.IsNotNull(judgement);
            Assert.IsInstanceOf<AccentDotJudgement>(judgement);
        }

        [Test]
        public void TestAccentDotIsAccent()
        {
            var accentDot = new AccentDot();
            Assert.IsTrue(accentDot.IsAccent);
        }
    }
}
