using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AyoLib.Cameras
{
    public class AyoCamera
    {
        public float Zoom { get; set; }
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }

        public Rectangle Bounds { get; private set; }

        public Matrix TransformMatrix
        {
            get {
                return
                    Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
                    Matrix.CreateRotationZ(Rotation) *
                    Matrix.CreateScale(Zoom) *
                    Matrix.CreateTranslation(new Vector3(Bounds.Width * 0.5f, Bounds.Height * 0.5f, 0));
            }
        }

        public Rectangle VisibleArea
        {
            get {

                Matrix inverseViewMatrix = Matrix.Invert(TransformMatrix);
                var tl = Vector2.Transform(Vector2.Zero, inverseViewMatrix);
                var tr = Vector2.Transform(new Vector2(AyoGame.ResolutionWidth, 0), inverseViewMatrix);
                var bl = Vector2.Transform(new Vector2(0, AyoGame.ResolutionHeight), inverseViewMatrix);
                var br = Vector2.Transform(new Vector2(AyoGame.ResolutionWidth, AyoGame.ResolutionHeight), inverseViewMatrix);
                var min = new Vector2(
                    MathHelper.Min(tl.X, MathHelper.Min(tr.X, MathHelper.Min(bl.X, br.X))),
                    MathHelper.Min(tl.Y, MathHelper.Min(tr.Y, MathHelper.Min(bl.Y, br.Y))));
                var max = new Vector2(
                    MathHelper.Max(tl.X, MathHelper.Max(tr.X, MathHelper.Max(bl.X, br.X))),
                    MathHelper.Max(tl.Y, MathHelper.Max(tr.Y, MathHelper.Max(bl.Y, br.Y))));

                return new Rectangle((int)min.X, (int)min.Y, (int)(max.X - min.X), (int)(max.Y - min.Y));
            }
        }

        public AyoCamera(Viewport viewport)
        {
            Bounds = viewport.Bounds;
        }

        
    }
}
