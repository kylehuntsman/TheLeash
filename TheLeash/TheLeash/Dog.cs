using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheLeash
{
    class Dog : Player
    {
        private Vector2 moveVector;
        private Vector2 velocity;

        private bool hasBarked;
        private bool hasGrowled;

        private AudioEmitter emitter;
        private SoundEffect barkSound;
        private SoundEffect growlSound;
        private SoundEffect wimperSound;

        public AudioEmitter Emitter
        {
            get { return emitter; }
        }

        public Dog(PlayerIndex index)
            : this(index, 0, 0) {}

        public Dog(PlayerIndex index, float x, float y)
            : base(index, x, y)
        {
            Speed = 20;
            moveVector = new Vector2();
            velocity = new Vector2(0, 0);

            hasBarked = false;
            hasGrowled = false;

            Bounds = new Rectangle((int)X, (int)Y + 9, 26, 9);

            emitter = new AudioEmitter();
        }

        public override void LoadContent(ContentManager content)
        {
            Animation dogWalking = new Animation(content.Load<Texture2D>(@"Images/TestImages/TestDog_Anim"),
               new Point(26, 18), new Point(0, 0), new Point(1, 2), 100);
            Players.Dog.AddAnimation("walking", dogWalking);

            Animation dogStanding = new Animation(content.Load<Texture2D>(@"Images/TestImages/TestDog_Anim"),
                new Point(26, 18), new Point(0, 0), new Point(1, 1), int.MaxValue);
            Players.Dog.AddAnimation("standing", dogStanding);

            Players.Dog.CurrentAnimationName = "standing";

            barkSound = content.Load<SoundEffect>(@"Audio/bark");
            growlSound = content.Load<SoundEffect>(@"Audio/growl");
            wimperSound = content.Load<SoundEffect>(@"Audio/dogYelp");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (!Alive)
            {
                return;
            }

            Move(gameTime);
            Actions();
        }

        // Movement
        public override void Move(GameTime gameTime)
        {
            GamePadDPad dPad = GamePadState.DPad;
            moveVector = GamePadState.ThumbSticks.Left;
            velocity.X = 0;
            velocity.Y = 0;

            CurrentAnimationName = "standing";

            // DPad Movement
            if (dPad.Left == ButtonState.Pressed)
            {
                velocity.X = -Speed;
                CurrentAnimationName = "walking";
            }

            if (dPad.Right == ButtonState.Pressed)
            {
                velocity.X = Speed;
                CurrentAnimationName = "walking";
            }

            if (dPad.Up == ButtonState.Pressed)
            {
                velocity.Y = -Speed;
                CurrentAnimationName = "walking";
            }

            if (dPad.Down == ButtonState.Pressed)
            {
                velocity.Y = Speed;
                CurrentAnimationName = "walking";
            }

            // Left Thumbstick 
            if (moveVector.X > 0)
            {
                velocity.X = Speed;
                CurrentAnimationName = "walking";
            }

            if (moveVector.X < 0)
            {
                velocity.X = -Speed;
                CurrentAnimationName = "walking";
            }

            if (moveVector.Y > 0)
            {
                velocity.Y = -Speed;
                CurrentAnimationName = "walking";
            }

            if (moveVector.Y < 0)
            {
                velocity.Y = Speed;
                CurrentAnimationName = "walking";
            }

            if (X < 0)
            {
                X = 0;
                velocity.X = 0;
            }

            if (X + Bounds.Width > 1080)
            {
                X = 1080 - Bounds.Width;
                velocity.X = 0;
            }

            if (Y < 0)
            {
                Y = 0;
                velocity.Y = 0;
            }

            if (Y + Bounds.Height > 720)
            {
                Y = 720 - Bounds.Height;
                velocity.Y = 0;
            }

            X += velocity.X * (float)(gameTime.ElapsedGameTime.Milliseconds / 200f);
            Y += velocity.Y * (float)(gameTime.ElapsedGameTime.Milliseconds / 200f);

            Bounds = new Rectangle((int)X, (int)Y + 9, 26, 9);
            emitter.Position = new Vector3(X / 8f, 0, Y / 8f);
        }

        // Actions
        private void Actions()
        {
            if (GamePadState.Buttons.A == ButtonState.Pressed && hasBarked == false)
            {
                Bark();
                hasBarked = true;
            }

            if (GamePadState.Buttons.B == ButtonState.Pressed && hasGrowled == false)
            {
                Growl();
                hasGrowled = true;
            }

            if (GamePadState.Buttons.A == ButtonState.Released)
            {
                hasBarked = false;
            }

            if (GamePadState.Buttons.B == ButtonState.Released)
            {
                hasGrowled = false;
            }
        }

        private void Bark()
        {
            Vector2 toOldMan = new Vector2(Players.OldMan.X - X, -1 * (Players.OldMan.Y - Y));
            float distanceToOldMan = toOldMan.Length();

            SoundEffectInstance bark = barkSound.CreateInstance();
            bark.Apply3D(Players.OldMan.AudioListener, emitter);
            bark.Play();

            Console.WriteLine("Bark!");
        }

        private void Growl()
        {
            Vector2 toOldMan = new Vector2(Players.OldMan.X - X, -1 * (Players.OldMan.Y - Y));
            float distanceToOldMan = toOldMan.Length();

            SoundEffectInstance growl = growlSound.CreateInstance();
            growl.Apply3D(Players.OldMan.AudioListener, emitter);
            growl.Play();
            Console.WriteLine("Grrrrrrrr!");
        }

        public override void Hit()
        {
            base.Hit();
            if (Alive)
            {
                Vector2 toOldMan = new Vector2(Players.OldMan.X - X, -1 * (Players.OldMan.Y - Y));
                float distanceToOldMan = toOldMan.Length();

                SoundEffectInstance wimper = wimperSound.CreateInstance();
                wimper.Apply3D(Players.OldMan.AudioListener, emitter);
                wimper.Play();

                Console.WriteLine("Dog has died!");
                CurrentAnimationName = "standing";
                Alive = false;
            }
        }
    }
}
