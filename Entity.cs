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
        public bool Visible = true;
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
        
        public void Update()
        {
            UpdateComponents();
        }

        public void Draw()
        {
            DrawComponents();
        }

        private void UpdateComponents()
        {
            foreach (var component in _components)
            {
                component.Update();
            }
        }

        private void DrawComponents()
        {
            foreach (var component in _components)
            {
                component.Draw();
            }
        }
    }
}
