﻿using AyoLib.Colliders;
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
        public string Tag { get; set; }
        
        public bool Active = true;
        public bool Visible = true;
            
        public Vector2 Position = Vector2.Zero;
        public Vector2 Origin = Vector2.Zero;
        public Vector2 Speed = Vector2.Zero;
        public Vector2 Acceleration = Vector2.Zero;

        public Vector2 Direction { get; private set; }
        public float RotationSpeed = 0f;
        public float LinearSpeed = 0f;

        public float LifeSpan = 0f;

        public Collider HitBox { get; private set; }

        private Graphic _graphic;
        private float _rotation;
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

        public AyoBasic()
        {
            Position = Vector2.Zero;
        }

        public AyoBasic(Vector2 position)
        {
            Position = position;
        }

        public AyoBasic(float x, float y)
        {
            Position = new Vector2 {
                X = x,
                Y = y
            };
        }

        public AyoBasic(Graphic graphic, float x, float y)
        {
            _graphic = graphic;

            Position = new Vector2
            {
                X = x,
                Y = y
            };
        }

        public virtual void Update(GameTime gameTime)
        {
            Speed += Acceleration;
            
            _rotation += MathHelper.ToRadians(RotationSpeed);
            Direction = new Vector2( (float) Math.Cos(_rotation), (float) Math.Sin(_rotation));

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
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if(_graphic != null)
            {
                spriteBatch.Draw(
                    texture: _graphic.Texture2D, 
                    destinationRectangle: new Rectangle((int)Position.X, (int)Position.Y, _graphic.Width, _graphic.Height), 
                    sourceRectangle: null,
                    color: Color.White,
                    rotation: _rotation,
                    origin: Origin,
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
            _graphic = graphic;
        }

        public void SetGraphic(string graphicName)
        {
            _graphic = new Graphic(AyoGame.CurrentGame.Content.Load<Texture2D>(graphicName));
        }

        public void SetGraphic(string graphicName, int width, int height)
        {
            _graphic = new Graphic(AyoGame.CurrentGame.Content.Load<Texture2D>(graphicName), width, height);
        }

        public Graphic GetGraphic()
        {
            return _graphic;
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
            if(_graphic != null)
            {
                Origin = new Vector2(_graphic.Width / 2, _graphic.Height / 2);
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
            _rotation += MathHelper.ToRadians(degrees);
        }

        public void LookAt(Vector2 positionToLook)
        {
            Vector2 directionToLook = positionToLook - Position;
            _rotation = (float) Math.Atan2(directionToLook.Y, directionToLook.X);
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
    }
}
