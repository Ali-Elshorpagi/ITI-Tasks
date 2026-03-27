# Lab06 — Events and Observer Architectures

## Lab Description

This lab applies the **C#** Events system to assemble a reactive publisher-subscriber architecture pattern.

## Topics Covered

- Separating logic natively using `event` combined with custom `EventArgs`.
- Building pure "reactive" architectures avoiding intensive loop polling systems mapping states naturally via delegates.
- Implementing an entity observer strategy (Ball changing state notifying listeners).

## Projects

| Project | Description |
|---------|-------------|
| **Task01** | Simulates a basic reactive football system. Whenever the `Ball` changes coordinates natively invoking its `.ChangePosition()`, multiple disparate endpoints (`Player`, `Referee`, `Audience`) implicitly hook onto the custom `BallPositionChanged` event responding immediately inside their own independent routines asynchronously. |
