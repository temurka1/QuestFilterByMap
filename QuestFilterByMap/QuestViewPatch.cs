namespace QuestFilterByMap
{
    using System.Linq;
    using System.Collections.Generic;
    
    using HarmonyLib;

    using UnityEngine;
    using UnityEngine.UI;

    using Duckov.Quests.UI;

    using SortingMode = Duckov.Quests.Quest.SortingMode;

    [HarmonyPatch(typeof(QuestView), "RefreshEntryList")]
    public class QuestViewPatch
    {
        private static readonly Dictionary<QuestLocation, Color> _questColorMap = Utils.GetQuestsColorMap();

        private static Color _initialColor;
        private static bool _initialColorSet = false;

        public static void Postfix(QuestView __instance)
        {
            var activeQuestEntries = GetActiveQuestEntries(__instance);
            StoreDefaultQuestEntryColor(activeQuestEntries);

            foreach (var questEntry in activeQuestEntries)
            {
                var questEntryColor = __instance.SortingMode == SortingMode.Location
                    ? _questColorMap[Utils.GetQuestLocation(questEntry)]
                    : _initialColor;

                SetQuestEntryColor(questEntry, questEntryColor);
            }
        }

        private static List<QuestEntry> GetActiveQuestEntries(QuestView questView)
        {
            return Utils.GetFieldValue<List<QuestEntry>, QuestView>(questView, "activeEntries");
        }

        private static void SetQuestEntryColor(QuestEntry questEntry, Color questEntryColor)
        {
            var backgroundComponent = questEntry.GetComponent<Image>();
            backgroundComponent.color = questEntryColor;
        }

        private static void StoreDefaultQuestEntryColor(List<QuestEntry> activeQuestEntries)
        {
            if (!activeQuestEntries.Any() || _initialColorSet)
                return;

            if (!_initialColorSet)
            {
                _initialColorSet = true;
                _initialColor = activeQuestEntries.First().GetComponent<Image>().color;
            }
        }
    }
}
