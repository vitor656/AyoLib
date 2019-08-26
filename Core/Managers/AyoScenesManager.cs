using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AyoLib.Core.Managers
{
    public class AyoScenesManager
    {
        public AyoScene CurrentScene { get; private set; }

        public void SetupScene(AyoScene ayoScene)
        {
            if (ayoScene == null)
            {
                ayoScene = new BasicScene();
            }

            CurrentScene = ayoScene;
        }

        public void Initialize()
        {
            CurrentScene.Initialize();
        }

        public void Update(GameTime gameTime)
        {
            CurrentScene.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentScene.Draw(spriteBatch);
        }
    }
}
