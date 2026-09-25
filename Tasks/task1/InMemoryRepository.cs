using System;
using System.Collections.Generic;
using System.Text;

namespace task1
{
    public class InMemoryRepository
    {
        private List<Supplier> _suppliers;
        private List<Category> _categories;
        private List<Product> _products;

        public InMemoryRepository()
        {
            _suppliers = new List<Supplier>
        {
            new Supplier { Id = 1, Name = "Samsung", Country = "Южная Корея" },
            new Supplier { Id = 2, Name = "Adidas", Country = "Германия" },
            new Supplier { Id = 3, Name = "LG", Country = "Южная Корея" }
        };

            _categories = new List<Category>
        {
            new Category { Id = 1, Name = "Электроника", Description = "бытовая техника" },
            new Category { Id = 2, Name = "Одежда", Description = "текстиль и обувь" }
        };

            _products = new List<Product>
        {
            new Product { Id = 1, Name = "Ноутбук", Price = 75000, Quantity = 10, SupplierId = 1, CategoryId = 1 },
            new Product { Id = 2, Name = "Куртка", Price = 12000, Quantity = 5, SupplierId = 2, CategoryId = 2 },
            new Product { Id = 3, Name = "Телевизор", Price = 45000, Quantity = 3, SupplierId = 3, CategoryId = 1 }
        };
        }

        public List<Supplier> GetSuppliers() => _suppliers;
        public List<Category> GetCategories() => _categories;
        public List<Product> GetProducts() => _products;
    }
}
