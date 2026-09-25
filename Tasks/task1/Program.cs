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

                foreach (var p in products)
                {
                    string catName = "—";
                    foreach (var c in categories)
                    {
                        if (c.Id == p.CategoryId)
                        {
                            catName = c.Name;
                            break;
                        }
                    }
                    string supplierName = "—";
                    foreach (var s in suppliers)
                    {
                        if (s.Id == p.SupplierId)
                        {
                            supplierName = s.Name;
                            break;
                        }
                    }

                    Console.WriteLine($"{p.GetInfo()} — категория \"{catName}\", поставщик {supplierName}");
                }
            }
        }
    }

