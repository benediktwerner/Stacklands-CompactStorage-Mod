using System;

namespace CompactStorage
{
    class Card
    {
        public readonly string Id;
        public readonly string Name;
        public readonly string Description;
        public readonly int Value;
        public readonly CardType CardType;
        public readonly Type ScriptType;
        public readonly bool IsBuilding;
        public readonly Action<CardData> Init;

        public Card(
            string id,
            string name,
            string description,
            int value,
            CardType cardType,
            Type scriptType = null,
            bool building = false,
            Action<CardData> init = null
        )
        {
            Id = id;
            Name = name;
            Description = description;
            Value = value;
            CardType = cardType;
            ScriptType = scriptType ?? typeof(CardData);
            IsBuilding = building;
            Init = init;
        }

        public static Card Create<T>(
            string id,
            string name,
            string description,
            int value,
            CardType cardType,
            bool building = false,
            Action<T> init = null
        ) where T : CardData
        {
            return new Card(
                id,
                name,
                description,
                value,
                cardType,
                typeof(T),
                building,
                init == null ? null : (c) => init((T)c)
            );
        }

        public static bool IsAlive(CardData card)
        {
            var typ = card.MyCardType;
            return typ == CardType.Fish || typ == CardType.Mobs || typ == CardType.Humans;
        }

        public static string Currency =>
            WorldManager.instance.CurrentBoard?.Id == Consts.ISLAND ? Consts.SHELL : Consts.COIN;
    }
}
