// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.

using NUnit.Framework;
using osu.Game.Rulesets.Constellations.Judgements;
using osu.Game.Rulesets.Scoring;

namespace osu.Game.Rulesets.Constellations.Tests.Judgements
{
    [TestFixture]
    public class ConstellationsHitWindowsTest
    {
        private ConstellationsHitWindows _hitWindows = null!;

        [SetUp]
        public void SetUp()
        {
            _hitWindows = new ConstellationsHitWindows();
        }

        [Test]
        public void TestHitWindowRanges()
        {
            Assert.That(_hitWindows.WindowFor(HitResult.Perfect), Is.EqualTo(80));
            Assert.That(_hitWindows.WindowFor(HitResult.Great), Is.EqualTo(140));
            Assert.That(_hitWindows.WindowFor(HitResult.Good), Is.EqualTo(200));
            Assert.That(_hitWindows.WindowFor(HitResult.Miss), Is.EqualTo(400));
        }

        [Test]
        public void TestResultFor()
        {
            Assert.That(_hitWindows.ResultFor(40), Is.EqualTo(HitResult.Perfect));
            Assert.That(_hitWindows.ResultFor(100), Is.EqualTo(HitResult.Great));
            Assert.That(_hitWindows.ResultFor(170), Is.EqualTo(HitResult.Good));
            Assert.That(_hitWindows.ResultFor(500), Is.EqualTo(HitResult.None));
        }
    }
}
