namespace cn.justwin.pdf
{
    using System;
    using System.Runtime.CompilerServices;

    public class Point
    {
        public Point(float left, float top)
        {
            this.Left = left;
            this.Top = top;
        }

        public float Left { get; set; }

        public float Top { get; set; }
    }
}

