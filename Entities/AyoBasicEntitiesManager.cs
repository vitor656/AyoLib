using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AyoLib.Entities
{
    public class AyoBasicEntitiesManager
    {
        public List<AyoBasic> Entities { get; private set; }

        public void Initialize()
        {
            if(Entities == null)
                Entities = new List<AyoBasic>();
        }

        public void Update(GameTime gameTime)
        {
            if (Entities == null)
            {
                Entities = new List<AyoBasic>();
            }

            foreach (var entity in Entities.ToArray())
            {
                if (entity.Active)
                    entity.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var entity in Entities)
            {
                if (entity.Visible)
                    entity.Draw(spriteBatch);
            }
        }

        public void Add(AyoBasic entity)
        {
            Entities.Add(entity);
        }
    }
}
