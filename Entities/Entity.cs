using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AyoLib
{
    public class Entity : AyoBasic
    {
        private List<Component> _components = new List<Component>();

        public void AddComponent(Component component)
        {
            bool foundComponent = false;
            foreach (var c in _components)
            {
                if(c.GetType() == component.GetType())
                {
                    foundComponent = true;
                }
            }

            if (!foundComponent)
            {
                component.Initialize(this);
                _components.Add(component);
            }
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
            foreach (var c in _components)
            {
                if(c.GetType() == component.GetType())
                {
                    _components.Remove(c);
                    break;
                }
            }
        }
        
        public override void Update(GameTime gameTime)
        {
            UpdateComponents(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawComponents(spriteBatch);
            base.Draw(spriteBatch);
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
