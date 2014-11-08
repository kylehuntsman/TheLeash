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
        private Vector2 feelVector = new Vector2();
        private double vibratePercentage;

        public OldMan(PlayerIndex index)
            : base(index)
        {
            Speed = 10;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            feelVector = GamePadState.ThumbSticks.Right;
            vibratePercentage = 0;

            if (feelVector.Length() > .75f)
            {
                FeelAround();
            }

            if (vibratePercentage > .8d)
            {
                GamePad.SetVibration(PlayerIndex, (float) vibratePercentage * 1f, (float) vibratePercentage * 1f);
            }
            else
            {
                GamePad.SetVibration(PlayerIndex, 0f, (float)vibratePercentage * 1f);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            
        }

        public override void Move()
        {
            
        }

        public void FeelAround()
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

        /// <summary>
        /// Returns the direction of the vector in degrees, because degrees are easy
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
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
