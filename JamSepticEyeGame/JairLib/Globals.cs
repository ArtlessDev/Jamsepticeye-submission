using JairLib.TileGenerators;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Input;

namespace JairLib
{
    public static class Globals
    {
        public static ContentManager GlobalContent;

        public static Texture2D puzzleSet, gameObjectSet;
        public static Texture2DAtlas atlas, gameObjectAtlas;
        public static int PUZZLE_SIZE = 25;
        public static int PUZZLE_SIZE_ADJUSTED = (int)(2 + Math.Sqrt(PUZZLE_SIZE)) * (int)(2 + Math.Sqrt(PUZZLE_SIZE));
        public static SpriteSheet spriteSheet, gameObjectSheet;
        public static List<TileSpace> tileSpaces;
        public static int mapWidth = 30;
        public static int mapHeight = 40;
        public static int TileSize = 32;

        public static KeyboardStateExtended keyb;
        public static string seed;
        public static string[] gridSeed;
        public static SpriteFont font;
        public static int currentLevel = 1;
        public static int CountOfTiles = 8;

        public static int ViewportHeight = 480;
        public static int ViewportWidth = 800;
        public static OrthographicCamera MainCamera;

        public static Vector2 STARTING_POSITION = new Vector2(256,640);

        public static void Load(PlayerOverworld player)
        {
            puzzleSet = GlobalContent.Load<Texture2D>("grayboxedMap_Sheet");
            atlas = Texture2DAtlas.Create("tileSpaceSet", puzzleSet, 32, 32);
            spriteSheet = new SpriteSheet("SpriteSheet/tileSpaceSetJSON", atlas);
            
            gameObjectSet = GlobalContent.Load<Texture2D>("graybox");
            gameObjectAtlas = Texture2DAtlas.Create("gameObjectSet", gameObjectSet, 32, 32);
            gameObjectSheet = new SpriteSheet("SpriteSheet/gameObjectJSON", gameObjectAtlas);
            
            font = GlobalContent.Load<SpriteFont>("File");
            tileSpaces = new List<TileSpace>();
            QuestSystem.SetFirstQuestAsCurrent();
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
            MainCamera.Position = new(player.X-(ViewportWidth/2) +16, player.Y-(ViewportHeight/2)+16);
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
