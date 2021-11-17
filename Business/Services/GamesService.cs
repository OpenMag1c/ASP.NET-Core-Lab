using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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

        public ProductDTO FindProductById(int id)
        {
            if (id <= 0)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, Messages.WrongInputData);
            }

            var products = _repo.GetAll().ToArray();
            var neededProduct = _mapper.Map<ProductDTO>(products.FirstOrDefault(prod => prod.Id == id));
            if (neededProduct is null)
            {
                throw new HttpStatusException(HttpStatusCode.NotFound, Messages.ProductNotFound);
            }

            return neededProduct;
        }

        public async Task<ProductDTO> AddNewProductAsync(ProductDTO productDto)
        {
            if (productDto is null)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, Messages.WrongInputData);
            }

            var newProduct = _mapper.Map<Product>(productDto);
            var result = await _repo.AddNewAsync(newProduct);
            if (result is null)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, Messages.NotCompleted);
            }

            var resultDto = _mapper.Map<ProductDTO>(result);

            return resultDto;
        }

        public async Task<ProductDTO> UpdateProductAsync(ProductDTO productDto)
        {
            if (productDto is null)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, Messages.WrongInputData);
            }

            var products = _repo.GetAll().ToArray();
            var oldProduct = products.FirstOrDefault(prod => prod.Name == productDto.Name);
            if (oldProduct is null)
            {
                throw new HttpStatusException(HttpStatusCode.NotFound, Messages.ProductNotFound);
            }

            var newProduct = _mapper.Map(productDto, oldProduct);
            var result = await _repo.UpdateAsync(newProduct);
            var resultDto = _mapper.Map<ProductDTO>(result);

            return resultDto;
        }

        public async void DeleteProductAsync(int id)
        {
            if (id <= 0)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, Messages.WrongInputData);
            }

            var products = _repo.GetAll().ToArray();
            var deletedProduct = products.FirstOrDefault(prod => prod.Id == id);
            if (deletedProduct is null)
            {
                throw new HttpStatusException(HttpStatusCode.NotFound, Messages.ProductNotFound);
            }

            var result = await _repo.DeleteAsync(deletedProduct);
            if (!result)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, Messages.NotCompleted);
            }
        }
    }
}