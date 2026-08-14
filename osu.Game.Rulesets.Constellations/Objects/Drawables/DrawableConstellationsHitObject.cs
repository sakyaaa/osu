// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using JetBrains.Annotations;
using osu.Framework.Graphics;
using osu.Game.Rulesets.Objects.Drawables;
using osuTK;

namespace osu.Game.Rulesets.Constellations.Objects.Drawables
{
    public abstract partial class DrawableConstellationsHitObject : DrawableHitObject<ConstellationsHitObject>
    {
        protected DrawableConstellationsHitObject([CanBeNull] ConstellationsHitObject hitObject)
            : base(hitObject)
        {
            Origin = Anchor.Centre;
            Position = hitObject?.Position ?? Vector2.Zero;
        }

        /// <summary>
        /// Whether the cursor is currently within the hit radius of this object.
        /// </summary>
        public bool IsCursorInHitRadius(Vector2 cursorPosition)
        {
            float dist = Vector2.Distance(cursorPosition, HitObject.StackedPosition);
            return dist <= ConstellationsHitObject.HIT_RADIUS - ConstellationsHitObject.DOT_RADIUS;
        }

        /// <summary>
        /// Forcefully applies the maximum result to this object.
        /// </summary>
        public void HitForcefully() => ApplyMaxResult();

        /// <summary>
        /// Forcefully applies the minimum result to this object.
        /// </summary>
        public void MissForcefully() => ApplyMinResult();
    }
}