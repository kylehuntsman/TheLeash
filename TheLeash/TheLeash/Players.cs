using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheLeash
{
    class Players
    {
        public static OldMan OldMan = new OldMan(PlayerIndex.One);
        public static Dog Dog = new Dog(PlayerIndex.Two);
    }
}
