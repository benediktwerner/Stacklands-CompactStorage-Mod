namespace CompactStorage
{
    class StackedWarehouses : CardData
    {
        public override bool CanHaveCard(CardData otherCard) =>
            otherCard.Id == Consts.WAREHOSUE || otherCard.Id == Consts.SHED;

        [ExtraData(Consts.MAGIC_POUCH + ".value")]
        public int StoredValue;

        [ExtraData(Consts.STACKED_WAREHOUSES + ".count")]
        public int Count = 26;

        public void Start()
        {
            if (StoredValue == 0)
            {
                StoredValue = Value;
            }
            else
            {
                Value = StoredValue;
            }
        }

        public override void UpdateCard()
        {
            MyGameCard.SpecialValue = Count;
            if (MyGameCard.Parent == null)
            {
                foreach (var card in MyGameCard.GetChildCards())
                {
                    var cardData = card.CardData;
                    if (cardData.Id == Consts.WAREHOSUE || cardData.Id == Consts.SHED)
                    {
                        Count += cardData.Id == Consts.WAREHOSUE ? 13 : 3;
                        Value += cardData.Value;
                        StoredValue = Value;
                        cardData.MyGameCard.DestroyCard(true, true);
                    }
                }
            }
            base.UpdateCard();
        }
    }
}
