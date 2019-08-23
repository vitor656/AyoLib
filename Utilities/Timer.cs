using AyoLib.Entities.Services;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AyoLib.Utilities
{
    public class Timer
    {
        public float Time { get; private set; }
        public bool IsTicking { get; private set; }
        public Action CallBack { get; private set; }
        public bool IsLoop { get; private set; }
        public bool Done { get; private set; }

        private float _timeCounter = 0;

        public Timer()
        {
            TimerService.RegisterTimer(this);

            IsTicking = false;
            Done = false;
        }

        public void Update(GameTime gameTime)
        {
            if(IsTicking)
            {
                _timeCounter += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_timeCounter > Time)
                {
                    if(IsLoop)
                    {
                        _timeCounter = 0;
                    }
                    else
                    {
                        IsTicking = false;
                        Done = true;
                    }

                    CallBack();
                }
            }
            
        }

        public void Start(float time, Action callBack, bool isLoop = false)
        {
            Time = time;
            CallBack = callBack;
            IsLoop = isLoop;
            IsTicking = true;
        }


    }
}
