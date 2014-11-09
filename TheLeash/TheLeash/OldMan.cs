using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheLeash
{
    class OldMan : Player
    {
        private const double MIN_PERCENT_FOR_LEFT_VIB = .8d;
        private const double RIGHT_VIB_PENALTY = .02d;
        private const int MAX_DOG_DISTANCE = 64;

        private bool alive;

        private Vector2 feelVector;
        private double leftVibPercentage;
        private double rightVibPercentage;

        private Vector2 moveVector;
        private Vector2 velocity;

        private bool hasAlerted;

        public OldMan(PlayerIndex index) 
            : this(index, 0, 0) {}

        public OldMan(PlayerIndex index, float x, float y)
            : base(index, x, y)
        {
            Speed = 10;
            feelVector = new Vector2();
            moveVector = new Vector2();
            velocity = new Vector2(0, 0);
            hasAlerted = false;

            Bounds = new Rectangle((int)X, (int)Y + 16, 19, 16);
        }

        public override void LoadContent(ContentManager content)
        {
            Animation oldManWalking = new Animation(content.Load<Texture2D>(@"Images/TestImages/TestOldMan_Anim"),
               new Point(19, 32), new Point(0, 0), new Point(1, 2), 100);
            Players.OldMan.AddAnimation("walking", oldManWalking);

            Animation oldManStanding = new Animation(content.Load<Texture2D>(@"Images/TestImages/TestOldMan_Anim"),
               new Point(19, 32), new Point(0, 0), new Point(1, 1), 100);
            Players.OldMan.AddAnimation("standing", oldManStanding);

            Players.OldMan.CurrentAnimationName = "standing";
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (!IsAlive)
            {
                return;
            }

            FeelingMechanic();
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

            // DPad
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

            X += velocity.X * (float) (gameTime.ElapsedGameTime.Milliseconds / 200f);
            Y += velocity.Y * (float) (gameTime.ElapsedGameTime.Milliseconds / 200f);

            Bounds = new Rectangle((int)X, (int)Y + 16, 19, 16);
        }

        // Feeling Mechanic
        private void FeelingMechanic()
        {
            if (!Players.Dog.IsAlive)
            {
                return;
            }

            Vector2 toDog = new Vector2(Players.Dog.X - X, -1 * (Players.Dog.Y - Y));
            float distanceToDog = toDog.Length();
            toDog.Normalize();

            feelVector = GamePadState.ThumbSticks.Right;
            feelVector.Normalize();
            leftVibPercentage = 0;
            rightVibPercentage = 0;

            if (feelVector.Length() > .75f)
            {
                double toDogDirection = GetDirection(toDog);
                double feelDirection = GetDirection(feelVector);

                double c = Math.Sqrt((toDog.X - feelVector.X) * (toDog.X - feelVector.X) + (toDog.Y - feelVector.Y) * (toDog.Y - feelVector.Y));
                double numerator = toDog.LengthSquared() + feelVector.LengthSquared() - (c * c);
                double denominator = 2f * toDog.Length() * feelVector.Length();
                double value = numerator / denominator;
                double diffRadians = Math.Acos(Math.Round(value, 6));
                double diffDegrees = diffRadians * (360d / ( 2d * Math.PI));

                if (diffDegrees < 90d)
                {
                    rightVibPercentage = 1 - (diffDegrees / 90d);
                }
            }

            if (rightVibPercentage > MIN_PERCENT_FOR_LEFT_VIB && distanceToDog <= MAX_DOG_DISTANCE)
            {
                leftVibPercentage = rightVibPercentage;
            }
            else if (distanceToDog > MAX_DOG_DISTANCE)
            {
                double outerDist = distanceToDog - MAX_DOG_DISTANCE;
                leftVibPercentage = 0d;
                rightVibPercentage -= outerDist * RIGHT_VIB_PENALTY;

                if (rightVibPercentage < 0)
                {
                    rightVibPercentage = 0d;
                }
            }

            GamePad.SetVibration(PlayerIndex, (float)leftVibPercentage * 1f, (float)rightVibPercentage * 1f);
        }

        // Actions
        private void Actions()
        {
            if (GamePadState.Buttons.RightStick == ButtonState.Pressed && hasAlerted == false)
            {
                Alert();
                hasAlerted = true;
            }

            if (GamePadState.Buttons.RightStick == ButtonState.Released)
            {
                hasAlerted = false;
            }
        }

        private void Alert()
        {
            Console.WriteLine("Where's my dog!!!");
        }

        public override void Hit()
        {
            if (IsAlive)
            {
                Console.WriteLine("The old man has died!");
                CurrentAnimationName = "standing";
                IsAlive = false;
            }
        }

        private double GetDirection(Vector2 vector)
        {
            double degrees = 0;
            double radians = Math.Asin(vector.Y / vector.Length());
            if (radians < 0)
            {
                radians = 2 * Math.PI + radians;
            }

            degrees = radians * (360d / (2d * Math.PI));
            return degrees;
        }
    }
}
