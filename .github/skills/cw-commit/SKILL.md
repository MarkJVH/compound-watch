---
name: cw-commit
description: 'Commit phase: groups finished, reviewed and documented changes into conventional commits and pushes only after the user says yes. USE WHEN: the document phase is done, or the user says "commit" or "push".'
---

# Commit

## 1. Preconditions
- `cw-document` has run on the current diff.
- `cw-build` verification passed on the current diff; re-run it if unsure. A failing check is unfinished work.
- Check `git config user.email` (repo-local) is the private address the owner set for this project, not a work address. Stop and tell the user otherwise.
- Run the sensitive-data check below on the staged diff before every commit.

## 2. Group the commits
- One concern per commit; tests go with the code they test.
- ADRs and `CONTEXT.md`/skill updates go in their own `docs:` commit.
- Conventional commits: `<type>(<project>): <summary>`, e.g. `feat(domain): add Instrument entity`. Types: `feat|fix|chore|docs|refactor|test`. English, imperative.
- Each commit should leave the solution building.
- Never commit `.plans/`, secrets or build output.

## Sensitive-data check (mandatory)
This is a public-style repo; nothing personal, work-related or secret may be committed. Review `git diff --cached` and the file list for:
- Secrets: passwords, tokens, API keys, connection strings with real credentials, private keys, `.env` files, certificates.
- Personal data: real names, e-mail addresses, phone numbers, home paths (`/Users/<name>/`), IP addresses, account IDs.
- Work data: employer names, internal hostnames, project or ticket names, code or text copied from work repositories.
- Local artefacts: `.plans/`, IDE settings, logs, `*.user`, database dumps.
Use `git grep -n -I -i -E "password|secret|token|apikey|api_key|BEGIN .*PRIVATE KEY|@[a-z0-9.-]+\.[a-z]{2,}"` as a first pass. Local dev credentials in `docker-compose.yml` are the only accepted exception. If anything matches, stop, show it to the owner and do not commit.
- Add the trailer `Co-authored-by: Copilot <223556219+Copilot@users.noreply.github.com>` to each commit message.

## 3. Ask once, then act
Show the commit groups and messages, then ask: "Commit and push?" (Dutch). Skip the question only when the user already answered it.

**Yes**: per group `git add -- <paths>` and `git commit`, then `git push` (remote `origin`, SSH alias `github-prive`).
**No**: print the commands ready to copy.

Work happens on `master` for this hobby project unless the user asks for a feature branch (`<type>/<kebab-desc>`).
