# Fun Books And Videos

Demonstration Application implemented using Clean Architecture.

## Overview
FunBooksAndVideosis an e-commerce shop where customers can view books and watch online videos. Users can have memberships for the book dub, the video club or for both clubs (premium).

## Technology Stack and Design Patterns:
  1. **Clean Architecture**
  2. **Swagger**
  3. **MediatoR & MediratoR behaviors**
  4. **AutoMapper**
  5. **FluentValidation**
  6. **EF**
  7. **SQLite**
  8. **Custom Exception Middleware**
  9. **Unit Tests**

## Usage

### Setup the Database
    
    PM> dotnet ef database update --project FunBooksAndVideos.Persistence --startup-project FunBooksAndVideos.Api  
Or from the PMC Set the Default Project to FunBooksAndVideos.Persistence and:

    PM> Update-database

### APIs
**swagger URL** https://localhost:7245/swagger/index.html <br>
**NOTE** all commands bellow are compatible with Windows Command Prompt. If this does not work for You, please use the swagger directly.

### User Account CRUD

#### Create User Account
   
    curl -k -X POST "https://localhost:7245/api/CustomerAccount" ^
       -H "Content-Type: application/json" ^
       -d "{\"name\":\"John Smith\"}"
    
#### Get ALL
    curl -k "https://localhost:7245/api/CustomerAccount"

#### Get By Specific ID
    curl -k "https://localhost:7245/api/CustomerAccount/1"    

#### Update Existing

    curl -k -X PUT "https://localhost:7245/api/CustomerAccount/1" ^
      -H "Content-Type: application/json" ^
      -d "{\"name\":\"John Smith Jr\"}"  

#### Delete

    curl -k -X DELETE "https://localhost:7245/api/CustomerAccount/1"    

### Product Get ALL

    curl -k "https://localhost:7245/api/Product"

### PurchaseOrder

#### Create New
    curl -k -X POST "https://localhost:7245/api/PurchaseOrder" ^
       -H "Content-Type: application/json" ^
       -d "{\"customerId\":1,\"items\":[{\"itemLineType\":0,\"productId\":1,\"membershipType\":0},{\"itemLineType\":3,\"productId\":0,\"membershipType\":1}]}"

    curl -k -X POST "https://localhost:7245/api/PurchaseOrder" ^
       -H "Content-Type: application/json" ^
       -d "{\"customerId\":3,\"items\":[{\"itemLineType\":0,\"productId\":1,\"membershipType\":0},{\"itemLineType\":3,\"productId\":0,\"membershipType\":1}, {\"itemLineType\":3,\"productId\":0,\"membershipType\":2}]}"

#### Get All
    curl -k "https://localhost:7245/api/PurchaseOrder"

#### Get By ID
    curl -k "https://localhost:7245/api/PurchaseOrder/1"

## Project Structure

### Core/FunBooksAndVideos.Domain
Domain project that contains all core entitties:

1. **CustomerAccount**
2. **Product**
3. **PurchaseOrder**
4. **PurchaseOrderItem**

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
1. Implementation of the DbContext & Entity Framework for SQLite DB Server.
2. Implements the Entity and UoF patterns
3. Responsible for defining the DB Schema & Migrations

### API/FunBooksAndVideos.Api
Defines Controllers for REST APIs

1. **CustomerAccountController**: GET/POST/PUT/DELETE requests for CustomerAccount
2. **ProductController**: GET access to all predefines Products from the system
3. **PurchaseOrderController**: POST request for create a new Order; GET access to created orders.


### Test/FunBooksAndVideos.Application.UnitTests
Defines Unit Tests to test the Application features.
