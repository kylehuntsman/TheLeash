using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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

        private bool hasBarked;
        private bool hasGrowled;

        public Dog(PlayerIndex index)
            : this(index, 0, 0) {}

        public Dog(PlayerIndex index, float x, float y)
            : base(index, x, y)
        {
            Speed = 20;
            moveVector = new Vector2();
            velocity = new Vector2(0, 0);

            hasBarked = false;
            hasGrowled = false;
        }

        public override void LoadContent(ContentManager content)
        {
            Animation dogWalking = new Animation(content.Load<Texture2D>(@"Images/TestImages/TestDog_Anim"),
               new Point(32, 32), new Point(0, 0), new Point(1, 2), 100);
            Players.Dog.AddAnimation("walking", dogWalking);

            Animation dogStanding = new Animation(content.Load<Texture2D>(@"Images/TestImages/TestDog_Anim"),
                new Point(32, 32), new Point(0, 0), new Point(1, 1), int.MaxValue);
            Players.Dog.AddAnimation("standing", dogStanding);

            Players.Dog.CurrentAnimationName = "standing";
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Move(gameTime);
            Actions();
        }

        // Movement
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

        // Actions
        private void Actions()
        {
            if (GamePadState.Buttons.A == ButtonState.Pressed && hasBarked == false)
            {
                Bark();
                hasBarked = true;
            }

            if (GamePadState.Buttons.B == ButtonState.Pressed && hasGrowled == false)
            {
                Growl();
                hasGrowled = true;
            }

            if (GamePadState.Buttons.A == ButtonState.Released)
            {
                hasBarked = false;
            }

            if (GamePadState.Buttons.B == ButtonState.Released)
            {
                hasGrowled = false;
            }
        }

        private void Bark()
        {
            Console.WriteLine("Bark!");
        }

        private void Growl()
        {
            Console.WriteLine("Grrrrrrrr!");
        }

        public override void Hit()
        {
            base.Hit();
        }
    }
}
