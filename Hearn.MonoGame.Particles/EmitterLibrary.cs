using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearn.MonoGame.Particles
{
    public class EmitterLibrary
    {

        public static Emitter Fire(Vector2 pos)
        {
            return new Emitter()
            {
                TotalParticles = 250,
                EmissionRate = 250 / 7,
                Pos = pos,
                PosVar = new Vector2(30, 20),
                Angle = 90,
                AngleVar = 10,
                Speed = 60,
                SpeedVar = 20,
                Life = 7,
                LifeVar = 2,
                StartScale = 1,
                EndScale = 1,
                StartColor = new Color(193, 63, 30, 255),
                EndColor = new Color(0, 0, 0, 0),
                Active = true,
                Duration = 0
            };
        }

        public static Emitter Fireworks(Vector2 pos)
        {
            return new Emitter()
            {
                TotalParticles = 1500,
                EmissionRate = 1500 / 3.5f,
                Pos = pos,
                Angle = 90,
                AngleVar = 20,
                Gravity = new Vector2(0, -90),
                Speed = 180,
                SpeedVar = 50,
                Life = 3.5f,
                LifeVar = 1,
                StartScale = 1,
                EndScale = 1,
                StartColor = new Color(127, 127, 127, 255),
                StartColorVar = new Color(127, 127, 127, 0),
                EndColor = new Color(25, 25, 25, 25),
                EndColorVar = new Color(25, 25, 25, 25),
                Active = true,
                Duration = 0
            };
        }

        public static Emitter Galaxy(Vector2 pos)
        {
            return new Emitter()
            {
                TotalParticles = 200,
                EmissionRate = 200 / 4,
                Pos = pos,
                Angle = 90,
                AngleVar = 360,
                Speed = 30,
                SpeedVar = 20,
                Life = 4,
                StartScale = 1,
                EndScale = 1,
                RadialAccel = -4,
                TangentialAccel = 4,
                StartColor = new Color(30, 63, 193, 255),
                EndColor = new Color(0, 0, 0, 255),
                Active = true,
                Duration = 0
            };
        }
        
        public static Emitter Meteor(Vector2 pos)
        {
            return new Emitter()
            {
                TotalParticles = 150,
                EmissionRate = 150 / 2f,
                Pos = pos,
                Angle = 90,
                AngleVar = 360,
                Speed = 15,
                SpeedVar = 5,
                Life = 2,
                LifeVar = 1,
                StartScale = 1,
                EndScale = 1,
                StartColor = new Color(51, 102, 178, 255),
                StartColorVar = new Color(0, 0, 51, 1),
                EndColor = new Color(0, 0, 0, 255),
                EndColorVar = new Color(0, 0, 0, 255),
                Active = true,
                Duration = 0
            };
        }

        public static Emitter RingOfFire(Vector2 pos)
        {
            return new Emitter()
            {
                TotalParticles = 400,
                EmissionRate = 180,
                Pos = pos,
                PosVar = new Vector2(180, 20),
                Angle = 90,
                AngleVar = 10,
                Speed = 60,
                SpeedVar = 20,
                Life = 1,
                LifeVar = 1,
                StartScale = 1,
                EndScale = 1,
                StartColor = new Color(193, 63, 30, 255),
                EndColor = new Color(0, 0, 0, 0),
                PosVarTransform = (posVar =>
                {
                    var r = MathHelper.ToRadians(posVar.X);
                    return new Vector2(
                        (float)Math.Cos(r) * 80,
                        (float)Math.Sin(r) * 80
                    );
                }),
                Active = true,
                Duration = 0
            };
        }

        public static Emitter Snow(Vector2 pos)
        {
            return new Emitter()
            {
                TotalParticles = 700,
                EmissionRate = 10,
                Pos = pos,
                PosVar = new Vector2(pos.X, 0),
                Gravity = new Vector2(0, 8),
                Angle = -90,
                AngleVar = 10,
                Speed = 9,
                SpeedVar = 1,
                Life = 45,
                LifeVar = 15,
                StartScale = 1.25f,
                StartScaleVar = 0.5f,
                EndScale = 0.3f,
                StartColor = new Color(255, 255, 255, 255),
                EndColor = new Color(255, 255, 255, 0),
                Active = true,
                Duration = 0
            };
        }

        public static Emitter WaterGeyser(Vector2 pos)
        {
            return new Emitter()
            {
                TotalParticles = 400,
                EmissionRate = 100,
                Pos = pos, 
                Angle = 90,
                AngleVar = 10,
                Speed = 180,
                SpeedVar = 50,
                Life = 4f,
                LifeVar = 1,
                StartScale = 1.25f,
                StartScaleVar = 0.5f,
                EndScale = 0.5f,
                EndScaleVar = 0.2f,
                StartColor = new Color(20, 60, 255, 255),
                StartColorVar = new Color(0, 0, 48, 75),
                EndColor = new Color(199, 199, 255, 0),
                EndColorVar = new Color(0, 0, 0, 0),
                Gravity = new Vector2(0, 150),
                Active = true,
                Duration = 0
            };
        }

    }
}
