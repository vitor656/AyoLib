using AyoLib.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace AyoLib.Inputs
{
    public class Input : GameSystemEntity
    {
        private static KeyboardState _previousKeyboardState;
        private static KeyboardState _keyboardState;

        private static GamePadState _previousGamePadState;
        private static GamePadState _gamePadState;

        private static MouseState _previousMouseState;
        private static MouseState _mouseState;

        public override void Update(GameTime gameTime)
        {
            _previousKeyboardState = _keyboardState;
            _keyboardState = Keyboard.GetState();

            _previousGamePadState = _gamePadState;
            _gamePadState = GamePad.GetState(0);

            _previousMouseState = _mouseState;
            _mouseState = Mouse.GetState();

            base.Update(gameTime);
        }

        public static bool IsKeyDown(Keys key)
        {
            bool isDown = false;
            if(_keyboardState != null)
            {
                isDown = _keyboardState.IsKeyDown(key);
            }

            return isDown;
        }

        public static bool IsAnyKeyDown(Keys[] keys)
        {
            bool isAnyKeyDown = false;
            if(_keyboardState != null)
            {
                foreach (Keys key in keys)
                {
                    isAnyKeyDown = _keyboardState.IsKeyDown(key);

                    if (isAnyKeyDown)
                        break;
                }
            }

            return isAnyKeyDown;
        }

        public static bool IsKeyPressed(Keys key)
        {
            bool keyPressed = false;
            if(_keyboardState != null && _previousKeyboardState != null)
            {
                if(_keyboardState.IsKeyDown(key) && _previousKeyboardState.IsKeyUp(key))
                {
                    keyPressed = true;
                }
            }

            return keyPressed;
        }

        public static bool IsAnyKeyPressed(Keys[] keys)
        {
            bool keyPressed = false;
            if (_keyboardState != null && _previousKeyboardState != null)
            {
                foreach (Keys key in keys)
                {
                    if (_keyboardState.IsKeyDown(key) && _previousKeyboardState.IsKeyUp(key))
                    {
                        keyPressed = true;
                    }

                    if (keyPressed)
                        break;
                }
            }

            return keyPressed;
        }

        public static Vector2 GetMousePositionOnScreen()
        {
            return _mouseState.Position.ToVector2();
        }
    }
}
