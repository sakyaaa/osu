// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Linq;
using osu.Framework.Localisation;
using osu.Game.Rulesets.Constellations.Objects;
using osu.Game.Rulesets.Constellations.Objects.Drawables;
using osu.Game.Rulesets.Constellations.UI;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.UI;
using osu.Game.Screens.Play;
using osuTK;

namespace osu.Game.Rulesets.Constellations.Mods
{
    /// <summary>
    /// Aim-only mod — the cursor auto-hits dots when within the hit radius, no clicking required.
    /// </summary>
    public class ConstellationsModRelax : ModRelax, IUpdatableByPlayfield, IApplicableToDrawableRuleset<ConstellationsHitObject>, IApplicableToPlayer, IHasNoTimedInputs
    {
        public override LocalisableString Description => @"You don't need to click. Just aim!";

        public override Type[] IncompatibleMods => Array.Empty<Type>();

        private DrawableConstellationsRuleset ruleset = null!;

        public void ApplyToDrawableRuleset(DrawableRuleset<ConstellationsHitObject> drawableRuleset)
        {
            ruleset = (DrawableConstellationsRuleset)drawableRuleset;
        }

        public void ApplyToPlayer(Player player)
        {
        }

        public void Update(Playfield playfield)
        {
            double time = playfield.Clock.CurrentTime;

            foreach (var h in playfield.HitObjectContainer.AliveObjects.OfType<DrawableConstellationsHitObject>())
            {
                switch (h)
                {
                    case DrawableDot dot:
                        handleDot(dot, time);
                        break;
                }
            }
        }

        private void handleDot(DrawableDot dot, double time)
        {
            if (dot.Judged)
                return;

            if (time < dot.HitObject.StartTime - RELAX_LENIENCY)
                return;

            if (dot.HitObject.HitWindows == null || !dot.HitObject.HitWindows.CanBeHit(time - dot.HitObject.StartTime))
                return;

            if (!dot.IsCursorInHitRadius(getCursorPosition(dot)))
                return;

            dot.HitForcefully();
        }

        private Vector2 getCursorPosition(DrawableDot dot)
        {
            var cursor = ruleset.Playfield.Cursor?.ActiveCursor;
            if (cursor == null)
                return dot.HitObject.StackedPosition;

            return dot.ToLocalSpace(cursor.ScreenSpaceDrawQuad.Centre);
        }

        public const float RELAX_LENIENCY = 12;
    }
}