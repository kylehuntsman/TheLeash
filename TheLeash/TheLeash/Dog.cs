using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheLeash
{
    class Dog : Player
    {

        private Vector2 velocity;

        public Dog(PlayerIndex index)
            : base(index)
        {
            Speed = 20;

            velocity = new Vector2(0, 0);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Move(gameTime);
        }

        public override void Move(GameTime gameTime)
        {
            GamePadDPad dPad = GamePadState.DPad;
            velocity.X = 0;
            velocity.Y = 0;

            if (dPad.Left == ButtonState.Pressed)
            {
                velocity.X = -Speed;
            }

            if (dPad.Right == ButtonState.Pressed)
            {
                velocity.X = Speed;
            }

            if (dPad.Up == ButtonState.Pressed)
            {
                velocity.Y = -Speed;
            }

            if (dPad.Down == ButtonState.Pressed)
            {
                velocity.Y = Speed;
            }

            /*
            if (Math.Abs(moveVector.X) > Math.Abs(moveVector.Y))
            {
                velocity.X = Speed * (moveVector.X / Math.Abs(moveVector.X));
                velocity.Y = 0;
            }
            else if (Math.Abs(moveVector.X) < Math.Abs(moveVector.Y))
            {
                velocity.X = 0;
                velocity.Y = Speed * (moveVector.Y / Math.Abs(moveVector.Y));
            }
            else
            {
                velocity.X = 0;
                velocity.Y = 0;
            }
            */

            X += velocity.X * (float)(gameTime.ElapsedGameTime.Milliseconds / 200f);
            Y += velocity.Y * (float)(gameTime.ElapsedGameTime.Milliseconds / 200f);
        }

        public override void Hit()
        {
            base.Hit();
        }
    }
}
