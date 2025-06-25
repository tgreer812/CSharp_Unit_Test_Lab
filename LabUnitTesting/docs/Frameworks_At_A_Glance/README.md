# Unit Testing Frameworks for .NET — At a Glance

Welcome to your ten-minute introduction to the major unit testing frameworks in the .NET ecosystem. This collection provides a comprehensive yet concise overview of the three most popular frameworks: MSTest, xUnit.net, and NUnit.

## What You'll Learn

This reading collection covers:

- **Framework Overviews**: History, philosophy, and core concepts for each framework
- **Code Examples**: Side-by-side examples showing how the same test looks in each framework
- **Design Philosophy Comparison**: Understanding the different approaches and when to use each
- **Quick Reference**: Essential attributes, patterns, and features

## Reading Guide

**Estimated reading time: 8-12 minutes**

Start with any framework that interests you, or follow this suggested order:

1. **[MSTest](MSTest.md)** - Microsoft's built-in testing framework
2. **[xUnit.net](xUnit.md)** - Modern, minimal testing framework
3. **[NUnit](NUnit.md)** - Feature-rich, JUnit-inspired framework
4. **[Design Philosophy Comparison](Comparison.md)** - Understanding the differences

## Quick Reference Table

|               | MSTest | xUnit.net | NUnit |
|---------------|--------|----------|-------|
| **Origin**    | Built-in Visual Studio team | OSS, ex‑ASP.NET team | OSS, inspired by JUnit |
| **Core Attribute** | `[TestMethod]` | `[Fact]` | `[Test]` |
| **Parameterized** | `[DataTestMethod]` + `[DataRow]` | `[Theory]` + `[InlineData]` | `[TestCase]` |
| **Setup / Teardown** | `[TestInitialize]` / `[TestCleanup]` | constructor / `IDisposable` | `[SetUp]` / `[TearDown]` |
| **Philosophy** | Attribute-oriented, tight VS integration | "Code-like tests", minimal magic | Familiar JUnit style |

## Why This Matters

Understanding the different testing frameworks helps you:
- Choose the right tool for your project
- Work effectively across different codebases
- Understand the trade-offs between approaches
- Write more maintainable tests

---

*Note: The examples in this lab focus on MSTest because the existing codebase already uses it. Equivalent concepts in xUnit and NUnit are noted where relevant throughout the lab.*