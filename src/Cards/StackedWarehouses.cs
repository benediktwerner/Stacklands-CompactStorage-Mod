namespace CompactStorage
{
    class StackedWarehouses : CardData
    {
        public override bool CanHaveCard(CardData otherCard) =>
            otherCard.Id == Consts.WAREHOUSE || otherCard.Id == Consts.SHED || otherCard.Id == Consts.LIGHTHOUSE;

        [ExtraData(Consts.MAGIC_POUCH + ".value")]
        public int StoredValue;

        [ExtraData(Consts.STACKED_WAREHOUSES + ".count")]
        public int Count = 26;

        [ExtraData(Consts.STACKED_WAREHOUSES + ".lighthouse_count")]
        public int LighthouseCount = 0;

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
                    if (
                        cardData.Id == Consts.WAREHOUSE
                        || cardData.Id == Consts.SHED
                        || cardData.Id == Consts.LIGHTHOUSE
                    )
                    {
                        if (cardData.Id == Consts.LIGHTHOUSE)
                            LighthouseCount += 1;
                        Count += cardData.Id == Consts.SHED ? 3 : 13;
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
