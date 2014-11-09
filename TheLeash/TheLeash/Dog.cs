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
        private Vector2 moveVector;
        private Vector2 velocity;

        public Dog(PlayerIndex index)
            : this(index, 0, 0) {}

        public Dog(PlayerIndex index, float x, float y)
            : base(index, x, y)
        {
            Speed = 20;
            moveVector = new Vector2();
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
            moveVector = GamePadState.ThumbSticks.Left;
            velocity.X = 0;
            velocity.Y = 0;

            CurrentAnimationName = "standing";

            // DPad Movement
            if (dPad.Left == ButtonState.Pressed)
            {
                velocity.X = -Speed;
                CurrentAnimationName = "walking";
            }

            if (dPad.Right == ButtonState.Pressed)
            {
                velocity.X = Speed;
                CurrentAnimationName = "walking";
            }

            if (dPad.Up == ButtonState.Pressed)
            {
                velocity.Y = -Speed;
                CurrentAnimationName = "walking";
            }

            if (dPad.Down == ButtonState.Pressed)
            {
                velocity.Y = Speed;
                CurrentAnimationName = "walking";
            }

            // Left Thumbstick 
            if (moveVector.X > 0)
            {
                velocity.X = Speed;
                CurrentAnimationName = "walking";
            }

            if (moveVector.X < 0)
            {
                velocity.X = -Speed;
                CurrentAnimationName = "walking";
            }

            if (moveVector.Y > 0)
            {
                velocity.Y = -Speed;
                CurrentAnimationName = "walking";
            }

            if (moveVector.Y < 0)
            {
                velocity.Y = Speed;
                CurrentAnimationName = "walking";
            }

            X += velocity.X * (float)(gameTime.ElapsedGameTime.Milliseconds / 200f);
            Y += velocity.Y * (float)(gameTime.ElapsedGameTime.Milliseconds / 200f);
        }

        public override void Hit()
        {
            base.Hit();
        }
    }
}
