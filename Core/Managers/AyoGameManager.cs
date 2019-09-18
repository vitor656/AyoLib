using AyoLib.Core;
using AyoLib.Core.Managers;
using AyoLib.Entities;
using AyoLib.Entities.Services;
using AyoLib.Inputs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AyoLib
{
    public class AyoGameManager
    {
        private static AyoGameManager _manager;
        public static AyoGameManager Manager {
            get {
                if (_manager == null)
                    _manager = new AyoGameManager();

                return _manager;
            }
        }
        
        public SpriteBatch SpriteBatch;
        public GraphicsDeviceManager Graphics;

        public AyoScenesManager AyoScenesManager = new AyoScenesManager();
        public List<GameSystemEntity> GameSystemEntities = new List<GameSystemEntity>();

        public VirtualScreen VirtualScreen;

        public void Initialize()
        {
            AyoScenesManager.Initialize();
            InitializeDefaultGameSystemEntities();
        }

        public void InitializeVirtualScreen(GraphicsDevice graphicsDevice, int resolutionWidth, int resolutionHeight)
        {
            Manager.VirtualScreen = new VirtualScreen(graphicsDevice, resolutionWidth, resolutionHeight);
            AyoGame.CurrentGame.Window.ClientSizeChanged += Manager.VirtualScreen.Window_ClientSizeChanged;
        }

        public void LoadContent(GraphicsDevice graphicsDevice)
        {
            Manager.SpriteBatch = new SpriteBatch(graphicsDevice);
        }

        public void Update(GameTime gameTime)
        {
            if (VirtualScreen != null)
                VirtualScreen.Update(gameTime);

            AyoScenesManager.Update(gameTime);
            UpdateGameSystemEntities(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            AyoScenesManager.Draw(spriteBatch);
        }

        public void DrawAtVirtualScreen()
        {
            if(VirtualScreen != null)
            {
                VirtualScreen.InitRenderer();

                Manager.SpriteBatch.Begin();
                Manager.Draw(Manager.SpriteBatch);
                Manager.SpriteBatch.End();

                VirtualScreen.ClearRenderer();
            }
        }

        public void DrawAtBackBuffer()
        {
            GraphicsDevice graphicsDevice = AyoGame.CurrentGame.GraphicsDevice;

            graphicsDevice.Clear(Color.Black);

            // Drawing BackBuffer
            Manager.SpriteBatch.Begin(
                sortMode: SpriteSortMode.Deferred,
                blendState: null,
                samplerState: SamplerState.PointClamp,
                depthStencilState: null,
                rasterizerState: null,
                effect: null,
                transformMatrix: null
            );

            if(VirtualScreen != null)
                VirtualScreen.Draw(Manager.SpriteBatch);

            Manager.SpriteBatch.End();

        }

        private void InitializeDefaultGameSystemEntities()
        {
            GameSystemEntities.Add(new Input());
            GameSystemEntities.Add(new TimerService());
        }

        private void UpdateGameSystemEntities(GameTime gameTime)
        {
            foreach (GameSystemEntity entity in GameSystemEntities)
            {
                entity.Update(gameTime);
            }
        }
    }
}
