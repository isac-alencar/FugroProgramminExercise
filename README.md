# Fugro Programming Exercise

![Build](https://img.shields.io/badge/build-passing-brightgreen)
![Tests](https://img.shields.io/badge/tests-passing-success)
![Language](https://img.shields.io/badge/language-C%23-blue)

This repository contains a **technical programming exercise** developed in **C# (.NET)** as part of a software engineering evaluation for Fugro.

The objective of this exercise is to demonstrate:
- Software design and code organization
- Object-oriented programming principles
- Geometric reasoning
- Testability and code correctness

---

## Scope and Objectives

The solution focuses on **geometric computations** and **polyline processing**, including:

- Representation of geometric primitives (points, line segments)
- Algorithms operating on polylines
- Separation of concerns between domain logic and application logic
- Automated unit testing to validate correctness

The codebase prioritizes **clarity**, **maintainability**, and **deterministic behavior**, which are essential qualities in engineering-oriented software systems.

---

## Solution Structure

```plaintext
FugroProgramminExercise/
├── Geometry/                     # Core geometry domain (points, math utilities)
├── PolylineChallenge/            # Polyline-related algorithms and file processing
├── UnitTests/                    # Automated unit tests
├── .gitignore
├── FugroProgrammingExercise.sln  # .NET solution file
└── README.md
```

---

## Project Responsibilities

* **Geometry**: Contains reusable geometric primitives and mathematical operations, independent of application logic.
* **PolylineChallenge**: Implements the main challenge logic, including reading point data and performing operations on polylines.
* **UnitTests**: Ensures functional correctness and validates edge cases using automated tests.

---

## Technology Stack

* **Language**: C#
* **Framework**: .NET
* **Testing Framework**: MSTest