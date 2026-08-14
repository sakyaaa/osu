// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.

using NUnit.Framework;
using osu.Game.Rulesets.Constellations.Mods;

namespace osu.Game.Rulesets.Constellations.Tests.Mods
{
    [TestFixture]
    public class ConstellationsModRelaxTest
    {
        private ConstellationsModRelax _mod = null!;

        [SetUp]
        public void SetUp()
        {
            _mod = new ConstellationsModRelax();
        }

        [Test]
        public void TestModName()
        {
            Assert.That(_mod.Name, Is.EqualTo("Relax"));
        }

        [Test]
        public void TestModAcronym()
        {
            Assert.That(_mod.Acronym, Is.EqualTo("RX"));
        }
    }
}
