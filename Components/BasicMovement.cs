using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace AyoLib.Components
{
    public class BasicMovement : Component
    {
        public float Speed { get; set; }
        public bool HorizontalEnabled { get; set; }
        public bool VerticalEnabled { get; set; }

        public BasicMovement(float speed, bool horizontalEnabled = true, bool verticalEnabled = true)
        {
            Speed = speed;
            HorizontalEnabled = horizontalEnabled;
            VerticalEnabled = verticalEnabled;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            KeyboardState keyboardState = Keyboard.GetState();

            if (HorizontalEnabled)
            {

                if (keyboardState.IsKeyDown(Keys.A))
                {
                    Owner.SetXSpeed(-Speed);
                }
                else if (keyboardState.IsKeyDown(Keys.D))
                {
                    Owner.SetXSpeed(Speed);
                }
                else
                {
                    Owner.SetXSpeed(0);
                }
            }

            if(VerticalEnabled)
            {
                if (keyboardState.IsKeyDown(Keys.S))
                {
                    Owner.SetYSpeed(Speed);
                }
                else if (keyboardState.IsKeyDown(Keys.W))
                {
                    Owner.SetYSpeed(-Speed);
                }
                else
                {
                    Owner.SetYSpeed(0);
                }
            }

        }
    }
}
