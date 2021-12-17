using System.Collections.Generic;
using DAL.Enum;
using DAL.Models;

namespace DAL.Seeds
{
    public static class SeedDbProducts
    {
        public static readonly IEnumerable<Product> Products = new List<Product>()
        {
            new Product()
            {
                Id = 1, Name = "Horizon Zero Dawn", DateCreated = 2018, Platform = Platforms.Console,
                Genre = Genres.ActionRPG, Rating = Ratings.MoreThanSixteen,
                Logo = ImagesLogo.Horizon, Background = ImagesBackgrounds.Background1, Price = 24.99, Count = 50
            },
            new Product()
            {
                Id = 2, Name = "Counter Strike GO", DateCreated = 2016, Platform = Platforms.PersonalComputer,
                Genre = Genres.Shooter, Rating = Ratings.MoreThanEighteen,
                Logo = ImagesLogo.CounterStrike, Background = ImagesBackgrounds.Background3, Price = 4.99, Count = 100
            },
            new Product()
            {
                Id = 3, Name = "Brawl Stars", DateCreated = 2017, Platform = Platforms.Mobile,
                Genre = Genres.MOBA, Rating = Ratings.MoreThanSeven, Logo = ImagesLogo.BrawlStars,
                Background = ImagesBackgrounds.Background5, Price = 0.99, Count = 500
            },
            new Product()
            {
                Id = 4, Name = "Half-Life VR", DateCreated = 2020, Platform = Platforms.VirtualReality,
                Genre = Genres.Shooter, Rating = Ratings.MoreThanEighteen,
                Logo = ImagesLogo.HalfLifeVr, Background = ImagesBackgrounds.Background2, Price = 29.99, Count = 25
            },
            new Product()
            {
                Id = 5, Name = "TES V Skyrim", DateCreated = 2008, Platform = Platforms.PersonalComputer,
                Genre = Genres.RPG, Rating = Ratings.MoreThanSixteen, Logo = ImagesLogo.Skyrim,
                Background = ImagesBackgrounds.Background1, Price = 19.99, Count = 90
            },
            new Product()
            {
                Id = 6, Name = "Clash Royale", DateCreated = 2015, Platform = Platforms.Mobile,
                Genre = Genres.Strategy, Rating = Ratings.MoreThanSeven, Logo = ImagesLogo.ClashRoyale,
                Background = ImagesBackgrounds.Background3, Price = 0.99, Count = 200
            },
            new Product()
            {
                Id = 7, Name = "Beat Saber", DateCreated = 2017, Platform = Platforms.VirtualReality,
                Genre = Genres.MusicGame, Rating = Ratings.MoreThanThree,
                Logo = ImagesLogo.BeatSaber, Background = ImagesBackgrounds.Background2, Price = 5.99, Count = 75
            },
            new Product()
            {
                Id = 8, Name = "Terraria", DateCreated = 2011, Platform = Platforms.PersonalComputer,
                Genre = Genres.RPG, Rating = Ratings.MoreThanTwelve, Logo = ImagesLogo.Terraria,
                Background = ImagesBackgrounds.Background5, Price = 2.99, Count = 40
            },
            new Product()
            {
                Id = 9, Name = "Genshin Impact", DateCreated = 2020, Platform = Platforms.PersonalComputer,
                Genre = Genres.ActionRPG, Rating = Ratings.MoreThanSeven,
                Logo = ImagesLogo.GenshinImpact, Background = ImagesBackgrounds.Background2, Price = 5.99, Count = 700
            },
            new Product()
            {
                Id = 10, Name = "Snake", DateCreated = 2000, Platform = Platforms.Web,
                Genre = Genres.Puzzle, Rating = Ratings.MoreThanEighteen, Logo = ImagesLogo.Snake,
                Background = ImagesBackgrounds.Background2, Price = 0, Count = 999
            },
            new Product()
            {
                Id = 11, Name = "Contra city", DateCreated = 2007, Platform = Platforms.Web,
                Genre = Genres.Shooter, Rating = Ratings.MoreThanSixteen, Logo = ImagesLogo.ContraCity,
                Background = ImagesBackgrounds.Background4, Price = 4.99, Count = 120
            }
        };
    }
}