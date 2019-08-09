using AyoLib.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AyoLib
{
    public class AyoBasic
    {
        public string Tag { get; set; }
        
        public bool Active = true;
        public bool Visible = true;

        public Vector2 Position = Vector2.Zero;
        public Vector2 Speed = Vector2.Zero;

        private Graphic _graphic;

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
            Position += Speed;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if(_graphic != null)
            {
                spriteBatch.Draw(
                    _graphic.Texture2D, 
                    new Rectangle((int)Position.X, (int)Position.Y, _graphic.Width, _graphic.Height), 
                    Color.White
                );
            }
            
        }

        public void SetGraphic(Graphic graphic)
        {
            _graphic = graphic;
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
    }
}
