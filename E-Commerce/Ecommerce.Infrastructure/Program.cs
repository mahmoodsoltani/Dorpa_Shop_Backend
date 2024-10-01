using System.Text;
using System.Text.Json;
using Ecommerce.Controller.src.CustomAuthorization;
using Ecommerce.Controller.src.Middleware;
using Ecommerce.Infrastructure.src.Database;
using Ecommerce.Infrastructure.src.Repository;
using Ecommerce.Infrastructure.src.Repository.OrderRepoAggregate;
using Ecommerce.Infrastructure.src.Repository.ProductRepoAggreate;
using Ecommerce.Infrastructure.src.Repository.Shared;
using Ecommerce.Infrastructure.src.Repository.UserRepoAggregate;
using Ecommerce.Infrastructure.src.Service;
using Ecommerce.Model.src.Entity.OrderAggregate;
using Ecommerce.Model.src.Entity.ProductAggregate;
using Ecommerce.Model.src.Entity.UserAggregate;
using Ecommerce.Model.src.Repository.OrderRepoAggregate;
using Ecommerce.Model.src.Repository.ProductRepoAggreate;
using Ecommerce.Model.src.Repository.Shared;
using Ecommerce.Model.src.Repository.UserRepoAggregate;
using Ecommerce.Service.src.AuthService;
using Ecommerce.Service.src.CategoryServiceAggregate.CategoryAggregate;
using Ecommerce.Service.src.CategoryServiceAggregate.ProductAggregate;
using Ecommerce.Service.src.OrderServiceAggregate.CartDetailAggregate;
using Ecommerce.Service.src.OrderServiceAggregate.OrderAggregate;
using Ecommerce.Service.src.OrderServiceAggregate.OrderDetailAggregate;
using Ecommerce.Service.src.OrderServiceAggregate.OrderDetailAggregate.OrderDetailAggregate;
using Ecommerce.Service.src.ProductServiceAggregate;
using Ecommerce.Service.src.ProductServiceAggregate.BrandAggregate;
using Ecommerce.Service.src.ProductServiceAggregate.DiscountAggregate;
using Ecommerce.Service.src.ProductServiceAggregate.ProductAggregate;
using Ecommerce.Service.src.Shared.ImageServiceAggregate;
using Ecommerce.Service.src.UserServiceAggregate.AddressAggregate;
using Ecommerce.Service.src.UserServiceAggregate.FavoutiteAggregate;
using Ecommerce.Service.src.UserServiceAggregate.ReviewAggregate;
using Ecommerce.Service.src.UserServiceAggregate.UserAggregate;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

void AddServices(WebApplicationBuilder builder)
{
    builder.Services.AddDbContext<AppDbContext>(option =>
        option
            .AddInterceptors(new TimeStampInterceptor())
            .UseNpgsql(builder.Configuration.GetConnectionString("Neonhost"))
            .UseSnakeCaseNamingConvention()
    );
    builder.Services.AddTransient<ExceptionHandlerMiddleware>();
    // Auth
    builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
    builder.Services.AddScoped<IAuthService, AuthService>();
    builder.Services.AddScoped<ITokenService, TokenService>();

    // User
    builder.Services.AddScoped<BaseRepo<User>, UserRepo>();
    builder.Services.AddScoped<IUserRepo, UserRepo>();
    builder.Services.AddScoped<IUserService, UserService>();

    // Address
    builder.Services.AddScoped<BaseRepo<Address>, AddressRepo>();
    builder.Services.AddScoped<IAddressRepo, AddressRepo>();
    builder.Services.AddScoped<IAddressService, AddressService>();

    // Color
    builder.Services.AddScoped<BaseRepo<Color>, ColorRepo>();
    builder.Services.AddScoped<IColorRepo, ColorRepo>();
    builder.Services.AddScoped<IColorService, ColorService>();

    // Size
    builder.Services.AddScoped<BaseRepo<Size>, SizeRepo>();
    builder.Services.AddScoped<ISizeRepo, SizeRepo>();
    builder.Services.AddScoped<ISizeService, SizeService>();

    // Favourite
    builder.Services.AddScoped<BaseRepo<Favourite>, FavouriteRepo>();
    builder.Services.AddScoped<IFavouriteRepo, FavouriteRepo>();
    builder.Services.AddScoped<IFavouriteService, FavouriteService>();

    // Review
    builder.Services.AddScoped<BaseRepo<Review>, ReviewRepo>();
    builder.Services.AddScoped<IReviewRepo, ReviewRepo>();
    builder.Services.AddScoped<IReviewService, ReviewService>();

    // Product

    builder.Services.AddScoped<BaseRepo<Product>, ProductRepo>();
    builder.Services.AddScoped<IProductRepo, ProductRepo>();
    builder.Services.AddScoped<IProductService, ProductService>();
    // ProductImage

    builder.Services.AddScoped<BaseRepo<ProductImage>, ProductImageRepo>();
    builder.Services.AddScoped<IProductImageRepo, ProductImageRepo>();
    builder.Services.AddScoped<IProductImageService, ProductImageService>();
    // Image

    builder.Services.AddScoped<IImageRepo, ImageRepo>();
    builder.Services.AddScoped<IImageService, ImageService>();
    // Category

    builder.Services.AddScoped<BaseRepo<Category>, CategoryRepo>();
    builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
    builder.Services.AddScoped<ICategoryService, CategoryService>();
    // Order

    builder.Services.AddScoped<BaseRepo<Order>, OrderRepo>();
    builder.Services.AddScoped<IOrderRepo, OrderRepo>();
    builder.Services.AddScoped<IOrderService, OrderService>();
    // Cart Detail

    builder.Services.AddScoped<BaseRepo<CartDetail>, CartDetailRepo>();
    builder.Services.AddScoped<ICartDetailRepo, CartDetailRepo>();
    builder.Services.AddScoped<ICartDetailService, CartDetailService>();

    // Cart Detail
    builder.Services.AddScoped<BaseRepo<OrderDetail>, OrderDetailRepo>();
    builder.Services.AddScoped<IOrderDetailRepo, OrderDetailRepo>();
    builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();
    // Discount

    builder.Services.AddScoped<BaseRepo<Discount>, DiscountRepo>();
    builder.Services.AddScoped<IDiscountRepo, DiscountRepo>();
    builder.Services.AddScoped<IDiscountService, DiscountService>();
    // Brand

    builder.Services.AddScoped<BaseRepo<Brand>, BrandRepo>();
    builder.Services.AddScoped<IBrandRepo, BrandRepo>();
    builder.Services.AddScoped<IBrandService, BrandService>();

    // Brand
    builder.Services.AddScoped<BaseRepo<Brand>, BrandRepo>();
    builder.Services.AddScoped<IBrandRepo, BrandRepo>();
    builder.Services.AddScoped<IBrandService, BrandService>();
}

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            Description =
                "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT"
        }
    );

    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
            }
        }
    );
    // options.OperationFilter<SecurityRequirementsOperationFilter>();
});
AddServices(builder);

builder
    .Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
            )
        };
        options.Events = new JwtBearerEvents
        {
            OnChallenge = context =>
            {
                context.HandleResponse();
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";
                var result = JsonSerializer.Serialize(new { error = "Unauthorized" });
                return context.Response.WriteAsync(result);
            }
        };
    })
    .AddGoogle(googleOptions =>
    {
        googleOptions.ClientId =
            "31679719050-em69l38t04bjvma0qcbq4397pdt7puer.apps.googleusercontent.com";
        googleOptions.ClientSecret = "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";
    });
builder.Services.AddAuthorization(option =>
{
    option.AddPolicy(
        "Ownership",
        policy => policy.AddRequirements(new OwnershipAuthorizationRequirement())
    );
    option.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    option.AddPolicy(
        "AdminAndOwnerPolicy",
        policy =>
            policy.RequireRole("Admin").Requirements.Add(new OwnershipAuthorizationRequirement())
    );
});

//For File...
builder.Services.AddDirectoryBrowser();

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowAllOrigins",
        builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
    );
});
builder.Services.AddControllers(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});

var app = builder.Build();
app.UseStaticFiles();
app.Use(
    async (context, next) =>
    {
        context.Response.Headers.Remove("Cross-Origin-Opener-Policy");
        // Or set a different policy if needed:
        // context.Response.Headers["Cross-Origin-Opener-Policy"] = "same-origin-allow-popups";
        await next();
    }
);

var fileProvider = new PhysicalFileProvider(
    Path.Combine("../",builder.Environment.ContentRootPath, "images")
// Path.Combine(builder.Environment.WebRootPath, "images")
);
var requestPath = "/images";
app.UseStaticFiles(
    new StaticFileOptions { FileProvider = fileProvider, RequestPath = requestPath }
);
app.UseDirectoryBrowser(
    new DirectoryBrowserOptions { FileProvider = fileProvider, RequestPath = requestPath }
);
app.UseCors("AllowAllOrigins");
app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI();

// Active Exception Middleware
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.MapControllers();
app.UseHttpsRedirection();

app.Run();
