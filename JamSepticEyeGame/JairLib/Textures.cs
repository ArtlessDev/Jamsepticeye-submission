using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Graphics;

namespace JairLib
{
    public class TileTextures : ITileObject
    {

        public TileTextures()
        {
            rectangle = new Rectangle(0, 0, 64, 64);
            region = new Texture2DRegion(Globals.puzzleSet, 0, 0, 64, 64);
            color = Color.White;
        }
        public TileTextures(Texture2DRegion txtregion)
        {
            rectangle = new Rectangle(0, 0, 64, 64);
            region = txtregion;
            color = Color.White;
        }

        public Texture2DRegion region { get; set; }
        public string identifier { get; set; }
        public Rectangle rectangle { get; set; }
        public Color color { get; set; }
        
    }

    public enum Texture
    {
        GroundTile,
    }
}
