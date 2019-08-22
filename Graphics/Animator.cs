using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AyoLib.Graphics
{
    public class Animator
    {
        public AyoBasic Owner { get; private set; }

        public Animation CurrentAnimation { get; private set; }
        public int CurrentFrame { get; private set; }
        public int FrameCount { get; private set; }

        public List<Animation> Animations = new List<Animation>();

        private float _timer;

        public Animator(AyoBasic owner)
        {
            Owner = owner;
        }

        public void Update(GameTime gameTime)
        {
            if(CurrentAnimation != null)
            {
                _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (_timer > CurrentAnimation.FrameSpeed)
                {
                    _timer = 0;
                    CurrentFrame++;

                    if (CurrentFrame >= FrameCount)
                    {
                        CurrentFrame = 0;
                    }
                }
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                Owner.Graphic.Texture2D,
                Owner.Position,
                new Rectangle(CurrentFrame * Owner.Graphic.Width, 0, Owner.Graphic.Width, Owner.Graphic.Height),
                Color.White
            );
        }

        public void Play(string animationName)
        {
            Animation animation = Animations.Find(a => a.Name == animationName);
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
