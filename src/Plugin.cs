using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace CompactStorage
{
    [BepInPlugin("de.benediktwerner.stacklands.compactstorage", PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public static Plugin Instance;
        public static ManualLogSource StaticLogger;

        private void Awake()
        {
            Instance = this;
            StaticLogger = Logger;
            Harmony.CreateAndPatchAll(typeof(Patches));
        }
    }
}
