using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MonoGame.Extended;
using System.Diagnostics;

namespace JairLib
{
    public class Quest
    {
        public Quest() { }

        public string QuestIdentifier { get; set; }
        public KeyObjective StartingObjective { get; set; }
        public KeyObjective MiddleObjective { get; set; }
        public KeyObjective EndingObjective { get; set; }
    }

    public static class QuestSystem
    {
        public static Quest? CurrentQuest;

        public static string FirstQuest = "C:\\Code\\Jamsepticeye-submission\\JamSepticEyeGame\\JamSepticEyeGame\\Content\\JsonFiles\\FirstQuest.json";
        public static string FirstQuestMod = "FirstQuest.json";

        public static void SetFirstQuestAsCurrent()
        {
            string jsonString = File.ReadAllText(FirstQuest);
            CurrentQuest = JsonSerializer.Deserialize<Quest>(jsonString);
            Debug.WriteLine(CurrentQuest.StartingObjective.rectangle);
        }

        public static void DrawCurrentQuestObjective(SpriteBatch _spriteBatch)
        {
            if (!CurrentQuest.StartingObjective.IsCompletedFlag){
                _spriteBatch.Draw(CurrentQuest.StartingObjective.texture, new Vector2(CurrentQuest.StartingObjective.rectangle.X, CurrentQuest.StartingObjective.rectangle.Y), CurrentQuest.StartingObjective.color);
                //Debug.WriteLine($"{CurrentQuest.StartingObjective.rectangle.X}, {CurrentQuest.StartingObjective.rectangle.X}");
                CurrentQuest.StartingObjective.texture = Globals.atlas[CurrentQuest.StartingObjective.textureValue];

            }
            else if (!CurrentQuest.MiddleObjective.IsCompletedFlag)
                _spriteBatch.Draw(CurrentQuest.StartingObjective.texture, new Vector2(CurrentQuest.StartingObjective.rectangle.X, CurrentQuest.StartingObjective.rectangle.Y), CurrentQuest.StartingObjective.color);
            else if (!CurrentQuest.EndingObjective.IsCompletedFlag)
                _spriteBatch.Draw(CurrentQuest.StartingObjective.texture, new Vector2(CurrentQuest.StartingObjective.rectangle.X, CurrentQuest.StartingObjective.rectangle.Y), CurrentQuest.StartingObjective.color);
        }
    }

}
