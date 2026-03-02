# Quantity Measurement Application
### Progressive Evolution from Basic Equality to Capability-Constrained Generic Architecture
.NET 8 | C# | Generics | SOLID | DRY | ISP | Enum Strategy | Functional Interfaces

---

# 1. Executive Summary

The Quantity Measurement Application models real-world measurement systems through a strongly typed, generic, and extensible architecture.  

The system evolves across fourteen structured use cases (UC1–UC14), gradually introducing:

- Cross-unit equality logic
- Generic abstractions
- Arithmetic operations
- DRY refactoring
- Centralized arithmetic delegation
- Capability-based restrictions
- Interface evolution
- Selective arithmetic enforcement (Temperature domain)

The final architecture supports:

- Length
- Weight
- Volume
- Temperature (conversion + equality only)

while preserving backward compatibility across all prior use cases.

---

# 2. Architectural Goals

The project was designed to demonstrate:

1. Generic type-safe modeling of physical quantities.
2. Elimination of code duplication (DRY enforcement).
3. Clear separation of concerns.
4. Scalable arithmetic dispatch using enum-based strategy.
5. Interface evolution without breaking existing implementations.
6. Selective operation restriction using capability validation.
7. Mathematical correctness across measurement domains.
8. Compile-time category safety and runtime cross-category validation.

---

# 3. Core Architectural Components

## 3.1 IMeasurable Interface

Defines the minimal contract required by all measurable units.

Responsibilities:
- Convert value to base unit.
- Convert value from base unit.
- Provide unit name.
- Declare arithmetic capability.
- Validate operation support.

The interface evolved in UC14 to support selective arithmetic enforcement without breaking existing units.

---

## 3.2 Quantity<U> (Generic Immutable Wrapper)

```
public sealed class Quantity<U> where U : IMeasurable
```

Responsibilities:
- Store numeric value + unit.
- Normalize values to base unit.
- Implement equality comparison.
- Perform arithmetic via centralized helper.
- Delegate operation capability validation.
- Enforce cross-category protection.

Design properties:
- Immutable
- Epsilon-based floating comparison
- Single source of arithmetic truth
- Backward compatible

---

## 3.3 ArithmeticOperation Enum (UC13)

Introduced to eliminate duplication across arithmetic methods.

Operations:
- ADD
- SUBTRACT
- DIVIDE

Each constant encapsulates operation logic.

Benefits:
- Replaces if-else branching
- Enables enum-based dispatch
- Improves extensibility
- Enforces DRY principle

---

## 3.4 Unit Categories

### LengthUnit
- Feet
- Inches
- Yards
- Centimeters
- Base unit: Feet

### WeightUnit
- Kilogram
- Gram
- Pound
- Base unit: Kilogram

### VolumeUnit
- Litre
- Millilitre
- Gallon
- Base unit: Litre

### TemperatureUnit (UC14)
- Celsius (Base)
- Fahrenheit
- Kelvin

Implements non-linear conversion formulas.

---

# 4. Use Case Evolution (UC1–UC14)

---

# UC1 – Same Unit Equality

Objective:
Validate equality between quantities of identical unit.

Implementation:
Direct numeric comparison with epsilon tolerance.

---

# UC2 – Cross-Unit Equality (Length)

Objective:
Allow comparison across length units.

Implementation:
Convert both values to base unit before comparison.

---

# UC3 – Weight Equality

Introduced weight category with same base-unit normalization approach.

---

# UC4 – Volume Equality

Introduced volume category.

---

# UC5 – Cross-Category Protection

Prevent invalid comparison:

Example:
1 foot == 1 kilogram → false

Mechanism:
Runtime check using unit type.

---

# UC6 – Length Conversion

Introduced ConvertTo(targetUnit) method.

Logic:
1. Convert to base unit
2. Convert base to target unit

---

# UC7 – Weight & Volume Conversion

Extended conversion support to all categories.

---

# UC8 – Generic Abstraction

Refactored into:

```
Quantity<U> where U : IMeasurable
```

Eliminated duplication across categories.

---

# UC9 – Addition

Added add() method:

- Validate category
- Convert both operands to base
- Perform addition
- Convert back to desired unit

---

# UC10 – Addition with Target Unit

Overload allowing explicit target unit.

---

# UC11 – Division

Return dimensionless scalar.

Division logic:
- Convert both operands to base
- Divide base values
- Prevent division by zero

---

# UC12 – Subtraction

Mirrors addition structure.

Subtraction is:
- Non-commutative
- Supports explicit and implicit target units

---

# UC13 – Centralized Arithmetic Logic (DRY Refactor)

Problem:
Each arithmetic method duplicated:
- Null validation
- Category validation
- Base conversion
- Target conversion
- Finite number validation

Solution:
1. Extract validation into helper.
2. Extract base arithmetic into helper.
3. Use ArithmeticOperation enum.
4. Delegate public methods to helper.

Public API unchanged.

Benefits:
- Single source of truth
- Reduced bug surface
- Cleaner public methods
- Scalable for new operations

---

# UC14 – Temperature Domain Integration

## Architectural Challenge

Temperature is fundamentally different:

Valid:
- Equality
- Conversion

Invalid:
- Addition
- Subtraction
- Division

Because:
Absolute temperatures cannot be combined meaningfully.

---

# UC15 – Enterprise Layered Architecture, API Exposure & System Consolidation

UC15 represents the architectural culmination of the Quantity Measurement Application.

It transforms the system from a domain-centric console model into a fully layered, production-style, API-enabled application while preserving all prior use case guarantees (UC1–UC14).

UC15 validates:

* Layered architectural separation
* Transport abstraction
* Dependency injection orchestration
* Middleware-based exception governance
* DTO boundary protection
* Swagger-based external verification
* End-to-end execution integrity

---

#  Architectural Transformation

Before UC15:

```
Console → Service → Domain
```

After UC15:

```
API Layer
   ↓
Business Layer
   ↓
Service Layer
   ↓
Domain Layer
```

Each layer enforces strict responsibility boundaries.

---

#  API Layer Introduction

UC15 introduces an ASP.NET Core Web API project.

Responsibilities:

* Accept HTTP requests
* Bind DTOs
* Delegate to Business layer
* Return standardized JSON responses
* Configure middleware
* Register services via dependency injection
* Expose Swagger documentation

Key characteristics:

* Stateless
* Thin controllers
* No domain logic
* No arithmetic logic
* No conversion formulas

The API layer is purely orchestration and transport.

---

#  Business Layer Formalization

UC15 introduces a Business layer between API and Service.

Purpose:

* Enforce validation rules
* Orchestrate service calls
* Standardize response formatting
* Prevent direct API-to-Service coupling

Business interfaces:

```
ILengthBusiness
IWeightBusiness
IVolumeBusiness
ITemperatureBusiness
```

Each implementation depends only on service abstractions.

This enforces:

* Dependency Inversion Principle
* Layer isolation
* Replaceable service implementations

---

#  DTO Boundary Isolation

UC15 introduces request and response DTOs.

Request DTOs:

* ConvertRequestDto
* EqualityRequestDto
* ArithmeticRequestDto

Response DTO:

```
ApiResponseDto<T>
```

Structure:

```
{
  Success: bool,
  Data: T,
  Message: string
}
```

Purpose:

* Prevent domain leakage to transport layer
* Maintain response uniformity
* Enable frontend compatibility
* Centralize error contract

---

#  Global Exception Middleware

UC15 replaces scattered try-catch logic with centralized middleware.

Responsibilities:

* Catch unhandled exceptions
* Map exceptions to HTTP status codes
* Return standardized JSON error responses
* Prevent stack trace exposure

Exception Mapping:

| Exception Type        | HTTP Code |
| --------------------- | --------- |
| ArgumentException     | 400       |
| ArithmeticException   | 400       |
| NotSupportedException | 400       |
| Unknown Exception     | 500       |

This eliminates duplication and strengthens error consistency.

---

#  Dependency Injection Consolidation

UC15 centralizes service registration using extension methods.

Dependency direction:

```
API → Business → Service → Domain
```

No reverse dependency is allowed.

This guarantees:

* Compile-time boundary enforcement
* Layer integrity
* Testability

---

#  Swagger Integration

Swagger validates:

* Controller routing
* DTO binding
* Dependency resolution
* Middleware interception
* JSON formatting

Successful Swagger execution confirms:

* API operational integrity
* End-to-end system flow
* No layer violation

Swagger acts as a verification instrument for UC15.

---

#  End-to-End Execution Flow

Example: Length Conversion

1. HTTP POST `/api/length/convert`
2. Controller receives ConvertRequestDto
3. Controller calls ILengthBusiness.Convert()
4. Business validates request
5. Business calls ILengthService.Convert()
6. Service constructs Quantity<LengthUnit>
7. Domain conversion executes
8. Result returned upward
9. ApiResponseDto wrapped
10. Middleware handles exceptions
11. JSON response returned

This confirms complete pipeline integration.

---

#  Preservation of Domain Constraints

UC15 preserves all prior domain guarantees:

* Cross-category protection
* Arithmetic centralization
* Epsilon-based comparison
* Base-unit normalization
* Temperature arithmetic restriction

Temperature arithmetic remains blocked even when invoked via API.

Example:

Attempting temperature addition:

* Domain throws NotSupportedException
* Middleware intercepts
* API returns 400

No special API logic required.

Constraint propagation remains intact.

---


#  Architectural Strength Achieved in UC15

UC15 confirms:

* Clean Architecture layering
* Strict separation of concerns
* Interface-driven contracts
* Centralized error governance
* Capability-based domain enforcement
* DTO boundary isolation
* Scalable arithmetic dispatch
* No duplication across layers
* Backward compatibility across 15 use cases

---

#  Educational Concepts Demonstrated in UC15

UC15 adds demonstration of:

* ASP.NET Core Web API architecture
* Dependency Injection in .NET
* Middleware pipeline design
* DTO pattern
* HTTP status code mapping
* Multi-project solution structuring
* Layered system orchestration
* Transport abstraction
* Production-grade exception handling

---

#  Final System State

After UC15, the Quantity Measurement Application is:

* Domain-correct
* Architecturally layered
* Capability-constrained
* Transport-agnostic
* API-enabled
* Middleware-governed
* Swagger-validated
* Fully backward compatible


## Interface Evolution

IMeasurable gained:

```
bool SupportsArithmetic();
void ValidateOperationSupport(string operation);
```

Default implementation:
Allows arithmetic.

Temperature overrides:
Throws NotSupportedException.

---

## Temperature Conversion Formulas

Celsius Base:

Fahrenheit:
F = (C × 9/5) + 32

Celsius:
C = (F - 32) × 5/9

Kelvin:
K = C + 273.15

---

## Arithmetic Blocking

Inside Quantity arithmetic methods:

```
Unit.ValidateOperationSupport(operation);
```

If unit is TemperatureUnit:
Throws NotSupportedException.

---

## Behavioral Guarantee

Temperature:
- Fully compatible with equality and conversion.
- Impossible to use in arithmetic.
- Cross-category protected.
- Backward compatibility preserved.

---

# 5. Validation Strategy

Uniform validation across arithmetic:

- Null operand
- Null unit
- Cross-category check
- Finite numeric validation
- Division-by-zero check
- Operation support validation

All centralized.

---

#  Immutability

All operations return new Quantity<U>.
Original operands remain unchanged.

---

#  Error Handling Strategy

| Error Type | Exception |
|------------|------------|
| Null operand | ArgumentException |
| Cross-category | ArgumentException |
| Division by zero | ArithmeticException |
| Unsupported temperature arithmetic | NotSupportedException |

Consistent error semantics.

---

#  Mathematical Guarantees

- Addition and subtraction respect unit conversion.
- Division returns dimensionless scalar.
- Temperature conversion is non-linear but correct.
- Epsilon-based floating comparison.
- Symmetric equality.
- Transitive equality.
- Reflexive equality.

---

#  Final Capability Matrix

| Category | Equality | Conversion | Add | Subtract | Divide |
|----------|----------|------------|-----|----------|--------|
| Length   | Yes | Yes | Yes | Yes | Yes |
| Weight   | Yes | Yes | Yes | Yes | Yes |
| Volume   | Yes | Yes | Yes | Yes | Yes |
| Temperature | Yes | Yes | No | No | No |

---

#  Architectural Strengths

- Fully generic
- Extensible for new categories
- Enum-based operation dispatch
- Centralized arithmetic logic
- Interface segregation compliance
- Capability-based validation
- No behavioral regression
- Zero duplication in arithmetic
- Backward compatible

---

#  Educational Concepts Demonstrated

- Generics with type constraints
- Enum strategy pattern
- Lambda expressions
- Functional interfaces
- Default interface methods
- Interface evolution
- Capability-based design
- DRY refactoring
- Immutability
- Precision handling
- Non-linear transformation modeling
- Compile-time vs runtime validation
- Backward-compatible refactoring

---

 Conclusion

This system demonstrates progressive architectural refinement:

From:
Simple equality checks

To:
A scalable, constraint-aware, extensible measurement framework.

The architecture now reflects both mathematical correctness and software engineering rigor.


