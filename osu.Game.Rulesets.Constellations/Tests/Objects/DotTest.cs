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

            Assert.That(dot.StartTime, Is.EqualTo(1000));
            Assert.That(dot.Position, Is.EqualTo(new Vector2(256, 192)));
        }

        [Test]
        public void TestDotJudgement()
        {
            var dot = new Dot();
            var judgement = dot.CreateJudgement();

            Assert.That(judgement, Is.Not.Null);
            Assert.That(judgement, Is.InstanceOf<DotJudgement>());
        }
    }
}
