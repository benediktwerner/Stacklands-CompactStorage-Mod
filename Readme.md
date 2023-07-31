# Stacklands CompactStorage Mod

Store cards more compactly to reduce clutter and lag.

This adds three new cards:

- Food Warehouse: Stores 100 food points worth of food and food inside won't spoil.
- Magic Pouch: Stores any 30 cards
- Stacked Warehouses: Combines all sheds and warehouses into one (any warehouse or shed put on top gets absorbed)

The ideas for the cards can be found in the advanced ideas booster pack on the Mainland (Stacked Warehouses) and Island (Food Warehouse and Magic Pouch).

## Development

- Build using `dotnet build`
- For release builds, add `-c Release`
- If you're using VSCode, the `.vscode/tasks.json` file allows building via `Run Build`/`Ctrl+Shift+B`

## Links

- Github: https://github.com/benediktwerner/Stacklands-CompactStorage-Mod
- Steam Workshop: https://steamcommunity.com/sharedfiles/filedetails/?id=3012122691

## Changelog

- v1.1.1: Show CompactStorage cards in the mod category in the Cardopedia
- v1.1.0: Steam Workshop Support
- v1.0.6:
  - Always prefer Food Warehouses when selecting food (like Hotpots) so you don't need to put them on Mess Halls
  - Don't allow placing stacks on Food Warehouses that don't fit (for better Golem Automation behavior when they are full)
- v1.0.5: Fix Food Warehouses not re-stacking left-over cards if full
- v1.0.4: Make it work with lighthouses
- v1.0.3: Update mod icon (Thanks to @lopidav for the art!)
- v1.0.2: Add card icons (Thanks a lot to @lopidav for making all the art!)
- v1.0.1: Fix card names not showing up for non-English languages
- v1.0: Initial release
