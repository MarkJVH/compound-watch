---
name: cw-review
description: 'Reviews current changes on four axes (Spec, Standards, Security, Slop). Fixes easy findings itself and discusses hard ones with the user. Part of the build/review loop in cw-flow. USE WHEN: after cw-build, reviewing a branch or current changes.'
---

# Review

Talk to the user in Dutch. Keep the axes separate: a change can pass one and fail another.

## 1. Gather
- Read `.github/context.md`, the `CONTEXT.md` of touched projects, `dotnet-standards`.
- Diff includes uncommitted work: `git diff "$(git merge-base origin/master HEAD)"` (or `git diff HEAD` before the first push) plus untracked files from `git status --short`.
- Spec: the plan in `.plans/<kebab-desc>.md` if present.

## 2. Axes

**Spec**: missing or partial requirements, scope creep (behaviour nobody asked for), implemented differently than agreed. Quote the plan line. Skip when no plan.

**Standards**: apply the `dotnet-standards` checklist. Search all callers and contracts of what changed. Check missing or weakened tests and unrelated changes. A new main folder or project without a `CONTEXT.md` is a finding (easy: create it).

**Security**: secrets or personal/work data in code, docs, compose or tests (see the sensitive-data check in `cw-commit`), SQL built from strings, missing input validation at API boundaries, missing authz, sensitive data in logs, unsafe deserialization, outbound calls without timeouts.

**Slop**: lines the task did not need.
- An input's shape re-checked downstream instead of once at its boundary
- An option, parameter, interface or layer without a real caller today (premature abstraction, repository over EF Core without reason)
- `try/catch` or fallback that swallows a fault the code cannot repair
- Vague names (`data`, `info`, `util`, `helper`, `manager`, `Service` for everything), type-repeating suffixes
- Comments restating the code, dead paths, unused usings, leftovers of replaced code, speculative config

## 3. Triage every finding

**Easy** = unambiguous, local, no behaviour change or design choice: dead code, unused usings, naming, formatting, missing `CancellationToken` pass-through, missing `AsNoTracking` on read-only queries, obvious null/validation gaps matching existing patterns, small test gaps.
→ Fix it yourself, deletion first, one kind at a time. Re-run the touched tests after each pass; back out any cleanup that turns a check red.

**Hard** = needs a decision: behaviour or API contract change, data model or migration change, new dependency, concurrency/idempotency/retry semantics, trade-offs between options, anything you are less than sure about.
→ Do not change it. Present it to the user one finding at a time (`ask_user`): impact, options, recommended fix. Apply only what the user decides.

## 4. Validate
Run the `cw-build` verification commands again. Report each command with its result.

## 5. Output
Dutch, short. Sections:
- **Gedaan**: easy findings fixed (with `file:line`).
- **Voor overleg**: hard findings, ordered P1 must fix / P2 should fix / P3 nice to have, each with `file:line`, impact, recommended fix.
- **Geen bevindingen** when the review is clean; name any unrun checks.

If a finding should never happen again, add an English **Improvement suggestion** naming the target (`CONTEXT.md`, a skill) and wording; `cw-document` picks it up.

The loop in `cw-flow` continues (build, then review) until there are no findings or the user says to continue to the commit phase.
