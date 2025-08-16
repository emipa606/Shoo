Below is the content for the `.github/copilot-instructions.md` file, tailored for the "Shoo! (Continued)" RimWorld mod project. 


# Copilot Instructions for Shoo! (Continued) RimWorld Mod

## Mod Overview and Purpose
*Shoo! (Continued)* is an updated version of the mod originally created by jamaicancastles, now supporting RimWorld version 1.5 as of update 3/16/24. The primary purpose of the mod is to introduce a humane method to manage wild animals that tend to wander into player-controlled colonies, potentially disrupting construction and agriculture. By adding a "Shoo" designator accessible through the Orders menu, players can command pawns with animal handling skills to guide wild animals out of the colony territory.

## Key Features and Systems
- **Shoo Designator:** Found in the Orders menu or when selecting a wild animal. Directs pawns to shoo animals away, helping to maintain colony order and safety.
- **Animal Behavior Adjustments:** Introduces a chance system based on animal size and taming difficulty. Wilder, larger animals are harder to influence, and failure can result in the need for a cooldown period before retrying.
- **Automatic Navigation:** Animals will navigate through available doors when being shooed. They are, however, blocked by fences unless a gate is provided, requiring strategic placement by players.
- **Dynamic Risk Assessment:** Incorporates a minor risk element where dangerous animals may attack if disturbed.
- **Game Compatibility:** Safe to add to existing games and designed to be removable, provided certain conditions are met regarding ongoing shoo instructions.

## Coding Patterns and Conventions
- **Class and File Organization:** Separate files host distinct classes for individual functionalities, such as `Designator_Shoo.cs`, `JobDriver_Shoo.cs`, and `WorkGiver_Shoo.cs`.
- **Naming Conventions:** PascalCase for class and method names, following C# conventions. Internal classes use `internal` modifier for encapsulation.

## XML Integration
- XML files should define designations and job drivers for the interactions. Ensure consistency in XML naming with those in C# definitions, e.g., references for job definitions or designations should align with the in-game registry.

## Harmony Patching
- **Harmony Version:** Make use of Harmony 2.0 for patching, as it is bundled with the mod. Avoid compatibility issues with Harmony 1.x by ensuring no similar mods are running concurrently.
- **Patch Usage:** Apply patches primarily to methods affecting animal behavior and GUI updates for designators, ensuring smooth integration with the base game.

## Suggestions for Copilot
- **Expand Methods:** Copilot can assist in detailing methods for specific functionalities, such as optimizing `FindShooFleeCell` for more efficient pathfinding based on pawn skills and animal behavior.
- **Utility Functions:** Develop utility functions for repetitive actions, like checking for available exits or calculating cooldown effects based on animal and pawn stats.
- **Error Handling:** Implement error handling routines around player actions, particularly in `JobDriver_Shoo` for scenarios involving dangerous animal interactions.
- **Testing Stubs:** Create testing scenarios for the mod's logic to assist in debugging and testing steps before releases.
- **Code Comments:** Use detailed inline comments to clarify complex logic, especially around the Harmony patches and job assignments.

For more detailed discussions, join the Ludeon forums or connect with other developers via linked discord channels and community resources.


This document is structured to provide a comprehensive guide for developers using GitHub Copilot in conjunction with this mod, promoting seamless development and mod maintenance.
