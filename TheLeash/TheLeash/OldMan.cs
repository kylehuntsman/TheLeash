using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheLeash
{
    class OldMan : Player
    {
        private bool alive;

        private Vector2 feelVector ;
        private double vibratePercentage;

        private Vector2 moveVector;
        private Vector2 velocity;

        public OldMan(PlayerIndex index)
            : base(index)
        {
            Speed = 10;
            feelVector = new Vector2();
            moveVector = new Vector2();
            velocity = new Vector2(0, 0);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            FeelingMechanic();
            Move(gameTime);
            Console.WriteLine(X + " " + Y);
        }

        public override void Move(GameTime gameTime)
        {
            moveVector = GamePadState.ThumbSticks.Left;
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

            X += velocity.X * (float) (gameTime.ElapsedGameTime.Milliseconds / 200f);
            Y += velocity.Y * (float) (gameTime.ElapsedGameTime.Milliseconds / 200f);
        }

        // Feeling Mechanic
        private void FeelingMechanic()
        {
            feelVector = GamePadState.ThumbSticks.Right;
            vibratePercentage = 0;

            if (feelVector.Length() > .75f)
            {
                FeelAround();
            }

            if (vibratePercentage > .8d)
            {
                GamePad.SetVibration(PlayerIndex, (float)vibratePercentage * 1f, (float)vibratePercentage * 1f);
            }
            else
            {
                GamePad.SetVibration(PlayerIndex, 0f, (float)vibratePercentage * 1f);
            }
        }

        private void FeelAround()
        {
            Vector2 toDog = new Vector2(Players.Dog.X - X, -1 * (Players.Dog.Y - Y));
            toDog.Normalize();
            feelVector.Normalize();
            double toDogDirection = GetDirection(toDog);
            double feelDirection = GetDirection(feelVector);

            double c = Math.Sqrt((toDog.X - feelVector.X) * (toDog.X - feelVector.X) + (toDog.Y - feelVector.Y) * (toDog.Y - feelVector.Y));
            double numerator = toDog.LengthSquared() + feelVector.LengthSquared() - (c * c);
            double denominator = 2f * toDog.Length() * feelVector.Length();
            double value = numerator / denominator;
            double diffRadians = Math.Acos(Math.Round(value, 6));
            double diffDegrees = 360d * (diffRadians / (2d * Math.PI));

            if (diffDegrees < 90d)
            {
                vibratePercentage = 1 - (diffDegrees / 90d);
            }
        }

        private double GetDirection(Vector2 vector)
        {
            double degrees = 0;
            double radians = Math.Asin(vector.Y / vector.Length());
            if (radians < 0)
            {
                radians = 2 * Math.PI + radians;
            }

            degrees = 360 * (radians / 2 * Math.PI);
            return degrees;
        }
    }
}
