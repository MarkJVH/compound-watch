---
name: cw-document
description: 'Documentation phase before committing: writes ADRs for hard decisions that were made and creates or updates the CONTEXT.md files spread through the codebase. USE WHEN: the build/review loop is finished and before any commit, or the user says "remember this" or "document".'
---

# Document

Runs once after the build/review loop ends and always before `cw-commit`. Most changes need little here; capture only what saves real time later.

## 1. ADRs
Sources: "ADR candidates" in the plan, and decisions taken during the loop (review discussions with the user).

Write an ADR only when the decision is **hard to reverse**, **surprising without context** and **a real trade-off**.
- Location: `docs/adr/NNNN-kebab-title.md`, numbered one past the highest existing (check right before writing).
- Specific title describing the decision. Status `Accepted`; use `Superseded by NNNN` when replaced.
- Template:

```markdown
# NNNN. <decision>

Status: Accepted
Date: YYYY-MM-DD

## Context
<forces and constraints>

## Decision
<what we chose>

## Alternatives considered
<options and why not>

## Consequences
<what gets easier, what gets harder>
```

## 2. CONTEXT.md files
Each project or significant folder (`src/Api`, `src/Worker`, `src/Domain`, `src/Infrastructure`, later `web/`) has its own `CONTEXT.md`, next to the code it describes. The root context is `.github/context.md`.

**Mandatory**: every main folder has a `CONTEXT.md`. When the change adds a new project, a new top-level folder, or a new feature/domain folder inside one (for example `src/Domain/Watchlists/`), create its `CONTEXT.md` in the same change.

For every touched project:
- Missing: create it (what is in here, how it works, standards, how to run/test, gotchas). Keep it under ~60 lines.
- Existing: update stale facts; add one or two precise bullets for a convention the AI got wrong or did not know. Say what to do, not the story.
- Repo-wide facts (phase status, stack, conventions) go in `.github/context.md`; update the phase list when a phase finished. Update `README.md` when run instructions or structure changed.
- Also apply "Improvement suggestions" from `cw-review` to the relevant `CONTEXT.md` or skill.

## 3. Rules
- Search first; update instead of duplicating.
- Propose the exact wording and target file, and write after the user approves (batch the proposal in one message).
- Leave changes uncommitted; `cw-commit` puts them in a `docs:` commit.
