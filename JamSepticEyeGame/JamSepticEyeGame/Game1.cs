using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Graphics;
using JairLib;
using MonoGame.Extended.ViewportAdapters;
using MonoGame.Extended;
using JairLib.TileGenerators;
using System.Drawing;
using System.Diagnostics;

namespace JamSepticEyeGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        PlayerOverworld player;
        MapBuilder mapBuilder;
        Texture2D shader, one, two;
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
            Globals.MainCamera.Position = new Vector2(Globals.STARTING_POSITION.X-(Globals.ViewportWidth / 2) + 16, Globals.STARTING_POSITION.Y + 16 - (Globals.ViewportWidth/2)+160);//Globals.STARTING_POSITION;//new(Globals.STARTING_POSITION.X/2, Globals.STARTING_POSITION.Y/2);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Globals.Load(player);
            player = new PlayerOverworld();

            one = Globals.GlobalContent.Load<Texture2D>("lowVisibilityFakeShader");
            two = Globals.GlobalContent.Load<Texture2D>("lowVisibilityFakeShader2");

            mapBuilder = new MapBuilder();

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Globals.Update(gameTime);

            player.Update(gameTime, mapBuilder);

            QuestSystem.Update(gameTime, player);

            var deltaTime = (float)gameTime.TotalGameTime.Milliseconds;

            if (deltaTime < 500)
            {
                shader = one;
            }
            else
            {
                shader = two;
            }


            foreach (var space in mapBuilder.Spaces)
            {
                if (space.rectangle.Intersects(player.rectangle) && space.isCollidable)
                {
                    //player.rectangle = new Rectangle(player.rectangle.X + (playerSpeed * 2), rectangle.Y, 64, 64);
                    Debug.WriteLine("collision");
                }
            }
            //foreach(var item in mapBuilder.Spaces)
            //{
            //    item.Update(player);
            //}

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.Black);

            // TODO: Add your drawing code here

            var transformMatrix = Globals.MainCamera.GetViewMatrix();
            
            _spriteBatch.Begin(transformMatrix: transformMatrix);
            mapBuilder.DrawMapFromList(_spriteBatch);
            _spriteBatch.Draw(player.texture, new Vector2(player.rectangle.X, player.rectangle.Y), player.color, 0f, new Vector2(1, 1), new Vector2(1, 1), player.flipper, 0f);
            _spriteBatch.Draw(shader, new Vector2(Globals.MainCamera.Position.X, Globals.MainCamera.Position.Y), Microsoft.Xna.Framework.Color.White);

            QuestSystem.DrawCurrentQuestObjective(_spriteBatch, player);

            if (QuestSystem.InitiatedFirstQuest == false)
            {
                _spriteBatch.DrawString(Globals.font, "I should get to bed..", new Vector2((float)player.rectangle.X-64, (float)player.rectangle.Y - 32), Microsoft.Xna.Framework.Color.White);

            }

            _spriteBatch.DrawString(Globals.font, "Movement[wasd/arrows]\n\nInteract[E]", new Vector2((float)Globals.MainCamera.Position.X, (float)Globals.MainCamera.Position.Y+400), Microsoft.Xna.Framework.Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
