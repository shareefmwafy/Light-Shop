# 📋 Project Requirements

## 📌 Overview
This document outlines the business and technical requirements for the **Light-Shop E-Commerce Platform** built with ASP.NET Core.

---

## 🎯 Functional Requirements

### 🛒 User Module
- Users can register and log in
- Users can view product listings
- Users can search/filter products
- Users can add/remove items from the cart
- Users can place orders
- Users can view their order history

### 📦 Admin Module
- Admin can manage products (Create, Update, Delete)
- Admin can manage categories and inventory
- Admin can view and manage user orders
- Admin can view sales reports (optional)

---

## 🔐 Non-Functional Requirements
- Authentication using JWT
- Secure password hashing
- Responsive and scalable backend API
- RESTful API design
- Efficient database queries using Entity Framework Core

---

## 🧰 Technical Requirements
- ASP.NET Core Web API (v8)
- SQL Server
- Entity Framework Core
- AutoMapper
- Swagger for API documentation
- Postman for API testing

---

## 📚 Use Case Example

### Use Case: Add Product to Cart
- **Actor:** Authenticated User
- **Description:** A user selects a product and adds it to the cart.
- **Precondition:** User is logged in.
- **Postcondition:** Product appears in the cart with selected quantity.

---

## 🗂️ Future Enhancements
- Payment gateway integration (e.g., Stripe or PayPal)
- Product reviews and ratings
- Email notifications
- Wishlist functionality
