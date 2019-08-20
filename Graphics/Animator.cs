using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AyoLib.Graphics
{
    public class Animator
    {
        public Animation CurrentAnimation { get; private set; }
        public int CurrentFrame { get; private set; }
        public int FrameCount { get; private set; }
        public int FrameHeight { get; private set; }
        public int FrameWidth { get; private set; }
        public int FrameSpeed { get; set; }

        private float _timer;

        public void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(_timer > FrameSpeed)
            {
                _timer = 0;
                CurrentFrame++;

                if(CurrentFrame >= FrameCount)
                {
                    CurrentFrame = 0;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, AyoBasic Owner)
        {
            spriteBatch.Draw(
                Owner.Graphic.Texture2D,
                Owner.Position,
                new Rectangle(CurrentFrame * FrameWidth, 0, FrameWidth, FrameHeight),
                Color.White
            );
        }

        public void Play(Animation animation)
        {
            if (CurrentAnimation == animation)
                return;

            CurrentAnimation = animation;
            CurrentFrame = 0;
            _timer = 0;
        }

        public void Stop()
        {
            _timer = 0f;
            CurrentFrame = 0;
        }
    }
}
