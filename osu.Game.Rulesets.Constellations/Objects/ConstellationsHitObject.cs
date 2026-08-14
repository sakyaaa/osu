// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Bindables;
using osu.Game.Rulesets.Constellations.Judgements;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Objects.Types;
using osu.Game.Rulesets.Scoring;
using osuTK;

namespace osu.Game.Rulesets.Constellations.Objects
{
    public abstract class ConstellationsHitObject : HitObject, IHasPosition, IHasComboInformation, IHasTimePreempt
    {
        /// <summary>
        /// The radius within which the cursor must be to hit this object.
        /// </summary>
        public const float HIT_RADIUS = 64;

        /// <summary>
        /// The radius of the visual dot itself.
        /// </summary>
        public const float DOT_RADIUS = 12;

        public double TimePreempt => 600;

        public double TimeFadeIn => 200;

        private HitObjectProperty<Vector2> position;

        public Vector2 Position
        {
            get => position.Value;
            set => position.Value = value;
        }

        public float X
        {
            get => Position.X;
            set => Position = new Vector2(value, Y);
        }

        public float Y
        {
            get => Position.Y;
            set => Position = new Vector2(X, value);
        }

        /// <summary>
        /// The stacked position of this object, taking into account stacking.
        /// </summary>
        public Vector2 StackedPosition => Position + StackOffset;

        public Vector2 StackOffset { get; set; }

        #region IHasComboInformation implementation

        private HitObjectProperty<int> comboIndex = new HitObjectProperty<int>();
        private HitObjectProperty<int> comboIndexWithOffsets = new HitObjectProperty<int>();
        private HitObjectProperty<int> indexInCurrentCombo = new HitObjectProperty<int>();
        private HitObjectProperty<bool> lastInCombo = new HitObjectProperty<bool>();
        private HitObjectProperty<bool> newCombo = new HitObjectProperty<bool>();
        private HitObjectProperty<int> comboOffset = new HitObjectProperty<int>();

        public Bindable<int> ComboIndexBindable => comboIndex.Bindable;
        public int ComboIndex
        {
            get => comboIndex.Value;
            set => comboIndex.Value = value;
        }

        public Bindable<int> ComboIndexWithOffsetsBindable => comboIndexWithOffsets.Bindable;
        public int ComboIndexWithOffsets
        {
            get => comboIndexWithOffsets.Value;
            set => comboIndexWithOffsets.Value = value;
        }

        public Bindable<int> IndexInCurrentComboBindable => indexInCurrentCombo.Bindable;
        public int IndexInCurrentCombo
        {
            get => indexInCurrentCombo.Value;
            set => indexInCurrentCombo.Value = value;
        }

        public Bindable<bool> LastInComboBindable => lastInCombo.Bindable;
        public bool LastInCombo
        {
            get => lastInCombo.Value;
            set => lastInCombo.Value = value;
        }

        public bool NewCombo
        {
            get => newCombo.Value;
            set => newCombo.Value = value;
        }

        public int ComboOffset
        {
            get => comboOffset.Value;
            set => comboOffset.Value = value;
        }

        public void UpdateComboInformation(IHasComboInformation? lastObj)
        {
            int index = lastObj?.ComboIndex ?? 0;
            int indexWithOffsets = lastObj?.ComboIndexWithOffsets ?? 0;
            int inCurrentCombo = (lastObj?.IndexInCurrentCombo + 1) ?? 0;

            if (NewCombo || lastObj == null)
            {
                inCurrentCombo = 0;
                index++;
                indexWithOffsets += ComboOffset + 1;

                if (lastObj != null)
                    lastObj.LastInCombo = true;
            }

            ComboIndex = index;
            ComboIndexWithOffsets = indexWithOffsets;
            IndexInCurrentCombo = inCurrentCombo;
        }

        #endregion

        protected override HitWindows CreateHitWindows() => new ConstellationsHitWindows();
    }
}