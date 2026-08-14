// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Linq;
using osu.Game.Rulesets.Replays;
using osuTK;

namespace osu.Game.Rulesets.Constellations.Replays
{
    public class ConstellationsReplayFrame : ReplayFrame
    {
        public List<ConstellationsAction> Actions = new List<ConstellationsAction>();
        public Vector2 Position;

        public ConstellationsReplayFrame(ConstellationsAction? button = null)
        {
            if (button.HasValue)
                Actions.Add(button.Value);
        }

        public override bool IsEquivalentTo(ReplayFrame other)
            => other is ConstellationsReplayFrame constellationsFrame && Time == constellationsFrame.Time && Position == constellationsFrame.Position && Actions.SequenceEqual(constellationsFrame.Actions);
    }
}