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

            Assert.That(accentDot.StartTime, Is.EqualTo(2000));
            Assert.That(accentDot.Position, Is.EqualTo(new Vector2(300, 250)));
        }

        [Test]
        public void TestAccentDotJudgement()
        {
            var accentDot = new AccentDot();
            var judgement = accentDot.CreateJudgement();

            Assert.That(judgement, Is.Not.Null);
            Assert.That(judgement, Is.InstanceOf<AccentDotJudgement>());
        }
    }
}
