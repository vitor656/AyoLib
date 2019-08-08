using AyoLib.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AyoLib
{
    public class AyoBasic : Entity
    {
        public string Tag { get; set; }

        public bool Active = true;
        public bool Visible = true;

        public Vector2 Position = Vector2.Zero;

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

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_graphic.Texture2D, new Rectangle((int) Position.X, (int) Position.Y, _graphic.Width, _graphic.Height), Color.White);
            base.Draw(spriteBatch);
        }

        public void SetGraphic(Graphic graphic)
        {
            _graphic = graphic;
        }
    }
}
