﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Graphics;
using System.Diagnostics;

namespace JairLib.TileGenerators
{
    public static class SeedBuilder
    {
        public static string TheStringGetsThisLength(int length)
        {
            var str = "";

            for (int i = 0; i < length; i++)
            {
                str += "0";
            }

            return str;
        }

        public static string TheSeedGetsSomeOnes(string seed)
        {
            int[] tempSeed = new int[seed.Length];
            var newSeed = "";

            for (int i = 0; i < seed.Length; i++)
            {
                int rand = Random.Shared.Next(0, 10);

                if (rand < Globals.CountOfTiles)
                {
                    ///IM AN IDIOT STRINGS ARE IMMUTABLE AHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH
                    tempSeed[i] = rand;
                }
                else
                {
                    tempSeed[i] = 0;
                }
                
            }

            foreach (var i in tempSeed)
            {
                newSeed += i;
            }

            return newSeed;
        }

        public static string[] SplitTheSeedToAGrid(string seed)
        {

            int splitbyThisAmount = (int)Math.Sqrt(seed.Length);
            int splitbyAdjustedAmount = (int)Math.Sqrt(seed.Length) + 2;

            string[] gridSeed = new string[splitbyAdjustedAmount];

            int splitterIndicator = 1;

            for (int i = 0; i <= splitbyAdjustedAmount; i++)
            {
                gridSeed[0] += "1";
            }

            foreach (var i in seed)
            {
                gridSeed[splitterIndicator] += i; 

                if (gridSeed[splitterIndicator].Length == splitbyThisAmount)
                {
                    splitterIndicator++;
                }
            }

            for (int i = 0; i <= splitbyAdjustedAmount; i++)
            {
                gridSeed[gridSeed.Length-1] += "1";
            }

            return gridSeed;
        }

        public static void DrawThePlayer(SpriteBatch _spriteBatch, PlayerOverworld player)
        {
            _spriteBatch.Draw(player.texture, new Vector2(player.rectangle.X, player.rectangle.Y), player.color, 0f, new Vector2(1,1), new Vector2(1,1), player.flipper, 0f);
        }

        public static void DrawtheSeedGrid(SpriteBatch _spriteBatch, string[] gridSeed)
        {
            if (gridSeed != null)
            {
                //need to change to these foreach loops into for loops
                foreach (var item in gridSeed)
                {
                    int xValue = -1;
                    int height = (64 * (Array.IndexOf(gridSeed, item) + 1));
                    foreach (var digit in item)
                    {
                        if (Array.IndexOf(gridSeed, item) == 0 || Array.IndexOf(gridSeed, item) == gridSeed.Length-1)
                        {

                        }
                        else
                        {
                            xValue = (64 * (Array.IndexOf(item.ToCharArray(), digit) + 1));
                        }

                        TileSpace tileSpace = new TileSpace();

                        var what = digit - '0';

                        tileSpace.texture = Globals.atlas[what];

                        _spriteBatch.Draw(tileSpace.texture, new Vector2(xValue, height), Color.White);
                    }
                }
            }
        }

        public static void DrawSeedGridFromList(SpriteBatch _spriteBatch, MapBuilder map)
        {
            if (map.Spaces != null)
            {
                var mapTilesValues = map.Spaces;
                //TileSpace g = mapTilesValues[6];
                //_spriteBatch.Draw(g.texture, new Vector2(64, 64), Color.White);
                var indexer = 0;


                ///currently will make a square and does not fill out the entire map, the map is however coming in correctly and has 1200 values
                for (int down = 0; down < map.rows; down++)
                {
                    //TileSpace g = mapTilesValues[down];
                    //_spriteBatch.Draw(mapTilesValues[indexer].texture, new Vector2(32 * down, 32), Color.White);

                    for (int left = 0; left < map.columns; left++)
                    {
                        //var indexer = (down + left)-1;
                        var t = map.Spaces[indexer];
                        _spriteBatch.Draw(t.texture, new Vector2(32 * left, 32 * down), Color.White);
                        //_spriteBatch.Draw(mapTilesValues[indexer].texture, new Vector2(32 * left, 32 * down), Color.White);
                        //Debug.WriteLine($"down: {down} | left: {left} | map obj: {map.records[0]}");
                        indexer++;
                    }
                }

                //for (int down = 0; down < 8; down++)
                //{
                //    var xValue = (down + 1) * 64;
                //    var yValue = 0;
                //    var adjSquareRoot = (int)Math.Sqrt(Globals.PUZZLE_SIZE_ADJUSTED);

                //    for (int left = 0 + down; left < 10 + adjSquareRoot; left++)
                //    {
                //        yValue = (left + 1) * 64;

                //        if (Globals.tileSpaces[left] == null)
                //        {
                //            break;
                //        }

                //        _spriteBatch.Draw(Globals.tileSpaces[down].texture, new Vector2(xValue, yValue), Color.White);
                //    }

                //}
            }
        }

        public static void MakeSeedGridFromList()
        {
            if (Globals.tileSpaces != null)
            {
                foreach (var tileSpace in Globals.tileSpaces)
                {
                    if (tileSpace.texture == null)
                    {
                        tileSpace.texture = Globals.atlas[1 - '0'];
                    }
                }
            }
        }

        public static void MaketheSeedGrid(string[] gridSeed)
        {
            if (gridSeed != null)
            {
                foreach (var item in gridSeed)
                {
                    int height = (64 * (Array.IndexOf(gridSeed, item) + 1));
                    foreach (var digit in item)
                    {
                        var xValue = (64 * (Array.IndexOf(item.ToCharArray(), digit) + 1));

                        TileSpace tileSpace = new TileSpace();
                        tileSpace.texture = Globals.atlas[digit - '0'];

                        Globals.tileSpaces.Add(tileSpace);
                        //_spriteBatch.Draw(tileSpace.texture, new Vector2(xValue, height), Color.White);
                    }
                }
            }
        }

        
    }
}
