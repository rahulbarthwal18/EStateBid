using AuctionService.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Data;

public class DBInitializer
{
    public static void Initialize(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        SeedAuctionData(context);
    }

    private static void SeedAuctionData(ApplicationDbContext context)
    {
        context.Database.Migrate();
        var auctions = GetAuctions();
        if (context.Auctions.Any()) 
        {
            return;
        }

        context.Auctions.AddRange(auctions);
        context.SaveChanges();
    }
    private static List<Auction> GetAuctions()
    {
        return new List<Auction>
        {
            new Auction
            {
                Id = Guid.NewGuid(),
                ReservePrice = 1200000,
                Seller = "Bob",
                AuctionEnd = DateTime.UtcNow.AddDays(10),
                Status = AuctionStatus.Live,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Property = new Property
                {
                    Id = Guid.NewGuid(),
                    Title = "Luxury Villa",
                    Description = "Beautiful luxury villa with swimming pool",
                    Address = "Palm Street 1",
                    City = "Dubai",
                    State = "Dubai",
                    Bedrooms = 5,
                    Bathrooms = 4,
                    AreaSqFt = 5200,
                    ImageUrl = "https://images.unsplash.com/photo-1613977257363-707ba9348227"
                }
            },
            new Auction
            {
                Id = Guid.NewGuid(),
                ReservePrice = 850000,
                Seller = "Alice",
                AuctionEnd = DateTime.UtcNow.AddDays(12),
                Status = AuctionStatus.Live,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Property = new Property
                {
                    Id = Guid.NewGuid(),
                    Title = "Modern Beach House",
                    Description = "Stunning glass-fronted house with private beach access.",
                    Address = "12 Ocean Drive",
                    City = "Malibu",
                    State = "California",
                    Bedrooms = 3,
                    Bathrooms = 3,
                    AreaSqFt = 3100,
                    ImageUrl = "https://images.unsplash.com/photo-1499793983690-e29da59ef1c2"
                }
            },
            new Auction
            {
                Id = Guid.NewGuid(),
                ReservePrice = 2100000,
                Seller = "Tom",
                AuctionEnd = DateTime.UtcNow.AddDays(15),
                Status = AuctionStatus.ReserveNotMet,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Property = new Property
                {
                    Id = Guid.NewGuid(),
                    Title = "Penthouse Suite",
                    Description = "Sky-high living with 360-degree views of the city skyline.",
                    Address = "5th Ave Highrise",
                    City = "New York",
                    State = "NY",
                    Bedrooms = 4,
                    Bathrooms = 5,
                    AreaSqFt = 4500,
                    ImageUrl = "https://images.unsplash.com/photo-1522708323590-d24dbb6b0267"
                }
            },
            new Auction
            {
                Id = Guid.NewGuid(),
                ReservePrice = 650000,
                Seller = "Sarah",
                AuctionEnd = DateTime.UtcNow.AddDays(5),
                Status = AuctionStatus.Live,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Property = new Property
                {
                    Id = Guid.NewGuid(),
                    Title = "Cozy Mountain Cabin",
                    Description = "Rustic A-frame cabin perfect for ski season.",
                    Address = "99 Pine Trail",
                    City = "Aspen",
                    State = "Colorado",
                    Bedrooms = 2,
                    Bathrooms = 1,
                    AreaSqFt = 1500,
                    ImageUrl = "https://images.unsplash.com/photo-1464822759023-fed622ff2c3b"
                }
            },
            new Auction
            {
                Id = Guid.NewGuid(),
                ReservePrice = 450000,
                Seller = "Mike",
                AuctionEnd = DateTime.UtcNow.AddDays(-2),
                Status = AuctionStatus.Finished,
                CreatedAt = DateTime.UtcNow.AddDays(-10),
                UpdatedAt = DateTime.UtcNow,
                Property = new Property
                {
                    Id = Guid.NewGuid(),
                    Title = "Urban Loft",
                    Description = "Industrial style loft with exposed brick and high ceilings.",
                    Address = "Brick Lane 42",
                    City = "London",
                    State = "Greater London",
                    Bedrooms = 1,
                    Bathrooms = 2,
                    AreaSqFt = 1200,
                    ImageUrl = "https://images.unsplash.com/photo-1560448204-e02f11c3d0e2"
                }
            },
            new Auction
            {
                Id = Guid.NewGuid(),
                ReservePrice = 3200000,
                Seller = "Empire Estates",
                AuctionEnd = DateTime.UtcNow.AddDays(20),
                Status = AuctionStatus.Live,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Property = new Property
                {
                    Id = Guid.NewGuid(),
                    Title = "Historic Manor",
                    Description = "Authentic 18th-century manor set on 20 acres of land.",
                    Address = "Castle Road",
                    City = "Edinburgh",
                    State = "Scotland",
                    Bedrooms = 8,
                    Bathrooms = 6,
                    AreaSqFt = 11000,
                    ImageUrl = "https://images.unsplash.com/photo-1512917774080-9991f1c4c750"
                }
            },
            new Auction
            {
                Id = Guid.NewGuid(),
                ReservePrice = 550000,
                Seller = "Jessica",
                AuctionEnd = DateTime.UtcNow.AddDays(30),
                Status = AuctionStatus.ReserveNotMet,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Property = new Property
                {
                    Id = Guid.NewGuid(),
                    Title = "Suburb Family Home",
                    Description = "Quiet neighborhood home with a large backyard.",
                    Address = "7 Applewood Court",
                    City = "Toronto",
                    State = "Ontario",
                    Bedrooms = 4,
                    Bathrooms = 3,
                    AreaSqFt = 2800,
                    ImageUrl = "https://images.unsplash.com/photo-1568605114967-8130f3a36994"
                }
            },
            new Auction
            {
                Id = Guid.NewGuid(),
                ReservePrice = 1800000,
                Seller = "Global Real Estate",
                AuctionEnd = DateTime.UtcNow.AddDays(18),
                Status = AuctionStatus.Live,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Property = new Property
                {
                    Id = Guid.NewGuid(),
                    Title = "Minimalist Mansion",
                    Description = "Ultra-modern design focusing on light and open space.",
                    Address = "Zen Garden Way",
                    City = "Tokyo",
                    State = "Kanto",
                    Bedrooms = 4,
                    Bathrooms = 4,
                    AreaSqFt = 6000,
                    ImageUrl = "https://images.unsplash.com/photo-1518780664697-55e3ad937233"
                }
            },
            new Auction
            {
                Id = Guid.NewGuid(),
                ReservePrice = 920000,
                Seller = "Frank",
                AuctionEnd = DateTime.UtcNow.AddDays(14),
                Status = AuctionStatus.Live,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Property = new Property
                {
                    Id = Guid.NewGuid(),
                    Title = "Mediterranean Retreat",
                    Description = "Classic white-walled villa with a view of the sea.",
                    Address = "Via Roma 101",
                    City = "Santorini",
                    State = "Cyclades",
                    Bedrooms = 3,
                    Bathrooms = 2,
                    AreaSqFt = 2200,
                    ImageUrl = "https://images.unsplash.com/photo-1502672260266-1c1ef2d93688"
                }
            },
            new Auction
            {
                Id = Guid.NewGuid(),
                ReservePrice = 1400000,
                Seller = "Luxury Living Ltd",
                AuctionEnd = DateTime.UtcNow.AddDays(25),
                Status = AuctionStatus.Live,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Property = new Property
                {
                    Id = Guid.NewGuid(),
                    Title = "Eco-Friendly Smart Home",
                    Description = "Fully solar-powered home with integrated smart systems.",
                    Address = "Green Path 5",
                    City = "Austin",
                    State = "Texas",
                    Bedrooms = 4,
                    Bathrooms = 3,
                    AreaSqFt = 3500,
                    ImageUrl = "https://images.unsplash.com/photo-1580587771525-78b9dba3b914"
                }
            }
        };
    }
}
