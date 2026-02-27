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

# 6. Immutability

All operations return new Quantity<U>.
Original operands remain unchanged.

---

# 7. Error Handling Strategy

| Error Type | Exception |
|------------|------------|
| Null operand | ArgumentException |
| Cross-category | ArgumentException |
| Division by zero | ArithmeticException |
| Unsupported temperature arithmetic | NotSupportedException |

Consistent error semantics.

---

# 8. Mathematical Guarantees

- Addition and subtraction respect unit conversion.
- Division returns dimensionless scalar.
- Temperature conversion is non-linear but correct.
- Epsilon-based floating comparison.
- Symmetric equality.
- Transitive equality.
- Reflexive equality.

---

# 9. Final Capability Matrix

| Category | Equality | Conversion | Add | Subtract | Divide |
|----------|----------|------------|-----|----------|--------|
| Length   | Yes | Yes | Yes | Yes | Yes |
| Weight   | Yes | Yes | Yes | Yes | Yes |
| Volume   | Yes | Yes | Yes | Yes | Yes |
| Temperature | Yes | Yes | No | No | No |

---

# 10. Folder Structure

```
QuantityMeasurementApp/
│
├── Interface/
│   └── IMeasurable.cs
│
├── Models/
│   ├── Quantity.cs
│   ├── LengthUnit.cs
│   ├── WeightUnit.cs
│   ├── VolumeUnit.cs
│   └── TemperatureUnit.cs
│
├── Services/
│   ├── GenericQuantityService.cs
│   ├── QuantityMeasurementService.cs
│   ├── WeightMeasurementService.cs
│   └── TemperatureMeasurementService.cs
│
├── Menu/
│   └── ConsoleMenu.cs
│
├── Utilities/
│   └── RoundingHelper.cs
│
└── Tests/
    ├── UC1–UC13 Tests
    └── UC14TemperatureTests.cs
```

---

# 11. Architectural Strengths

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

# 12. System Status

All UC1–UC13 tests pass unchanged.  
UC14 adds new capability without breaking prior logic.  
Architecture supports future expansion (e.g., Currency, Time, Pressure).

---

# 13. Educational Concepts Demonstrated

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

# 14. Conclusion

This system demonstrates progressive architectural refinement:

From:
Simple equality checks

To:
A scalable, constraint-aware, extensible measurement framework.

UC14 introduces domain realism by recognizing that not all measurable quantities support identical arithmetic semantics.

The architecture now reflects both mathematical correctness and software engineering rigor.
