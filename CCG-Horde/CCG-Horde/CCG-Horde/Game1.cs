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

namespace CCG_Horde
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>su
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static int screenWidth;
        public static int screenHeight;
        public static Rectangle screenRectangle;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            screenWidth = 1200;
            screenHeight = 720;
            screenRectangle = new Rectangle(0, 0, screenWidth, screenHeight);

            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            this.graphics.IsFullScreen = true;

        }

 

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            spriteBatch = new SpriteBatch(GraphicsDevice);

            TextureManager.sharedTextureManager.addTexture("clown", Content.Load<Texture2D>("Sprites/Clown"));
            TextureManager.sharedTextureManager.addTexture("background", Content.Load<Texture2D>("Sprites/CHESS_TEST"));
            TextureManager.sharedTextureManager.addTexture("board", Content.Load<Texture2D>("Sprites/CHESS_TILES"));
            TextureManager.sharedTextureManager.addTexture("player", Content.Load<Texture2D>("Sprites/Queen"));
            TextureManager.sharedTextureManager.addTexture("king", Content.Load<Texture2D>("Sprites/King"));

            GameFlowManager.myGame = this;
            GameFlowManager.mySpriteBatch = spriteBatch;
            GameFlowManager.sharedGameFlowManager.manualInit();



    
            Components.Add(GameFlowManager.sharedGameFlowManager);

            base.Initialize();
        }


        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
          //  spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

    

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

  


        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            base.Update(gameTime);
        }

  



        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
      

            base.Draw(gameTime);

            spriteBatch.End();
        }
    }
}
