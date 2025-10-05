using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Graphics;
using System.Diagnostics;

namespace JairLib
{
    public class TileSpace : ITileObject
    {
        public string identifier { get; set; }
        public Rectangle rectangle { get; set; }
        public Texture2DRegion texture { get; set; }
        public Color color { get; set; }
        public bool isCollidable { get; set; }
        public int csvValue { get; set; }

        public TileSpace()
        {
            isCollidable = false;
            texture = Globals.atlas[0];
            rectangle = new Rectangle();
            color = Color.White;
        }
        public TileSpace(int value)
        {
            csvValue = value;
            isCollidable = setCollision();
            texture = Globals.atlas[value];
            rectangle = new Rectangle();
            color = Color.White;
        }

        public TileSpace(Texture2DAtlas specificAtlas, int value)
        {
            csvValue = value;
            isCollidable = setCollision();
            texture = specificAtlas[value];
            rectangle = new Rectangle();
            color = Color.White;
        }

        public bool setCollision()
        {
            switch (csvValue)
            {
                case 12:
                case 19:
                case 21:
                case 22:
                case 23:
                case 24:
                    return false;
                default:
                    return true;
            }
        }

        public void Update(PlayerOverworld player)
        {
            if (player.rectangle.Intersects(rectangle))
            {
                Debug.WriteLine(csvValue);
            }
        }
    }
}
