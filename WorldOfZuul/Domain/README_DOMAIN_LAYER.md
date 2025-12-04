# Domain Layer - Architecture Compliance

## Overview
The Domain layer has been refactored to comply with proper 3-layer architecture principles.

## Key Principles Applied

### 1. **Independence from Other Layers**
- ? No dependencies on ConsoleUI layer
- ? No dependencies on Data layer
- ? Pure business logic only

### 2. **No I/O Operations**
- ? Removed all `Console.WriteLine()` and `Console.ReadLine()` calls
- ? Methods now return data instead of printing
- ? UI concerns moved to presentation layer

### 3. **Separation of Concerns**
- ? Each class has a single, well-defined responsibility
- ? Domain entities (Villager, Resources) are pure data models
- ? Business logic encapsulated in appropriate classes

## Changes Made

### Core Domain Classes

#### **Advisor.cs**
- Removed Console I/O operations
- Removed dependencies on `Game` class (ConsoleUI)
- Now returns formatted strings instead of printing
- Methods: `GetNextTip()`, `GetNextStat()`, `FormatResources()`, etc.

#### **DataHandler.cs**
- Removed dependency on `IDataInitializer` (Data layer)
- Now accepts pre-loaded data through constructor
- Implements business logic for finding rooms, villagers, and jobs
- Methods: `ChangeRoom()`, `FindRoomByName()`, `FindVillagerById()`, etc.

#### **Resources.cs**
- Pure domain entity representing game resources
- No external dependencies
- Clean property accessors

#### **Villager.cs**
- Pure domain entity
- Simple data model with Id and Name

### Room Classes

#### **Room.cs** (Base Class)
- Implements `IRoom` interface
- Removed Console operations
- Returns messages instead of printing: `GetEnterRoomMessage()`
- Added utility methods: `SetExit()`, `GetExit()`

#### **Farmland.cs**
- Removed Console and Game dependencies
- Methods return tuples with results and sustainability changes
- Business logic methods: `BuildFarmland()`, `CutForest()`, `PlantFarmland()`, `Farm()`
- All methods accept `Resources` as parameter

#### **Forest.cs**
- Removed Console and Game dependencies
- Methods return business logic results
- Methods: `CutTree()`, `PlantTree()`, `KillAnimal()`
- Returns sustainability impact calculations

#### **Lake.cs**
- Removed Console and Game dependencies
- Separated fishing logic from UI
- Methods: `StartFishing()`, `CompleteFishing()`, `FeedFish()`
- Returns wait times and results for UI to handle

#### **Village.cs**
- Removed Console dependencies
- Methods: `Feed()`, `Assign()`
- Uses DataHandler for business logic

#### **School.cs**
- Removed Console and Game dependencies
- Methods return educational content
- Methods: `GetAboutProject()`, `LearnSustainability()`

#### **Land.cs**
- Basic room implementation
- Follows Room base class pattern

### Job Classes

#### **Job.cs** (Base Class)
- Implements `IJob` interface
- Pure domain entity with business logic
- Properties: Id, Name, Description, Villagers
- Methods: `AddVillager()`, `Work()`

#### **Hunter.cs**
- Removed DataHandler dependency
- Returns work output instead of modifying state directly
- Method: `CalculateWorkOutput()` returns (foodGained, animalsKilled)

#### **Lumberjack.cs**
- Removed DataHandler dependency
- Returns work output instead of modifying state directly
- Method: `CalculateWorkOutput()` returns (woodGained, treesCut)

#### **Unemployed.cs**
- Simple job type for unassigned villagers
- Proper description added

### Command Handler

#### **Parser.cs**
- Pure domain logic for parsing commands
- No dependencies on other layers

#### **Command.cs**
- Simple value object representing a command
- Properties: Name, SecondWord, ThirdWord

#### **CommandWords.cs**
- Validates command names
- List of valid commands

### Interfaces

#### **IJob.cs**
- Defines contract for job types
- Properties and methods all jobs must implement

#### **IRoom.cs**
- Defines contract for room types
- Properties and methods all rooms must implement

## Architecture Benefits

1. **Testability**: Domain logic can now be unit tested without UI or data dependencies
2. **Maintainability**: Clear separation makes code easier to understand and modify
3. **Reusability**: Domain entities can be used in different contexts (Console, Web, Desktop)
4. **Flexibility**: UI can be changed without affecting business logic
5. **SOLID Principles**: Classes follow Single Responsibility and Dependency Inversion

## Usage Pattern

The Domain layer should now be used as follows:

```csharp
// UI Layer creates and initializes domain objects
var resources = new Resources();
var advisor = new Advisor();

// UI Layer calls domain methods and handles the results
var tipMessage = advisor.GetNextTip();
Console.WriteLine(tipMessage); // UI decides how to display

// Domain returns data, UI handles presentation
var (message, sustainabilityChange) = farmland.PlantFarmland(resources);
Console.WriteLine(message);
sustainabilityPoints += sustainabilityChange;
```

## Next Steps for Other Layers

- **ConsoleUI Layer**: Should handle all Console I/O and call Domain layer methods
- **Data Layer**: Should load data and pass it to Domain layer, not be called by Domain
