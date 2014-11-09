using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheLeash
{
    class Car
    {
        private float x, y;
        private float speed;
        private Animation animation;
        private Rectangle bounds;

        private AudioEmitter emitter;
        private SoundEffect soundEffect;
        private SoundEffectInstance soundInstance;

        public SoundEffect SoundEffect
        {
            get { return soundEffect; }
            set
            {
                soundEffect = value;
                soundInstance = soundEffect.CreateInstance();
                soundInstance.IsLooped = true;
            }
        }

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
            get { return this.speed; }
            set { this.speed = value; }
        }

        public Rectangle Bounds
        {
            get { return bounds; }
            set { bounds = value; }
        }

        public AudioEmitter Emitter
        {
            get { return emitter; }
        }

        public Car(Animation animation, float x, float y, float speed) 
        {
            this.x = x;
            this.y = y;
            this.speed = speed;
            this.animation = animation;

            bounds = new Rectangle((int)X, (int)Y + animation.FrameSize.Y / 2, (int)animation.FrameSize.X, animation.FrameSize.Y / 2);

            emitter = new AudioEmitter();
        }

        public virtual void Update(GameTime gameTime)
        {
            Rectangle currentBounds = new Rectangle((int)X, (int)Y + animation.FrameSize.Y / 2, (int)animation.FrameSize.X, animation.FrameSize.Y / 2);
            x += speed * (float)(gameTime.ElapsedGameTime.Milliseconds / 200f);

            emitter.Position = new Vector3(X / 10f, 0, Y / 10f);
            animation.Update(gameTime);

            PlaySound();

            if (currentBounds.Intersects(Players.OldMan.Bounds))
            {
                Players.OldMan.Hit();
            }

            if (currentBounds.Intersects(Players.Dog.Bounds))
            {
                Players.Dog.Hit();
            }
        }

        private void PlaySound()
        {
            Vector2 toOldMan = new Vector2(Players.OldMan.X - X, -1 * (Players.OldMan.Y - Y));
            float distanceToOldMan = toOldMan.Length();

            if (distanceToOldMan < 120)
            {
                soundInstance.Apply3D(Players.OldMan.AudioListener, emitter);
                soundInstance.Play();
            }
            else if (soundInstance.State == SoundState.Playing)
            {
                soundInstance.Stop();
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            SpriteEffects effect;
            if (Speed > 0)
                effect = SpriteEffects.None;
            else
                effect = SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(animation.TextureImage,
                new Vector2(x, y), animation.GetCurrentFrameRectangle(), 
                Color.White, 0, new Vector2(0, 0), 1, effect, 0);
        } 
    }
}
