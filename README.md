# Agreeable Park Tickets

This mod momentarily reduces park entrance fee
so that it does not exceed 85% of guest's cash
and to leave them with at least $20.  
The discount will be visible in the guest's Thoughts.

For example, if the park entrance fee is $80 and the guest only brought $70,
they'll be given a $30 discount so that they spend $50 and are left with $20.

This addresses two situations:
1. The guest doesn't have enough money to get in the park
2. The guest has just enough money to get in, but subsequently cannot afford anything inside the park.

Note that the mod will not _increase_ the fee or spare you of any other situations (like guests thinking that your park entrance fee is too much).

Inspired by one time when a friend did this by hand to let everyone enjoy our slightly absurd park.

## Is this cheating?
The discount goes out of your pocket, so you get _less_ money.

There might be occasional benefits to filling up a park with guests quicker,
but overall the purpose of mod is aesthetical - to not impose misery upon the guests.

## Setting Up

- Clone the repository
- Copy Parkitect DLLs from `Parkitect/Parkitect_Data/Managed` to `Libs/`
- Download [Harmony](https://github.com/pardeike/Harmony/releases/tag/v2.2.2.0) and copy `net472/0Harmony.dll` to `Libs/`

## Building

Open the Visual Studio project and compile. Post-build event should automatically copy the necessary files to the game's `Mods` sub-directory inside My Documents.

## Credits
- Mod by [YellowAfterlife](https://yal.cc).
