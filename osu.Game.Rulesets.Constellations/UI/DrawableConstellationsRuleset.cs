// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Input;
using osu.Game.Beatmaps;
using osu.Game.Input.Handlers;
using osu.Game.Replays;
using osu.Game.Rulesets.Constellations.Objects;
using osu.Game.Rulesets.Constellations.Objects.Drawables;
using osu.Game.Rulesets.Constellations.Replays;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Constellations.UI
{
    [Cached]
    public partial class DrawableConstellationsRuleset : DrawableRuleset<ConstellationsHitObject>
    {
                                        public DrawableConstellationsRuleset(ConstellationsRuleset ruleset, IBeatmap beatmap, IReadOnlyList<Mod>? mods = null)
            : base(ruleset, beatmap, mods)
        {
        }

        protected override Playfield CreatePlayfield() => new ConstellationsPlayfield();

        protected override ReplayInputHandler CreateReplayInputHandler(Replay replay) => new ConstellationsFramedReplayInputHandler(replay);

        public override DrawableHitObject<ConstellationsHitObject> CreateDrawableRepresentation(ConstellationsHitObject h)
        {
            switch (h)
            {
                case AccentDot:
                    return new DrawableAccentDot((AccentDot)h);

                case Dot:
                    return new DrawableDot((Dot)h);

                case TraceLine:
                    return new DrawableTraceLine((TraceLine)h);

                case TraceTick:
                    return new DrawableTraceTick((TraceTick)h);
            }

            return null!;
        }

        protected override PassThroughInputManager CreateInputManager() => new ConstellationsInputManager(Ruleset!.RulesetInfo);
    }
}