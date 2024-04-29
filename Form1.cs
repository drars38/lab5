using lab5.Objects;
using LAB5.Objects;
using System.DirectoryServices.ActiveDirectory;

namespace lab5
{
    public partial class Form1 : Form
    {
        List<BaseObjects> objects = new();
        Player player; //���� ��� ������
        Marker marker;
        Target myCircle;
        Target myCircle2;
        

        int score = 0;
        public Form1()
        {
            InitializeComponent();
            player = new Player(pbMain.Width / 2, pbMain.Height / 2, 0);

          
            // ������� �� �����������
            player.OnOverlap += (p, obj) =>
            {
                txtLog.Text = $"[{DateTime.Now:HH:mm:ss:ff}] ����� ��������� � {obj}\n" + txtLog.Text;
            };
            // ������� �� ����������� � ��������
            player.OnMarkerOverlap += (m) =>
            {
                objects.Remove(m);
                marker = null;
            };



            marker = new Marker(pbMain.Width / 2 + 50, pbMain.Height / 2 + 50, 0);
            myCircle = new Target(pbMain.Width / 2 + 100, pbMain.Height / 2 + 100, 0);
            myCircle2 = new Target(pbMain.Width / 2 + 100, pbMain.Height / 2 + 100, 0);

            myCircle.OnTimeExpired += (circle) =>
            {
                objects.Remove(circle);
            };
            myCircle2.OnTimeExpired += (circle) =>
            {
                objects.Remove(circle);
            };

            objects.Add(marker);
            objects.Add(player);
            objects.Add(myCircle);
            objects.Add(myCircle2);
            



        }
        




        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            g.Clear(Color.White);

            updatePlayer();

            // ������������� �����������
            foreach (var obj in objects.ToList())
            {
                if (obj != player && player.Overlaps(obj, g))
                {
                    player.Overlap(obj);
                    obj.Overlap(player);

                    if (obj is Target)
                    {
                        score+=1;
                       lblScore.Text = score.ToString("���� : " + score); // ��������� ��������� ���� � ����������� �����
                    }
                }
            }

            // �������� �������
            foreach (var obj in objects)
            {
                g.Transform = obj.GetTransorm();
                obj.Render(g);
            }
        }

        //����� ��� ����������� ������
        private void updatePlayer()
        {
            if (marker != null)
            {
                float dx = marker.X - player.X;
                float dy = marker.Y - player.Y;
                float length = MathF.Sqrt(dx * dx + dy * dy);
                dx /= length;
                dy /= length;


                player.vX += dx * 0.5f;
                player.vY += dy * 0.5f;

                // ����������� ���� �������� ������ 
                player.Angle = 90 - MathF.Atan2(player.vX, player.vY) * 180 / MathF.PI;
            }

            // ���������� ������,
            // ����� �����, ����� ����� ��������� ������� ��������� ����������� ����������
            player.vX += -player.vX * 0.1f;
            player.vY += -player.vY * 0.1f;

            // �������� ������� ������ � ������� ������� ��������
            player.X += player.vX;
            player.Y += player.vY;

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            pbMain.Invalidate();

            // ��������� ����� ��� ������� �����
            foreach (var obj in objects.OfType<Target>())
            {
                obj.Tick();
            }

            

            // ��������� ����� ��� ������� �����
            foreach (var obj in objects.OfType<Target>())
            {
                obj.Tick();
            }
        }



        private void pbMain_MouseClick(object sender, MouseEventArgs e)
        {
            // ��� ������� �������� ������� �� ����� ���� �� ��� �� ������
            if (marker == null)
            {
                marker = new Marker(0, 0, 0);
                objects.Add(marker); // � ������� �� ������ ��������� � objects
            }

            // � ��� ��� � ��������
            marker.X = e.X;
            marker.Y = e.Y;
        }


    }
}
