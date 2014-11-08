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


        public OldMan(PlayerIndex index) : base(index)
        {
            Speed = 10;
        }

        public override void Update(GameTime gameTime)
        {
            feelVector = GamePadState.ThumbSticks.Right;
            if (feelVector.Length() > 50f)
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
            
        }

        public void OnHit()
        {

        }
    }
}
