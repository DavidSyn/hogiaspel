using HogiaSpel.Entities;
using HogiaSpel.Enums;
using HogiaSpel.GlobalLists;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace HogiaSpel
{
    public class Engine : Game
    {
        private static int WINDOW_WIDTH = 1280;
        private static int WINDOW_HEIGHT = 720;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _fpsFont;

        public Engine()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            _graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
            _graphics.ApplyChanges();

            this.Content.RootDirectory = "Content";
            this.Window.Title = "HogiaSpel";
            this.IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //--- FPS Stuff ---//
            _fpsFont = Content.Load<SpriteFont>("text");

            // TODO: use this.Content to load your game content here
            LevelFactory.LoadLevelOne(Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            var entities = EntityList.Instance;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            // TODO: Add your update logic here

            for (int i = 0; i < entities.Count(); i++)
            {
                entities.GetEntity(i).Update(gameTime);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            var entities = EntityList.Instance;
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            for (int i = 0; i < entities.Count(); i++)
            {
                entities.GetEntity(i).Draw(_spriteBatch);
            }

            RenderFPS(gameTime);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void RenderFPS(GameTime gameTime)
        {
            var fps = Math.Round(1 / gameTime.ElapsedGameTime.TotalSeconds, 2);
            _spriteBatch.DrawString(_fpsFont, "fps: " + fps, new Vector2(5, 5), Color.Black);
        }
    }
}
