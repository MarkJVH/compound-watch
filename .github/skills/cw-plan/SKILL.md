---
name: cw-plan
description: 'Grill-me planning phase: researches the codebase, interviews the user one question at a time, proposes ADRs for hard decisions and ends with an approved plan. Does not implement. USE WHEN: starting a feature or phase, stress-testing a design, or the user says "grill me".'
---

# Plan

Output: an approved plan in `.plans/<kebab-desc>.md` (gitignored, never committed) plus zero or more proposed ADRs. This skill implements nothing.

## 1. Research before asking
Read `.github/context.md`, the `CONTEXT.md` of every project the work touches, and `docs/adr/`. Find facts yourself (code, config, existing patterns). Only ask the user for decisions.

## 2. Trivial? Take the light path
Trivial = one obvious change without a design choice (typo, config value, small dependency bump). Skip the interview, write a short plan, go to step 5. When in doubt, it is not trivial.

## 3. Interview (grill-me)
Map the work as a design tree: each decision branches into the decisions that depend on it. Ask **one question at a time**, never a batch. Pick the next question whose prerequisites are settled, number it and give a recommended answer. Use the `ask_user` tool when available:

```
❓ **Q1** - <title>: <question, with options>
➡️ <recommended answer>
```

Answers can unblock, change or remove later questions. Done when no branch is open and nothing is silently assumed. Typical branches for this stack: data model and constraints, API contract, sync vs async (RabbitMQ), failure and retry behaviour, idempotency, time and currency handling, testing approach.

## 4. ADR candidates
Propose an ADR only when a decision is **hard to reverse**, **surprising without context** and **a real trade-off**. Note it in the plan under "ADR candidates"; `cw-document` writes it after approval. Do not write ADRs here.

## 5. Learning
After the interview, read `LEARNING.md`. For each plan step name the concepts it exercises and ask the owner (one question) which learning mode applies: **AI builds** or **I build** (AI hints only). Recommend "I build" for concepts at level 0–2 that the owner wants to master. Record the mode per step in the plan.

## 6. Write the plan and get approval

```markdown
Status: plan

# <title>

## Goal
## Decisions
- <decision> (ADR candidate: yes/no)
## Out of scope
## Steps
1. <behaviour change> — concepts: <matrix rows> — mode: AI builds | I build — verified by: <test or check>
## Context files to update
- <CONTEXT.md files likely affected>
```

Show it and ask for approval. Revise until approved. Do not start implementing.
