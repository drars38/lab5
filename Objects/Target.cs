using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace lab5.Objects
{
    internal class Target : BaseObjects
    {
        private readonly Random random;

        public Target(float x, float y, float angle) : base(x, y, angle)
        {
            random = new Random();
        }

        public override void Overlap(BaseObjects obj)
        {
            base.Overlap(obj);
            if (obj is Player)
            {
                // Disappear and reappear at a random location
                X = random.Next(100, 500); // Adjust according to your desired range
                Y = random.Next(100, 450); // Adjust according to your desired range
            }
        }

        public override GraphicsPath GetGraphicsPath()
        {
            // Create the graphics path for the circle
            var path = new GraphicsPath();
            path.AddEllipse(new RectangleF(-20, -20, 40, 40)); // Adjust size as needed
            return path;
        }

        public override void Render(Graphics g)
        {
            // Render the circle
            g.FillEllipse(Brushes.Blue, -20, -20, 40, 40); // Adjust color and size as needed
            g.DrawEllipse(Pens.Black, -20, -20, 40, 40); // Adjust color and size as needed
        }
    }
}
