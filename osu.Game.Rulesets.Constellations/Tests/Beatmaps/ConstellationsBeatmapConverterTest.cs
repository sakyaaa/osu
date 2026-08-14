// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.

using NUnit.Framework;
using osu.Game.Rulesets.Constellations.Beatmaps;

namespace osu.Game.Rulesets.Constellations.Tests.Beatmaps
{
    [TestFixture]
    public class ConstellationsBeatmapConverterTest
    {
        private ConstellationsBeatmapConverter _converter = null!;

        [SetUp]
        public void SetUp()
        {
            _converter = new ConstellationsBeatmapConverter(null!, null!);
        }

        [Test]
        public void TestConverterCreation()
        {
            Assert.That(_converter, Is.Not.Null);
        }
    }
}
