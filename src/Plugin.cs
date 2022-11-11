using BepInEx;
using HarmonyLib;

namespace CompactStorage
{
    [BepInPlugin("de.benediktwerner.stacklands.compactstorage", PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            Harmony.CreateAndPatchAll(typeof(Patches));
        }
    }
}
