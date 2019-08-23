using System;
using System.Collections.Generic;
using System.Text;

namespace AyoLib.Graphics
{
    public class Animation
    {
        public Animator AnimatorOwner;

        public string Name { get; private set; }
        public int[] FramesArray { get; private set; }
        public float FrameDuration { get; set; }
        public bool IsLoop { get; set; }
        public int FramesCount
        {
            get
            {
                if(FramesArray != null && FramesArray.Length > 0)
                {
                    return FramesArray.Length;
                }
                else
                {
                    return AnimatorOwner.Owner.Graphic.Texture2D.Width / AnimatorOwner.Owner.Graphic.Width;
                }
                
            }
        }

        public Animation(string name, int[] framesArray, bool isLoop, float frameDuration)
        {
            Name = name;
            FramesArray = framesArray;
            IsLoop = isLoop;
            FrameDuration = frameDuration;
        }
    }
}
