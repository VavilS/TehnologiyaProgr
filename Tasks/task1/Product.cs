using System;
using System.Collections.Generic;
using System.Text;

namespace task1
{
    /// <summary>
    /// Товар на складе
    /// </summary>
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int SupplierId { get; set; }
        public int CategoryId { get; set; }

        /// <summary>
        /// Общая стоимость партии товара
        /// </summary>
        public decimal TotalPrice => Price * Quantity;

        /// <summary>
        /// Считается ли товар дорогим
        /// </summary>
        public bool IsExpensive
        {
            get { return Price > 10000; }
        }

        public string GetInfo() => $"{Name} ({Price:F2} руб., {Quantity} шт.)";
    }
}

