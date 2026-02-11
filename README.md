# Fun Books And Videos

Demonstration Application implemented using Clean Architecture.

## Overview
FunBooksAndVideosis an e-commerce shop where customers can view books and watch online videos. Users can have memberships for the book dub, the video club or for both clubs (premium).

## Technology Stack and Design Patterns
  **Clean Architecture**<br>
  **MediatoR & MediratoR behaviors** <br>
  **AutoMapper** <br>
  **FluentValidation** <br>
  **EF** <br>
  **SQLite** <br>
  **Custom Exception Middleware** <br>
  **Unit Tests** <br>

## Project Structure

**Core/FunBooksAndVideos.Domain**:       Domain project that contains all core entitties<br>
**Core/FunBooksAndVideos.Application**:  App project that contains all contracts, features (including the business rules)<br>
**Infrastructure/FunBooksAndVideos.Persistence**  Project responsible for Entity Persistance and UoF pattern <br>
**API/FunBooksAndVideos.Api**  Defines Controllers for API <br>
**Test/FunBooksAndVideos.Application.UnitTests** Defines Unit Tests to test the Application features.<br>

### Core/FunBooksAndVideos.Domain
Domain project that contains all core entitties:
**CustomerAccount**<br>
**Product**<br>
**PurchaseOrder**<br>
**PurchaseOrderItem**<br>

**ItemLineType**, **MembershipType**: Defines the item types and membership types.

**MembershipCatalog**, **MembershipInfo**: Helper static features to return all types of memberships (BookClub, VideoClub, PremiumClub)

### Core/FunBooksAndVideos.Application
App project that contains all contracts, features (including the business rules) and others.

#### Contracts
1. Contracts for Persistence. Defines the foundation of Entity and UoW pattern.
2. Contracts to drive MediatoR message behaviours:  **Queries**, **Commands** and **ITransactionalCommand**

#### Features
1. **Customer**. Defines full CRUD access
2. **Order**. Defines features for create order and read access of orders
3. **OrderProduct**. Get list with all availabe Products (books and videos)
4. **Purchase**. Defines the Business rules for processing orders.

#### Behaviors
Implementation of the Save and Transaction behavior patterns. E.g. Mediator open Behavior implementations for:
1. Save Db Changes for commands
2. Transaction Scope for all commands that requires multiple changes grouped in atomic operation.

#### Application related Exceptions
Custome Exceptions for validation errors, persistence problems etc...

#### Mapping Profiles
Custom Mapping Profiles for Automapper

### Infrastructure/FunBooksAndVideos.Persistence
Project responsible for Entity Persistance and UoF pattern.

### API/FunBooksAndVideos.Api
Defines Controllers for REST APIs

1. **CustomerAccountController**: GET/POST/PUT/DELETE requests for CustomerAccount
2. **ProductController**: GET access to all predefines Products from the system
3. **PurchaseOrderController**: POST request for create a new Order; GET access to created orders.
