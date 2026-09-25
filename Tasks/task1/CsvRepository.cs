using System;
using System.Collections.Generic;
using System.Text;

namespace task1
{
    public class CsvRepository
    {
        private string _basePath;
        public CsvRepository(string basePath) { _basePath = basePath; }

        public List<Product> GetProducts()
        {
            List<Product> result = new List<Product>();
            string[] lines = File.ReadAllLines(Path.Combine(_basePath, "products.csv"));
            if (lines.Length < 2) return result;
            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');
                if (parts.Length < 6) continue;
                Product b = new Product();
                b.Id = int.Parse(parts[0]);
                b.Name = parts[1];
                b.Price = decimal.Parse(parts[2]);
                b.SupplierId = int.Parse(parts[3]);
                b.CategoryId = int.Parse(parts[4]);
                b.Quantity = int.Parse(parts[5]);
                result.Add(b);
            }
            return result;
        }
        public List<Category> GetCategories()
        {
            var result = new List<Category>();
            string filePath = Path.Combine(_basePath, "categories.csv");

            if (!File.Exists(filePath)) return result;

            string[] lines = File.ReadAllLines(filePath);
            if (lines.Length < 2) return result;

            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i])) continue;

                string[] parts = lines[i].Split(',');
                if (parts.Length < 3) continue;

                Category a = new Category();
                a.Id = int.Parse(parts[0]);
                a.Name = parts[1];
                a.Description = parts[2];
                result.Add(a);

            }
            return result;
        }
        public List<Supplier> GetSuppliers()
        {
            var result = new List<Supplier>();
            string filePath = Path.Combine(_basePath, "suppliers.csv");

            if (!File.Exists(filePath)) return result;

            string[] lines = File.ReadAllLines(filePath);
            if (lines.Length < 2) return result;

            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i])) continue;

                string[] parts = lines[i].Split(',');
                if (parts.Length < 3) continue;

                Supplier c = new Supplier();
                c.Id = int.Parse(parts[0]);
                c.Name = parts[1];
                c.Country = parts[2];

                result.Add(c);
            }
            return result;
        }
    }

}
