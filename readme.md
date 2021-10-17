# Introduction

Draft project to simplify database fixtures management by using raw SQL files.
It's manual database management with raw SQL files.
No ORM libraries and complex behavior.

## ToDo

1. [ ] Database versioning
   - [ ] DatabaseVersion table (auto-creation if not exists).
   - [ ] Default versoning strategy: semantic (MAJOR.MINOR.PATCH) kept in sql file names prefix.
   - [ ] Custom versioning providers.
   - [ ] The guard for files, that don't contain the correct version pattern.
   - [ ] The guard for files, that are not applied and contain versions older than the latest one. Detect any SQL files added accidentally.
   - [ ] Order SQL files by the version.
2. [ ] Create a database automatically:
   - [ ] By default without auto-creation
   - [ ] The option to set the auto-creation flag.
3. [ ] Apply only SQL files that are not applied already.
   - [x] SQL files executer, that works with every SQL content (see [SMO](https://stackoverflow.com/a/40830/14163658)).
4. [ ] Snapshots: detected by the SQL files pattern *-snapshot.sql.
5. [ ] DatabaseFixture.Testing extension to re-create the whole database from scratch.
6. [ ] Different adtabase providers:
   - [ ] Microsoft SQL Server
   - [ ] MySQL
   - [ ] PostgreSQL
7. [ ] Automation testing with Docker
8. [ ] Marketing (logo, docs, website, etc.)
9. [ ] Nuget
10. [ ] Make it an open-source project
11. [ ] Start some extensions:
    -  [ ] Nuke.Build
    -  [ ] xUnit, NUnit, and more fixtures