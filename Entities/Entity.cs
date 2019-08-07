using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AyoLib
{
    public class Entity
    {
        private List<Component> _components;

        public Entity()
        {
            _components = new List<Component>();
        }

        public void AddComponent(Component component)
        {
            _components.Add(component);
        }

        public Component GetComponent<T>() where T : Component
        {
            foreach (var component in _components)
            {
                if(component.GetType() == typeof(T))
                {
                    return component;
                }
            }

            return null;
        }

        public void RemoveComponent(Component component)
        {
            _components.Remove(component);
        }
        
        public virtual void Update(GameTime gameTime)
        {
            UpdateComponents(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            DrawComponents(spriteBatch);
        }

        private void UpdateComponents(GameTime gameTime)
        {
            foreach (var component in _components)
            {
                component.Update(gameTime);
            }
        }

        private void DrawComponents(SpriteBatch spriteBatch)
        {
            foreach (var component in _components)
            {
                component.Draw(spriteBatch);
            }
        }
    }
}
