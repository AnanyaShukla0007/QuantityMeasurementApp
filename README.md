# QuantityMeasurementApp

## Project Overview

QuantityMeasurementApp is a scalable and extensible .NET-based application designed to validate equality between length measurements across multiple units.

The application evolved through multiple use cases:

* UC1: Feet equality validation

* UC2: Separate Feet and Inches comparison

* UC3: Refactoring to a generic QuantityLength class (DRY principle)

* UC4: Extended unit support (Yards and Centimeters)

  and continued..

The system began with separate unit implementations (Feet and Inches) and evolved into a generic, DRY-compliant design using a unified `QuantityLength` class and `LengthUnit` enum.

The application supports cross-unit comparison by converting all measurements into a common base unit (Feet) before evaluating equality.

This design ensures:

* Maintainability
* Scalability
* Mathematical accuracy
* Clean architecture principles

---

## Features

* Value-based equality comparison
* Cross-unit equality support

### Supported Units

* Feet

* Inches

* Yards

* Centimeters

* DRY-compliant generic design

* Centralized conversion logic

* Type-safe enum-based unit management

* Symmetric and transitive equality validation

* Tolerance-based floating-point comparison

* Unit test coverage with xUnit

* Clean architecture (Models, Services, Tests separation)

---

## Supported Conversions

* 1 Foot = 12 Inches
* 1 Yard = 3 Feet
* 1 Yard = 36 Inches
* 1 Centimeter = 0.393701 Inches
* All units convert internally to Feet as base unit

---

## Tech Stack

* .NET 8
* C#
* xUnit (Unit Testing Framework)
* Git (Version Control)
* Clean Architecture principles

---

## How to Run

### 1️⃣ Clone the Repository

```
git clone <your-repository-url>
cd QuantityMeasurementApp
```

### 2️⃣ Restore Dependencies

```
dotnet restore
```

### 3️⃣ Build the Project

```
dotnet build
```

### 4️⃣ Run the Application

```
dotnet run --project QuantityMeasurementApp
```

### 5️⃣ Run Unit Tests

```
dotnet test
```

---

## Branch Strategy

This project follows a structured Git branching model:

### main

* Production-ready code
* Stable and tested
* Contains finalized documentation

### develop

* Integration branch
* Aggregates completed features
* Tested before merging into main

### feature/<feature-name>

* Isolated feature development

Example:

* feature/UC1-FeetEquality
* feature/UC3-GenericQuantity
* feature/UC4-ExtendedUnits

Each feature branch is merged into develop, tested, and then promoted to main.

---

## Architectural Design

### DRY Principle

The refactoring from separate unit classes (Feet, Inches) to a generic `QuantityLength` class eliminates duplication and centralizes conversion logic.

### Single Responsibility Principle

* `QuantityLength` → Handles measurement logic
* `LengthUnit` → Manages supported units and conversion factors
* `QuantityMeasurementService` → Handles application-level validation
* `Program` → Entry point only

### Extensibility

Adding new units requires only:

* Extending the `LengthUnit` enum
* Defining the correct conversion factor

No modification to equality logic is required.

---

## Equality Contract Compliance

The implementation respects:

* Reflexive property
* Symmetric property
* Transitive property
* Consistency
* Null safety

All comparisons are value-based and type-safe.

---

## Scalability

The generic design enables:

* Addition of new measurement units
* Support for arithmetic operations (future enhancement)
* Extension to other measurement domains (mass, temperature, volume)

---

## Author

Ananya
