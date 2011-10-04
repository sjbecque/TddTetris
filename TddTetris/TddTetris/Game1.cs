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

namespace TddTetris
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Texture2D block;
        private DateTime lastUpdateTime;
        private Vector2 blockPos;
        private int numberOfRows;
        private Field field;
        private InputQueue inputQueue;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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

            lastUpdateTime = DateTime.Now;
            blockPos = new Vector2(0, 0);
            inputQueue = new InputQueue();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            block = Content.Load<Texture2D>("Block");

            numberOfRows = (GraphicsDevice.Viewport.Height - 60)/ block.Height;

            field = new Field(12, numberOfRows);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            KeyboardState keyState = Keyboard.GetState();
            var pressedKeys = keyState.GetPressedKeys().ToList<Keys>();

            List<Keys> newlyPressedKeys = inputQueue.keyPress(pressedKeys);

            // Allows the game to exit
            if (newlyPressedKeys.Contains(Keys.Escape))
            {
                this.Exit();
            }

            if (newlyPressedKeys.Contains(Keys.Left))
            {
                blockPos.X--;
            }

            if (newlyPressedKeys.Contains(Keys.Right))
            {
                blockPos.X++;
            }

            // TODO: Add your update logic here
            if ((DateTime.Now - lastUpdateTime).TotalMilliseconds > 500)
            {
                blockPos.Y++;
                if (blockPos.Y >= numberOfRows)
                {
                    blockPos.Y = 0;
                    blockPos.X++;
                }


                lastUpdateTime = DateTime.Now;
                base.Update(gameTime);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();
            spriteBatch.Draw(block, new Rectangle((int)blockPos.X*block.Width, (int)blockPos.Y*block.Height, block.Width, block.Height), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
