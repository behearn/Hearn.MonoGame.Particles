using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearn.MonoGame.Particles
{

    public class Particle
    {

        public Particle()
        {
            Scale = Vector2.One;
            Color = Color.White;
            DeltaColor = new float[4];
        }

        public Vector2 Pos { get; set; }
        public Vector2 Vel { get; set; }
        public float RadialAccel { get; set; }
        public float TangentialAccel { get; set; }
        public float Life { get; set; }
        public Vector2 Scale { get; set; }
        public float DeltaScale { get; set; }
        public Color Color { get; set; }
        public float[] DeltaColor { get; set; }
        public Vector2 Forces { get; set; }
        public Vector2 Radial { get; set; }
        public Vector2 Tangential { get; set; }

        public void SetVelocity(float angle, float speed)
        {
            Vel = new Vector2(
                (float)Math.Cos(MathHelper.ToRadians(angle)) * speed,
                -(float)Math.Sin(MathHelper.ToRadians(angle)) * speed
            );
        }

    }
}
