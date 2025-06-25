# Part 8 – Coverage Tools

Run tests with Coverlet to enforce 90% line coverage locally:

```bash
dotnet test /p:CollectCoverage=true \
             /p:CoverletOutputFormat="cobertura" \
             /p:Threshold=90 /p:ThresholdType=line
```

The command exits non-zero if coverage is below the threshold.
