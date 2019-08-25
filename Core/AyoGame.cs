using System;
using AyoLib.Core;
using AyoLib.Inputs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AyoLib
{
    public class AyoGame : Game
    {
        public static AyoGame CurrentGame;

        public static int ResolutionWidth { get; private set; }
        public static int ResolutionHeight { get; private set; }
        public static int WindowWidth { get; private set; }
        public static int WindowHeight { get; private set; }

        private string _title;
        private AyoScene _currentScene;
        private VirtualScreen _virtualScreen;
        
        public AyoGame(
            AyoScene startingScene = null, 
            string title = "AyoGame", 
            int resolutionWidth = 320, 
            int resolutionHeight = 180, 
            int windowWidth = 640, 
            int windowHeight = 360, 
            bool fullScreen = false)
        {
            CurrentGame = this;
            Content.RootDirectory = "Content";

            ResolutionWidth = resolutionWidth;
            ResolutionHeight = resolutionHeight;

            WindowWidth = windowWidth;
            WindowHeight = windowHeight;

            AyoGameManager.Manager.Graphics = new GraphicsDeviceManager(this);
            AyoGameManager.Manager.Graphics.IsFullScreen = fullScreen;
            AyoGameManager.Manager.Graphics.PreferredBackBufferWidth = WindowWidth;
            AyoGameManager.Manager.Graphics.PreferredBackBufferHeight = WindowHeight;

            AyoGameManager.Manager.InitializeDefaultGameSystemEntities();

            _title = title;
            
            if (startingScene == null)
            {
                startingScene = new BasicScene();
            }

            _currentScene = startingScene;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Window.Title = _title;

            _virtualScreen = new VirtualScreen(AyoGame.CurrentGame.GraphicsDevice, ResolutionWidth, ResolutionHeight);

            _currentScene.Initialize();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            AyoGameManager.Manager.SpriteBatch = new SpriteBatch(GraphicsDevice);

            //TODO: use this.Content to load your game content here 
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // For Mobile devices, this logic will close the Game when the Back button is pressed
            // Exit() is obsolete on iOS
#if !__IOS__ && !__TVOS__
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
#endif

            _virtualScreen.Update(gameTime);
            _currentScene.Update(gameTime);

            AyoGameManager.Manager.UpdateGameSystemEntities(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //Drawing Render Target
            _virtualScreen.InitRenderer();
            AyoGameManager.Manager.SpriteBatch.Begin(
                sortMode: SpriteSortMode.Deferred,
                blendState: null,
                samplerState: SamplerState.PointClamp,
                depthStencilState: null,
                rasterizerState: null,
                effect: null,
                transformMatrix: null
            );

            _currentScene.Draw(AyoGameManager.Manager.SpriteBatch);

            AyoGameManager.Manager.SpriteBatch.End();
            _virtualScreen.ClearRenderer();



            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Drawing BackBuffer
            AyoGameManager.Manager.SpriteBatch.Begin();

            _virtualScreen.Draw(AyoGameManager.Manager.SpriteBatch);

            AyoGameManager.Manager.SpriteBatch.End();

            base.Draw(gameTime);
        }

        public AyoScene GetCurrentScene()
        {
            return _currentScene;
        }

        
    }
}

