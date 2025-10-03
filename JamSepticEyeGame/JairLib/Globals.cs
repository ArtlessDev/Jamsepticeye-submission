using JairLib.TileGenerators;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Input;
using System;
using System.Reflection.Metadata;

namespace JairLib
{
    public static class Globals
    {
        public static ContentManager GlobalContent;
        public static Texture2D puzzleSet;
        public static Texture2DAtlas atlas;
        public static int PUZZLE_SIZE = 25;
        public static int PUZZLE_SIZE_ADJUSTED = (int)(2 + Math.Sqrt(PUZZLE_SIZE)) * (int)(2 + Math.Sqrt(PUZZLE_SIZE));
        public static KeyboardStateExtended keyb;
        public static SpriteSheet spriteSheet;
        public static string seed;
        public static string[] gridSeed;
        public static SpriteFont font;
        public static int currentLevel = 1;
        public static int CountOfTiles = 8;
        public static List<TileSpace> tileSpaces;

        public static int ViewportHeight = 480;
        public static int ViewportWidth = 800;
        public static OrthographicCamera MainCamera;
        public static int mapWidth = 30;
        public static int mapHeight = 40;

        public static Vector2 STARTING_POSITION = new Vector2(256,640);

        public static void Load(PlayerOverworld player)
        {
            puzzleSet = GlobalContent.Load<Texture2D>("graybox");
            atlas = Texture2DAtlas.Create("tileSpaceSet", puzzleSet, 32, 32);
            //font = GlobalContent.Load<SpriteFont>("PrettyPixelBIG");
            spriteSheet = new SpriteSheet("SpriteSheet/tileSpaceSetJSON", Globals.atlas);
            tileSpaces = new List<TileSpace>();
            QuestSystem.SetFirstQuestAsCurrent();
            //MainCamera.Position = new(player.rectangle.X - (ViewportWidth / 2), player.rectangle.Y - (ViewportHeight / 2));
        }

        public static void Update(GameTime gameTime)
        {
            KeyboardExtended.Update();
            keyb = KeyboardExtended.GetState();

            //GenerateSeed();

            //seed = SeedBuilder.TheStringGetsThisLength(Globals.PUZZLE_SIZE);
        }

        public static void CamMoveHorizontal(PlayerOverworld player)
        {
            MainCamera.Move(new(player.rectangle.X-100, player.rectangle.Y-100));
        }

        public static void CamMove(Rectangle player)
        {
            MainCamera.Position = new(player.X-(ViewportWidth/2), player.Y-(ViewportHeight/2));
            //MainCamera.Move(new(, player.rectangle.Y-100));
        }

        private static void GenerateSeed()
        {
            if (keyb.WasKeyPressed(Keys.Enter))
            {
                Globals.tileSpaces.Clear();
                seed = SeedBuilder.TheSeedGetsSomeOnes(seed);
                SeedBuilder.MaketheSeedGrid(gridSeed);
                //SeedBuilder.MakeSeedGridFromList();
                gridSeed = SeedBuilder.SplitTheSeedToAGrid(seed);
            }
        }

        public static void Draw(GameTime gameTime, SpriteBatch _spriteBatch)
        {
            
            if (!string.IsNullOrEmpty(seed))
            {
                _spriteBatch.DrawString(font, seed, new Vector2(8, 8), Color.DarkGreen);
            }

            _spriteBatch.DrawString(font, "press enter to generate a new seed", new Vector2(0, 32), Color.White);

            //SeedBuilder.DrawtheSeedGrid(_spriteBatch, gridSeed);
            //SeedBuilder.DrawSeedGridFromList(_spriteBatch, map);
        }

        
    }
}
