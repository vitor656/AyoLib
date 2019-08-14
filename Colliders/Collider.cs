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
                return new Rectangle((int)Owner.Position.X, (int)Owner.Position.Y, Width, Height);
            }
        }

        public int Width { get; set; }
        public int Height { get; set; }

        public bool ShowCollider = false;

        private Dictionary<AyoBasic, Action> _otherWhileOverlapping;
        private List<AyoBasic> _others;

        public Collider()
        {

        }

        public Collider(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public void Initialize(AyoBasic owner)
        {
            Owner = owner;

            _otherWhileOverlapping = new Dictionary<AyoBasic, Action>();
            _others = new List<AyoBasic>();

            if(Width == 0 && Height == 0)
            {
                if(Owner.Graphic != null)
                {
                    Width = Owner.Graphic.Width;
                    Height = Owner.Graphic.Height;
                }
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            CheckIsOverlappingList();
            CheckCollisionsWithRegisteredObjects();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if(ShowCollider)
            {
                int thickness = 2;

                Texture2D colliderTexture = new Texture2D(AyoGame.CurrentGame.GraphicsDevice, 1, 1);
                colliderTexture.SetData(new[] { Color.White });

                spriteBatch.Draw(colliderTexture, Bounds, Color.Red);
            }
        }

        public void WhileOverlapping(AyoBasic other, Action Callback)
        {
            _otherWhileOverlapping.Add(other, Callback);
        }

        public void RegisterCollisionWith(AyoBasic other)
        {
            _others.Add(other);
        }

        public bool IsTouchingLeft(AyoBasic other)
        {
            Vector2 NormalizedSpeed = Owner.Speed;
            NormalizedSpeed.Normalize();

            return Bounds.Right + NormalizedSpeed.X + 2 > other.HitBox.Bounds.Left &&
                   Bounds.Left < other.HitBox.Bounds.Left &&
                   Bounds.Bottom > other.HitBox.Bounds.Top &&
                   Bounds.Top < other.HitBox.Bounds.Bottom;

        }

        public bool IsTouchingRight(AyoBasic other)
        {
            Vector2 NormalizedSpeed = Owner.Speed;
            NormalizedSpeed.Normalize();

            return Bounds.Left + NormalizedSpeed.X - 2 < other.HitBox.Bounds.Right &&
                   Bounds.Right > other.HitBox.Bounds.Right &&
                   Bounds.Bottom > other.HitBox.Bounds.Top &&
                   Bounds.Top < other.HitBox.Bounds.Bottom;

        }

        public bool IsTouchingTop(AyoBasic other)
        {
            Vector2 NormalizedSpeed = Owner.Speed;
            NormalizedSpeed.Normalize();

            return Bounds.Bottom + NormalizedSpeed.Y + 2 > other.HitBox.Bounds.Top &&
                   Bounds.Top < other.HitBox.Bounds.Top &&
                   Bounds.Right > other.HitBox.Bounds.Left &&
                   Bounds.Left < other.HitBox.Bounds.Right;

        }

        public bool IsTouchingBottom(AyoBasic other)
        {
            Vector2 NormalizedSpeed = Owner.Speed;
            NormalizedSpeed.Normalize();

            return Bounds.Top + NormalizedSpeed.Y - 2 < other.HitBox.Bounds.Bottom &&
                   Bounds.Bottom > other.HitBox.Bounds.Bottom &&
                   Bounds.Right > other.HitBox.Bounds.Left &&
                   Bounds.Left < other.HitBox.Bounds.Right;

        }

        private void CheckIsOverlappingList()
        {
            foreach (var obj in _otherWhileOverlapping)
            {
                if (obj.Key.HitBox != null)
                {
                    if (Bounds.Intersects(obj.Key.HitBox.Bounds))
                    {
                        obj.Value();
                    }
                }
            }
        }

        private void CheckCollisionsWithRegisteredObjects()
        {
            Vector2 NormalizedSpeed = Owner.Speed;
            NormalizedSpeed.Normalize();

            foreach (var other in _others.ToArray())
            {
                if (NormalizedSpeed.X > 0 && IsTouchingLeft(other) || NormalizedSpeed.X < 0 && IsTouchingRight(other))
                {
                    Owner.SetXSpeed(0f);
                }

                if (NormalizedSpeed.Y > 0 && IsTouchingTop(other) || NormalizedSpeed.Y < 0 && IsTouchingBottom(other))
                {
                    Owner.SetYSpeed(0f);
                }
            }

        }


    }
}
