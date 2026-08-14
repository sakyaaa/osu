// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Input;
using osu.Framework.Input.Events;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Screens.Play;
using osuTK;

namespace osu.Game.Rulesets.Constellations.Objects.Drawables
{
    /// <summary>
    /// Handles tracking of the cursor along a <see cref="TraceLine"/> — adapted from <c>SliderInputManager</c>.
    /// </summary>
    public partial class TraceLineInputManager : Component, IRequireHighFrequencyMousePosition
    {
        /// <summary>
        /// Whether the line is currently being tracked.
        /// </summary>
        public bool Tracking { get; private set; }

        [Resolved]
        private IGameplayClock? gameplayClock { get; set; }

        private Vector2? screenSpaceMousePosition;
        private readonly DrawableTraceLine traceLine;

        public TraceLineInputManager(DrawableTraceLine traceLine)
        {
            this.traceLine = traceLine;
            this.traceLine.HitObjectApplied += resetState;
        }

        /// <summary>
        /// This component handles all input of the line, so it should receive input no matter the position.
        /// </summary>
        public override bool ReceivePositionalInputAt(Vector2 screenSpacePos) => true;

        protected override bool OnMouseMove(MouseMoveEvent e)
        {
            screenSpaceMousePosition = e.ScreenSpaceMousePosition;
            return base.OnMouseMove(e);
        }

        protected override void Update()
        {
            base.Update();
            updateTracking(IsMouseInFollowArea());
        }

        /// <summary>
        /// Whether the mouse is currently in the follow area of the line.
        /// </summary>
        public bool IsMouseInFollowArea()
        {
            if (screenSpaceMousePosition is not Vector2 pos)
                return false;

            var traceLineObject = (TraceLine)traceLine.HitObject;

            double followProgress = Math.Clamp((Time.Current - traceLineObject.StartTime) / traceLineObject.Duration, 0, 1);
            Vector2 followPosition = traceLineObject.PositionAt(followProgress);
            Vector2 mousePositionInLine = traceLine.ToLocalSpace(pos) - traceLine.OriginPosition;

            return (mousePositionInLine - followPosition).LengthSquared <= TraceLine.FOLLOW_RADIUS * TraceLine.FOLLOW_RADIUS;
        }

        private void updateTracking(bool isValidTrackingPosition)
        {
            if (gameplayClock?.IsRewinding == true)
            {
                Tracking = false;
                return;
            }

            Tracking =
                (!traceLine.AllJudged || Time.Current <= ((TraceLine)traceLine.HitObject).EndTime)
                && isValidTrackingPosition;
        }

        private void resetState(DrawableHitObject obj)
        {
            Tracking = false;
            screenSpaceMousePosition = null;
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);

            traceLine.HitObjectApplied -= resetState;
        }
    }
}