using System.Linq;

namespace CompactStorage
{
    class FoodWarehouse : Food
    {
        public const int VALUE = 5;

        public override bool CanHaveCard(CardData otherCard) =>
            otherCard.Id == Id || (otherCard is Food && GetWarehouseWithSpace() != null);

        public int MaxFood;

        private FoodWarehouse GetWarehouseWithSpace() =>
            this.MyGameCard
                .GetAllCardsInStack()
                .FirstOrDefault(card => card.CardData is FoodWarehouse w && w.FoodValue < w.MaxFood)
                ?.CardData as FoodWarehouse;

        public override void UpdateCard()
        {
            Value = FoodValue + VALUE;
            if (MyGameCard.Parent == null || MyGameCard.Parent.CardData is not FoodWarehouse)
            {
                foreach (var card in MyGameCard.GetChildCards())
                {
                    if (card.CardData is Food food && card.CardData is not FoodWarehouse)
                    {
                        var space = GetWarehouseWithSpace();
                        if (space == null)
                            break;
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
