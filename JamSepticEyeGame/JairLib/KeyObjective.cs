using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JairLib
{
    public class KeyObjective //: ITileObject
    {
        public KeyObjective() {
            //rectangle = new();
            texture = Globals.atlas[textureValue];
            color = Color.White;
        }
        public string objectiveTitle { get; set; }
        public string objectiveDescription { get; set; }
        public string identifier { get; set; }
        public int x {get; set;}
        public int y {get; set;}
        public int width {get; set;}
        public int height {get; set;}
        public Rectangle rectangle => new Rectangle(x,y,width,height);
        public int textureValue {  get; set; }
        public Texture2DRegion texture { get; set; }
        public Color color { get; set; }
        public bool IsCompletedFlag { get; set; }
    
        public void Update(GameTime gameTime, PlayerOverworld player)
        {
            if (player.rectangle.Intersects(this.rectangle) && Globals.keyb.WasKeyPressed(Keys.E))
            {
                Debug.WriteLine(this.objectiveTitle);
            }
        }

        public void isPlayerInteracting()
        {

        }
    }
}
