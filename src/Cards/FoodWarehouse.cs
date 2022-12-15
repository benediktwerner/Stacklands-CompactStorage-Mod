using System.Linq;

namespace CompactStorage
{
    class FoodWarehouse : Hotpot
    {
        public const int VALUE = 5;

        public override bool CanHaveCard(CardData otherCard) =>
            otherCard.Id == Id || FoodInStack(otherCard.MyGameCard) <= GetFoodSpace();

        public int MaxFood;

        private FoodWarehouse GetWarehouseWithSpace() =>
            this.MyGameCard
                .GetAllCardsInStack()
                .FirstOrDefault(card => card.CardData is FoodWarehouse w && w.FoodValue < w.MaxFood)
                ?.CardData as FoodWarehouse;

        private int GetFoodSpace() =>
            this.MyGameCard
                .GetAllCardsInStack()
                .Select(card => card.CardData is FoodWarehouse w ? w.MaxFood - w.FoodValue : 0)
                .Sum();

        private static int FoodInStack(GameCard card)
        {
            var total = 0;
            while (card != null)
            {
                if (card.CardData is Food f)
                    total += f.FoodValue;
                else
                    return int.MaxValue;
                card = card.Child;
            }
            return total;
        }

        public override void UpdateCard()
        {
            Value = FoodValue + VALUE;
            if (MyGameCard.Parent == null || MyGameCard.Parent.CardData is not FoodWarehouse)
            {
                foreach (var card in MyGameCard.GetChildCards())
                {
                    if (card.CardData is Food food && food.FoodValue > 0 && card.CardData is not FoodWarehouse)
                    {
                        var space = GetWarehouseWithSpace();
                        if (space == null)
                        {
                            break;
                        }
                        space.FoodValue += food.FoodValue;
                        card.Parent.SetChild(card.Child);
                        card.Child = null;
                        card.DestroyCard(true, true);
                    }
                }
            }
            base.UpdateCard();
        }
    }
}
