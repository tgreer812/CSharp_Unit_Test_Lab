# Quick Reading — Unit-Test Frameworks at a Glance

|               | MSTest | xUnit.net | NUnit |
|---------------|--------|----------|-------|
| **Origin**    | Built-in Visual Studio team | OSS, ex‑ASP.NET team | OSS, inspired by JUnit |
| **Core Attribute** | `[TestMethod]` | `[Fact]` | `[Test]` |
| **Parameterized** | `[DataTestMethod]` + `[DataRow]` / `[DynamicData]` | `[Theory]` + `[InlineData]` / `[MemberData]` | `[TestCase]` / `[TestCaseSource]` |
| **Setup / Teardown** | `[TestInitialize]` / `[TestCleanup]` | constructor / `IDisposable` | `[SetUp]` / `[TearDown]` |
| **Philosophy** | Attribute-oriented, tight VS integration | "Code-like tests", minimal magic | Familiar JUnit style |

We focus on MSTest because the existing codebase already uses it. Equivalent concepts in xUnit and NUnit are noted where relevant throughout the lab.
