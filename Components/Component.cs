using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AyoLib
{
    public class Component
    {
        private Entity Entity;
        
        private void Initialize(Entity entity)
        {
            Entity = entity;
        }

        public void RemoveSelf()
        {
            if(Entity != null)
            {
                Entity.RemoveComponent(this);
            }
        }

        public Entity GetOwner()
        {
            return Entity;
        }

        public virtual void Update(GameTime gameTime)
        {
            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}
