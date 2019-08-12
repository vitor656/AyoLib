using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace AyoLib.Colliders
{
    public class Collider : Component
    {
        public Rectangle HitBox;

        private Dictionary<AyoBasic, Action> _objectsToCollideWith;

        public Collider()
        {
            _objectsToCollideWith = new Dictionary<AyoBasic, Action>();

            if(Owner != null)
            {
                if(Owner.GetGraphic() != null && Owner.GetGraphic().Texture2D != null)
                {
                    HitBox = new Rectangle((int) Owner.Position.X, (int) Owner.Position.Y, Owner.GetGraphic().Texture2D.Width, Owner.GetGraphic().Texture2D.Height);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            Console.WriteLine("Collider Update");
            CheckCollisions();

            if(HitBox != null)
            {
                HitBox.X = (int) Owner.Position.X;
                HitBox.Y = (int) Owner.Position.Y;
            }

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
                    if(HitBox.Intersects(obj.Key.HitBox.HitBox))
                    {
                        obj.Value();
                    }
                }
            }
        }
    }
}
