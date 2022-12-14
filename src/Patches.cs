using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;

namespace CompactStorage
{
    class Patches
    {
        [HarmonyPatch(typeof(GameDataLoader), nameof(GameDataLoader.LoadModCards))]
        [HarmonyPostfix]
        private static void AddCards(List<CardData> __result)
        {
            CardLoader.AddCards(__result);
        }

        [HarmonyPatch(typeof(EndOfMonthCutscenes), nameof(EndOfMonthCutscenes.FeedVillagers), MethodType.Enumerator)]
        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> DontDestroyEatenFoodWarehouses(
            IEnumerable<CodeInstruction> instructions
        )
        {
            return new CodeMatcher(instructions)
                .MatchForward(
                    false,
                    new CodeMatch(OpCodes.Ldfld, AccessTools.Field(typeof(Food), "FoodValue")),
                    new CodeMatch(OpCodes.Ldc_I4_0),
                    new CodeMatch(OpCodes.Bgt)
                )
                .ThrowIfInvalid("Didn't find FoodValue <= 0 check")
                .RemoveInstruction()
                .Insert(Transpilers.EmitDelegate<Func<Food, int>>(food => food is FoodWarehouse ? 1 : food.FoodValue))
                .InstructionEnumeration();
        }

        [HarmonyPatch(typeof(WorldManager), nameof(WorldManager.CardCapIncrease))]
        [HarmonyPostfix]
        private static void StackedWarehousesCardCapIncrease(WorldManager __instance, ref int __result, GameBoard board)
        {
            foreach (var card in __instance.AllCards)
                if (card.MyBoard == board && card.CardData is StackedWarehouses w)
                    __result += w.Count;
        }

        [HarmonyPatch(typeof(WorldManager), nameof(WorldManager.BoardSizeIncrease))]
        [HarmonyPostfix]
        private static void StackedWarehousesBoardSizeIncrease(
            WorldManager __instance,
            ref int __result,
            GameBoard board
        )
        {
            foreach (var card in __instance.AllCards)
                if (card.MyBoard == board && card.CardData is StackedWarehouses w)
                    __result += w.LighthouseCount * 10;
        }

        [HarmonyPatch(typeof(GameDataLoader), MethodType.Constructor)]
        [HarmonyPostfix]
        public static void InsertDrops(GameDataLoader __instance)
        {
            AddBoosterIdea(__instance, SetCardBag.AdvancedBuildingIdea, Consts.Idea(Consts.STACKED_WAREHOUSES));
            AddBoosterIdea(
                __instance,
                SetCardBag.Island_AdvancedBuildingIdea,
                Consts.Idea(Consts.FOOD_WAREHOUSE),
                Consts.Idea(Consts.MAGIC_POUCH)
            );
        }

        public static void AddBoosterIdea(GameDataLoader __instance, SetCardBag bag, params string[] cardIds)
        {
            var existing = __instance.GetStringForBag(bag);
            __instance.result[bag] = existing + ", " + cardIds.Join(delimiter: ", ");
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(SokLoc), nameof(SokLoc.SetLanguage))]
        public static void LanguageChanged(SokLoc __instance)
        {
            if (SokLoc.instance == null)
                return;

            foreach (var term in CardLoader.Translations)
            {
                SokLoc.instance.CurrentLocSet.TermLookup[term.Id] = term;
            }
        }
    }
}
