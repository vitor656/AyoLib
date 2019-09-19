using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AyoLib.Entities
{
    public class Background : Entity
    {
        public int ScrollX { get; set; }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            var source = new Rectangle(ScrollX, 0, Graphic.Width, Graphic.Height);
            spriteBatch.Draw(Graphic.Texture2D, Position, source, Color.White);
        }
    }
}
