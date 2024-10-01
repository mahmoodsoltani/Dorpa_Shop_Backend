# Dorpa: the online bike shop (Backend)


![.NET Core](https://img.shields.io/badge/.NET%20Core-purple)
![EF Core](https://img.shields.io/badge/EF%20Core-cyan)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-drakblue)



> [!NOTE]  
> **You can shop [Here](https://dorpa.netlify.app)!**
> 
> **You can find my frontend project [Here](https://github.com/mahmoodsoltani/fs18_CSharp_FullStack_Frontend)**
> 
> **You can find the API's documentation [Here](https://dorpa.azurewebsites.net/swagger/index.html)**
> 
> **You can find my presentation slides [Here](https://github.com/user-attachments/files/17067319/Title.pptx)**
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

**Sample data flow for the login process:**
<p align="center">
<img src='https://github.com/user-attachments/assets/b8959ef7-b6e6-47a6-89a3-fef68decc5c6' width='80%' align='center'/>
</p>







