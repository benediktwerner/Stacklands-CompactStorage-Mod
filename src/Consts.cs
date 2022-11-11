namespace CompactStorage
{
    static class Consts
    {
        public const string PREFIX = "compactstorage.";
        public const string IDEA = "_blueprint";

        public const string FOOD_WAREHOUSE = PREFIX + "food_warehouse";
        public const string MAGIC_POUCH = PREFIX + "magic_pouch";
        public const string STACKED_WAREHOUSES = PREFIX + "stacked_warehouses";

        public const string FLINT = "flint";
        public const string STONE = "stone";
        public const string STICK = "stick";
        public const string ROPE = "rope";
        public const string CHARCOAL = "charcoal";
        public const string IRON_BAR = "iron_bar";
        public const string GOLD_BAR = "gold_bar";
        public const string GLASS = "glass";
        public const string BRICK = "brick";
        public const string PLANK = "plank";
        public const string COIN = "gold";
        public const string SHELL = "shell";
        public const string JUNGLE = "jungle";
        public const string ANY_VILL = "any_villager";
        public const string SHED = "shed";
        public const string WAREHOSUE = "warehouse";
        public const string STEW = "stew";
        public const string SEAFOOD_STEW = "seafood_stew";
        public const string MAGIC_DUST = "magic_dust";
        public const string FABRIC = "fabric";

        public const string MAINLAND = "main";
        public const string ISLAND = "island";

        public static string Idea(string id)
        {
            return id + IDEA;
        }
    }
}
