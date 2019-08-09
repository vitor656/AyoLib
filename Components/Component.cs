using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AyoLib
{
    public class Component
    {
        protected Entity Owner;
        
        public void Initialize(Entity entity)
        {
            Owner = entity;
            
        }

        public void RemoveSelf()
        {
            if(Owner != null)
            {
                Owner.RemoveComponent(this);
            }
        }

        public Entity GetOwner()
        {
            return Owner;
        }

        public virtual void Update(GameTime gameTime)
        {
            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}
