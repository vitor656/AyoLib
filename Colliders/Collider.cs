using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AyoLib.Colliders
{
    
    public enum ColliderDisplayMode
    {
        Border,
        Fill
    }

    public class Collider
    {

        protected AyoBasic Owner;

        
        public int Width { get; set; }
        public int Height { get; set; }

        public Vector2 Origin {
            get
            {
                if(Owner != null)
                {
                    return Owner.Position - Owner.Origin;
                }
                else
                {
                    return Vector2.Zero;
                }
                
            }

            set
            {
                Origin = value;
            }
        }

        public Rectangle Bounds
        {
            get
            {
                return new Rectangle((int)Origin.X, (int)Origin.Y, Width, Height);
            }
        }


        public bool ShowCollider = true;
        public ColliderDisplayMode ColliderDisplayMode = ColliderDisplayMode.Fill;

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
                Texture2D colliderTexture = new Texture2D(AyoGame.CurrentGame.GraphicsDevice, 1, 1);
                colliderTexture.SetData(new[] { Color.White });

                if (ColliderDisplayMode == ColliderDisplayMode.Border)
                {
                    int thickness = 1;

                    spriteBatch.Draw(colliderTexture, new Rectangle(Bounds.X, Bounds.Y, Bounds.Width, thickness), Color.Red);
                    spriteBatch.Draw(colliderTexture, new Rectangle(Bounds.X, Bounds.Y, thickness, Bounds.Height), Color.Red);
                    spriteBatch.Draw(colliderTexture, new Rectangle(Bounds.X + Bounds.Width, Bounds.Y, thickness, Bounds.Height), Color.Red);
                    spriteBatch.Draw(colliderTexture, new Rectangle(Bounds.X, Bounds.Y + Bounds.Height, Bounds.Width, thickness), Color.Red);
                }
                else if(ColliderDisplayMode == ColliderDisplayMode.Fill)
                {
                    spriteBatch.Draw(colliderTexture, Bounds, new Color(255f, 0, 0, 0.5f));
                }
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
            foreach (var other in _others.ToArray())
            {
                if (Owner.Speed.X > 0 && IsTouchingLeft(other) || Owner.Speed.X < 0 && IsTouchingRight(other))
                {
                    Owner.SetXSpeed(0f);
                }

                if (Owner.Speed.Y > 0 && IsTouchingTop(other) || Owner.Speed.Y < 0 && IsTouchingBottom(other))
                {
                    Owner.SetYSpeed(0f);
                }
            }
        }


    }
}
