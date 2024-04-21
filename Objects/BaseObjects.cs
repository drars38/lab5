using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;

namespace lab5.Objects
{
    internal class BaseObjects
    {
        public float X;
        public float Y;
        public float Angle;
        // добавил поле делегат, к которому можно будет привязать реакцию на собыития
        public Action<BaseObjects, BaseObjects> OnOverlap;


        public BaseObjects(float x, float y, float angle)
        {
            X = x;
            Y = y;
            Angle = angle;
        }

        public Matrix GetTransorm()
        {
            var matrix = new Matrix();
            matrix.Translate(X, Y);
            matrix.Rotate(Angle);

            return matrix;


        }
        public virtual GraphicsPath GetGraphicsPath()
        {
            // пока возвращаем пустую форму
            return new GraphicsPath();
        }

        public virtual bool Overlaps(BaseObjects obj, Graphics g)
        {
            // берем информацию о форме
            var path1 = this.GetGraphicsPath();
            var path2 = obj.GetGraphicsPath();

            // применяем к объектам матрицы трансформации
            path1.Transform(this.GetTransorm());
            path2.Transform(obj.GetTransorm());

            // используем класс Region, который позволяет определить 
            // пересечение объектов в данном графическом контексте
            var region = new Region(path1);
            region.Intersect(path2); // пересекаем формы
            return !region.IsEmpty(g); // если полученная форма не пуста то значит было пересечение
        }
    
        public virtual void Overlap(BaseObjects obj)
        {
            if(this.OnOverlap != null)
            {
                this.OnOverlap(this,obj );
            }
        }


    public virtual void Render(Graphics g)
        {

        }

    }
}
