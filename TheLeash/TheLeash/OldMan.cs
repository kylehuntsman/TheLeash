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

            if (feelVector.Length() > .25f)
            {
                FeelAround();
            }

            GamePad.SetVibration(PlayerIndex, 0f, (float) vibratePercentage * 1f);
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
            double toDogDirection = GetDirection(toDog);
            double feelDirection = GetDirection(feelVector);

            double c = Math.Sqrt((toDog.X - feelVector.X) * (toDog.X - feelVector.X) + (toDog.Y - feelVector.Y) * (toDog.Y - feelVector.Y));
            double diffRadians = Math.Acos((toDog.LengthSquared() + feelVector.LengthSquared() - c * c) / 2 * toDog.Length() * feelVector.Length());
            double diffDegrees = 360 * (diffRadians / (2 * Math.PI));


            if (diffDegrees < 90f)
            {
                vibratePercentage = 1 - (diffDegrees / 90);
            }

            vibratePercentage = 0;
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
