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
        public bool IsAnimationEnd;
        public int CurrentFrame { get; private set; }
        public List<Animation> Animations = new List<Animation>();

        private float _timer;
        private int _currentArrayIndex;

        public Animator(AyoBasic owner)
        {
            Owner = owner;
        }

        public void Update(GameTime gameTime)
        {
            if(CurrentAnimation != null && !IsAnimationEnd)
            {
                _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (_timer > CurrentAnimation.FrameDuration)
                {
                    _timer = 0;

                    if(CurrentAnimation.FramesArray != null && CurrentAnimation.FramesArray.Length > 0)
                    {
                        
                        _currentArrayIndex++;
                        if(_currentArrayIndex >= CurrentAnimation.FramesCount)
                        {
                            _currentArrayIndex = 0;
                            IsAnimationEnd = true;
                        }

                        CurrentFrame = CurrentAnimation.FramesArray[_currentArrayIndex];

                    }
                    else
                    {
                        CurrentFrame++;

                        if (CurrentFrame >= CurrentAnimation.FramesCount)
                        {
                            CurrentFrame = 0;
                            IsAnimationEnd = true;
                        }
                    }
                }

                if (CurrentAnimation.IsLoop)
                    IsAnimationEnd = false;
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                texture: Owner.Graphic.Texture2D,
                position: Owner.Position,
                sourceRectangle: new Rectangle(CurrentFrame * Owner.Graphic.Width, 0, Owner.Graphic.Width, Owner.Graphic.Height),
                color: Color.White,
                rotation: Owner.Rotation,
                origin: Owner.Origin,
                scale: Owner.Scale,
                effects: SpriteEffects.None,
                layerDepth: 0f
            );
        }

        public void Play(string animationName)
        {
            Animation animation = Animations.Find(a => a.Name == animationName);
            if (CurrentAnimation == animation && !CurrentAnimation.IsLoop && !IsAnimationEnd)
                return;

            if (CurrentAnimation == animation && CurrentAnimation.IsLoop)
                return;

            CurrentAnimation = animation;
            _timer = 0;
            _currentArrayIndex = 0;
            IsAnimationEnd = false;

            if (CurrentAnimation.FramesArray != null && CurrentAnimation.FramesArray.Length > 0)
            {
                CurrentFrame = CurrentAnimation.FramesArray[_currentArrayIndex];
            }
            else
            {
                CurrentFrame = 0;
            }
        }

        public void Stop()
        {
            _timer = 0f;
            _currentArrayIndex = 0;
            CurrentFrame = 0;
            CurrentAnimation = null;
        }

        public void AddAnimation(Animation animation)
        {
            animation.AnimatorOwner = this;
            Animations.Add(animation);
        }
    }
}
