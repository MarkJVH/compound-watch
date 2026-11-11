# tests/Domain.Tests

## What
Unit tests for `src/Domain`. No database, no Docker, no I/O; they must stay fast.

## Standards
- xUnit. `[Fact]` for one scenario, `[Theory]` + `[InlineData]` when only the input value changes.
- Test names: `Method_Situation_ExpectedResult`.
- Assert expected first: `Assert.Equal(expected, actual)`.
- Invalid input: `Assert.Throws<T>` needs the exact exception type; use `Assert.ThrowsAny<ArgumentException>` when `null` (`ArgumentNullException`) and empty (`ArgumentException`) are both valid outcomes.
- Check that a test protects something: removing the rule it covers must turn it red.

## Run
```bash
dotnet test tests/Domain.Tests
```
