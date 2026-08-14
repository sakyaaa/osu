// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.

using NUnit.Framework;
using osu.Game.Rulesets.Constellations.Mods;

namespace osu.Game.Rulesets.Constellations.Tests.Mods
{
    [TestFixture]
    public class ConstellationsModRelaxTest
    {
        private ConstellationsModRelax _mod;

        [SetUp]
        public void SetUp()
        {
            _mod = new ConstellationsModRelax();
        }

        [Test]
        public void TestModName()
        {
            Assert.AreEqual("Relax", _mod.Name);
        }

        [Test]
        public void TestModAcronym()
        {
            Assert.AreEqual("RX", _mod.Acronym);
        }

        [Test]
        public void TestModType()
        {
            Assert.IsTrue(_mod.Type.Implements(typeof(osu.Game.Rulesets.Mods.ModRelax)));
        }
    }
}
