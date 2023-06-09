﻿using Microsoft.Extensions.Options;
using MongoDB.Driver;
using seecreativa_backend.Classifications.Repositories;
using seecreativa_backend.Core;
using seecreativa_backend.Core.MongoDb;
using seecreativa_backend.Products.Entities;
using seecreativa_backend.Products.Models;
using seecreativa_backend.Products.Persistance;

namespace seecreativa_backend.Products.Repositories {
    public interface IProductsRepository : IRepository<Product, ProductCreateDto, ProductUpdateDto> {
        public Task<IEnumerable<ProductWithClassificationResponseDto>> GetAllAsync(string? q);
        public Task<Product?> GetByCodeAsync(string code);
        public Task<ProductWithClassificationResponseDto?> GetProductByIdWithClassificationAsync(string id);
    }

    public class ProductsRepository : MongoDbRepository<Product, ProductCreateDto, ProductUpdateDto>, IProductsRepository {
        private readonly IClassificationsRepository _classificationsRepository;

        public ProductsRepository(IOptions<MongoDbSettings> settings, IClassificationsRepository classificationsRepository) : base(new ProductsContext(settings).Products) {
            _classificationsRepository = classificationsRepository;
        }

        public async Task<IEnumerable<ProductWithClassificationResponseDto>> GetAllAsync(string? q) {
            List<Product> products = new List<Product>();
            if (q == null)
                products = await _collection.Find(x => true).SortBy(x => x.Code).ToListAsync();
            else
                products = await _collection.Find(x => x.Name.Contains(q, StringComparison.CurrentCultureIgnoreCase)).SortBy(x => x.Code).ToListAsync();
            List<ProductWithClassificationResponseDto> productsWithClassification = new List<ProductWithClassificationResponseDto>();
            foreach (Product product in products) {
                productsWithClassification.Add((await GetProductByIdWithClassificationAsync(product.Id.ToString()))!);
            }
            return productsWithClassification;
        }

        public async Task<Product?> GetByCodeAsync(string code) {
            var entity = await _collection.Find(x => x.Code == code).FirstOrDefaultAsync();
            if (entity == null) return null;
            return entity;
        }

        public async Task<ProductWithClassificationResponseDto?> GetProductByIdWithClassificationAsync(string id) {
            var entity = await _collection.Find(x => x.Id.ToString() == id).FirstOrDefaultAsync();
            if (entity == null) return null;
            var classification = await _classificationsRepository.GetByIdAsync(entity.ClassificationId);
            return entity.ToResponseWithClassification(classification!.ToResponse());
        }
    }
}
