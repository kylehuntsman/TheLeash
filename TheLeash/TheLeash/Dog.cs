using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheLeash
{
    class Dog : Player
    {
        public Dog(PlayerIndex index)
            : base(index)
        {
            X = 20;
            Y = 20;
            Speed = 5;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void Move()
        {
            base.Move();
        }

        public override void Hit()
        {
            base.Hit();
        }
    }
}
