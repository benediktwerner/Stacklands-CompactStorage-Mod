# Stacklands CompactStorage Mod

Store cards more compactly to reduce clutter and lag.

This adds three new cards:
- Food Warehouse: Stores 100 food points worth of food and food inside won't spoil.
- Magic Pouch: Stores any 30 cards
- Stacked Warehouses: Combines all sheds and warehouses into one (any warehouse or shed put on top gets absorbed)

The ideas for the cards can be found in the advanced ideas booster pack on the Mainland (Stacked Warehouses) and Island (Food Warehouse and Magic Pouch).

## Manual Installation
This mod requires BepInEx to work. BepInEx is a modding framework which allows multiple mods to be loaded.

1. Download and install BepInEx from the [Thunderstore](https://stacklands.thunderstore.io/package/BepInEx/BepInExPack_Stacklands/).
4. Download this mod and extract it into `BepInEx/plugins/`
5. Launch the game

## Development
1. Install BepInEx
2. This mod uses publicized game DLLs to get private members without reflection
   - Use https://github.com/CabbageCrow/AssemblyPublicizer for example to publicize `Stacklands/Stacklands_Data/Managed/GameScripts.dll` (just drag the DLL onto the publicizer exe)
   - This outputs to `Stacklands_Data\Managed\publicized_assemblies\GameScripts_publicized.dll` (if you use another publicizer, place the result there)
3. Compile the project. This copies the resulting DLL into `<GAME_PATH>/BepInEx/plugins/`.
   - Your `GAME_PATH` should automatically be detected. If it isn't, you can manually set it in the `.csproj` file.
   - If you're using VSCode, the `.vscode/tasks.json` file should make it so that you can just do `Run Build`/`Ctrl+Shift+B` to build.

## Links
- Github: https://github.com/benediktwerner/Stacklands-CompactStorage-Mod
- Thunderstore: https://stacklands.thunderstore.io/package/benediktwerner/CompactStorage

## Changelog

- v1.0.6:
  - Always prefer Food Warehouses when selecting food (like Hotpots) so you don't need to put them on Mess Halls
  - Don't allow placing stacks on Food Warehouses that don't fit (for better Golem Automation behavior when they are full)
- v1.0.5: Fix Food Warehouses not re-stacking left-over cards if full
- v1.0.4: Make it work with lighthouses
- v1.0.3: Update mod icon (Thanks to @lopidav for the art!)
- v1.0.2: Add card icons (Thanks a lot to @lopidav for making all the art!)
- v1.0.1: Fix card names not showing up for non-English languages
- v1.0: Initial release
