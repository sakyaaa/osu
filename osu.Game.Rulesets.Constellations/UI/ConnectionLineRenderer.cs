// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Lines;
using osu.Framework.Graphics.Shapes;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Constellations.Objects;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Constellations.UI
{
    /// <summary>
    /// Renders animated gradient lines between combo-ordered hit objects — the "constellation" effect.
    /// </summary>
    public partial class ConnectionLineRenderer : CompositeDrawable
    {
        [Resolved]
        private IBeatmap beatmap { get; set; } = null!;

        private readonly List<ConnectionLine> lines = new List<ConnectionLine>();

        public ConnectionLineRenderer()
        {
            RelativeSizeAxes = Axes.Both;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            var objects = beatmap.HitObjects.OfType<ConstellationsHitObject>().OrderBy(h => h.StartTime).ToList();

            for (int i = 0; i < objects.Count - 1; i++)
            {
                var prev = objects[i];
                var next = objects[i + 1];

                lines.Add(new ConnectionLine(prev, next));
            }

            AddRangeInternal(lines);
        }

        protected override void Update()
        {
            base.Update();

            foreach (var line in lines)
                line.UpdateProgress(Time.Current);
        }

        public partial class ConnectionLine : CompositeDrawable
        {
            private readonly ConstellationsHitObject prev;
            private readonly ConstellationsHitObject next;

            private readonly Path path;

            public ConnectionLine(ConstellationsHitObject prev, ConstellationsHitObject next)
            {
                this.prev = prev;
                this.next = next;

                Origin = Anchor.Centre;

                path = new Path
                {
                    PathRadius = 2,
                    Colour = Color4.White,
                };

                AddInternal(path);
            }

            public void UpdateProgress(double time)
            {
                double start = prev.StartTime;
                double end = next.StartTime;

                if (time < start)
                {
                    Alpha = 0;
                    return;
                }

                double progress = (time - start) / (end - start);
                progress = System.Math.Clamp(progress, 0, 1);

                Alpha = 0.4f * (float)(1 - progress);

                // line "draws" from prev to next.
                Vector2 startPos = prev.StackedPosition;
                Vector2 endPos = next.StackedPosition;

                path.ClearVertices();
                path.AddVertex(startPos);
                path.AddVertex(Vector2.Lerp(startPos, endPos, (float)progress));
            }
        }
    }
}