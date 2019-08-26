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

        public void Initialize()
        {
            AyoScenesManager.Initialize();
            InitializeDefaultGameSystemEntities();
        }

        public void Update(GameTime gameTime)
        {
            AyoScenesManager.Update(gameTime);
            UpdateGameSystemEntities(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            AyoScenesManager.Draw(spriteBatch);
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
