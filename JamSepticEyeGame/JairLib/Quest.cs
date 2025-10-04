using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text.Json;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;

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
        }

        public static void DrawCurrentQuestObjective(SpriteBatch _spriteBatch)
        {
            KeyObjective[] objectives =
            {
                CurrentQuest.StartingObjective,
                CurrentQuest.MiddleObjective,
                CurrentQuest.EndingObjective,
            };

            foreach (var objective in objectives)
            {

                if (!objective.IsCompletedFlag)
                {
                    objective.Draw(_spriteBatch);
                    objective.texture = Globals.gameObjectAtlas[objective.textureValue];
                    return;
                }
                
                if (objective.IsCompletedFlag)
                {
                    objective.Draw(_spriteBatch);
                }
            }

        }

        public static void Update(GameTime gameTime, PlayerOverworld player)
        {
            if (CurrentQuest.StartingObjective.IsCompletedFlag == false)
            {
                if (player.rectangle.Intersects(CurrentQuest.StartingObjective.rectangle) && Globals.keyb.WasKeyPressed(Keys.E))
                {
                    CurrentQuest.StartingObjective.IsCompletedFlag = true;
                    Debug.WriteLine(CurrentQuest.StartingObjective.objectiveTitle);
                }
            }
            if (CurrentQuest.MiddleObjective.IsCompletedFlag == false)
            {
                if (player.rectangle.Intersects(CurrentQuest.MiddleObjective.rectangle) && Globals.keyb.WasKeyPressed(Keys.E))
                {
                    CurrentQuest.MiddleObjective.IsCompletedFlag = true;
                    Debug.WriteLine(CurrentQuest.MiddleObjective.objectiveTitle);
                }
            }
            if (CurrentQuest.EndingObjective.IsCompletedFlag == false)
            {
                if (player.rectangle.Intersects(CurrentQuest.EndingObjective.rectangle) && Globals.keyb.WasKeyPressed(Keys.E))
                {
                    CurrentQuest.EndingObjective.IsCompletedFlag = true;
                    Debug.WriteLine(CurrentQuest.EndingObjective.objectiveTitle);
                }
            }
        }
    }

}
