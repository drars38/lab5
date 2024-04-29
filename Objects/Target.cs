﻿using lab5.Objects;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace LAB5.Objects
{
    class Target : BaseObjects
    {
        private int timeLeft; // Переменная для отслеживания времени
        private const int maxTime = 450; // Максимальное время
        public event Action<Target> OnTimeExpired;


        public Target(float x, float y, float angle) : base(x, y, angle)
        {
        }

        public override void Render(Graphics g)
        {
            // Вычисляем текущий размер круга
            float circleSize = 50 * ((float)timeLeft / maxTime);

            // Отрисовываем круг
            g.FillEllipse(new SolidBrush(Color.Aqua), -circleSize / 2, -circleSize / 2, circleSize, circleSize);
            g.DrawEllipse(new Pen(Color.Black, 2), -circleSize / 2, -circleSize / 2, circleSize, circleSize);

            // Отображаем оставшееся время
            g.DrawString(
                $"{timeLeft} мс",
                new Font("Verdana", 10),
                new SolidBrush(Color.Blue),
                25, 15
            );
        }

        public override GraphicsPath GetGraphicsPath()
        {
            // Создаем графический путь для круга
            var path = new GraphicsPath();
            float circleSize = 20 * ((float)timeLeft / maxTime); // Вычисляем текущий размер круга
            path.AddEllipse(new RectangleF(-circleSize / 2, -circleSize / 2, circleSize, circleSize));
            return path;
        }


        public override void Overlap(BaseObjects obj)
        {
            base.Overlap(obj);
            if (obj is Player)
            {
                // Перемещаем объект на новое место
                Random rnd = new Random();
                X = rnd.Next(0, 400);
                Y = rnd.Next(0, 400);
                ResetTimer(); // Перезапуск таймера при касании игроком
            }
        }
        // Отсчет времени
        public void Tick()
        {
            timeLeft--;
            if (timeLeft <= 0)
            {
                // Перерождение кругов
                OverlapTick();
            }
        }
        // Пересоздание кружка при истечении времени
        private void OverlapTick()
        {
            Random rnd = new Random();
            X = rnd.Next(0, 700);
            Y = rnd.Next(100, 350);
            ResetTimer(); // Перезапуск таймера
        }
        //Рестарт времени
        private void ResetTimer()
        {
            timeLeft = maxTime;
        }

    }
}
