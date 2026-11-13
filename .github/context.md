# CompoundWatch – project context

## Language
- **Everything in the repository is English**: code, identifiers, comments, commit messages, README, ADRs, `CONTEXT.md` files, skills and plans.
- **Communication with the owner (chat, questions, review findings) is in Dutch**, but keep established English stack and industry terms as they are (for example "rich domain model", "anemic model", "dependency injection", "migration", "idempotent", "dead-letter queue", "N+1"). Never invent Dutch translations of technical terms; this also helps the owner learn the vocabulary used on the job.

## Goal
Hobby project to broaden full-stack knowledge, with emphasis on the backend and a stack that is relevant on the job market. A stock tracker with a watchlist, statistics and price alerts. Not investment advice.

## Stack
- Backend: ASP.NET Core (.NET 10), EF Core
- Database: PostgreSQL 17
- Messaging: RabbitMQ
- Mail (dev): Mailpit
- Frontend: Next.js (later, in `web/`)

## Structure
- `src/Api` – REST API
- `src/Worker` – background jobs and consumers
- `src/Domain` – entities and domain logic (no dependency on infrastructure)
- `src/Infrastructure` – database and external integrations
- `docker-compose.yml` – local services (Postgres, RabbitMQ :15672, Mailpit :8025)

## Phases
1. Foundation: solution, Compose stack, `/health` (done)
2. Domain and database with EF Core (in progress: domain entities done; DbContext, migration and integration tests open)
3. Later phases: see the owner's plan (not in the repo)

## Conventions
- Commits: Conventional Commits (`feat:`, `fix:`, `chore:`), English, imperative.
- Dependencies: Api, Worker and Infrastructure may use Domain; Domain references nothing.
- Every main folder (`src/*`, later `web/`, and any new top-level domain folder or project) has a `CONTEXT.md` explaining what is in it, how it works and its standards. Create it together with the folder; keep it current.
- Keep changes small and verify with `dotnet build` and tests.
- No secrets in the repo; local dev credentials live only in `docker-compose.yml`.
- **Never push personal, work-related or secret information**: no credentials, tokens, keys, `.env` files, e-mail addresses, home paths, employer or work-project details, or code copied from work repositories. Skills and docs must not contain such data either. `cw-commit` runs a mandatory check on every commit.

## Workflow (skills in `.github/skills/`)
`cw-flow` orchestrates: `cw-plan` (grill-me, ADR candidates) → `cw-build` ⇄ `cw-review` (loops until no findings or the owner says continue; easy findings are fixed directly, hard ones discussed) → `cw-document` (ADRs in `docs/adr/`, `CONTEXT.md` per project) → `cw-commit`. Stack checklist: `dotnet-standards`. Plans live in `.plans/` (gitignored).

## Learning
Primary goal of the project: the owner learns the core principles of the stack. Progress is tracked in `LEARNING.md` (competency matrix with levels 0–4, only raised with evidence). Each flow ends with `cw-learn`. Prefer the "I build" mode for concepts the owner has not mastered yet.

## Git
- Remote: `origin` over SSH using the private SSH alias (see `~/.ssh/config`, not in the repo).
- Local git identity is private; use no work details in this project.
- Main branch: `master`.

## Known issues
- None.
