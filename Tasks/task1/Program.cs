using task1;

namespace task1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Выберите источник данных:");
            Console.WriteLine("1. InMemory");
            Console.WriteLine("2. CSV (папка data)");

            string choiceStr = Console.ReadLine();

            if (!int.TryParse(choiceStr, out int choice))
            {
                Console.WriteLine("Неверный ввод.");
                return;
            }

            List<Supplier> suppliers;
            List<Category> categories;
            List<Product> products;


            try
            {
                switch (choice)
                {
                    case 1:
                        var memoryRepo = new InMemoryRepository();
                        suppliers = memoryRepo.GetSuppliers();
                        categories = memoryRepo.GetCategories();
                        products = memoryRepo.GetProducts();
                        break;
                    case 2:
                        var csvRepo = new CsvRepository("data");
                        suppliers = csvRepo.GetSuppliers();
                        categories = csvRepo.GetCategories();
                        products = csvRepo.GetProducts();
                        break;
                    default:
                        Console.WriteLine("Неверный выбор");
                        return;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
            PrintAllProducts(products, categories, suppliers);
        }

        /// <summary>
        /// Выводит все товары в формате: 
        /// "<GetInfo()>" — категория "<Name>", поставщик <Name>.
        /// Если связь не найдена, выводит "—".
        /// </summary>
        private static void PrintAllProducts(List<Product> products, List<Category> categories, List<Supplier> suppliers)
        {
            if (products.Count == 0)
            {
                Console.WriteLine("—");
                return;
            }

            foreach (var b in products)
            {
                string catName = "—";
                foreach (var a in categories)
                {

                    if (a.Id == b.CategoryId)
                    {
                        catName = a.Name;
                        break;
                    }
                }
                string supplierName = "—";
                foreach (var c in suppliers)
                {
                    if (c.Id == b.SupplierId)
                    {
                        supplierName = c.Name;
                        break;
                    }
                }

                Console.WriteLine($"{b.GetInfo()} — категория \"{catName}\", поставщик {supplierName}");
            }
        }
    }
}

