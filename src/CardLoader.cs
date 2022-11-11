using System.Collections.Generic;
using UnityEngine;

namespace CompactStorage
{
    static class CardLoader
    {
        private static GameObject cardContainer;

        public static void AddTranslation(string id, string text)
        {
            var term = new SokTerm(SokLoc.FallbackSet, id, text);
            if (SokLoc.instance != null)
                SokLoc.instance.CurrentLocSet.TermLookup[id] = term;
            SokLoc.FallbackSet.TermLookup[id] = term;
        }

        public static void AddCards(List<CardData> allCards)
        {
            cardContainer = new GameObject("CardContainer - " + PluginInfo.PLUGIN_NAME);
            cardContainer.gameObject.SetActive(false);

            foreach (var c in Cards)
            {
                var go = new GameObject(c.Id);
                go.transform.SetParent(cardContainer.transform);
                var card = (CardData)go.AddComponent(c.ScriptType);
                card.Id = c.Id;
                card.NameTerm = c.Id;
                AddTranslation(card.NameTerm, c.Name);
                card.DescriptionTerm = c.Id + "_desc";
                AddTranslation(card.DescriptionTerm, c.Description);
                card.Value = c.Value;
                card.MyCardType = c.CardType;
                card.IsBuilding = c.IsBuilding;
                if (c.CardType == CardType.Structures)
                {
                    card.PickupSoundGroup = PickupSoundGroup.Heavy;
                }
                if (c.Init != null)
                    c.Init(card);
                allCards.Add(card);
            }

            foreach (var idea in Ideas)
            {
                var id = Consts.Idea(idea.Name);
                var go = new GameObject(id);
                go.transform.SetParent(cardContainer.transform);
                var card = go.AddComponent<Blueprint>();
                card.Id = id;
                card.NameTerm = idea.Name;
                card.MyCardType = CardType.Ideas;
                card.BlueprintGroup = idea.Group;
                card.Subprints = idea.Subprints;
                card.NeedsExactMatch = idea.NeedsExactMatch;
                foreach (var sub in card.Subprints)
                {
                    var term = Consts.PREFIX + sub.StatusTerm.Replace(" ", "").ToLower();
                    AddTranslation(term, sub.StatusTerm);
                    sub.StatusTerm = term;
                }
                allCards.Add(card);
            }
        }

        public static readonly Card[] Cards = new Card[]
        {
            Card.Create<FoodWarehouse>(
                Consts.FOOD_WAREHOUSE,
                "Food Warehouse",
                "Can store 100 food points worth of food. Careful: Once inside, food can't be taken back out.",
                FoodWarehouse.VALUE,
                CardType.Structures,
                building: true,
                init: (card) =>
                {
                    card.MaxFood = 100;
                    card.FoodValue = 0;
                    card.CanSpoil = false;
                }
            ),
            Card.Create<MagicPouch>(
                Consts.MAGIC_POUCH,
                "Magic Pouch",
                "Can store 30 cards of any kind.\n\nDon't put in stuff with special characterstics unless you're fine with them getting lost/reset.",
                10,
                CardType.Structures,
                building: true,
                init: (card) =>
                {
                    card.MaxCards = 30;
                }
            ),
            Card.Create<StackedWarehouses>(
                Consts.STACKED_WAREHOUSES,
                "Stacked Warehouses",
                "Combines multiple sheds and warehoses into one card. Put further ones on top to add them to the pile.",
                10,
                CardType.Structures,
                building: true
            )
        };

        public static readonly Idea[] Ideas = new Idea[]
        {
            new Idea(
                Consts.FOOD_WAREHOUSE,
                BlueprintGroup.Building,
                new List<Subprint>
                {
                    new Subprint
                    {
                        RequiredCards = new[]
                        {
                            Consts.IRON_BAR,
                            Consts.IRON_BAR,
                            Consts.BRICK,
                            Consts.BRICK,
                            Consts.MAGIC_DUST,
                            Consts.STEW,
                            Consts.ANY_VILL,
                            Consts.ANY_VILL,
                        },
                        ResultCard = Consts.FOOD_WAREHOUSE,
                        Time = 15f,
                        StatusTerm = "Building a Food Warehouse",
                    },
                    new Subprint
                    {
                        RequiredCards = new[]
                        {
                            Consts.IRON_BAR,
                            Consts.IRON_BAR,
                            Consts.BRICK,
                            Consts.BRICK,
                            Consts.MAGIC_DUST,
                            Consts.SEAFOOD_STEW,
                            Consts.ANY_VILL,
                            Consts.ANY_VILL,
                        },
                        ResultCard = Consts.FOOD_WAREHOUSE,
                        Time = 15f,
                        StatusTerm = "Building a Food Warehouse",
                    }
                }
            ),
            new Idea(
                Consts.MAGIC_POUCH,
                BlueprintGroup.Building,
                new List<Subprint>
                {
                    new Subprint
                    {
                        RequiredCards = new[]
                        {
                            Consts.FABRIC,
                            Consts.ROPE,
                            Consts.MAGIC_DUST,
                            Consts.GOLD_BAR,
                            Consts.ANY_VILL,
                        },
                        ResultCard = Consts.MAGIC_POUCH,
                        Time = 15f,
                        StatusTerm = "Making a magic pouch",
                    }
                }
            ),
            new Idea(
                Consts.STACKED_WAREHOUSES,
                BlueprintGroup.Building,
                new List<Subprint>
                {
                    new Subprint
                    {
                        RequiredCards = new[]
                        {
                            Consts.IRON_BAR,
                            Consts.IRON_BAR,
                            Consts.STONE,
                            Consts.STONE,
                            Consts.MAGIC_DUST,
                            Consts.ANY_VILL,
                        },
                        ResultCard = Consts.STACKED_WAREHOUSES,
                        Time = 15f,
                        StatusTerm = "Stacking warehouses",
                    }
                }
            )
        };
    }
}
