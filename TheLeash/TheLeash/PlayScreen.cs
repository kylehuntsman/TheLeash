﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace TheLeash
{
    public class PlayScreen : GameScreen
    {
        Game1 game;
        KeyboardState keyboardState;
        Texture2D image;
        CarManager carManager;
        SoundEffect bgSound;
        SoundEffectInstance bgSoundInstance;
        Rectangle winZone;

        public PlayScreen(Game1 game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
            this.game = game;
            carManager = new CarManager();
            Players.OldMan = new OldMan(PlayerIndex.One, 540, 650);
            Players.Dog = new Dog(PlayerIndex.Two, 560, 670);
            winZone = new Rectangle(460, 100, 60, 40);
        }

        public override void LoadContent(ContentManager content)
        {
            Console.WriteLine("Loading Content For PlayScreen");
            this.image = content.Load<Texture2D>("Images/Backgrounds/Level");
            this.bgSound = content.Load<SoundEffect>(@"Audio/trafAmbi");
            this.bgSoundInstance = bgSound.CreateInstance();
            carManager.LoadContent(content);
            Players.OldMan.LoadContent(content);
            Players.Dog.LoadContent(content);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Escape))
                game.Exit();

            if (carManager.CarCount() < 10)
            {
                carManager.AddCar();
            }

            carManager.Update(gameTime);

            Players.OldMan.Update(gameTime);
            Players.Dog.Update(gameTime);

            if (winZone.Intersects(Players.OldMan.Bounds))
            {    
                game.ActiveScreen = game.StartScreen;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(image, new Vector2(0,0), Color.White);
            base.Draw(gameTime);

            DrawPlayers(gameTime, spriteBatch);
            carManager.Draw(gameTime, spriteBatch);
        }

        private void DrawPlayers(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Players.OldMan.Draw(gameTime, spriteBatch);
            Players.Dog.Draw(gameTime, spriteBatch);
        }

        public override void Show()
        {
            base.Show();
            bgSoundInstance = bgSound.CreateInstance();
            bgSound.Play();
        }

        public override void Hide()
        {
            base.Hide();
            bgSoundInstance.Stop();
            bgSoundInstance.Dispose();
        }
    }
}
