---
name: cw-learn
description: 'Learning phase after cw-commit: maps the session to stack concepts, asks the owner questions to verify understanding, updates the competency matrix in LEARNING.md based on evidence and proposes the next exercise. USE WHEN: a flow finished, or the user says "learn", "quiz me" or "update my progress".'
---

# Learn

Purpose: the owner must learn the core principles of the stack. Progress is measured in `LEARNING.md` and only moves with evidence. Talk to the user in Dutch; write `LEARNING.md` in English.

## 1. Map the session
Read `LEARNING.md` and the session's diff/commits. List the concepts touched (rows in the matrix; add rows for new concepts). Note who wrote what: the owner or the AI (check the learning mode in the plan).

## 2. Verify understanding
Ask 2–3 questions about what was just built, one at a time with `ask_user`, mixing:
- Explain: "Why does this consumer need to be idempotent?"
- Predict: "What happens if this message is delivered twice?"
- Trade-off: "Why a decimal column instead of double here?"

Do not give the answer before the owner replied. Judge the answer, then explain gaps briefly.

## 3. Update the matrix (evidence rule)
- Level 1: explained to the owner this session.
- Level 2: the owner wrote part of the change (point to commit/test).
- Level 3: the owner wrote it independently; the AI only reviewed.
- Level 4: correct answers in their own words, or an ADR they wrote.
- Never raise a level without evidence in the table. Lower it when the owner repeats a mistake on a "known" concept.
- Add a session log entry (built, concepts, what was hard, what the owner does themselves next time) and update "Last updated".
Show the proposed changes and write after the owner agrees.

## 4. Next exercise
Propose one small exercise for the weakest relevant concept, to be done without AI (for example "write the migration for X yourself", "add an integration test for Y"). Add it to the exercises list in `LEARNING.md`. Leave changes uncommitted or commit as `docs: update learning progress` after the owner says yes.

## Learning modes (set in `cw-plan`)
- **AI builds**: the AI implements; the owner reviews and answers questions.
- **I build**: the owner writes the code. The AI gives hints and questions, not solutions, until asked. `cw-review` still reviews.
