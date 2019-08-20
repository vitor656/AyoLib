using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AyoLib.Gui
{
    public class AyoText : Entity
    {
        public SpriteFont SpriteFont { get; set; }
        public string Text { get; set; }
        public Color TextColor { get; set; }

        public AyoText(string text = "", float x = 0, float y = 0)
        {
            Text = text;
            Position = new Vector2(x, y);
            SpriteFont = AyoGame.CurrentGame.Content.Load<SpriteFont>("AyoFont");
        }

        public void SetFont(string fontName)
        {
            SpriteFont = AyoGame.CurrentGame.Content.Load<SpriteFont>(fontName);
        }

        public void SetFont(SpriteFont spriteFont)
        {
            SpriteFont = spriteFont;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            Console.WriteLine(Text);
            spriteBatch.DrawString(
                spriteFont: SpriteFont, 
                text: Text, 
                position: Position, 
                color: TextColor, 
                rotation: Rotation, 
                origin: Origin,
                scale: Scale,
                effects: SpriteEffects.None,
                layerDepth: 0f
            );

        }
    }
}
