// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.

using NUnit.Framework;
using osu.Game.Rulesets.Constellations.Judgements;

namespace osu.Game.Rulesets.Constellations.Tests.Judgements
{
    [TestFixture]
    public class ConstellationsHitWindowsTest
    {
        private ConstellationsHitWindows _hitWindows;

        [SetUp]
        public void SetUp()
        {
            _hitWindows = new ConstellationsHitWindows();
        }

        [Test]
        public void TestHitWindowRanges()
        {
            // Perfect: 50ms
            Assert.AreEqual(50, _hitWindows.WindowFor(HitResult.Perfect));
            
            // Great: 100ms
            Assert.AreEqual(100, _hitWindows.WindowFor(HitResult.Great));
            
            // Good: 150ms
            Assert.AreEqual(150, _hitWindows.WindowFor(HitResult.Good));
            
            // Miss: 200ms
            Assert.AreEqual(200, _hitWindows.WindowFor(HitResult.Miss));
        }

        [Test]
        public void TestIsHitResult()
        {
            Assert.IsTrue(_hitWindows.IsHitResult(HitResult.Perfect));
            Assert.IsTrue(_hitWindows.IsHitResult(HitResult.Great));
            Assert.IsTrue(_hitWindows.IsHitResult(HitResult.Good));
            Assert.IsFalse(_hitWindows.IsHitResult(HitResult.Miss));
        }

        [Test]
        public void TestResultFor()
        {
            // Within Perfect window (0-50ms)
            Assert.AreEqual(HitResult.Perfect, _hitWindows.ResultFor(25));
            
            // Within Great window (51-100ms)
            Assert.AreEqual(HitResult.Great, _hitWindows.ResultFor(75));
            
            // Within Good window (101-150ms)
            Assert.AreEqual(HitResult.Good, _hitWindows.ResultFor(125));
            
            // Outside all windows (>200ms)
            Assert.AreEqual(HitResult.None, _hitWindows.ResultFor(250));
        }
    }
}
