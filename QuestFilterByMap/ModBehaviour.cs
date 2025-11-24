namespace QuestFilterByMap
{
    using System.Reflection;
    using HarmonyLib;

    public class ModBehaviour : Duckov.Modding.ModBehaviour
    {
        private const string MOD_ID = "QuestFilterByMap";
        private const string MOD_VERSION = "0.0.1";

        private Harmony _patcher;

        void OnEnable()
        {
            _patcher = new Harmony(MOD_ID);
            _patcher.PatchAll(Assembly.GetExecutingAssembly());
        }

        void OnDisable()
        {
            _patcher.UnpatchAll(MOD_ID);
        }
    }
}
