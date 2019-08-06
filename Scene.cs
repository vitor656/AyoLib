using System;
using System.Collections.Generic;
using System.Text;

namespace AyoLib
{
    public class Scene
    {
        private List<Entity> _entities;

        public Scene()
        {
            _entities = new List<Entity>();
        }

        public void Update()
        {
            foreach (var entity in _entities)
            {
                entity.Update();
            }
        }

        public void Draw()
        {
            foreach (var entity in _entities)
            {
                if(entity.Visible)
                    entity.Draw();
            }
        }

        public void Add(Entity entity)
        {
            _entities.Add(entity);
        }
    }
}
