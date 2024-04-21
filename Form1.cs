using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using lab5.Objects;

namespace lab5
{
    public partial class Form1 : Form
    {
        List<BaseObjects> objects = new List<BaseObjects>();
        Player player;
        Marker marker;
        Target target;
        int score = 0; // Variable to store the score

        public Form1()
        {
            InitializeComponent();

            // Create player
            player = new Player(pbMain.Width / 2, pbMain.Height / 2, 0);
            player.OnOverlap += (p, obj) =>
            {
                txtLog.Text = $"[{DateTime.Now:HH:mm:ss:ff}] Player overlapped with {obj}\n" + txtLog.Text;

                // Increase score if player overlaps with the target
                if (obj is Target)
                {
                    score += 10; // Increase score by 10 points
                    UpdateScore(); // Update score display
                }
            };
            player.OnMarkerOverlap += (m) =>
            {
                objects.Remove(m);
                marker = null;
            };

            // Create marker
            marker = new Marker(pbMain.Width / 2 + 50, pbMain.Height / 2 + 50, 0);

            // Create target
            target = new Target(200, 200, 0); // Initial position, adjust as needed

            // Add objects to the list
            objects.Add(player);
            objects.Add(marker);
            objects.Add(target);

            // Add blue circles
            AddBlueCircles();
        }

        private void AddBlueCircles()
        {
            // Add multiple blue circles to the field
            for (int i = 0; i < 5; i++)
            {
                // Generate random coordinates within the field
                int x = new Random().Next(100, pbMain.Width - 100);
                int y = new Random().Next(100, pbMain.Height - 100);

                // Create blue circle object
                var blueCircle = new BaseObjects(x, y, 0)
                {
                    OnOverlap = (obj1, obj2) =>
                    {
                        // Increase score if player overlaps with the blue circle
                        if (obj2 is Player)
                        {
                            score += 3; // Increase score by 3 points
                            UpdateScore(); // Update score display
                        }
                    }
                };

                objects.Add(blueCircle); // Add blue circle to the objects list
            }
        }

        private void UpdateScore()
        {
            lblScore.Text = $"Score: {score}"; // Update score label text
        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            g.Clear(Color.White);
            UpdatePlayer();

            // Check for overlaps and render objects
            foreach (var obj in objects.ToList())
            {
                if (obj != player && player.Overlaps(obj, g))
                {
                    player.Overlap(obj);
                    obj.Overlap(player);
                }

                g.Transform = obj.GetTransorm();
                obj.Render(g);
            }
        }

        private void UpdatePlayer()
        {
            if (marker != null)
            {
                float dx = marker.X - player.X;
                float dy = marker.Y - player.Y;
                float length = (float)Math.Sqrt(dx * dx + dy * dy);
                dx /= length;
                dy /= length;

                player.vX += dx * 0.5f;
                player.vY += dy * 0.5f;

                player.Angle = 90 - (float)(Math.Atan2(player.vX, player.vY) * 180 / Math.PI);
            }

            player.vX += -player.vX * 0.1f;
            player.vY += -player.vY * 0.1f;

            player.X += player.vX;
            player.Y += player.vY;
        }

        private void pbMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (marker == null)
            {
                marker = new Marker(0, 0, 0);
                objects.Add(marker);
            }

            marker.X = e.X;
            marker.Y = e.Y;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pbMain.Invalidate();
        }

       
    }
}
