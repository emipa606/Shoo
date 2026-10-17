# Shoo! (Continued) GitHub Copilot Instructions

## Mod Overview and Purpose

Shoo! (Continued) is an updated version of the original Shoo! mod by jamaicancastle, now supporting RimWorld version 1.5. This mod adds a unique gameplay mechanic by introducing a designator that allows players to harmlessly "shoo" wild animals away from their base. The purpose of the mod is to provide players with a tool to manage wildlife interactions with their colony without causing harm to animals, especially during construction phases or when securing fields from potential pests.

## Key Features and Systems

- **Shoo Designator**: Added to the Orders menu, the Shoo designator enables pawns with the Handling skill to shoo animals away. It is also accessible when a wild animal is selected.
- **Animal Behavior Adjustments**: When animals are shooed, they will navigate through doors automatically. However, they remain blocked by fences unless a gate is available.
- **Difficulty Variations**: Larger and wilder animals possess a higher chance of resisting shooing attempts, influenced by the pawn's taming skill. Failed attempts lead to cooldowns, and there is a minor chance for dangerous animals to become aggressive.
- **Compatibility**: Safe to add or remove mid-game, given certain conditions regarding ongoing shooing actions and marked animals.

## Coding Patterns and Conventions

- **Naming Conventions**: Follow C# standard practices with PascalCase for class names and members, and camelCase for variables.
- **File Structure**: Organize classes by function, such as `Designator_Shoo.cs` for designator functionalities and `JobDriver_Shoo.cs` for associated job behaviors.
- **Commenting**: Provide descriptive comments for complex methods to aid understanding and maintainability.

## XML Integration

- **Patches.xml**: Utilizes XML for defining content patches, important for ensuring compatibility and integration with the core game.
- **Localization Support**: Translations of job and work giver definitions are managed through respective XML files to support multiple languages.

## Harmony Patching

- **Library Inclusion**: The mod includes the Harmony 2.0 patching library to modify existing game behavior without altering original game files.
- **Patching Methods**: Commonly used postfix methods, e.g., `Building_Door_PawnCanOpen`, ensure smooth integration of new behaviors and features.

## Suggestions for Copilot

- Assist in generating more Harmony patches by suggesting appropriate postfix or prefix methods.
- Aid in defining XML content easily by providing boilerplate XML structures or suggesting common property tags.
- Enhance efficiency in coding repeated C# patterns such as new job drivers or designators.
- Offer suggestions to debug compatibility issues across different mod configurations.

This file serves as a comprehensive guide for using GitHub Copilot effectively in the continued development of Shoo! (Continued). Through proper understanding of its features and systems combined with coherent coding practices, contributors can maintain and expand the mod confidently.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

