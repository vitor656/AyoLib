using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AyoLib
{
    public class Entity
    {
        private List<Component> _components;

        public bool Active = true;
        public Vector2 Position = Vector2.Zero;

        public string Tag { get; set; }

        public float X
        {
            get { return Position.X; }
            set { Position.X = value; }
        }

        public float Y
        {
            get { return Position.Y; }
            set { Position.Y = value; }
        }

        public Entity()
        {
            Position = Vector2.Zero;
        }

        public Entity(Vector2 position)
        {
            Position = position;
            _components = new List<Component>();
        }

        protected void AddComponent(Component component)
        {
            _components.Add(component);
        }

        protected Component GetComponent<T>() where T : Component
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

        protected void RemoveComponent(Component component)
        {
            _components.Remove(component);
        }
        
    }
}
