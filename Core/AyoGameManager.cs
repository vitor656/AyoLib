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

        public List<GameSystemEntity> GameSystemEntities = new List<GameSystemEntity>();

        public void InitializeDefaultGameSystemEntities()
        {
            GameSystemEntities.Add(new Input());
            GameSystemEntities.Add(new TimerService());
        }

        public void UpdateGameSystemEntities(GameTime gameTime)
        {
            foreach (GameSystemEntity entity in GameSystemEntities)
            {
                entity.Update(gameTime);
            }
        }
    }
}
