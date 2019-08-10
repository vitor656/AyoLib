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

        public Graphic(Texture2D texture2D)
        {
            Texture2D = texture2D;
            Width = texture2D.Width;
            Height = texture2D.Height;
        }

        public Graphic(Texture2D texture2D, int width, int height)
        {
            Texture2D = texture2D;
            Width = width;
            Height = height;
        }

        public static Graphic CreateRectangle(int Width, int Height, Color color)
        {
            Texture2D rectTexture = new Texture2D(AyoGameManager.Manager.Graphics.GraphicsDevice, 1, 1);
            rectTexture.SetData(new[] { color });

            Graphic g = new Graphic(rectTexture, Width, Height);

            return g;
        }
    }
}
