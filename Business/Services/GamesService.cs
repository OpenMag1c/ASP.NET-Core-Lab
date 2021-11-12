using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Business.DTO;
using Business.Interfaces;
using DAL.Interfaces;
using DAL.Models;

namespace Business.Services
{
    public class GamesService : IGamesService
    {
        private readonly IRepository<Product> _repo;
        private readonly IMapper _mapper;

        public GamesService(IRepository<Product> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public IEnumerable<PlatformDTO> GetTopThreePlatforms()
        {
            var products = _repo.GetAll().ToArray();
            var sortedProducts =
                products.GroupBy(product => product.Platform)
                    .OrderByDescending(productGroupingItem => productGroupingItem.Count())
                    .Take(3)
                    .Select(productGroupingItem => new PlatformDTO()
                    {
                        Name = productGroupingItem.Key.ToString(),
                        Games = productGroupingItem.Select(product => product.Name)
                    });

            return sortedProducts;
        }
    }
}