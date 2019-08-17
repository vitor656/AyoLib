using AyoLib.Colliders;
using AyoLib.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AyoLib
{
    public class AyoBasic : ICloneable
    {
        public string Name { get; set; }
        public string Tag { get; set; }
        
        public bool Active = true;
        public bool Visible = true;
            
        public Vector2 Position = Vector2.Zero;
        public Vector2 Origin = Vector2.Zero;
        public Vector2 Scale = Vector2.One;

        public Vector2 Speed = Vector2.Zero;
        public Vector2 Acceleration = Vector2.Zero;

        public Vector2 Direction { get; private set; }
        public float RotationSpeed = 0f;
        public float LinearSpeed = 0f;

        public float LifeSpan = 0f;

        public Collider HitBox { get; private set; }

        public Graphic Graphic { get; private set; }
        public List<Animation> Animations;

        public float Rotation { get; private set; }
        private float _timer = 0f;

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

        public virtual void Update(GameTime gameTime)
        {
            Speed += Acceleration;

            Rotation += MathHelper.ToRadians(RotationSpeed);
            Direction = new Vector2( (float) Math.Cos(Rotation), (float) Math.Sin(Rotation));

            _timer += (float) gameTime.ElapsedGameTime.TotalSeconds;
            if(LifeSpan > 0f)
            {
                if(_timer > LifeSpan)
                {
                    Kill();
                }
            }

            if(HitBox != null)
            {
                HitBox.Update(gameTime);
            }

            Position += Speed;
            Position += Direction * LinearSpeed;

            Speed = Vector2.Zero;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if(Graphic != null)
            {
                spriteBatch.Draw(
                    texture: Graphic.Texture2D, 
                    position: Position, 
                    sourceRectangle: null, 
                    color: Color.White, 
                    rotation: Rotation, 
                    origin: Origin, 
                    scale: Scale, 
                    effects: SpriteEffects.None, 
                    layerDepth: 0f
                );
            }

            if(HitBox != null)
            {
                HitBox.Draw(spriteBatch);
            }
            
        }

        public void SetGraphic(Graphic graphic)
        {
            Graphic = graphic;
        }

        public void SetGraphic(string graphicName)
        {
            Graphic = new Graphic(AyoGame.CurrentGame.Content.Load<Texture2D>(graphicName));
        }

        public void SetGraphic(string graphicName, int width, int height)
        {
            Graphic = new Graphic(AyoGame.CurrentGame.Content.Load<Texture2D>(graphicName), width, height);
        }

        public void SetCollider(Collider collider)
        {
            collider.Initialize(this);
            HitBox = collider;
        }

        public void SetSpeed(Vector2 speed)
        {
            Speed = speed;
        }

        public void SetXSpeed(float XSpeed)
        {
            Speed.X = XSpeed;
        }

        public void SetYSpeed(float YSpeed)
        {
            Speed.Y = YSpeed;
        }

        public void CenterOrigin()
        {
            if(Graphic != null)
            {
                Origin = new Vector2(Graphic.Width / 2, Graphic.Height / 2);
            } 
        }

        public void ScreenCenter()
        {
            Position = new Vector2(
                AyoGameManager.Manager.Graphics.PreferredBackBufferWidth / 2,
                AyoGameManager.Manager.Graphics.PreferredBackBufferHeight / 2
            );
        }

        public void Rotate(float degrees)
        {
            Rotation += MathHelper.ToRadians(degrees);
        }

        public void LookAt(Vector2 positionToLook)
        {
            Vector2 directionToLook = positionToLook - Position;
            Rotation = (float) Math.Atan2(directionToLook.Y, directionToLook.X);
        }

        public void LookAt(AyoBasic other)
        {
            LookAt(other.Position);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public void Kill()
        {
            Active = false;
            Visible = false;
        }

        public void AddAnimation(string name, int[] framesArray, bool isLoop = true)
        {
            if (Animations == null)
                Animations = new List<Animation>();

            Animations.Add(new Animation(name, framesArray, isLoop));
        }
    }
}
