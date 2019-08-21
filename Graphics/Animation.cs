﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AyoLib.Graphics
{
    public class Animation
    {
        public string Name { get; private set; }
        public int[] FramesArray { get; private set; }
        public int FrameSpeed { get; set; }
        public bool IsLoop { get; set; }

        public Animation(string name, int[] framesArray, bool isLoop, int frameSpeed)
        {
            Name = name;
            FramesArray = framesArray;
            IsLoop = isLoop;
            FrameSpeed = frameSpeed;
        }
    }
}