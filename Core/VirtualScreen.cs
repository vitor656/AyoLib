using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AyoLib.Core
{
    public class VirtualScreen
    {
        public RenderTarget2D Screen;
        public int Width { get; private set; }
        public int Height { get; private set; }

        private GraphicsDevice _graphicsDevice;

        public VirtualScreen(GraphicsDevice graphicsDevice, int width, int height)
        {
            Width = width;
            Height = height;
            _graphicsDevice = graphicsDevice;

            Screen = new RenderTarget2D(_graphicsDevice, Width, Height);
        }

        public void InitRenderer()
        {
            _graphicsDevice.SetRenderTarget(Screen);
        }

        public void ClearRenderer()
        {
            _graphicsDevice.SetRenderTarget(null);
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Screen, new Rectangle(0, 0, AyoGame.WindowWidth, AyoGame.WindowHeight), Color.White);
        }
    }
}
