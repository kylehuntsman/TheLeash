using Microsoft.Xna.Framework;
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
        }

        public override void Draw(GameTime gameTime)
        {
            
        }

        public override void Move()
        {
            
        }

        public float FeelAround()
        {
            Vector2 toDog = new Vector2(Players.Dog.X - X, -1 * (Players.Dog.Y - Y));
            Vector2 dogLeftNormalDir = new Vector2(-toDog.Y, toDog.X);
            Vector2 dogRightNormalDir = new Vector2(toDog.Y, -toDog.X);

            double dogDirection = Math.Asin(toDog.Y / toDog.Length());
            double feelDirection = Math.Asin(feelVector.Y / feelVector.Length());

            Console.WriteLine(dogDirection);
            return 0f;
        }

        private double getDirection(Vector2 vector)
        {

        }
    }
}
