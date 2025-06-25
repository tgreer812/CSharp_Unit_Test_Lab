# Part 7 – Refactor for Testability

Replace any uses of `DateTime.UtcNow` with an injected `IClock` interface so that time-based logic such as Black Friday pricing can be tested deterministically.
