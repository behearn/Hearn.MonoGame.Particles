using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Hearn.MonoGame.Particles
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        List<Emitter> _emitters;
        int _currentEmitter = 0;
        bool _canChange = true;
        Texture2D _particleTexture;

        private int Width { get { return GraphicsDevice.Viewport.Width; } }
        private int Height { get { return GraphicsDevice.Viewport.Height; } }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        private void CreateEmitterList()
        {

            _emitters = new List<Emitter>();

            _emitters.Add(EmitterLibrary.Fire(new Vector2(Width / 2, Height)));

            _emitters.Add(EmitterLibrary.Fireworks(new Vector2(Width / 2, Height)));

            _emitters.Add(EmitterLibrary.Galaxy(new Vector2(Width / 2, Height / 2)));

            _emitters.Add(EmitterLibrary.Meteor(new Vector2(Width / 2, Height / 2)));

            _emitters.Add(EmitterLibrary.RingOfFire(new Vector2(Width / 2, Height / 2)));

            _emitters.Add(EmitterLibrary.Snow(new Vector2(Width / 2, -24)));

            _emitters.Add(EmitterLibrary.WaterGeyser(new Vector2(Width / 2, Height)));

        }

        protected override void Initialize()
        {
            CreateEmitterList();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            _particleTexture = Content.Load<Texture2D>("particle");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (_canChange && Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                _canChange = false;
                _currentEmitter += 1;
                if (_currentEmitter >= _emitters.Count)
                {
                    _currentEmitter = 0;
                }
                _emitters[_currentEmitter].Restart();
            }

            if (Keyboard.GetState().IsKeyUp(Keys.Space))
            {
                _canChange = true;
            }

            _emitters[_currentEmitter].Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);
            _emitters[_currentEmitter].Draw(spriteBatch, _particleTexture);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
