using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AyoLib.Graphics
{
    public class Graphic
    {
        public Texture2D Texture2D;

        public int Width;
        public int Height;

        public static Graphic CreateRectangle(int Width, int Height, Color color)
        {
            Texture2D rectTexture = new Texture2D(AyoGameManager.Manager.Graphics.GraphicsDevice, 1, 1);
            rectTexture.SetData(new[] { color });

            Graphic g = new Graphic
            {
                Texture2D = rectTexture,
                Width = Width,
                Height = Height
            };

            return g;
        }
    }
}
