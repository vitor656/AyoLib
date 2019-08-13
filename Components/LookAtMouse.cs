using System;
using System.Collections.Generic;
using System.Text;
using AyoLib.Inputs;
using Microsoft.Xna.Framework;

namespace AyoLib.Components
{
    public class LookAtMouse : Component
    {
        public override void Update(GameTime gameTime)
        {
            Owner.LookAt(Input.MousePosition);
            base.Update(gameTime);
        }
    }
}
