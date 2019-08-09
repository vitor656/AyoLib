using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace AyoLib.Components
{
    public class BasicMovement : Component
    {
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            KeyboardState keyboardState = Keyboard.GetState();

            if(keyboardState.IsKeyDown(Keys.A))
            {
                Owner.SetXSpeed(-1);
            }
            else if(keyboardState.IsKeyDown(Keys.D))
            {
                Owner.SetXSpeed(1);
            }
            else
            {
                Owner.SetXSpeed(0);
            }

            if(keyboardState.IsKeyDown(Keys.S))
            {
                Owner.SetYSpeed(1);
            }
            else if (keyboardState.IsKeyDown(Keys.W))
            {
                Owner.SetYSpeed(-1);
            }
            else
            {
                Owner.SetYSpeed(0);
            }
        }
    }
}
