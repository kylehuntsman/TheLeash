using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheLeash
{
    class Player
    {
        private float x, y;
        private float speed;
        private Dictionary<string, Animation> animations;
        private string currentAnimationName;

        private PlayerIndex playerIndex;
        private GamePadState gamePadState;

        public float X
        {
            get { return this.x; }
            set { this.x = value; }
        }

        public float Y
        {
            get { return this.y; }
            set { this.y = value; }
        }

        public float Speed
        {
            get;
            set;
        }

        public string CurrentAnimationName
        {
            get
            {
                return this.currentAnimationName;
            }
            set
            {
                this.currentAnimationName = value;
            }
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
            animations = new Dictionary<string, Animation>();
            currentAnimationName = "";
        }

        public void AddAnimation(string animationName, Animation animation) 
        {
            animations.Add(animationName, animation);
        }

        public virtual void Update(GameTime gameTime)
        {
            gamePadState = GamePad.GetState(playerIndex);

            if (animations.Keys.Contains(currentAnimationName))
            {
                Animation currentAnimation = animations[currentAnimationName];
                currentAnimation.Update(gameTime);
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (animations.Keys.Contains(currentAnimationName))
            {
                Animation currentAnimation = animations[currentAnimationName];
                spriteBatch.Draw(currentAnimation.TextureImage, new Vector2(x,y),
                    currentAnimation.GetCurrentFrameRectangle(), Color.White);
            }
        }

        public virtual void Move(GameTime gameTime)
        {

        }

        public virtual void Hit()
        {

        }
    }
}
