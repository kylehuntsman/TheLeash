#region File Information
/*
 * 
 */
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#endregion

namespace TheLeash
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        #region Initialize
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            InitializePlayers();
            base.Initialize();
        }
        #endregion

        private void InitializePlayers()
        {
            Players.OldMan = new OldMan(PlayerIndex.One);
            Players.Dog = new Dog(PlayerIndex.Two);
        }

        #region Load Content
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            LoadOldMan();
            LoadDog();
        }
        #endregion

        private void LoadOldMan()
        {
            Animation oldManWalking = new Animation(Content.Load<Texture2D>(@"Images/TestImages/TestOldMan_Anim"),
               new Point(32, 32), new Point(0, 0), new Point(1, 2), 100);
            Players.OldMan.AddAnimation("walking", oldManWalking);

            Animation oldManStanding = new Animation(Content.Load<Texture2D>(@"Images/TestImages/TestOldMan_Anim"),
               new Point(32, 32), new Point(0, 0), new Point(1, 1), 100);
            Players.OldMan.AddAnimation("standing", oldManStanding);

            Players.OldMan.CurrentAnimationName = "standing";
        }

        private void LoadDog()
        {
            Animation dogWalking = new Animation(Content.Load<Texture2D>(@"Images/TestImages/TestDog_Anim"),
               new Point(32, 32), new Point(0, 0), new Point(1, 2), 100);
            Players.Dog.AddAnimation("walking", dogWalking);

            Animation dogStanding = new Animation(Content.Load<Texture2D>(@"Images/TestImages/TestDog_Anim"),
                new Point(32, 32), new Point(0, 0), new Point(1, 1), int.MaxValue);
            Players.Dog.AddAnimation("standing", dogStanding);

            Players.Dog.CurrentAnimationName = "standing";
        }

        #region Unload Content
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            
        }
        #endregion

        #region Update
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
            Players.OldMan.Update(gameTime);
            Players.Dog.Update(gameTime);
        }
        #endregion

        #region Draw
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            Players.OldMan.Draw(gameTime, spriteBatch);
            Players.Dog.Draw(gameTime, spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
        #endregion
    }
}
