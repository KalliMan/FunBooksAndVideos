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







