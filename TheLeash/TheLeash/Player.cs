using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheLeash
{
    class Player
    {
        private float x, y;
        private int speed;
        private Dictionary<string, Animation> animations;
        private string currentAnimation;

        private PlayerIndex playerIndex;
        private GamePadState gamePadState;

        public float X
        {
            get;
            set;
        }

        public float Y
        {
            get;
            set;
        }

        public int Speed
        {
            get;
            set;
        }

        public PlayerIndex PlayerIndex
        {
            get { return playerIndex; }
        }

        public GamePadState GamePadState
        {
            get { return gamePadState; }
        }  

        public Player(PlayerIndex index)
        {
            x = 0;
            y = 0;
            speed = 0;

            playerIndex = index;
        }

        public void AddAnimation(string animationName, Animation animation) 
        {

        }

        public virtual void Update(GameTime gameTime)
        {
            gamePadState = GamePad.GetState(playerIndex);
        }

        public virtual void Draw(GameTime gameTime)
        {

        }

        public virtual void Move()
        {

        }

        public virtual void Hit()
        {

        }
    }
}
