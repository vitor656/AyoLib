using AyoLib.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AyoLib
{
    public class AyoScene
    {
        public AyoBasicEntitiesManager EntitiesManager = new AyoBasicEntitiesManager();
        
        public virtual void Initialize()
        {
            EntitiesManager.Initialize();
        }

        public virtual void Update(GameTime gameTime)
        {
            EntitiesManager.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            EntitiesManager.Draw(spriteBatch);
        }

        public void Add(AyoBasic entity)
        {
            EntitiesManager.Add(entity);
        }
    }
}
