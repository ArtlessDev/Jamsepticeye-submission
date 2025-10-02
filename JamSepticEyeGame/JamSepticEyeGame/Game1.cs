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
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Globals.Load();
            player = new PlayerOverworld();

            mapBuilder = new MapBuilder();

            //Globals.CamMove(player);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Globals.Update(gameTime);

            player.Update(gameTime);
            //string values = "";
            //foreach (var val in mapBuilder.Spaces)
            //{
            //    values += val.ToString();
            //}
            //Debug.WriteLine(values);
            //_camera.Move(GetMovementDirection() * movementSpeed * gameTime.GetElapsedSeconds());
            //_screenPosition = _camera.WorldToScreen(new Vector2(player.X, player.Y));
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            var transformMatrix = Globals.MainCamera.GetViewMatrix();
            
            _spriteBatch.Begin(transformMatrix: transformMatrix);
            //_spriteBatch.DrawRectangle(new RectangleF(250, 250, 50, 50), Color.Aqua, 1f);
            SeedBuilder.DrawSeedGridFromList(_spriteBatch, mapBuilder);

            //_spriteBatch.Draw(player.texture, player.rectangle, player.color);
            _spriteBatch.Draw(player.texture, new Vector2(player.rectangle.X, player.rectangle.Y), player.color, 0f, new Vector2(1, 1), new Vector2(1, 1), player.flipper, 0f);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
