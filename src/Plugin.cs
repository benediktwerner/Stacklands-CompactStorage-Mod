using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace CompactStorage
{
    [BepInPlugin("de.benediktwerner.stacklands.compactstorage", PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public static ManualLogSource StaticLogger;

        private void Awake()
        {
            StaticLogger = Logger;
            Harmony.CreateAndPatchAll(typeof(Patches));
        }
    }
}
