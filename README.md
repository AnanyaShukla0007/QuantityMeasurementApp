# **Quantity Measurement Application**

## **1. Overview**

The **Quantity Measurement Application** is a **constraint-driven, generic domain system** that models real-world measurement units with mathematical correctness and architectural rigor.

It evolves across **UC1–UC15** as a **continuous system refinement**, not isolated features:

```text
Equality → Normalization → Abstraction → Arithmetic → Centralization → Constraint Enforcement → API Exposure
```

---

## **2. Core Capabilities**

* Multi-domain support: **Length, Weight, Volume, Temperature**
* Generic, type-safe design using `Quantity<U>`
* Centralized arithmetic engine (DRY-compliant)
* Capability-based operation restriction (Temperature)
* Clean Architecture layering
* REST API with DTO isolation
* Middleware-based exception handling

---

## **Deep Evolution of UC1–UC18 (System Thinking Level)**

---

## **UC1 — Same Unit Equality (Baseline Constraint System)**

**Problem Being Solved:**
Basic comparison of two quantities with identical units.

**Naive Risk:**
Direct floating-point comparison leads to precision errors.

**Solution Introduced:**

* Epsilon-based comparison:

  ```text
  |a - b| < ε
  ```

**Why This Matters:**
This establishes the **first invariant**:

> Equality must be mathematically tolerant, not bit-exact.

**Failure Without This:**

* `0.1 + 0.2 != 0.3` type inconsistencies
* Unreliable domain behavior

---

## **UC2 — Cross-Unit Equality (Normalization Strategy)**

**Problem:**
Different units within same domain:

```text
12 inches == 1 foot
```

**Core Insight:**
Comparison must occur in a **common canonical space**.

**Solution: Base Unit Normalization**

```text
Value → Convert to Base → Compare
```

**Architectural Impact:**

* Introduces **implicit canonical representation**
* Decouples equality from unit representation

**Hidden Trade-off:**

* Requires every unit to define:

  * `ToBase()`
  * `FromBase()`

---

## **UC3–UC4 — Domain Scaling (Weight, Volume)**

**Problem:**
System must scale across domains without rewriting logic.

**Bad Approach:**
Duplicate logic per category.

**Chosen Approach:**
Reuse normalization strategy across domains.

**Insight:**

> Domain logic is identical; only conversion factors differ.

**Architectural Weakness Introduced:**

* Still **type-fragmented**
* No abstraction yet → duplication risk grows

---

## **UC5 — Cross-Category Protection (Domain Safety Barrier)**

**Critical Failure Identified:**

```text
1 foot == 1 kilogram  → logically invalid
```

**Solution: Runtime Type Validation**

```text
if (unitA.Category != unitB.Category) throw
```

**Architectural Upgrade:**

* Introduces **domain boundaries**
* Prevents semantic corruption

**Important Distinction:**

* This is not type safety (compile-time)
* This is **runtime semantic validation**

---

## **UC6–UC7 — Conversion System (Bidirectional Transformation)**

**Problem:**
System must not only compare, but **transform values across units**.

**Solution: Two-Step Conversion**

```text
Input → Base Unit → Target Unit
```

**Why Not Direct Conversion?**

* Avoids N² conversion mappings
* Reduces complexity to O(n)

**Example:**
Instead of:

```text
Feet → Inches
Feet → Yards
Feet → cm
...
```

You only define:

```text
Unit → Base
Base → Unit
```

**Architectural Benefit:**

* Scales linearly with number of units

---

## **UC8 — Generic Abstraction (System Unification)**

**Problem:**
Code duplication across Length, Weight, Volume.

**Solution:**

```csharp
Quantity<U> where U : IMeasurable
```

**Impact:**

* Collapses multiple domain implementations into one
* Enforces **compile-time constraints**

**Key Shift:**

> Behavior is now defined by the unit, not the container.

**Risk Introduced:**

* Incorrect unit implementation can break entire system

---

## **UC9 — Addition (First Composite Operation)**

**Problem:**
Arithmetic must respect unit semantics.

**Incorrect Approach:**

```text
10 ft + 12 in → invalid if directly added
```

**Correct Approach:**

```text
Convert both → Base → Add
```

**Flow:**

```text
A → Base
B → Base
Result = A + B
Return in unit
```

**New Invariant Introduced:**

> Arithmetic must operate only in canonical form.

---

## **UC10 — Target-Controlled Output**

**Problem:**
Users want output in specific units.

**Solution:**
Allow explicit target unit override.

**Impact:**

* Adds flexibility without breaking internal consistency

**Trade-off:**

* Slight API complexity increase

---

## **UC11 — Division (Dimensional Collapse)**

**Conceptual Shift:**
Division produces **dimensionless scalar**.

```text
10 ft / 5 ft = 2
```

**Key Rules:**

* Same category required
* Base normalization required

**New Risk Introduced:**

* Division by zero

**Mitigation:**

```text
if (denominator == 0) throw ArithmeticException
```

---

## **UC12 — Subtraction (Non-Commutative Behavior)**

**Key Property:**

```text
A - B ≠ B - A
```

**Implementation Mirrors Addition:**

* Same pipeline
* Different operation logic

**Insight:**

> Arithmetic operations differ only in operator, not pipeline.

---

## **UC13 — DRY Refactor (Architectural Correction)**

**Critical Problem:**
Each arithmetic method duplicates:

* Validation
* Conversion
* Operation
* Conversion back

**Result:**

* High bug surface
* Low maintainability

---

### **Solution: Centralized Arithmetic Engine**

```text
Validate → Normalize → Execute → Convert
```

---

### **Enum Strategy Introduction**

```csharp
ADD, SUBTRACT, DIVIDE
```

**Why Enum Strategy Works:**

* Encapsulates behavior
* Eliminates branching
* Enables extension

**Architectural Leap:**

> Behavior is now data-driven, not control-flow driven.

---

## **UC14 — Temperature (Breaking Linear Assumptions)**

**Core Problem:**
Temperature is **non-linear**.

```text
F ≠ k × C
```

**Implication:**
Arithmetic becomes **physically meaningless**.

Example:

```text
30°C + 20°C ≠ 50°C (invalid in physics)
```

---

### **Solution: Capability-Based Restriction**

Instead of changing architecture, **extend interface behavior**.

```csharp
SupportsArithmetic()
ValidateOperationSupport()
```

**Temperature Overrides:**

```text
Throws NotSupportedException
```

---

### **Architectural Significance**

* Introduces **behavioral polymorphism**
* System now respects **real-world physics constraints**

---

## **UC15 — Layered Architecture (System Externalization)**

**Problem:**
System exists only as domain logic — not usable externally.

---

### **Solution: Introduce Layered Architecture**

```text
API → Business → Service → Domain
```

---

### **Why Each Layer Exists**

#### **API Layer**

* Handles HTTP
* No business logic

#### **Business Layer**

* Validates input
* Orchestrates flow

#### **Service Layer**

* Executes operations

#### **Domain Layer**

* Core logic (already built UC1–UC14)

---

### **Critical Addition: DTO Isolation**

**Problem Without DTO:**

* Domain leakage to API

**Solution:**

```json
{
  "Success": true,
  "Data": {},
  "Message": ""
}
```

---

### **Middleware Introduction**

Replaces scattered try-catch with:

```text
Central Exception Handling

Mathematical Modeling → Generic Abstraction → Arithmetic Engine → Constraint System → API Layer → Persistence → Security
```

Final system capabilities:

* Domain-correct measurement engine
* Clean Architecture layering
* REST API exposure
* Database persistence (MS SQL)
* Secure authentication (JWT + OAuth2)
* Extensible and production-ready

---

## **UC16 — Database Integration (Persistence Layer)**

## **Problem**

System is stateless → no persistence.

## **Solution**

Introduce **database-backed storage layer using ADO.NET**.

---

## **Architecture Extension**

```text
API → Business → Service → Repository → Database
```

---

## **Key Concepts Implemented**

### **1. ADO.NET Integration**

* SqlConnection
* SqlCommand
* SqlDataReader

### **2. Parameterized Queries (Critical)**

```sql
INSERT INTO Quantities(Value, Unit, Category)
VALUES(@value, @unit, @category)
```

**Why:**

* Prevent SQL Injection
* Improve execution plan reuse

---

### **3. Connection Pooling**

* Managed by .NET automatically
* Reduces DB connection overhead

---

### **4. Transaction Management**

```text
BeginTransaction → Execute → Commit / Rollback
```

---

### **5. Database Schema Design**

```sql
Quantities
---------
Id (PK)
Value (FLOAT)
Unit (VARCHAR)
Category (VARCHAR)
CreatedAt (DATETIME)
```

---

### **6. Separation of Concerns**

* Repository Layer introduced
* Domain logic remains untouched

---

### **7. Configuration Management**

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=...;Database=...;"
}
```

---

### **8. Exception Hierarchy**

| Exception                 | Meaning           |
| ------------------------- | ----------------- |
| SqlException              | DB failure        |
| InvalidOperationException | Connection misuse |

---

### **9. Testing Strategy**

* Mock repositories
* No direct DB dependency in tests

---

## **Risk You Solved**

Without this:

* System is not production usable

---

## **UC17 — Advanced Backend System (.NET Core + ORM)**

## **Problem**

Manual SQL + ADO.NET = verbose, error-prone.

---

## **Solution**

Introduce **ASP.NET Core + Entity Framework (ORM)**

---

## **Key Upgrades**

### **1. ASP.NET Core Web API**

* Controllers
* Routing
* HTTP pipeline

---

### **2. Entity Framework Core**

```csharp
public class AppDbContext : DbContext
```

**Features:**

* LINQ queries
* Automatic mapping
* Change tracking

---

### **3. Migrations**

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

---

### **4. LINQ to Entities**

```csharp
context.Quantities.Where(q => q.Category == "Length")
```

---

### **5. CQRS Pattern (Optional Enhancement)**

* Read and write separation
* Improves scalability

---

### **6. Logging (NLog)**

* Structured logging
* Error tracing

---

### **7. API Documentation**

* Swagger integration

---

### **8. Testing Stack**

* MSTest
* Postman (API testing)

---

### **9. Filters & Middleware**

* Request validation
* Exception handling

---

### **10. Distributed Readiness**

* Stateless APIs
* Horizontal scaling possible

---

## **Risk Eliminated**

* Raw SQL complexity
* Tight coupling to DB logic

---

## **UC18 — Authentication & Security Layer**

## **Problem**

System is open → no identity, no protection.

---

## **Solution**

Introduce **JWT + OAuth2-based authentication**

---

## **Security Architecture**

```text
Client → Auth Server → JWT → API → Validation
```

---

## **Key Implementations**

### **1. JWT Authentication**

* Token-based stateless auth
* Claims-based identity

```text
Header.Payload.Signature
```

---

### **2. OAuth 2.0 (Google Auth)**

* External identity provider
* Secure login without password storage

---

### **3. Password Security**

* Hashing (e.g., BCrypt)
* No plaintext storage

---

### **4. API Security**

* Authorization headers
* Role-based access (optional)

---

### **5. Encryption**

* Sensitive data protection

---

## **Failure Without This**

* Open API → security breach risk

---

# **Folder Structure (Final)**

```text
QuantityMeasurementApp/
│
├── QuantityMeasurementApp.API/
│   ├── Controllers/
│   │   ├── AuthController.cs
│   │   └── QuantityMeasurementApiController.cs
│   │
│   ├── Middleware/
│   │   └── GlobalExceptionMiddleware.cs
│   │
│   ├── Services/
│   │   ├── JwtServices.cs
│   │   ├── PasswordServices.cs
│   │   ├── EncryptionService.cs
│   │   └── RedisCacheService.cs
│   │
│   ├── appsettings.json
│   ├── appsettings.Development.json
│   └── Program.cs
│
├── QuantityMeasurementApp.Business/
│   ├── Interface/
│   │   └── IQuantityMeasurementService.cs
│   │
│   ├── Services/
│   │   └── QuantityMeasurementServiceImpl.cs
│   │
│   ├── Validators/
│   │   └── RequestValidator.cs
│   │
│   └── Exceptions/
│       ├── DatabaseException.cs
│       └── QuantityMeasurementException.cs
│
├── QuantityMeasurementApp.Model/
│   ├── DTOs/
│   │   ├── AuthDTO.cs
│   │   ├── ConversionRequest.cs
│   │   ├── BinaryQuantityRequest.cs
│   │   ├── DivisionResponse.cs
│   │   ├── QuantityDTO.cs
│   │   └── QuantityResponse.cs
│   │
│   ├── Entities/
│   │   ├── QuantityMeasurementEntity.cs
│   │   └── UserEntity.cs
│   │
│   ├── Enums/
│   │   ├── MeasurementCategory.cs
│   │   └── OperationType.cs
│   │
│   └── Models/
│       └── QuantityModel.cs
│
├── QuantityMeasurementApp.Repository/
│   ├── Data/
│   │   └── QuantityDbContext.cs
│   │
│   ├── Database/
│   │   ├── DatabaseConfig.cs
│   │   └── QuantityMeasurementDB.sql
│   │
│   ├── Interface/
│   │   └── IQuantityMeasurementRepository.cs
│   │
│   ├── Services/
│   │   ├── QuantityMeasurementEfRepository.cs
│   │   ├── QuantityMeasurementDatabaseRepository.cs
│   │   ├── QuantityMeasurementCacheRepository.cs
│   │   └── UserRepository.cs
│   │
│   └── Migrations/
│       ├── InitialCreate.cs
│       ├── AddUsers.cs
│       └── QuantityDbContextModelSnapshot.cs
│
├── QuantityMeasurementApp.Controller/   (Console Layer - Legacy Support)
│   ├── Controllers/
│   │   └── QuantityMeasurementController.cs
│   │
│   ├── Factory/
│   │   └── ServiceFactory.cs
│   │
│   ├── Helpers/
│   │   └── ConsoleHelper.cs
│   │
│   ├── Interface/
│   │   ├── IMenu.cs
│   │   └── IQuantityMeasurementController.cs
│   │
│   └── Menu/
│       ├── MainMenu.cs
│       └── MeasurementMenu.cs
│
├── QuantityMeasurementApp/   (Core Domain - UC1–UC14)
│   ├── Interface/
│   │   └── IMeasurable.cs
│   │
│   ├── Models/
│   │   ├── Quantity.cs
│   │   ├── QuantityLength.cs
│   │   ├── QuantityWeight.cs
│   │   └── QuantityVolume.cs
│   │
│   ├── Units/
│   │   ├── LengthUnit.cs
│   │   ├── WeightUnit.cs
│   │   ├── VolumeUnit.cs
│   │   └── TemperatureUnit.cs
│   │
│   ├── Services/
│   │   ├── GenericQuantityService.cs
│   │   ├── QuantityMeasurementService.cs
│   │   ├── TemperatureMeasurementService.cs
│   │   └── WeightMeasurementService.cs
│   │
│   └── Utilities/
│       └── RoundingHelper.cs
│
├── QuantityMeasurementApp.Tests/
│   └── QuantityMeasurementServiceTests.cs
│
├── QuantityMeasurementApp.sln
└── README.md
```

---

# **9. Final Capability Matrix**

| Layer      | Responsibility |
| ---------- | -------------- |
| Domain     | Core logic     |
| Service    | Execution      |
| Business   | Validation     |
| API        | Transport      |
| Repository | Persistence    |
| Security   | Authentication |

---

# **Final Statement**

```text
This is not a unit converter.

It is a constraint-driven, layered, persistent, and secure system
demonstrating full-stack backend engineering evolution.
```

```text
Equality → Normalization → Abstraction → Arithmetic → Centralization → Constraint Enforcement → System Exposure
```

## UC structure

UC1–5   → Equality + Domain Safety  
UC6–8   → Conversion + Generics  
UC9–13  → Arithmetic + DRY Centralization  
UC14    → Capability Constraints (Temperature)  
UC15    → Layered API Architecture  
UC16    → Database Integration  
UC17    → Advanced Backend + ORM + Testing  
UC18    → Security + Authentication  

**Constraints enforced:**

* Same category
* Finite values
* Division safety


## **API Design**

### **Base Route**

```text
/api
```

### **Endpoints**

| Domain      | Endpoint                 | Method |
| ----------- | ------------------------ | ------ |
| Length      | /api/length/convert      | POST   |
| Length      | /api/length/add          | POST   |
| Weight      | /api/weight/equal        | POST   |
| Volume      | /api/volume/divide       | POST   |
| Temperature | /api/temperature/convert | POST   |

---

## **Sample Request**

```json
POST /api/length/convert

{
  "Value": 10,
  "FromUnit": "Feet",
  "ToUnit": "Inches"
}
```

---

## **Capability Matrix**

| Category    | Equality | Conversion | Add | Subtract | Divide |
| ----------- | -------- | ---------- | --- | -------- | ------ |
| Length      | Yes      | Yes        | Yes | Yes      | Yes    |
| Weight      | Yes      | Yes        | Yes | Yes      | Yes    |
| Volume      | Yes      | Yes        | Yes | Yes      | Yes    |
| Temperature | Yes      | Yes        | No  | No       | No     |

---

## ** Key Design Guarantees**

* Immutability (no mutation)
* Centralized arithmetic logic
* Domain constraint enforcement
* Compile-time + runtime safety
* Backward compatibility (UC1–UC15)

---

## ** References**

* Microsoft Docs — ASP.NET Core Web API
* Microsoft Docs — Dependency Injection
* Microsoft Docs — Middleware Pipeline
* Clean Architecture — Robert C. Martin
* SOLID Principles — Object-Oriented Design
* SI Unit Standards (Physics)
* Temperature Conversion (Thermodynamics Fundamentals)

---

## ** Final Statement**

```text
This system is not a unit converter.

It is a constraint-aware domain model that evolves into a layered, production-grade architecture
while preserving mathematical correctness and behavioral integrity.
```
