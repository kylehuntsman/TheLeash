using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        private Rectangle bounds;

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

        public Rectangle Bounds
        {
            get;
            set;
        }

        public Car(Animation animation, float x, float y, float speed) 
        {
            this.x = x;
            this.y = y;
            this.speed = speed;
            this.animation = animation;
        }

        public virtual void Update(GameTime gameTime)
        {
            x += speed * (float)(gameTime.ElapsedGameTime.Milliseconds / 200f);
            animation.Update(gameTime);
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(animation.TextureImage, new Vector2(x, y),
                    animation.GetCurrentFrameRectangle(), Color.White);
        } 
    }
}
