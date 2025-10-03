using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Graphics;
using JairLib;
using MonoGame.Extended.ViewportAdapters;
using MonoGame.Extended;
using JairLib.TileGenerators;
using System;
using System.Diagnostics;

namespace JamSepticEyeGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        PlayerOverworld player;
        MapBuilder mapBuilder;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Globals.GlobalContent = Content;
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            base.Initialize();

            var viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, Globals.ViewportWidth, Globals.ViewportHeight);
            Globals.MainCamera = new OrthographicCamera(viewportAdapter);
            Globals.MainCamera.Position = new(Globals.STARTING_POSITION.X/2, Globals.STARTING_POSITION.Y/2);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Globals.Load(player);

            player = new PlayerOverworld();

            mapBuilder = new MapBuilder();

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Globals.Update(gameTime);

            player.Update(gameTime);
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            var transformMatrix = Globals.MainCamera.GetViewMatrix();
            
            _spriteBatch.Begin(transformMatrix: transformMatrix);
            mapBuilder.DrawMapFromList(_spriteBatch);

            _spriteBatch.Draw(player.texture, new Vector2(player.rectangle.X, player.rectangle.Y), player.color, 0f, new Vector2(1, 1), new Vector2(1, 1), player.flipper, 0f);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
