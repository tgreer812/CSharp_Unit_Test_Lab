# Part 0 – Setup

Follow these commands to create the solution skeleton. They are reproduced here in case you wish to recreate the structure from scratch.

```bash
mkdir LabUnitTesting && cd LabUnitTesting

dotnet new sln -n LabUnitTesting
dotnet new classlib -n BusinessLogic
dotnet new mstest -n BusinessLogic.Tests

dotnet sln add BusinessLogic/ BusinessLogic.Tests/
dotnet add BusinessLogic.Tests reference BusinessLogic/
```

At this stage, the `BusinessLogic` project contains the `OrderService` class and the test project references it. Build the solution to ensure everything compiles.

Checkpoint: `git commit -m "scaffold order service"`
