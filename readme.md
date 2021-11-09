# Introduction

Draft project to simplify database fixtures management by using raw SQL files.

It's manual database management with raw SQL files.

No ORM libraries. No complex behavior.

Written quickly in two evenings as MVP for the proof of concept, so definitely there are things to improve, refactor, change there.

## Usage examples

TBD

## ToDo

* [x] Database versioning
   - [x] DatabaseVersion table (auto-creation if not exists).
   - [x] Default versioning strategy: semantic (MAJOR.MINOR.PATCH) kept in sql file names prefix.
   - [x] Custom versioning providers.
     - [x] SemVer
     - [x] Incremental
   - [x] The guard for files, that don't contain the correct version pattern.
   - [x] The guard for files, that are not applied and contain versions older than the latest one. Detect any SQL files added accidentally.
   - [x] Order SQL files by the version.
* [ ] Create a database automatically:
   - [ ] By default without auto-creation
   - [ ] The option to set the auto-creation flag.
* [x] Apply only SQL files that are not applied already.
   - [x] SQL files executer, that works with every SQL content (see [SMO](https://stackoverflow.com/a/40830/14163658)).
* [ ] Better errors handling
* [ ] Snapshots:
   - [ ] detected by the SQL files pattern *-snapshot.sql.
   - [ ] support backup & restore binary snapshots.
* [ ] DatabaseFixture.Testing extension to re-create the whole database from scratch.
* [ ] Different adtabase providers:
   - [x] Microsoft SQL Server
   - [ ] MySQL
   - [ ] PostgreSQL
* [ ] Automation testing with Docker
* [ ] Marketing:
  * [ ] logo
  * [ ] docs/ website
* [ ] Nuget
* [ ] Make it an open-source project
* [ ] Start some extensions:
    - [ ] Nuke.Build
    - [ ] xUnit, NUnit, and more fixtures
    - [ ] Testing: create database once, and the backup and restore it before every unit test
    - [ ] Testing: simultaneous integration tests by different databases auto-creation