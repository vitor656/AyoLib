using System;
using AyoLib.Core;
using AyoLib.Core.Managers;
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
        public static string Title { get; private set; }
        public static bool MouseVisible
        {
            get
            {
                return CurrentGame.IsMouseVisible;
            }

            set
            {
                CurrentGame.IsMouseVisible = value;
            }
        }

        public static bool FullScreen
        {
            get
            {
                return AyoGameManager.Manager.Graphics.IsFullScreen;
            }

            set
            {
                AyoGameManager.Manager.Graphics.IsFullScreen = value;
            }
        }

        public static AyoScenesManager ScenesManager
        {
            get
            {
                return AyoGameManager.Manager.AyoScenesManager;
            }
        }

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

            Title = title;

            AyoGameManager.Manager.Graphics = new GraphicsDeviceManager(this);
            AyoGameManager.Manager.Graphics.IsFullScreen = fullScreen;
            AyoGameManager.Manager.Graphics.PreferredBackBufferWidth = WindowWidth;
            AyoGameManager.Manager.Graphics.PreferredBackBufferHeight = WindowHeight;

            AyoGameManager.Manager.AyoScenesManager.SetupScene(startingScene);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Window.Title = Title;
            
            AyoGameManager.Manager.InitializeVirtualScreen(CurrentGame.GraphicsDevice, ResolutionWidth, ResolutionHeight);
            AyoGameManager.Manager.Initialize();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            AyoGameManager.Manager.LoadContent(GraphicsDevice);
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

            AyoGameManager.Manager.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            AyoGameManager.Manager.DrawAtVirtualScreen();
            AyoGameManager.Manager.DrawAtBackBuffer();

            base.Draw(gameTime);
        }

        public AyoScene GetCurrentScene()
        {
            return AyoGameManager.Manager.AyoScenesManager.CurrentScene;
        }

        public static void ToogleFullScreen()
        {
            AyoGameManager.Manager.Graphics.ToggleFullScreen();
        }
        
    }
}

