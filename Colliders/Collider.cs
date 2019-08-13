using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AyoLib.Colliders
{
    public class Collider
    {
        protected AyoBasic Owner;
        public Rectangle Bounds
        {
            get
            {
                return new Rectangle((int)Owner.Position.X, (int)Owner.Position.Y, Owner.GetGraphic().Width, Owner.GetGraphic().Height);
            }
        }

        public int Width;
        public int Height;

        private Dictionary<AyoBasic, Action> _objectsToCollideWith;
        private List<AyoBasic> _others;

        public void Initialize(AyoBasic owner)
        {
            Owner = owner;

            _objectsToCollideWith = new Dictionary<AyoBasic, Action>();
            _others = new List<AyoBasic>();
        }

        public virtual void Update(GameTime gameTime)
        {
            CheckCollisions();
            CheckCollisionsWithRegisteredObjects();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            
        }

        public void Intersects(AyoBasic other, Action Callback)
        {
            _objectsToCollideWith.Add(other, Callback);
        }

        public void RegisterCollisionWith(AyoBasic other)
        {
            _others.Add(other);
        }

        private void CheckCollisionsWithRegisteredObjects()
        {
            foreach (var other in _others.ToArray())
            {
                if(Owner.Speed.X > 0 && IsTouchingLeft(other) || Owner.Speed.X < 0 && IsTouchingRight(other))
                {
                    Owner.SetXSpeed(0f);
                }

                if(Owner.Speed.Y > 0 && IsTouchingTop(other) || Owner.Speed.Y < 0 && IsTouchingBottom(other))
                {
                    Owner.SetYSpeed(0f);
                }
            }
        }

        private void CheckCollisions()
        {
            foreach (var obj in _objectsToCollideWith)
            {
                if(obj.Key.HitBox != null)
                {
                    if(Bounds.Intersects(obj.Key.HitBox.Bounds))
                    {
                        obj.Value();
                    }
                }
            }
        }

        public bool IsTouchingLeft(AyoBasic other)
        {
            return Bounds.Right + Owner.Speed.X > other.HitBox.Bounds.Left &&
                   Bounds.Left < other.HitBox.Bounds.Left &&
                   Bounds.Bottom > other.HitBox.Bounds.Top &&
                   Bounds.Top < other.HitBox.Bounds.Bottom;

        }

        public bool IsTouchingRight(AyoBasic other)
        {
            return Bounds.Left + Owner.Speed.X < other.HitBox.Bounds.Right &&
                   Bounds.Right > other.HitBox.Bounds.Right &&
                   Bounds.Bottom > other.HitBox.Bounds.Top &&
                   Bounds.Top < other.HitBox.Bounds.Bottom;

        }

        public bool IsTouchingTop(AyoBasic other)
        {
            return Bounds.Bottom + Owner.Speed.Y > other.HitBox.Bounds.Top &&
                   Bounds.Top < other.HitBox.Bounds.Top &&
                   Bounds.Right > other.HitBox.Bounds.Left &&
                   Bounds.Left < other.HitBox.Bounds.Right;

        }

        public bool IsTouchingBottom(AyoBasic other)
        {
            return Bounds.Top + Owner.Speed.Y < other.HitBox.Bounds.Bottom &&
                   Bounds.Bottom > other.HitBox.Bounds.Bottom &&
                   Bounds.Right > other.HitBox.Bounds.Left &&
                   Bounds.Left < other.HitBox.Bounds.Right;

        }
    }
}
