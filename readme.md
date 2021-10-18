# Introduction

Draft project to simplify database fixtures management by using raw SQL files.
It's manual database management with raw SQL files.
No ORM libraries and complex behavior.

## ToDo

1. [ ] Database versioning
   - [x] DatabaseVersion table (auto-creation if not exists).
   - [x] Default versoning strategy: semantic (MAJOR.MINOR.PATCH) kept in sql file names prefix.
   - [ ] Custom versioning providers.
   - [x] The guard for files, that don't contain the correct version pattern.
   - [ ] The guard for files, that are not applied and contain versions older than the latest one. Detect any SQL files added accidentally.
   - [x] Order SQL files by the version.
2. [ ] Create a database automatically:
   - [ ] By default without auto-creation
   - [ ] The option to set the auto-creation flag.
3. [x] Apply only SQL files that are not applied already.
   - [x] SQL files executer, that works with every SQL content (see [SMO](https://stackoverflow.com/a/40830/14163658)).
4. [ ] Better errors handling
5. [ ] Snapshots:
   - [ ] detected by the SQL files pattern *-snapshot.sql.
   - [ ] support backup & restore binary snapshots.
6. [ ] DatabaseFixture.Testing extension to re-create the whole database from scratch.
7. [ ] Different adtabase providers:
   - [x] Microsoft SQL Server
   - [ ] MySQL
   - [ ] PostgreSQL
8. [ ] Automation testing with Docker
9. [ ] Marketing (logo, docs, website, etc.)
10.  [ ] Nuget
11. [ ] Make it an open-source project
12. [ ] Start some extensions:
    - [ ] Nuke.Build
    - [ ] xUnit, NUnit, and more fixtures
    - [ ] Testing: create database once, and the backup and restore it before every unit test
    - [ ] Testing: simultaneous integration tests by different databases auto-creation