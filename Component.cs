using System;
using System.Collections.Generic;
using System.Text;

namespace AyoLib
{
    public class Component
    {
        private Entity _entity;
        
        private void Initialize(Entity entity)
        {
            _entity = entity;
        }

        protected virtual void Update()
        {

        }

        protected virtual void Draw()
        {

        }
    }
}
