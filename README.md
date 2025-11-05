# WorldOfZuul

WorldOfZuul is a small text-based resource-management / educational console game written in C# (.NET 6). The game teaches simple sustainability concepts by having the player manage villagers, resources and actions that affect global sustainability points.

---

## Quick facts
- Language / TFM: C# 10 / .NET 6
- Project type: Console application
- Start point: instantiate `Game` and call `Game.Play()`

---

## Requirements
- .NET 6 SDK
- Visual Studio 2022 (recommended) or any editor that supports .NET 6

---

## Build & run

Visual Studio 2022
1. Open the solution.
2. In __Solution Explorer__, right-click the project containing `Game.cs` and choose __Set as Startup Project__.
3. Build: use __Build > Rebuild Solution__.
4. Run: press __F5__ (Start Debugging) or __Ctrl+F5__ (Start Without Debugging).

CLI
- From project folder:
  - `dotnet build`
  - `dotnet run`

---

## Gameplay overview
- The player starts in the first room added in `Game.CreateRooms()` (by default `Village`).
- The main loop is in `Game.Play()`: it reads input, parses it into a `Command` and executes either global commands or a room's `CommandList`.
- Global state includes `Resources`, villager list, current day, and `Game.SustainabilityPoints` (static).
- The game ends when the player quits or when `SustainabilityPoints` drops below zero (loss condition by implementation).

---

## Global commands (implemented in `Game.cs`)
- `ls <type>` — list items:
  - `ls v` — villagers
  - `ls r` — rooms
  - `ls j` — jobs (placeholder)
  - `ls i` — inventory / resources
- `cd <roomName>` — change room by `Room.ShortDescription` (case-insensitive)
- `sleep` — advance day
- `quit` — exit game
- `assign <villagerId> <jobId>` — assign a villager to a job (searches rooms with matching `Job.Id`)
- Resource / action commands:
  - `feed`, `hunt`, `farm`, `harvest`, `chop`, `plant`, `cook`
- `talk` — open the `Advisor` chat (type `help` while chatting to see advisor commands)

Room-specific commands are handled by each `Room` class through `CommandList(Command)`.

---

## Key files / structure
- `WorldOfZuul\Game.cs` — main loop, global commands, rooms and villager management.
- `WorldOfZuul\RoomType\` — room implementations (e.g., `Village.cs`, `Forest.cs`, `Lake.cs`, `School.cs`, `Farmland.cs`).
- `WorldOfZuul\Jobs\` — job-related classes (e.g., `Job`, `Unemployed`, `Advisor.cs`).
- `Command`, `Villager`, `Resources` — domain models used across the game.

Use backticked identifiers in code examples, e.g. `Game.SustainabilityPoints`, `Room.EnterRoom()`, `Room.CommandList(Command)`.

---

## Known issues & TODOs (actionable)
- Sustainability mismatch:
  - The game has a private `_sustainability` field and a static `Game.SustainabilityPoints`. Many action handlers change `_sustainability` while the loss check reads `SustainabilityPoints`. Consolidate to a single canonical property (recommend: `Game.SustainabilityPoints`) and update all modifiers to use it.
- `CreateVillagers()` assumes the start room has a `Job`; if null no villagers are created. Either ensure the start room has a job or create villagers independently.
- `RoomInfo` uses the case `"FarmlandMain"` while the room short description is `"Farmland"` — fix the mismatch to show room info correctly.
- `ls j` and many room methods (`Village.Feed`, `Lake.GoFishing`) are placeholders/empty and need implementation.
- Turn system is planned but not implemented (`MaxTurnPerDay` commented out).
- Input parsing uses `Convert.ToInt32` without robust validation — add safer parsing and range checks.

---

## How to extend
- Add a new room:
  - Create a new `Room` subclass in `RoomType\`.
  - Instantiate and add to `_rooms` in `Game.CreateRooms()`.
- Add a global command:
  - Add a `case` to the `switch` in `Game.Play()` for the command name or forward to rooms via `Room.CommandList`.
- Add or change jobs:
  - Implement a `Job` or subclass, set unique `Job.Id`, and attach to rooms via the `Room` constructor overload.

---

## Testing & debugging tips
- Use `ls i` during play to inspect resource values.
- If the game ends unexpectedly, check `Game.SustainabilityPoints`, `_currentDay` versus `MaxDay`, and places that modify resources.
- Add logging or `Console.WriteLine` statements in `Game.Play()` to trace command parsing and routing.

---

## Contribution
- Use standard git flow: feature branches, small PRs.
- Suggested branches: `fix/sustainability`, `feature/turn-system`, `feature/villager-behaviour`.

---

## License & contact
- No license file included in the repository. Add a `LICENSE` file if you want a specific open-source license.
- Repo remote: `https://github.com/dexterz-m/WorldOfZuul10` (origin)

---
