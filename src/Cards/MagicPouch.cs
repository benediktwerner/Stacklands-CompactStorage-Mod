using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CompactStorage
{
    class MagicPouch : CardData
    {
        public override bool CanHaveCard(CardData otherCard) =>
            otherCard is MagicPouch || (!otherCard.IsBuilding && !Card.IsAlive(otherCard));

        public int MaxCards;

        [ExtraData(Consts.MAGIC_POUCH + ".value")]
        public int StoredValue;

        [ExtraData(Consts.MAGIC_POUCH + ".cards")]
        public string contentData = "";
        public List<string> content = new List<string>();

        public void Start()
        {
            if (!string.IsNullOrWhiteSpace(contentData))
            {
                content = new List<string>(
                    contentData.Split(',').Where(x => WorldManager.instance.GameDataLoader.idToCard.ContainsKey(x))
                );
                UpdateDescription();
            }
            if (StoredValue == 0)
            {
                StoredValue = Value;
            }
            else
            {
                Value = StoredValue;
            }
        }

        private MagicPouch GetPouchWithSpace() =>
            this.MyGameCard.GetAllCardsInStack()
                .FirstOrDefault(card => card.CardData is MagicPouch x && x.content.Count < x.MaxCards)
                ?.CardData as MagicPouch;

        public override void UpdateCard()
        {
            MyGameCard.SpecialValue = content.Count;
            if (
                MyGameCard.Parent == null
                || (MyGameCard.Parent.CardData is not MagicPouch && MyGameCard.Parent.Parent == null)
            )
            {
                foreach (var card in MyGameCard.GetChildCards())
                {
                    var cardData = card.CardData;
                    if (!cardData.IsBuilding && !Card.IsAlive(cardData) && cardData is not MagicPouch)
                    {
                        var space = GetPouchWithSpace();
                        if (space == null)
                        {
                            break;
                        }
                        space.Value += cardData.Value;
                        space.StoredValue = Value;
                        space.content.Add(cardData.Id);
                        space.contentData += "," + cardData.Id;
                        space.UpdateDescription();
                        card.DestroyCard(true, true);
                    }
                }
            }
            base.UpdateCard();
        }

        public override void Clicked()
        {
            if (content.Count > 0)
            {
                var last = content[content.Count - 1];
                content.RemoveAt(content.Count - 1);
                contentData = string.Join(",", content.ToArray());

                var card = WorldManager.instance.CreateCard(
                    base.transform.position + Vector3.up * 0.2f,
                    last,
                    checkAddToStack: false
                );
                Value -= card.Value;
                StoredValue = Value;
                WorldManager.instance.StackSend(card.MyGameCard, Vector3.zero);
                UpdateDescription();
            }
            base.Clicked();
        }

        public void UpdateDescription()
        {
            if (content.Count == 0)
            {
                descriptionOverride = null;
                return;
            }

            var counts = new Dictionary<string, int>();
            foreach (var id in content)
            {
                var name = WorldManager.instance.GameDataLoader.GetCardFromId(id).Name;
                counts.TryGetValue(name, out var count);
                counts[name] = count + 1;
            }
            var array = counts.Keys.ToArray();
            Array.Sort(array);
            for (var i = 0; i < array.Length; i++)
            {
                var c = counts[array[i]];
                if (c > 1)
                    array[i] += " x" + c;
            }

            descriptionOverride = "Currently holding:\n" + string.Join("\n", array) + "\n\n" + "Click to remove cards";
        }
    }
}
