using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace AyoLib.Colliders
{
    public class Collider : Component
    {
        public Rectangle Rectangle;

        private Dictionary<AyoBasic, Action> _objectsToCollideWith;

        public Collider()
        {
            _objectsToCollideWith = new Dictionary<AyoBasic, Action>();
        }

        public override void Update(GameTime gameTime)
        {
            CheckCollisions();

            base.Update(gameTime);
        }

        public void Intersects(AyoBasic other, Action Callback)
        {
            _objectsToCollideWith.Add(other, Callback);
        }

        private void CheckCollisions()
        {
            foreach (var obj in _objectsToCollideWith)
            {
                if(obj.Key.HitBox != null)
                {
                    if(Rectangle.Intersects(obj.Key.HitBox.Rectangle))
                    {
                        obj.Value();
                    }
                }
            }
        }
    }
}
