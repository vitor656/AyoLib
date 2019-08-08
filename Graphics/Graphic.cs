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

        public static Texture2D CreateRectangle(int Width, int Height, Color color)
        {
            Texture2D rectTexture = new Texture2D(AyoGameManager.Manager.Graphics.GraphicsDevice, Width, Height);
            rectTexture.SetData(new[] { color });

            return rectTexture;
        }
    }
}
