using JairLib.TileGenerators;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Graphics;
using System.Diagnostics;

namespace JairLib;

public class PlayerOverworld : ITileObject
{
    public static int PLAYER_TILESIZE_IN_WORLD = 28;
    public string identifier { get; set; }
    public Rectangle rectangle { get; set; }
    public Texture2DRegion texture { get; set; }
    public Color color { get; set; }
    public PlayerState state { get; set; }

    public SpriteEffects flipper;
    public int playerSpeed { get; set; }

    public PlayerOverworld()
    {
        identifier = "blokkit";
        //texture = Globals.atlas[2 - '0'];
        texture = Globals.gameObjectAtlas[10]; //blue
        rectangle = new Rectangle((int)Globals.STARTING_POSITION.X, (int)Globals.STARTING_POSITION.Y, PLAYER_TILESIZE_IN_WORLD, PLAYER_TILESIZE_IN_WORLD);
        color = Color.White;
        flipper = SpriteEffects.None;
        state = PlayerState.Waiting;
        playerSpeed = 3;
        //Globals.CamMove(rectangle);
    }

    public void Movement(MapBuilder mapBuilder)
    {
        var precheck = rectangle;

        if (Globals.keyb.IsKeyDown(Keys.Left) || Globals.keyb.IsKeyDown(Keys.A))
        {
            flipper = SpriteEffects.None;
            rectangle = new Rectangle(rectangle.X - playerSpeed, rectangle.Y, PLAYER_TILESIZE_IN_WORLD, PLAYER_TILESIZE_IN_WORLD);            
            state = PlayerState.Walking;
            Globals.CamMove(this.rectangle);

            foreach (var space in mapBuilder.Spaces)
            {
                if (space.rectangle.Intersects(rectangle) && space.isCollidable)
                {
                    rectangle = new Rectangle(rectangle.X + playerSpeed, rectangle.Y, PLAYER_TILESIZE_IN_WORLD, PLAYER_TILESIZE_IN_WORLD);

                }
            }
        }
        else if (Globals.keyb.IsKeyDown(Keys.Right) || Globals.keyb.IsKeyDown(Keys.D))
        {
            
            flipper = SpriteEffects.FlipHorizontally;
            rectangle = new Rectangle(rectangle.X + playerSpeed, rectangle.Y, PLAYER_TILESIZE_IN_WORLD, PLAYER_TILESIZE_IN_WORLD);
            state = PlayerState.Walking;
            Globals.CamMove(this.rectangle);

            foreach (var space in mapBuilder.Spaces)
            {
                if (space.rectangle.Intersects(this.rectangle) && space.isCollidable)
                {
                    rectangle = new Rectangle(rectangle.X - playerSpeed, rectangle.Y, PLAYER_TILESIZE_IN_WORLD, PLAYER_TILESIZE_IN_WORLD);

                }
            }
        }
        else if (Globals.keyb.IsKeyDown(Keys.Up) || Globals.keyb.IsKeyDown(Keys.W))
        {

            rectangle = new Rectangle(rectangle.X, rectangle.Y - playerSpeed, PLAYER_TILESIZE_IN_WORLD, PLAYER_TILESIZE_IN_WORLD);
            state = PlayerState.Walking;
            Globals.CamMove(this.rectangle);

            foreach (var space in mapBuilder.Spaces)
            {
                if (space.rectangle.Intersects(this.rectangle) && space.isCollidable)
                {
                    rectangle = new Rectangle(rectangle.X, rectangle.Y + playerSpeed, PLAYER_TILESIZE_IN_WORLD, PLAYER_TILESIZE_IN_WORLD);
                }
            }
        }
        else if (Globals.keyb.IsKeyDown(Keys.Down) || Globals.keyb.IsKeyDown(Keys.S))
        {
            
            rectangle = new Rectangle(rectangle.X, rectangle.Y + playerSpeed, PLAYER_TILESIZE_IN_WORLD, PLAYER_TILESIZE_IN_WORLD);
            state = PlayerState.Walking;
            Globals.CamMove(this.rectangle);

            foreach (var space in mapBuilder.Spaces)
            {
                if (space.rectangle.Intersects(this.rectangle) && space.isCollidable)
                {
                    rectangle = new Rectangle(rectangle.X, rectangle.Y - playerSpeed, PLAYER_TILESIZE_IN_WORLD, PLAYER_TILESIZE_IN_WORLD);
                }
            }
        }

        
    }

    public void AnimatePlayerIdle(GameTime gameTime)
    {

        var deltaTime = (float)gameTime.TotalGameTime.Milliseconds;
        //could make methods to handle animations in this sort of way
        if (deltaTime < 500)
        {
            texture = Globals.gameObjectAtlas[11];
            Debug.WriteLine($"{deltaTime}");
        }
        else
        {
            texture = Globals.gameObjectAtlas[10];
            Debug.WriteLine($"{deltaTime}");
        }
    }


    public void AnimatePlayerMoving(GameTime gameTime)
    {
        texture = Globals.atlas[12];

        var deltaTime = (float)gameTime.TotalGameTime.Milliseconds;

        if (deltaTime == 0)
        {
            state = PlayerState.Waiting;

        }
    }

    public void Update(GameTime gameTime, MapBuilder mapBuilder)
    {
        Movement(mapBuilder);


        //if(state == PlayerState.Walking)
        //{
        //    AnimatePlayerMoving(gameTime);
        //}
        //else
        //{
        //    AnimatePlayerIdle(gameTime);
        //}

    }
}
