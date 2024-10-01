# Dorpa: the online bike shop (Backend)

![Entity Framework](https://img.shields.io/badge/-Entity%20Framework-8C5B3C?style=flat-square&logo=dotnet&logoColor=white)
![.NET Core](https://img.shields.io/badge/-NET%20Core-512BD4?style=flat-square&logo=dotnet&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/-PostgreSQL-336791?style=flat-square&logo=postgresql&logoColor=white)
![Docker](https://img.shields.io/badge/-Docker-2496ED?style=flat-square&logo=docker&logoColor=white)
![NuGet](https://img.shields.io/badge/-NuGet-9B4C20?style=flat-square&logo=nuget&logoColor=white)
![Swagger](https://img.shields.io/badge/-Swagger-85EA2D?style=flat-square&logo=swagger&logoColor=white)
![TypeScript](https://img.shields.io/badge/-TypeScript-007ACC?style=flat-square&logo=typescript&logoColor=white)
![React](https://img.shields.io/badge/-React-61DAFB?style=flat-square&logo=react&logoColor=white)
![Redux](https://img.shields.io/badge/-Redux-764ABC?style=flat-square&logo=redux&logoColor=white)

This repository forms a part of my  project for the Full Stack Program at [Integrify](https://www.integrify.io/program/finland/full-stack). It serves as the back-end component of an e-commerce application that demonstrates the functionalities essential for online retailers. The application retrieves, creates, deletes, and manages data through an API built on ASP.NET Core. User authentication is implemented with specific roles; registering as a customer allows you to create orders, manage your order history, and leave product reviews. Additionally, administrators can perform CRUD operations on products, users, and all orders directly from their dashboard.

**Frontend:** TypeScript, React, Redux Toolkit, React Router, Material UI, Jest
**Backend:** ASP.NET Core, Entity Framework Core, PostgreSQL

This repository contains solely the backend code of the application. For details on the frontend implementation, please check the [frontend repository](https://github.com/mahmoodsoltani/Dorpa_Shop_Frontend). You can also explore the live deployment of my frontend e-commerce project by visiting [Dorpa Shop](https://dorpa.netlify.app/).

> [!NOTE]  
> **You can shop [Here](https://dorpa.netlify.app)!**
> 
> **You can find my frontend project [Here](https://github.com/mahmoodsoltani/Dorpa_Shop_Frontend)**
> 
> **You can find the API's documentation [Here](https://dorpa.azurewebsites.net/swagger/index.html)**
 
-----------------------------------------------------------------------------------------------------------------

**Project feature:**

**Clean Architecture:** Separation of concerns for better maintainability.

**JWT Authentication:** Secure login and protected routes.

**Middleware:** Exception handling.

**DTOs & Validation:** Ensures correct data transfer.

**Swagger:** API documentation for easy testing and development.

**Generic Services & Repositories & Controllers:** CRUD operations.

**Image Handling:** Store and retrieve images.

**Pagination, Filtering & Sorting:** Optimized data handling for large datasets. 


-----------------------------------------------------------------------------------------------------------------
**Main entities:**
1) User related: (User,Address, Favourite, Review)
2) Product related (Product,Brand, Category, Color, Size, Discount, ProductImage)
3) Order related (Order, Order-detail, Cart-detail)
-----------------------------------------------------------------------------------------------------------------
**Key functionalities:**

<img src='https://github.com/user-attachments/assets/1d5e41da-34cc-46d3-982c-4dca1d1e0474' width='30px' /> **General User Capabilities:**
- [x] Create a new account (Sign Up)
- [x] Log in and log out of the system
- [x] Search, sort, and paginate through product listings
- [x] View detailed product reviews and descriptions
- [x] Browse recently added products and discover discounted items

<img src='https://github.com/user-attachments/assets/362b65f0-fcf5-4d09-b685-45bf7b472599' width='30px' />**Exclusive Features for Logged-in Users:**
- [x] Add products to your Favorite List for quick access
- [x] Add items to your Shopping Cart
- [x] Proceed to checkout and complete purchases
- [x] Submit product reviews and feedback
- [x] View and manage your Order History

<img src='https://github.com/user-attachments/assets/e041d0eb-e383-4950-b637-88feab0d1aaa' width='30px' />**Admin Privileges:**
- [x] Create and manage new products
- [x] Apply discounts to products
- [x] Add discount
- [x] Access a comprehensive list of users and products for management
-----------------------------------------------------------------------------------------------------------------

**Database Schema (ERD):**
<p align="center">
<img src='https://github.com/user-attachments/assets/333f142d-30c8-46ee-a178-44d9b4f5c132' width='80%' align='center'/>
</p>


**Sample data flow for the login process:**
<p align="center">
<img src='https://github.com/user-attachments/assets/b8959ef7-b6e6-47a6-89a3-fef68decc5c6' width='80%' align='center'/>
</p>




