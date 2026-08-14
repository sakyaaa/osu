// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Utils;
using osu.Game.Graphics;
using osu.Game.Graphics.Sprites;
using osu.Game.Utils;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Beatmaps.Drawables
{
    /// <summary>
    /// A pill that displays the star rating of a beatmap.
    /// </summary>
    public partial class StarRatingDisplay : CompositeDrawable, IHasCurrentValue<StarDifficulty>, IHasCustomTooltip<StarDifficulty>
    {
        private readonly bool animated;
        private readonly Box background;
        private readonly SpriteIcon starIcon;
        private readonly OsuSpriteText starsText;
        private readonly SpriteIcon valueIcon;
        private readonly Container mainPill;
        private readonly Container originalStarsCapsule;
        private readonly Box originalStarsBackground;
        private readonly SpriteIcon originalStarIcon;
        private readonly OsuSpriteText originalStarsText;
        private bool originalStarsShown;

        private readonly BindableWithCurrent<StarDifficulty> current = new BindableWithCurrent<StarDifficulty>();

        public Bindable<StarDifficulty> Current
        {
            get => current.Current;
            set => current.Current = value;
        }

        /// <summary>
        /// The difficulty colour currently displayed.
        /// Can be used to have other components match the spectrum animation.
        /// </summary>
        public Color4 DisplayedDifficultyColour => background.Colour;

        /// <summary>
        /// The difficulty text colour currently displayed.
        /// Can be used to have other components match the spectrum animation.
        /// </summary>
        public Color4 DisplayedDifficultyTextColour => starsText.Colour;

        private readonly Bindable<double> displayedStars = new BindableDouble();

        /// <summary>
        /// The currently displayed stars of this display wrapped in a bindable.
        /// This bindable gets transformed on change rather than instantaneous, if animation is enabled.
        /// </summary>
        public IBindable<double> DisplayedStars => displayedStars;

        [Resolved]
        private OsuColour colours { get; set; } = null!;

        /// <summary>
        /// Creates a new <see cref="StarRatingDisplay"/> using an already computed <see cref="StarDifficulty"/>.
        /// </summary>
        /// <param name="starDifficulty">The already computed <see cref="StarDifficulty"/> to display.</param>
        /// <param name="size">The size of the star rating display.</param>
        /// <param name="animated">Whether the star rating display will perform transforms on change rather than updating instantaneously.</param>
        public StarRatingDisplay(StarDifficulty starDifficulty, StarRatingDisplaySize size = StarRatingDisplaySize.Regular, bool animated = false)
        {
            this.animated = animated;

            Current.Value = starDifficulty;

            AutoSizeAxes = Axes.Both;

            MarginPadding margin = default;

            switch (size)
            {
                case StarRatingDisplaySize.Small:
                    margin = new MarginPadding { Horizontal = 7f };
                    break;

                case StarRatingDisplaySize.Range:
                    margin = new MarginPadding { Horizontal = 8f };
                    break;

                case StarRatingDisplaySize.Regular:
                    margin = new MarginPadding { Horizontal = 8f, Vertical = 2f };
                    break;
            }

            InternalChild = new FillFlowContainer
            {
                AutoSizeAxes = Axes.Both,
                Anchor = Anchor.CentreLeft,
                Origin = Anchor.CentreLeft,
                Direction = FillDirection.Horizontal,
                Spacing = new Vector2(4f, 0f),
                Children = new Drawable[]
                {
                    originalStarsCapsule = new CircularContainer
                    {
                        Masking = true,
                        AutoSizeAxes = Axes.Both,
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                        Alpha = 0,
                        Children = new Drawable[]
                        {
                            originalStarsBackground = new Box
                            {
                                RelativeSizeAxes = Axes.Both,
                            },
                            new FillFlowContainer
                            {
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                AutoSizeAxes = Axes.Both,
                                Direction = FillDirection.Horizontal,
                                Margin = new MarginPadding { Horizontal = 6f, Vertical = 2f },
                                Spacing = new Vector2(2f, 0f),
                                Children = new Drawable[]
                                {
                                    originalStarIcon = new SpriteIcon
                                    {
                                        Anchor = Anchor.Centre,
                                        Origin = Anchor.Centre,
                                        Icon = FontAwesome.Solid.Star,
                                        Size = new Vector2(7f),
                                    },
                                    originalStarsText = new OsuSpriteText
                                    {
                                        Anchor = Anchor.Centre,
                                        Origin = Anchor.Centre,
                                        Spacing = new Vector2(-1f),
                                        Font = OsuFont.Torus.With(size: 12f, weight: FontWeight.Bold, fixedWidth: true),
                                        Shadow = false,
                                    },
                                }
                            },
                        }
                    },
                    mainPill = new CircularContainer
                    {
                        Masking = true,
                        AutoSizeAxes = Axes.Both,
                        Children = new Drawable[]
                        {
                            background = new Box
                            {
                                RelativeSizeAxes = Axes.Both,
                            },
                            new GridContainer
                            {
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                AutoSizeAxes = Axes.Both,
                                Margin = margin,
                                ColumnDimensions = new[]
                                {
                                    new Dimension(GridSizeMode.AutoSize),
                                    new Dimension(GridSizeMode.Absolute, 3f),
                                    new Dimension(GridSizeMode.AutoSize, minSize: 25f),
                                    new Dimension(GridSizeMode.Absolute, 2f),
                                    new Dimension(GridSizeMode.AutoSize),
                                },
                                RowDimensions = new[] { new Dimension(GridSizeMode.AutoSize) },
                                Content = new[]
                                {
                                    new[]
                                    {
                                        starIcon = new SpriteIcon
                                        {
                                            Anchor = Anchor.Centre,
                                            Origin = Anchor.Centre,
                                            Icon = FontAwesome.Solid.Star,
                                            Size = new Vector2(8f),
                                        },
                                        Empty(),
                                        starsText = new OsuSpriteText
                                        {
                                            Anchor = Anchor.Centre,
                                            Origin = Anchor.Centre,
                                            Margin = new MarginPadding { Bottom = 1.5f },
                                            Spacing = new Vector2(-1.4f),
                                            Font = OsuFont.Torus.With(size: 14.4f, weight: FontWeight.Bold, fixedWidth: true),
                                            Shadow = false,
                                        },
                                        Empty(),
                                        valueIcon = new SpriteIcon
                                        {
                                            Anchor = Anchor.CentreLeft,
                                            Origin = Anchor.CentreLeft,
                                            Margin = new MarginPadding
                                            {
                                                Top = -1f,
                                            },
                                            Size = new Vector2(8),
                                        },
                                    }
                                }
                            },
                        }
                    },
                }
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            Current.BindValueChanged(c =>
            {
                if (animated)
                    // Animation roughly matches `StarCounter`'s implementation.
                    this.TransformBindableTo(displayedStars, c.NewValue.Stars, 100 + 80 * Math.Abs(c.NewValue.Stars - c.OldValue.Stars), Easing.OutQuint);
                else
                    displayedStars.Value = c.NewValue.Stars;
            });

            displayedStars.Value = Current.Value.Stars;

            displayedStars.BindValueChanged(s =>
            {
                starsText.Text = s.NewValue < 0 ? "-" : s.NewValue.FormatStarRating();

                background.Colour = colours.ForStarDifficulty(s.NewValue);

                starIcon.Colour = colours.ForStarDifficultyText(s.NewValue);
                starsText.Colour = colours.ForStarDifficultyText(s.NewValue);

                updateDisplay();
            }, true);
            updateDisplay();
        }

        private void updateDisplay()
        {
            bool adjustedByMods = Current.Value.Stars != Current.Value.NoModStars;

            if (!adjustedByMods)
            {
                valueIcon.ScaleTo(0, 300, Easing.OutQuint).OnComplete(_ =>
                {
                    valueIcon.Hide();
                });
            }
            else
            {
                valueIcon.Colour = starIcon.Colour;
                valueIcon.Icon = Current.Value.Stars > Current.Value.NoModStars ? FontAwesome.Solid.SortUp : FontAwesome.Solid.SortDown;
                valueIcon.ScaleTo(1, 300, Easing.OutQuint);
                valueIcon.Show();

                originalStarsText.Text = Current.Value.NoModStars.FormatStarRating();
                originalStarsBackground.Colour = colours.ForStarDifficulty(Current.Value.NoModStars).Opacity(0.45f);

                var originalStarsTextColour = colours.ForStarDifficultyText(Current.Value.NoModStars).Opacity(0.75f);
                originalStarsText.Colour = originalStarsTextColour;
                originalStarIcon.Colour = originalStarsTextColour;
            }

            // only play the "cell division" transition when the adjusted state actually changes,
            // since this method is invoked on every star rating change (e.g. every frame of an animated transform).
            if (adjustedByMods == originalStarsShown)
                return;

            originalStarsShown = adjustedByMods;

            if (adjustedByMods)
            {
                originalStarsCapsule.ScaleTo(0).FadeIn(100, Easing.OutQuint);
                originalStarsCapsule.ScaleTo(1.2f, 200, Easing.OutQuint).Then().ScaleTo(1f, 150, Easing.OutBack);

                mainPill.ScaleTo(0.9f, 100, Easing.OutQuint).Then().ScaleTo(1f, 200, Easing.OutBack);
            }
            else
            {
                originalStarsCapsule.ScaleTo(0, 200, Easing.OutQuint).FadeOut(200, Easing.OutQuint);
            }
        }

        public ITooltip<StarDifficulty> GetCustomTooltip() => new StarRatingDisplayTooltip();
        public StarDifficulty TooltipContent => Current.Value;
    }

    public enum StarRatingDisplaySize
    {
        Small,
        Range,
        Regular,
    }
}
