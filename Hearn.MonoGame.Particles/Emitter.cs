using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Hearn.MonoGame.Particles
{

    //This is a port of city41's javascript particle system
    //https://github.com/city41/particle.js/blob/master/public/javascripts/particlesystem/Emitter.js

    public class Emitter
    {

        private int _totalParticles;
        private int _particleCount;
        private int _particleIndex;
        private float _elapsed;
        private float _emitCounter;

        private Random _rnd;

        public Emitter()
        {
            _rnd = new Random();
        }

        public float EmissionRate { get; set; }

        public bool Active { get; set; }
        public int Duration { get; set; }

        public Vector2 Pos { get; set; }
        public Vector2 PosVar { get; set; }

        public float Speed { get; set; }
        public float SpeedVar { get; set; }

        public float Angle { get; set; }
        public float AngleVar { get; set; }

        public float Life { get; set; }
        public float LifeVar { get; set; }

        public float StartScale { get; set; }
        public float StartScaleVar { get; set; }
        public float EndScale { get; set; }
        public float EndScaleVar { get; set; }

        public Color StartColor { get; set; }
        public Color StartColorVar { get; set; }

        public Color EndColor { get; set; }
        public Color EndColorVar { get; set; }

        public Vector2 Gravity { get; set; }

        public float RadialAccel { get; set; }
        public float RadialAccelVar { get; set; }
        public float TangentialAccel { get; set; }
        public float TangentialAccelVar { get; set; }

        public Rectangle? SourceRectangle { get; set; }

        public Particle[] Particles { get; set; }

        public int TotalParticles
        {
            get => _totalParticles;
            set
            {
                if (_totalParticles != value)
                {
                    _totalParticles = value;
                    Restart();
                }
            }
        }

        public Func<Vector2, Vector2> PosVarTransform { get; set; }

        public void Restart()
        {

            Particles = new Particle[_totalParticles];

            for (var i = 0; i < _totalParticles; i++)
            {
                Particles[i] = new Particle();
            }

            _particleCount = 0;
            _particleIndex = 0;
            _elapsed = 0;
            _emitCounter = 0;

        }

        public void Update(GameTime gameTime)
        {

            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _elapsed += delta;

            Active = _elapsed < Duration || Duration == 0;

            if (Active && EmissionRate > 0)
            {
                // emit new particles based on how much time has passed and the emission rate

                var rate = 1.0f / EmissionRate;
                _emitCounter += delta;

                while (!IsFull() && _emitCounter > rate)
                {
                    AddParticle();
                    _emitCounter -= rate;
                }

            }

            if (_particleCount > 0)
            {
                _particleIndex = 0;
                while (_particleIndex < _particleCount)
                {
                    var particle = Particles[_particleIndex];
                    UpdateParticle(particle, delta, _particleIndex);
                }
            }

        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            for (var i = 0; i < Particles.Length; i++)
            {
                var particle = Particles[i];
                if (particle.Life > 0)
                {
                    spriteBatch.Draw(texture, particle.Pos, SourceRectangle, particle.Color, 0, Vector2.Zero, particle.Scale, SpriteEffects.None, 0);
                }
            }
        }

        // Private

        private bool AddParticle()
        {
            if (!IsFull())
            {

                var particle = Particles[_particleCount];
                InitParticle(particle);
                ++_particleCount;

            }

            return false;
        }

        private void InitParticle(Particle particle)
        {

            var posVar = new Vector2()
            {
                X = PosVar.X * _rnd.NextFloat(-1, 1),
                Y = PosVar.Y * _rnd.NextFloat(-1, 1)
            };

            if (PosVarTransform != null)
            {
                posVar = PosVarTransform(posVar);
            }

            particle.Pos = new Vector2(Pos.X + posVar.X, Pos.Y + posVar.Y);

            var angle = Angle + (AngleVar * _rnd.NextFloat(-1, 1));
            var speed = Speed + (SpeedVar * _rnd.NextFloat(-1, 1));

            particle.SetVelocity(angle, speed);

            particle.RadialAccel = RadialAccel + (RadialAccelVar * _rnd.NextFloat(-1, 1));
            particle.TangentialAccel = TangentialAccel + (TangentialAccelVar * _rnd.NextFloat(-1, 1));

            var life = Life + (LifeVar * _rnd.NextFloat(-1, 1));
            particle.Life = Math.Max(0, life);

            particle.Scale = new Vector2(StartScale, StartScale);
            particle.DeltaScale = EndScale - StartScale;
            particle.DeltaScale /= particle.Life;

            var startColor = new Color();
            startColor.R = (byte)(StartColor.R + (StartColorVar.R * _rnd.NextDouble(-1, 1)));
            startColor.G = (byte)(StartColor.G + (StartColorVar.G * _rnd.NextDouble(-1, 1)));
            startColor.B = (byte)(StartColor.B + (StartColorVar.B * _rnd.NextDouble(-1, 1)));
            startColor.A = (byte)(StartColor.A + (StartColorVar.A * _rnd.NextDouble(-1, 1)));
            StartColor = startColor;

            var endColor = new Color();
            endColor.R = (byte)(EndColor.R + (EndColorVar.R * _rnd.NextDouble(-1, 1)));
            endColor.G = (byte)(EndColor.G + (EndColorVar.G * _rnd.NextDouble(-1, 1)));
            endColor.B = (byte)(EndColor.B + (EndColorVar.B * _rnd.NextDouble(-1, 1)));
            endColor.A = (byte)(EndColor.A + (EndColorVar.A * _rnd.NextDouble(-1, 1)));
            EndColor = endColor;

            particle.Color = StartColor;

            particle.DeltaColor[0] = (EndColor.R - StartColor.R) / particle.Life;
            particle.DeltaColor[1] = (EndColor.G - StartColor.G) / particle.Life;
            particle.DeltaColor[2] = (EndColor.B - StartColor.B) / particle.Life;
            particle.DeltaColor[3] = (EndColor.A - StartColor.A) / particle.Life;

            for (var i = 0; i < 4; i++)
            {
                if (particle.DeltaColor[i] < 0)
                {
                    particle.DeltaColor[i] = 0;
                }
                if (particle.DeltaColor[i] > 255)
                {
                    particle.DeltaColor[i] = 255;
                }
            }
        }

        private bool IsFull()
        {
            return _particleCount == _totalParticles;
        }

        private void UpdateParticle(Particle particle, float delta, int i)
        {

            if (particle.Life > 0)
            {

                // these vectors are stored on the particle so we can reuse them, avoids
                // generating lots of unnecessary objects each frame

                particle.Forces = Vector2.Zero;
                particle.Radial = Vector2.Zero;

                if ((particle.Pos.X != Pos.X || particle.Pos.Y != Pos.Y) && (particle.RadialAccel != 0 || particle.TangentialAccel != 0))
                {
                    particle.Radial = new Vector2(
                        particle.Pos.X - Pos.X,
                        particle.Pos.Y - Pos.Y
                    );

                    particle.Radial.Normalize();
                }

                var oldRadialX = particle.Radial.X;
                var oldRadialY = particle.Radial.Y;

                particle.Radial = new Vector2(
                    particle.Radial.X * particle.RadialAccel,
                    particle.Radial.Y * particle.RadialAccel
                );

                particle.Tangential = new Vector2(
                    (-oldRadialY) * particle.TangentialAccel,
                    oldRadialX * particle.TangentialAccel
                );

                particle.Forces = new Vector2(
                    (particle.Radial.X + particle.Tangential.X + Gravity.X) * delta,
                    (particle.Radial.Y + particle.Tangential.Y + Gravity.Y) * delta
                );

                particle.Vel = new Vector2(
                    particle.Vel.X + particle.Forces.X,
                    particle.Vel.Y + particle.Forces.Y
                );

                particle.Pos = new Vector2(
                    particle.Pos.X + (particle.Vel.X * delta),
                    particle.Pos.Y + (particle.Vel.Y * delta)
                );

                particle.Life -= delta;

                particle.Scale = new Vector2(
                    particle.Scale.X + (particle.DeltaScale * delta),
                    particle.Scale.Y + (particle.DeltaScale * delta)
                );

                var newColor = new Color();
                newColor.R = (byte)(particle.Color.R + (particle.DeltaColor[0] * delta));
                newColor.G = (byte)(particle.Color.G + (particle.DeltaColor[1] * delta));
                newColor.B = (byte)(particle.Color.B + (particle.DeltaColor[2] * delta));
                newColor.A = (byte)(particle.Color.A + (particle.DeltaColor[3] * delta));
                particle.Color = newColor;

                ++_particleIndex;
            }
            else
            {
                // the particle has died, time to return it to the particle pool
                // take the particle at the current index

                var temp = Particles[i];

                // and move it to the end of the active particles, keeping all alive particles pushed
                // up to the front of the pool
                Particles[i] = Particles[_particleCount - 1];
                Particles[_particleCount - 1] = temp;

                // decrease the count to indicate that one less particle in the pool is active.
                --_particleCount;

            }

        }

    }
}
