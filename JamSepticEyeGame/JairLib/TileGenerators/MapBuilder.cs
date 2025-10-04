using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Graphics;
using System.Globalization;

namespace JairLib.TileGenerators
{
    public class MapBuilder
    {
        public MapBuilder()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };
            var defaultFilePath = "C:\\Code\\Jamsepticeye-submission\\JamSepticEyeGame\\JamSepticEyeGame\\Content\\Sprites\\grayboxedMap.csv";
            //var defaultFilePath = "C:\\Code\\Jamsepticeye-submission\\JamSepticEyeGame\\JamSepticEyeGame\\Content\\Sprites\\grayboxedMap.csv";
            //var defaultFilePath = "C:\\Code\\Jamsepticeye-submission\\JamSepticEyeGame\\JamSepticEyeGame\\Content\\Sprites\\test.csv";
            using (var reader = new StreamReader(defaultFilePath))
            using (var csv = new CsvReader(reader, config))
            {
                Spaces = new List<TileSpace>();
                records = new List<int>();
                var numberOfRows = 0;
                while (csv.Read())
                {
                    var indexer = 0;

                    for (int i = 0; i < Globals.mapWidth; i++)
                    {
                        var value = int.Parse(csv.GetField(indexer));

                        Spaces.Add(
                            new TileSpace(value)
                            {
                                rectangle = new Rectangle(i * 32, numberOfRows*32,32,32)
                            }
                            );
                        indexer++;
                    }
                    ///column needs to be the height value which is 40 in this case
                    /// rows need to be the width value, which in this case is 30
                    numberOfRows++;
                    columns = csv.ColumnCount;
                }
                rows = numberOfRows;

            }
        }

        public List<TileSpace> Spaces { get; set; }
        public int rows { get; set; }
        public int columns { get; set; }
        public List<int> records { get; set; }
        public static CsvReader csvFileReader {  get; set; }

        public void DrawMapFromList(SpriteBatch _spriteBatch)
        {
            if (Spaces != null)
            {
                var indexer = 0;

                ///currently will make a square and does not fill out the entire map, the map is however coming in correctly and has 1200 values
                for (int down = 0; down < rows; down++)
                {

                    for (int left = 0; left < columns; left++)
                    {
                        TileSpace t = Spaces[indexer];
                        _spriteBatch.Draw(Spaces[indexer].texture, new Vector2(t.rectangle.X, t.rectangle.Y), Color.White);
                        //_spriteBatch.Draw(Spaces[indexer].texture, new Vector2(32 * left, 32 * down), Color.White);
                        indexer++;
                    }
                }
            }
        }
    }
}
