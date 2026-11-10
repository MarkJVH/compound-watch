---
name: cw-build
description: 'Implements an approved plan or a review finding with tests, and proves it with fresh build/test output. Never commits. USE WHEN: implementing a plan step, fix or review finding.'
---

# Build

Input: the plan in `.plans/<kebab-desc>.md`, a review finding to fix, or a direct request. Read the `CONTEXT.md` of every project you touch and apply `dotnet-standards`.

## 1. Implement per plan step
Check the step's learning mode in the plan. **I build**: do not write the code; give a short outline, hints and guiding questions, review what the owner writes, and give the full solution only when asked. **AI builds**: explain non-obvious choices briefly while implementing.
1. Follow existing patterns in the project; Domain stays free of infrastructure dependencies.
2. Add or update tests for the changed behaviour (logic, validation, mappers, handlers, endpoints). Use real Postgres via Testcontainers for data access, not the EF in-memory provider. Skip tests that only restate code.
3. Run them and fix until green.

## 2. Verify: evidence before claims
Never say "done", "fixed" or "passes" without command output from **this** turn.
- `dotnet build` (no new warnings; the known `NU1903` is documented)
- `dotnet test`
- `dotnet format --verify-no-changes`
- `git diff --check`
- EF migrations: the migration applies to a clean database (`docker compose up -d postgres`).
- Bug fix: prove the test catches the bug (revert fix: fails; restore: passes).
- If a check fails, fix it or report the actual state.

## 3. Do not commit
Leave changes in the working tree. `cw-commit` groups them.

## Output
Short summary: what changed, tests added, each command run with its result. Then hand over to `cw-review`.
