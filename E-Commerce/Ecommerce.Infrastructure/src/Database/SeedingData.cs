using Ecommerce.Infrastructure.src.Service;
using Ecommerce.Model.src.Entity.ProductAggregate;
using Ecommerce.Model.src.Entity.UserAggregate;

namespace Ecommerce.Infrastructure.src.Database
{
    public class SeedingData
    {
        private static PasswordHasher _hasher = new();

        public static List<User> GetUsers()
        {
            _hasher.HashPassword("rkxmsd123", out string hp, out byte[] salt);
            return new List<User>
            {
                new()
                {
                    Id = 1,
                    FirstName = "Mahmoud",
                    LastName = "Soltani",
                    Email = "mahmoud.soltani@gmail.com",
                    IsAdmin = true,
                    Password = hp,
                    Salt = salt
                },
                new()
                {
                    Id = 2,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "John@gmail.com",
                    IsAdmin = false,
                    Password = hp,
                    Salt = salt
                },
                new()
                {
                    Id = 3,
                    FirstName = "Mike",
                    LastName = "Smite",
                    Email = "mike@gmail.com",
                    IsAdmin = false,
                    Password = hp,
                    Salt = salt
                }
            };
        }

        public static List<Category> GetCategories()
        {
            return new List<Category>
            {
                new Category
                {
                    Id = 1,
                    Name = "Bicycles",
                    Description = "All types of bicycles",
                    ParentId = null,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                // Bicycles -> Mountain Bikes
                new Category
                {
                    Id = 2,
                    Name = "Mountain Bikes",
                    Description = "Off-road bikes",
                    ParentId = 1,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Category
                {
                    Id = 3,
                    Name = "Cross-Country (XC)",
                    Description = "XC mountain bikes",
                    ParentId = 2,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Category
                {
                    Id = 4,
                    Name = "Hardtail XC",
                    Description = "Hardtail cross-country mountain bikes",
                    ParentId = 3,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Category
                {
                    Id = 5,
                    Name = "Full-Suspension XC",
                    Description = "Full-suspension cross-country mountain bikes",
                    ParentId = 3,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Category
                {
                    Id = 6,
                    Name = "Trail Bikes",
                    Description = "Trail bikes for rugged terrain",
                    ParentId = 2,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Category
                {
                    Id = 7,
                    Name = "Downhill Bikes",
                    Description = "Downhill mountain bikes",
                    ParentId = 2,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Category
                {
                    Id = 8,
                    Name = "Enduro Bikes",
                    Description = "Enduro mountain bikes",
                    ParentId = 2,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                // Bicycles -> Road Bikes
                new Category
                {
                    Id = 9,
                    Name = "Road Bikes",
                    Description = "Lightweight bikes for paved roads",
                    ParentId = 1,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Category
                {
                    Id = 10,
                    Name = "Race Bikes",
                    Description = "Bikes for racing",
                    ParentId = 9,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Category
                {
                    Id = 11,
                    Name = "Carbon Frame Race Bikes",
                    Description = "Lightweight carbon frame race bikes",
                    ParentId = 10,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Category
                {
                    Id = 12,
                    Name = "Endurance Bikes",
                    Description = "Comfort-oriented road bikes",
                    ParentId = 9,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Category
                {
                    Id = 13,
                    Name = "Aero Bikes",
                    Description = "Aerodynamic bikes for speed",
                    ParentId = 9,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                // Bicycles -> Hybrid Bikes
                new Category
                {
                    Id = 14,
                    Name = "Hybrid Bikes",
                    Description = "Versatile bikes for various terrains",
                    ParentId = 1,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Category
                {
                    Id = 15,
                    Name = "Urban Hybrid Bikes",
                    Description = "Hybrid bikes suited for urban environments",
                    ParentId = 14,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Category
                {
                    Id = 16,
                    Name = "Fitness Bikes",
                    Description = "Hybrid bikes designed for fitness",
                    ParentId = 14,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                // Bicycles -> Electric Bikes
                new Category
                {
                    Id = 17,
                    Name = "Electric Bikes",
                    Description = "Bikes with electric assistance",
                    ParentId = 1,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Category
                {
                    Id = 18,
                    Name = "E-Mountain Bikes",
                    Description = "Electric bikes for mountain terrain",
                    ParentId = 17,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Category
                {
                    Id = 19,
                    Name = "E-Road Bikes",
                    Description = "Electric road bikes",
                    ParentId = 17,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Category
                {
                    Id = 20,
                    Name = "E-Hybrid Bikes",
                    Description = "Electric hybrid bikes",
                    ParentId = 17,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                // Bicycles -> Kids' Bikes
                new Category
                {
                    Id = 21,
                    Name = "Kids' Bikes",
                    Description = "Bicycles designed for children",
                    ParentId = 1,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Category
                {
                    Id = 22,
                    Name = "Balance Bikes",
                    Description = "Bikes for toddlers to learn balance",
                    ParentId = 21,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Category
                {
                    Id = 23,
                    Name = "Pedal Bikes",
                    Description = "Traditional pedal bikes for kids",
                    ParentId = 21,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Category
                {
                    Id = 24,
                    Name = "Mountain Kids Bikes",
                    Description = "Mountain bikes designed for kids",
                    ParentId = 21,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                // Categories for Specific Genders
                new Category
                {
                    Id = 25,
                    Name = "Men",
                    Description = "Bicycles and accessories for men",
                    ParentId = 1,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Category
                {
                    Id = 26,
                    Name = "Women",
                    Description = "Bicycles and accessories for women",
                    ParentId = 1,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Category
                {
                    Id = 27,
                    Name = "Children",
                    Description = "Bicycles and accessories for children",
                    ParentId = 1,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                // Accessories
                new Category
                {
                    Id = 28,
                    Name = "Accessories",
                    Description = "Bike accessories",
                    ParentId = null,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Category
                {
                    Id = 29,
                    Name = "Helmets",
                    Description = "Safety helmets",
                    ParentId = 28,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Category
                {
                    Id = 30,
                    Name = "Lights",
                    Description = "Bike lights",
                    ParentId = 28,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Category
                {
                    Id = 31,
                    Name = "Locks",
                    Description = "Bike locks",
                    ParentId = 28,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Category
                {
                    Id = 32,
                    Name = "Bags",
                    Description = "Bike bags and carriers",
                    ParentId = 28,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                // Parts
                new Category
                {
                    Id = 33,
                    Name = "Parts",
                    Description = "Bike parts",
                    ParentId = null,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Category
                {
                    Id = 34,
                    Name = "Drivetrain",
                    Description = "Drivetrain components",
                    ParentId = 33,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Category
                {
                    Id = 35,
                    Name = "Brakes",
                    Description = "Brakes and braking components",
                    ParentId = 33,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Category
                {
                    Id = 36,
                    Name = "Wheels",
                    Description = "Wheels and rims",
                    ParentId = 33,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Category
                {
                    Id = 37,
                    Name = "Tires",
                    Description = "Bike tires",
                    ParentId = 33,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Category
                {
                    Id = 38,
                    Name = "Handlebars",
                    Description = "Handlebars and grips",
                    ParentId = 33,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                // Clothing
                new Category
                {
                    Id = 39,
                    Name = "Clothing",
                    Description = "Cycling clothing",
                    ParentId = null,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Category
                {
                    Id = 40,
                    Name = "Jerseys",
                    Description = "Cycling jerseys",
                    ParentId = 39,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Category
                {
                    Id = 41,
                    Name = "Shorts",
                    Description = "Cycling shorts",
                    ParentId = 39,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Category
                {
                    Id = 42,
                    Name = "Gloves",
                    Description = "Cycling gloves",
                    ParentId = 39,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Category
                {
                    Id = 43,
                    Name = "Jackets",
                    Description = "Cycling jackets",
                    ParentId = 39,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Category
                {
                    Id = 44,
                    Name = "Socks",
                    Description = "Cycling socks",
                    ParentId = 39,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                }
            };
        }

        public static IEnumerable<Color> GetColors()
        {
            return new List<Color>
            {
                new Color
                {
                    Id = 1,
                    Name = "Red",
                    Code = "#FF0000",
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Color
                {
                    Id = 2,
                    Name = "Green",
                    Code = "#00FF00",
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Color
                {
                    Id = 3,
                    Name = "Blue",
                    Code = "#0000FF",
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Color
                {
                    Id = 4,
                    Name = "Yellow",
                    Code = "#FFFF00",
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Color
                {
                    Id = 5,
                    Name = "Cyan",
                    Code = "#00FFFF",
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Color
                {
                    Id = 6,
                    Name = "Magenta",
                    Code = "#FF00FF",
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Color
                {
                    Id = 7,
                    Name = "Black",
                    Code = "#000000",
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Color
                {
                    Id = 8,
                    Name = "White",
                    Code = "#FFFFFF",
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Color
                {
                    Id = 9,
                    Name = "Gray",
                    Code = "#808080",
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Color
                {
                    Id = 10,
                    Name = "Maroon",
                    Code = "#800000",
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Color
                {
                    Id = 11,
                    Name = "Olive",
                    Code = "#808000",
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Color
                {
                    Id = 12,
                    Name = "Purple",
                    Code = "#800080",
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Color
                {
                    Id = 13,
                    Name = "Silver",
                    Code = "#C0C0C0",
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Color
                {
                    Id = 14,
                    Name = "Teal",
                    Code = "#008080",
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Color
                {
                    Id = 15,
                    Name = "Navy",
                    Code = "#000080",
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Color
                {
                    Id = 16,
                    Name = "Lime",
                    Code = "#00FF00",
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Color
                {
                    Id = 17,
                    Name = "Aqua",
                    Code = "#00FFFF",
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Color
                {
                    Id = 18,
                    Name = "Fuchsia",
                    Code = "#FF00FF",
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Color
                {
                    Id = 19,
                    Name = "Orange",
                    Code = "#FFA500",
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Color
                {
                    Id = 20,
                    Name = "Brown",
                    Code = "#A52A2A",
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Color
                {
                    Id = 21,
                    Name = "Pink",
                    Code = "#FFC0CB",
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Color
                {
                    Id = 22,
                    Name = "Indigo",
                    Code = "#4B0082",
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                }
            };
        }

        public static IEnumerable<Size> GetSizes()
        {
            return new List<Size>
            {
                new Size
                {
                    Id = 1,
                    Name = "Small",
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Size
                {
                    Id = 2,
                    Name = "Medium",
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Size
                {
                    Id = 3,
                    Name = "Large",
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Size
                {
                    Id = 4,
                    Name = "Extra Large",
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Size
                {
                    Id = 5,
                    Name = "XXL",
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Size
                {
                    Id = 6,
                    Name = "XS",
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Size
                {
                    Id = 7,
                    Name = "XXS",
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Size
                {
                    Id = 8,
                    Name = "One Size",
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Size
                {
                    Id = 9,
                    Name = "Youth",
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Size
                {
                    Id = 10,
                    Name = "Adult",
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Size
                {
                    Id = 11,
                    Name = "Regular",
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Size
                {
                    Id = 12,
                    Name = "Custom",
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Size
                {
                    Id = 13,
                    Name = "Standard",
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Size
                {
                    Id = 14,
                    Name = "Slim",
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Size
                {
                    Id = 15,
                    Name = "Fit",
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Size
                {
                    Id = 16,
                    Name = "Classic",
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Size
                {
                    Id = 17,
                    Name = "Plus Size",
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Size
                {
                    Id = 18,
                    Name = "Petite",
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Size
                {
                    Id = 19,
                    Name = "Tall",
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Size
                {
                    Id = 20,
                    Name = "Short",
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                }
            };
        }

        public static IEnumerable<Brand> GetBrands()
        {
            return new List<Brand>
            {
                new Brand
                {
                    Id = 1,
                    Name = "Trek",
                    Description =
                        "Trek is a leading bicycle manufacturer known for its high-performance bikes and innovative designs.",
                    AltText = "Trek logo",
                    ImageUrl = "https://example.com/trek.jpg",
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Brand
                {
                    Id = 2,
                    Name = "Specialized",
                    Description =
                        "Specialized offers a wide range of bikes for different riding styles and disciplines.",
                    AltText = "Specialized logo",
                    ImageUrl = "https://example.com/specialized.jpg",
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Brand
                {
                    Id = 3,
                    Name = "Giant",
                    Description =
                        "Giant is one of the largest bike manufacturers globally, known for producing reliable and affordable bicycles.",
                    AltText = "Giant logo",
                    ImageUrl = "https://example.com/giant.jpg",
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Brand
                {
                    Id = 4,
                    Name = "Cannondale",
                    Description =
                        "Cannondale is renowned for its innovation in bicycle technology and design.",
                    AltText = "Cannondale logo",
                    ImageUrl = "https://example.com/cannondale.jpg",
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Brand
                {
                    Id = 5,
                    Name = "Bianchi",
                    Description =
                        "Bianchi is an Italian brand with a long history of producing high-quality road bikes and racing bicycles.",
                    AltText = "Bianchi logo",
                    ImageUrl = "https://example.com/bianchi.jpg",
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Brand
                {
                    Id = 6,
                    Name = "Scott",
                    Description =
                        "Scott Sports is known for its range of high-performance bicycles and cycling gear.",
                    AltText = "Scott logo",
                    ImageUrl = "https://example.com/scott.jpg",
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Brand
                {
                    Id = 7,
                    Name = "Santa Cruz",
                    Description =
                        "Santa Cruz specializes in high-end mountain bikes with a focus on innovation and performance.",
                    AltText = "Santa Cruz logo",
                    ImageUrl = "https://example.com/santacruz.jpg",
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Brand
                {
                    Id = 8,
                    Name = "Norco",
                    Description =
                        "Norco is a Canadian brand known for its mountain bikes and cycling products.",
                    AltText = "Norco logo",
                    ImageUrl = "https://example.com/norco.jpg",
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Brand
                {
                    Id = 9,
                    Name = "Cube",
                    Description =
                        "Cube offers a wide range of bicycles, including road, mountain, and e-bikes.",
                    AltText = "Cube logo",
                    ImageUrl = "https://example.com/cube.jpg",
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Brand
                {
                    Id = 10,
                    Name = "Raleigh",
                    Description =
                        "Raleigh is a historic brand offering a diverse range of bicycles.",
                    AltText = "Raleigh logo",
                    ImageUrl = "https://example.com/raleigh.jpg",
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                }
            };
        }

        public static IEnumerable<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Mountain Bike X1",
                    Description = "High-performance mountain bike with advanced suspension.",
                    Price = 1200.00m,
                    BrandId = 1, // Trek
                    SizeId = 1, // Medium
                    CategoryId = 1, // Mountain Bikes
                    ColorId = 1, // Red
                    Stock = 10,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Product
                {
                    Id = 2,
                    Name = "Road Bike Pro",
                    Description = "Lightweight road bike designed for speed and efficiency.",
                    Price = 1500.00m,
                    BrandId = 2, // Specialized
                    SizeId = 2, // Large
                    CategoryId = 2, // Road Bikes
                    ColorId = 2, // Blue
                    Stock = 5,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Product
                {
                    Id = 3,
                    Name = "Hybrid Bike ZX",
                    Description = "Versatile hybrid bike for both urban and off-road adventures.",
                    Price = 900.00m,
                    BrandId = 3, // Giant
                    SizeId = 3, // Small
                    CategoryId = 3, // Hybrid Bikes
                    ColorId = 3, // Green
                    Stock = 7,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Product
                {
                    Id = 4,
                    Name = "Electric Bike E1",
                    Description = "Electric bike with long battery life and powerful motor.",
                    Price = 2000.00m,
                    BrandId = 4, // Cannondale
                    SizeId = 1, // Medium
                    CategoryId = 4, // Electric Bikes
                    ColorId = 4, // Black
                    Stock = 3,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Product
                {
                    Id = 5,
                    Name = "Touring Bike Expert",
                    Description = "Comfortable touring bike with ample storage and stability.",
                    Price = 1100.00m,
                    BrandId = 5, // Bianchi
                    SizeId = 2, // Large
                    CategoryId = 5, // Touring Bikes
                    ColorId = 5, // Yellow
                    Stock = 6,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Product
                {
                    Id = 6,
                    Name = "Kids Bike Fun",
                    Description = "Colorful and durable bike designed for children.",
                    Price = 300.00m,
                    BrandId = 6, // Scott
                    SizeId = 4, // Kids
                    CategoryId = 6, // Kids Bikes
                    ColorId = 6, // Pink
                    Stock = 12,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Product
                {
                    Id = 7,
                    Name = "Comfort Bike C1",
                    Description = "Comfortable bike for city commuting and leisure rides.",
                    Price = 800.00m,
                    BrandId = 7, // Raleigh
                    SizeId = 3, // Small
                    CategoryId = 7, // Comfort Bikes
                    ColorId = 7, // Orange
                    Stock = 8,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Product
                {
                    Id = 8,
                    Name = "Foldable Bike X2",
                    Description = "Compact foldable bike ideal for urban environments.",
                    Price = 700.00m,
                    BrandId = 8, // Brompton
                    SizeId = 3, // Small
                    CategoryId = 8, // Foldable Bikes
                    ColorId = 8, // Silver
                    Stock = 9,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Product
                {
                    Id = 9,
                    Name = "Racing Bike S1",
                    Description = "High-speed racing bike with aerodynamic design.",
                    Price = 1800.00m,
                    BrandId = 9, // Pinarello
                    SizeId = 2, // Large
                    CategoryId = 2, // Road Bikes
                    ColorId = 9, // Red
                    Stock = 4,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Product
                {
                    Id = 10,
                    Name = "Mountain Bike Y2",
                    Description = "Durable mountain bike with enhanced shock absorbers.",
                    Price = 1300.00m,
                    BrandId = 10, // Merida
                    SizeId = 1, // Medium
                    CategoryId = 1, // Mountain Bikes
                    ColorId = 10, // Gray
                    Stock = 7,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Product
                {
                    Id = 11,
                    Name = "Electric Scooter E2",
                    Description = "Electric scooter with compact design and fast charging.",
                    Price = 600.00m,
                    BrandId = 4, // Cannondale
                    SizeId = 3, // Small
                    CategoryId = 4, // Electric Bikes
                    ColorId = 1, // Red
                    Stock = 15,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Product
                {
                    Id = 12,
                    Name = "Cruiser Bike C2",
                    Description = "Stylish cruiser bike with comfortable seating.",
                    Price = 950.00m,
                    BrandId = 6, // Scott
                    SizeId = 2, // Large
                    CategoryId = 7, // Comfort Bikes
                    ColorId = 2, // Blue
                    Stock = 6,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Product
                {
                    Id = 13,
                    Name = "Kids Bike X3",
                    Description = "Durable bike for kids with adjustable seat.",
                    Price = 350.00m,
                    BrandId = 7, // Raleigh
                    SizeId = 4, // Kids
                    CategoryId = 6, // Kids Bikes
                    ColorId = 3, // Green
                    Stock = 20,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Product
                {
                    Id = 14,
                    Name = "Hybrid Bike Z3",
                    Description = "Hybrid bike suitable for both road and trail riding.",
                    Price = 1000.00m,
                    BrandId = 8, // Brompton
                    SizeId = 1, // Medium
                    CategoryId = 3, // Hybrid Bikes
                    ColorId = 4, // Black
                    Stock = 9,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Product
                {
                    Id = 15,
                    Name = "Touring Bike T1",
                    Description = "High-comfort touring bike with multiple gears.",
                    Price = 1200.00m,
                    BrandId = 9, // Pinarello
                    SizeId = 2, // Large
                    CategoryId = 5, // Touring Bikes
                    ColorId = 5, // Yellow
                    Stock = 5,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Product
                {
                    Id = 16,
                    Name = "Performance Bike P1",
                    Description = "High-performance bike designed for speed enthusiasts.",
                    Price = 1500.00m,
                    BrandId = 10, // Merida
                    SizeId = 2, // Large
                    CategoryId = 2, // Road Bikes
                    ColorId = 6, // Pink
                    Stock = 8,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Product
                {
                    Id = 17,
                    Name = "Mountain Bike M2",
                    Description = "Advanced mountain bike with reinforced frame.",
                    Price = 1400.00m,
                    BrandId = 1, // Trek
                    SizeId = 1, // Medium
                    CategoryId = 1, // Mountain Bikes
                    ColorId = 7, // Orange
                    Stock = 4,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Product
                {
                    Id = 18,
                    Name = "Electric Bike E3",
                    Description = "Powerful electric bike with extended range.",
                    Price = 2200.00m,
                    BrandId = 2, // Specialized
                    SizeId = 3, // Small
                    CategoryId = 4, // Electric Bikes
                    ColorId = 8, // Silver
                    Stock = 3,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Product
                {
                    Id = 19,
                    Name = "Road Bike R2",
                    Description = "Speed-oriented road bike with aerodynamic frame.",
                    Price = 1700.00m,
                    BrandId = 3, // Giant
                    SizeId = 2, // Large
                    CategoryId = 2, // Road Bikes
                    ColorId = 9, // Red
                    Stock = 6,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                },
                new Product
                {
                    Id = 20,
                    Name = "Cruiser Bike C3",
                    Description = "Classic cruiser bike with retro design.",
                    Price = 950.00m,
                    BrandId = 4, // Cannondale
                    SizeId = 3, // Small
                    CategoryId = 7, // Comfort Bikes
                    ColorId = 10, // Gray
                    Stock = 7,
                    Create_Date = DateTime.UtcNow,
                    Update_Date = DateTime.UtcNow
                }
            };
        }
    }
}
