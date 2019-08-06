using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AyoLib
{
    public class AyoGameManager
    {
        private static AyoGameManager _manager;
        public static AyoGameManager Manager {
            get {
                if (_manager == null)
                    _manager = new AyoGameManager();

                return _manager;
            }
        }

        public SpriteBatch SpriteBatch;
        public GraphicsDeviceManager Graphics;
    }
}
