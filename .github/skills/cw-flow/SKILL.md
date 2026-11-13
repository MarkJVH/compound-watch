---
name: cw-flow
description: 'Orchestrates the full CompoundWatch workflow: plan (grill-me) -> build <-> review loop -> document -> learn -> commit. USE WHEN: starting a feature, fix or phase end to end, or the user says "start flow" or "nieuwe feature".'
---

# Flow

Run the phases in order. Each phase is its own skill; follow it exactly. Talk to the user in Dutch, keeping English technical terms untranslated (see `.github/context.md`).

```
cw-plan -> [ cw-build <-> cw-review ]* -> cw-document -> cw-learn -> cw-commit
```

The owner's main goal is learning the stack's core principles. `cw-plan` sets a learning mode per step; `cw-learn` runs before every commit and updates `LEARNING.md`.

## 1. Plan: `cw-plan`
Grill-me interview, ADR candidates, approved plan. Do not continue without approval.

## 2. Build and review loop: `cw-build` then `cw-review`
Repeat until **one** of these holds:
- `cw-review` reports no findings (and all checks are green), or
- the user says to continue to the commit phase.

Rules for the loop:
- `cw-review` fixes easy findings itself and re-verifies. Hard findings (design choice, behaviour change, trade-off, migration, public API change) go to the user first, one at a time with a recommendation.
- After fixes, run `cw-build` verification again, then `cw-review` again. Never declare the loop finished from a review that ran before the last change.
- Stop the loop when the same finding returns twice; ask the user.

## 3. Document: `cw-document`
Runs once the loop ends, before any commit. Creates or updates ADRs and `CONTEXT.md` files.

## 4. Learn: `cw-learn`
Runs after `cw-document` and before every commit, so `LEARNING.md` is updated in the same flow and lands in the `docs:` commit.

## 5. Commit: `cw-commit`
Groups commits and, only after the user says yes, commits and pushes.

## State
The plan lives in `.plans/<kebab-desc>.md` (gitignored). Keep a short "Status" line at the top: `plan | build | review | document | learn | commit`.
