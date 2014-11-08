using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheLeash
{
    class VibrationTest
    {
        public void Update(GameTime gameTime)
        {
            GamePad.SetVibration(PlayerIndex.One, 0, 1);
        }
    }
}
