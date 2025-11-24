namespace QuestFilterByMap
{
    using System.Collections.Generic;
    using System.Reflection;

    using UnityEngine;

    using Duckov.Quests.UI;

    public class Utils
    {
        public static T GetFieldValue<T, U>(U typeInstance, string fieldName) where T : class
        {
            var field = typeof(U).GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
            return field?.GetValue(typeInstance) as T ?? default(T);
        }

        public static QuestLocation GetQuestLocation(QuestEntry questEntry)
        {
            var locationMap = new Dictionary<string, QuestLocation>
            {
                { "Bunker", QuestLocation.Bunker },
                { "Ground Zero", QuestLocation.Zero },
                { "Warehouse Area", QuestLocation.Warehouse },
                { "Farm Town", QuestLocation.Farm },
                { "J-Lab", QuestLocation.Lab },
                { "Storm Area", QuestLocation.Storm }
            };

            if (questEntry.Target.RequireSceneInfo == null)
                return QuestLocation.Any;

            return locationMap[questEntry.Target.RequireSceneInfo.DisplayName];
        }

        public static Dictionary<QuestLocation, Color> GetQuestsColorMap()
        {
            ColorUtility.TryParseHtmlString("#435663", out Color anyColor);
            ColorUtility.TryParseHtmlString("#6B5B95", out Color zeroColor);
            ColorUtility.TryParseHtmlString("#FFA239", out Color warehouseColor);
            ColorUtility.TryParseHtmlString("#628141", out Color farmColor);
            ColorUtility.TryParseHtmlString("#62109F", out Color stormColor);
            ColorUtility.TryParseHtmlString("#1581BF", out Color labColor);
            ColorUtility.TryParseHtmlString("#313647", out Color bunkerColor);

            return new Dictionary<QuestLocation, Color>()
            {
                { QuestLocation.Any, anyColor  },
                { QuestLocation.Bunker, bunkerColor },
                { QuestLocation.Zero, zeroColor },
                { QuestLocation.Warehouse, warehouseColor },
                { QuestLocation.Farm, farmColor },
                { QuestLocation.Lab, labColor },
                { QuestLocation.Storm, stormColor }
            };
        }
    }
}
