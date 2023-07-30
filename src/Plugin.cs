using HarmonyLib;

namespace CompactStorage
{
    public class Plugin : Mod
    {
        public static Plugin Instance;
        public static ModLogger StaticLogger;

        private void Awake()
        {
            Instance = this;
            StaticLogger = Logger;
            Harmony.PatchAll(typeof(Patches));
        }

        public override void Ready()
        {
            WorldManager.instance.GameDataLoader.AddCardToSetCardBag(
                SetCardBagType.AdvancedBuildingIdea,
                Consts.Idea(Consts.STACKED_WAREHOUSES),
                1
            );
            WorldManager.instance.GameDataLoader.AddCardToSetCardBag(
                SetCardBagType.Island_AdvancedBuildingIdea,
                Consts.Idea(Consts.FOOD_WAREHOUSE),
                1
            );
            WorldManager.instance.GameDataLoader.AddCardToSetCardBag(
                SetCardBagType.Island_AdvancedBuildingIdea,
                Consts.Idea(Consts.MAGIC_POUCH),
                1
            );
        }
    }
}
