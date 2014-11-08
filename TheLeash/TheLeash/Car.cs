using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheLeash
{
    class Car
    {
        private float x, y;
        private float speed;
        private Animation animation;

        public float X
        {
            get { return this.x; }
            set { this.x = value; }
        }

        public float Y
        {
            get { return this.y; }
            set { this.y = value; }
        }

        public float Speed
        {
            get { return this.speed; }
            set { this.speed = value; }
        }

        public Car(Animation animation, float x, float y, float speed) 
        {
            this.x = x;
            this.y = y;
            this.speed = speed;
            this.animation = animation;
        }
    }
}
