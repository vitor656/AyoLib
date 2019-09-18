using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AyoLib.Core
{
    public class VirtualScreen
    {
        public RenderTarget2D RenderTarget { get; private set; }
        public int VirtualWidth { get; private set; }
        public int VirtualHeight { get; private set; }
        public float VirtualAspectRatio { get; private set; }
        public Rectangle ViewRect { get; private set; }

        public GraphicsDevice GraphicsDevice { get; private set; }
        private bool _windowSizeChanged = false;

        public VirtualScreen(GraphicsDevice graphicsDevice, int width, int height)
        {
            VirtualWidth = width;
            VirtualHeight = height;
            VirtualAspectRatio = ((float) width) / ((float) height);
            GraphicsDevice = graphicsDevice;

            RenderTarget = new RenderTarget2D(GraphicsDevice, VirtualWidth, VirtualHeight);
            ViewRect = new Rectangle(0, 0, AyoGameManager.Manager.Graphics.PreferredBackBufferWidth, AyoGameManager.Manager.Graphics.PreferredBackBufferHeight);
        }

        public void InitRenderer()
        {
            GraphicsDevice.SetRenderTarget(RenderTarget);
        }

        public void ClearRenderer()
        {
            GraphicsDevice.SetRenderTarget(null);
        }

        public void ToogleResizingWindow()
        {
            _windowSizeChanged = true;
        }

        public void Update(GameTime gameTime)
        {
            if (!_windowSizeChanged)
                return;

            _windowSizeChanged = false;

            int physicalWidth = GraphicsDevice.Viewport.Width;
            int physicalHeight = GraphicsDevice.Viewport.Height;
            float physicalAspectRatio = GraphicsDevice.Viewport.AspectRatio;

            if ((int)(physicalAspectRatio * 10) == (int)(VirtualAspectRatio * 10))
            {
                ViewRect = new Rectangle(0, 0, physicalWidth, physicalHeight);
                return;
            }

            if (VirtualAspectRatio > physicalAspectRatio)
            {
                var scaling = (float)physicalWidth / (float)VirtualWidth;
                var width = (float)(VirtualWidth) * scaling;
                var height = (float)(VirtualHeight) * scaling;
                var borderSize = (int)((physicalHeight - height) / 2);
                ViewRect = new Rectangle(0, borderSize, (int)width, (int)height);
            }
            else
            {
                var scaling = (float)physicalHeight / (float)VirtualHeight;
                var width = (float)(VirtualWidth) * scaling;
                var height = (float)(VirtualHeight) * scaling;
                var borderSize = (int)((physicalWidth - width) / 2);
                ViewRect = new Rectangle(borderSize, 0, (int)width, (int)height);
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                RenderTarget, 
                ViewRect, 
                Color.White
            );
        }


        public void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            ToogleResizingWindow();
        }

    }
}
