# Domain Driven Design

## Core Principles of DDD

1. Focus on the Core Domain
   Build software around the business problem, not the technology.

Identify and isolate the most valuable part of the domain — the Core Domain — where the business gains a competitive advantage.

2. Collaborate with Domain Experts
   Developers and domain experts work together to create a shared understanding of the domain.

Use a Ubiquitous Language (see below) to avoid ambiguity and bridge the communication gap.

3. Ubiquitous Language
   Develop a common language shared by both developers and domain experts.

This language should be used in:

Code (class names, method names, etc.)

Conversations

Documentation

Example: Instead of calling it a DataRow, call it an InvoiceItem if that’s what it is in the business.

4. Model the Domain (Rich Domain Model)
   The domain model represents business concepts, rules, and behaviors.

Use Entities, Value Objects, Aggregates, and Domain Events to model the problem domain accurately.

5. Layered Architecture
   Typical DDD layers:

Presentation Layer – UI or API

Application Layer – Orchestrates use cases

Domain Layer – Business logic (Entities, Aggregates, Events)

Infrastructure Layer – Persistence, messaging, etc.

6. Entities and Value Objects
   Entity: Has a unique identity (e.g., Customer, Order).

Value Object: Immutable, no identity (e.g., Money, Address).

7. Aggregates and Aggregate Roots
   Aggregate: A cluster of related domain objects treated as a single unit for data changes.

Aggregate Root: The entry point for interacting with an Aggregate.

Example: An Order is an aggregate root; it manages OrderItems.

8. Domain Events
   Used to express something important that happened in the domain.

Promotes decoupling of side effects and reactive behavior.

Example: OrderShippedEvent triggers notification and inventory update.

9. Repositories
   Abstracts access to Aggregates.

Acts like an in-memory collection for aggregates (IOrderRepository).

10. Bounded Context
    A logical boundary within which a specific domain model applies.

Helps split large systems into autonomous subsystems.

Each context can have its own:

Ubiquitous Language

Entities

Models

Example: ShippingContext vs. BillingContext

11. Anti-Corruption Layer (ACL)
    Prevents foreign models or legacy systems from corrupting your domain model.

Use translators or adapters to integrate external systems.

12. Strategic Design
    Focuses on high-level decisions:

Bounded Contexts

Team boundaries

Context Mapping (how contexts interact)
