using System.Collections.Generic;
using System.Linq;
using System.Net;
using AutoMapper;
using Business.DTO;
using Business.ExceptionMiddleware;
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

        public IEnumerable<ProductDTO> SearchProductsByTerm(string term, int limit, int offset)
        {
            if (term == null)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, Messages.WrongInputData);
            }

            var products = _repo.GetAll().ToArray();
            term = term.ToLower();
            var neededProducts = 
                products.Where(prod => prod.Name.ToLower().Contains(term))
                    .Skip(offset)
                    .Take(limit)
                    .Select(prod => _mapper.Map<ProductDTO>(prod));

            if (neededProducts == null)
            {
                throw new HttpStatusException(HttpStatusCode.NotFound, Messages.ProductNotFound);
            }

            return neededProducts;
        }
    }
}