using System;
using System.Collections.Generic;
using System.Text;
using AyoLib.Utilities;
using Microsoft.Xna.Framework;

namespace AyoLib.Entities.Services
{
    public class TimerService : GameSystemEntity
    {
        private static List<Timer> timers;

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            if(timers != null && timers.Count > 0)
            {
                foreach (var timer in timers.ToArray())
                {
                    if (!timer.Done)
                        timer.Update(gameTime);
                    else
                        timers.Remove(timer);
                }
            }
        }

        public static void RegisterTimer(Timer timer)
        {
            if(timers == null)
            {
                timers = new List<Timer>();
            }

            timers.Add(timer);
        }
    }
}
