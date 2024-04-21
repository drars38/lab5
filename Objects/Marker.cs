﻿using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5.Objects
{
    internal class Marker : BaseObjects
    {
        public Marker (float x, float y, float angle) : base (x, y, angle)
        {
        
        }
        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-15, -15, 30, 30);
            return path;
        }
        public override void Render(Graphics g)
        {
            g.FillEllipse(
               new SolidBrush(Color.Red),
              -3,-3,6,6);
            g.DrawEllipse(
                new Pen(Color.Red, 2),
                -6,-6,12,12);
            g.DrawEllipse(new Pen(Color.Red, 2), -10,-10,20,20);

        }

    }
}
