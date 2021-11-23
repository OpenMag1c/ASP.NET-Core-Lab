using System.Collections.Generic;
using System.Runtime.InteropServices;
using DAL.Enum;
using DAL.Models;

namespace DAL.Database
{
    public static class SeedDbProducts
    {
        public static IEnumerable<Product> GetSeedProducts()
        {
            IEnumerable<Product> products = new List<Product>()
            {
                new Product()
                {
                    Id = 1, Name = "Horizon Zero Dawn", DateCreated = 2018, Platform = Platforms.Console,
                    TotalRating = 78
                },
                new Product()
                {
                    Id = 2, Name = "Counter Strike GO", DateCreated = 2016, Platform = Platforms.PersonalComputer,
                    TotalRating = 85
                },
                new Product()
                    {Id = 3, Name = "Brawl Stars", DateCreated = 2017, Platform = Platforms.Mobile, TotalRating = 100},
                new Product()
                {
                    Id = 4, Name = "Half-Life VR", DateCreated = 2020, Platform = Platforms.VirtualReality,
                    TotalRating = 70
                },
                new Product()
                {
                    Id = 5, Name = "TES V Skyrim", DateCreated = 2008, Platform = Platforms.PersonalComputer,
                    TotalRating = 89
                },
                new Product()
                    {Id = 6, Name = "Clash Royale", DateCreated = 2015, Platform = Platforms.Mobile, TotalRating = 80},
                new Product()
                {
                    Id = 7, Name = "Beat Saber", DateCreated = 2017, Platform = Platforms.VirtualReality,
                    TotalRating = 87
                },
                new Product()
                {
                    Id = 8, Name = "Terraria", DateCreated = 2011, Platform = Platforms.PersonalComputer,
                    TotalRating = 93
                },
                new Product()
                {
                    Id = 9, Name = "Genshin Impact", DateCreated = 2020, Platform = Platforms.PersonalComputer,
                    TotalRating = 90
                },
                new Product()
                    {Id = 10, Name = "Snake", DateCreated = 2000, Platform = Platforms.Web, TotalRating = 100},
                new Product()
                    {Id = 11, Name = "Contra city", DateCreated = 2007, Platform = Platforms.Web, TotalRating = 99}
            };

            return products;
        }
    }
}