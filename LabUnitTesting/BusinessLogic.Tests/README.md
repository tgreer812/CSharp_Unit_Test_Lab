# BusinessLogic.Tests

This project contains unit tests for the BusinessLogic component of the CSharp Unit Test Lab. The tests are written using the standard .NET testing framework and include code coverage analysis.

## Prerequisites

- .NET SDK (version compatible with the project)
- A code editor or IDE (Visual Studio, VS Code, etc.)

## Getting Started

### Basic Test Execution

To clean, build, and run all tests in sequence:

```bash
dotnet clean
dotnet build
dotnet test
```

**Command Breakdown:**
- `dotnet clean` - Removes all build artifacts and temporary files
- `dotnet build` - Compiles the project and its dependencies
- `dotnet test` - Discovers and runs all unit tests in the project

### Code Coverage Analysis

To run tests with comprehensive code coverage reporting:

```bash
dotnet test 
/p:CollectCoverage=true /p:CoverletOutputFormat="cobertura" /p:Threshold=80 /p:ThresholdType=line /p:CoverletOutput=./coverage/ --verbosity normal
```

**Coverage Parameters Explained:**
- `/p:CollectCoverage=true` - Enables code coverage collection during test execution
- `/p:CoverletOutputFormat="cobertura"` - Generates coverage report in Cobertura XML format (compatible with most CI/CD tools)
- `/p:Threshold=80` - Sets the minimum required code coverage to 80%
- `/p:ThresholdType=line` - Applies the threshold to line coverage (other options: branch, method)
- `/p:CoverletOutput=./coverage/` - Specifies the output directory for coverage reports
- `--verbosity normal` - Sets the output detail level for test execution

## Coverage Reports

After running the coverage command, you'll find the following files in the `./coverage/` directory:
- `coverage.cobertura.xml` - Machine-readable coverage data
- Coverage reports can be viewed in various tools or CI/CD pipelines

## Quality Gates

This project enforces a minimum of **80% line coverage**. Tests will fail if coverage falls below this threshold, ensuring code quality standards are maintained.

## Project Structure

```
BusinessLogic.Tests/
├── README.md           # This file
├── coverage/          # Generated coverage reports (git-ignored)
└── [test files]       # Unit test classes
```

## Best Practices

- Run `dotnet clean` before important builds to ensure a fresh compilation
- Monitor coverage reports to identify untested code paths
- Maintain the 80% coverage threshold to ensure comprehensive testing
- Use descriptive test method names that explain the scenario being tested