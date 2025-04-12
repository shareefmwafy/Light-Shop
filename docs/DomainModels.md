# Domain Models Documentation

This document describes the domain models used in the **Light-Shop E-Commerce Platform**. These models represent the core entities of the system and define the structure of the database and the business logic.

---
## Products
Represents an item available for sale in the Light-Shop platform.

| Property     | Type      | Description                                 |
|--------------|-----------|---------------------------------------------|
| Id           | int       | Unique identifier for the product           |
| Name         | string    | Name/title of the product                   |
| Description  | string    | Detailed product description                |
| Price        | decimal   | Original price of the product               |
| Discount     | decimal   | Discount amount or percentage (if any)      |
| MainImage    | string    | URL or path to the main image               |
| Quantity     | int       | Number of items available in stock          |
| Rate         | decimal   | Average user rating (e.g., 4.5 stars)       |
| Status       | string    | Availability status (e.g., Active/Inactive) |
| CategoryId   | int       | Foreign key referencing the Categories table|
| BrandId      | int       | Foreign key referencing the Brands table    |

---

## Brands

Represents the manufacturer or label associated with a product.

| Property     | Type      | Description                                   |
|--------------|-----------|-----------------------------------------------|
| Id           | int       | Unique identifier for the brand               |
| Name         | string    | Brand name                                    |
| Description  | string    | Description or details about the brand        |

🔗 One `Brand` can be associated with multiple `Products`.

---

## Categories

Represents a classification or grouping for products in the shop.

| Property     | Type      | Description                                   |
|--------------|-----------|-----------------------------------------------|
| Id           | int       | Unique identifier for the category            |
| Name         | string    | Category name                                 |
| Description  | string    | Description of the category                   |
| Status       | bool      | Indicates whether the category is active      |

🔗 One `Category` can be associated with multiple `Products`.


---

## ApplicationUser

Represents a user in the system. Inherits from `IdentityUser` provided by ASP.NET Core Identity.

| Property       | Type                     | Description                               |
|----------------|--------------------------|-------------------------------------------|
| Id             | string (inherited)       | Unique identifier for the user            |
| UserName       | string (inherited)       | Username used for login                   |
| Email          | string (inherited)       | User's email address                      |
| PhoneNumber    | string (inherited)       | User's contact phone number               |
| FirstName      | string                   | User’s first name                         |
| LastName       | string                   | User’s last name                          |
| Gender         | `ApplicationUserGender`  | Gender enum: Male or Female               |
| BirthOfDate    | DateTime                 | User’s date of birth                      |
| State          | string                   | User’s state of residence                 |
| City           | string                   | User’s city of residence                  |
| Street         | string                   | User’s street address                     |


