using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AyoLib
{
    public class AyoScene
    {
        private List<AyoBasic> _entities;
        
        public virtual void Initialize()
        {
            if (_entities == null)
            {
                _entities = new List<AyoBasic>();
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            if(_entities == null)
            {
                _entities = new List<AyoBasic>();
            }

            foreach (var entity in _entities)
            {
                if(entity.Active)
                    entity.Update(gameTime);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (var entity in _entities)
            {
                if(entity.Visible)
                    entity.Draw(spriteBatch);
            }
        }

        public void Add(AyoBasic entity)
        {
            _entities.Add(entity);
        }
    }
}
