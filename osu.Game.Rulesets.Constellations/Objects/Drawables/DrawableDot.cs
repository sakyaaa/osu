// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Diagnostics;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Bindings;
using osu.Framework.Input.Events;
using osu.Game.Rulesets.Constellations.Objects.Drawables.Components;
using osu.Game.Rulesets.Constellations.UI;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Scoring;
using osu.Game.Rulesets.UI;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Constellations.Objects.Drawables
{
    public partial class DrawableDot : DrawableConstellationsHitObject, IKeyBindingHandler<ConstellationsAction>
    {
        public DotPiece DotPiece { get; private set; } = null!;
        public TimingRing TimingRing { get; private set; } = null!;
        public HitExplosion HitExplosion { get; private set; } = null!;

        private Container scaleContainer = null!;

        [Resolved]
        private Playfield playfield { get; set; } = null!;

        public DrawableDot()
            : this(null)
        {
        }

        public DrawableDot(Dot? h = null)
            : base(h)
        {
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Origin = Anchor.Centre;

            AddRangeInternal(new Drawable[]
            {
                scaleContainer = new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Origin = Anchor.Centre,
                    Anchor = Anchor.Centre,
                    Children = new Drawable[]
                    {
                        DotPiece = new DotPiece(),
                        TimingRing = new TimingRing(),
                        HitExplosion = new HitExplosion(),
                    }
                }
            });

            Size = new Vector2(ConstellationsHitObject.HIT_RADIUS * 2);
        }

        protected override void Update()
        {
            base.Update();

            // pulse active dots.
            if (!AllJudged && Time.Current >= HitObject.StartTime - HitObject.TimePreempt)
            {
                double progress = (Time.Current - (HitObject.StartTime - HitObject.TimePreempt)) / HitObject.TimePreempt;
                float scale = 1f + 0.1f * (float)System.Math.Sin(progress * System.Math.PI * 4);
                scaleContainer.Scale = new Vector2(scale);
            }
        }

        public bool OnPressed(KeyBindingPressEvent<ConstellationsAction> e)
        {
            if (e.Action != ConstellationsAction.LeftButton && e.Action != ConstellationsAction.RightButton)
                return false;

            if (AllJudged)
                return false;

            UpdateResult(true);
            return true;
        }

        public void OnReleased(KeyBindingReleaseEvent<ConstellationsAction> e)
        {
        }

        protected override void CheckForResult(bool userTriggered, double timeOffset)
        {
            Debug.Assert(HitObject.HitWindows != null);

            if (!userTriggered)
            {
                if (!HitObject.HitWindows.CanBeHit(timeOffset))
                    ApplyResult(HitResult.Miss);

                return;
            }

            var result = HitObject.HitWindows.ResultFor(timeOffset);

            if (result == HitResult.None)
                return;

            // check cursor is within the hit radius.
            if (!IsCursorInHitRadius(getCursorPosition()))
                return;

            ApplyResult(result);
        }

        private Vector2 getCursorPosition()
        {
            if (playfield.Cursor?.ActiveCursor == null)
                return HitObject.StackedPosition;

            return ToLocalSpace(playfield.Cursor.ActiveCursor.ScreenSpaceDrawQuad.Centre);
        }

        protected override void UpdateInitialTransforms()
        {
            base.UpdateInitialTransforms();

            DotPiece.FadeInFromZero(HitObject.TimeFadeIn);

            TimingRing.FadeTo(0.9f, System.Math.Min(HitObject.TimeFadeIn * 2, HitObject.TimePreempt));
            TimingRing.ScaleTo(1f, HitObject.TimePreempt);
            TimingRing.Expire(true);
        }

        protected override void UpdateStartTimeStateTransforms()
        {
            base.UpdateStartTimeStateTransforms();

            TimingRing.FadeOut(50);
        }

        protected override void UpdateHitStateTransforms(ArmedState state)
        {
            this.Delay(800).FadeOut();

            switch (state)
            {
                default:
                    TimingRing.FadeOut();
                    break;

                case ArmedState.Idle:
                    break;

                case ArmedState.Hit:
                    HitExplosion.FadeInFromZero(50);
                    HitExplosion.ScaleTo(1.5f, 100, Easing.Out);
                    HitExplosion.FadeOut(200);
                    this.FadeOut(100);
                    break;

                case ArmedState.Miss:
                    this.FadeColour(Color4.Red, 100);
                    this.FadeOut(200, Easing.In);
                    break;
            }

            Expire();
        }
    }
}