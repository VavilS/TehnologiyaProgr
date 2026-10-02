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
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// 
        private decimal _price;
        public decimal Price {
            get { return _price; } 
            set 
            {
                if (value < 0) throw new ArgumentException("цЕНА ДОЛЖНА БЫТЬ НЕ ОТРИЦАТЕЛЬНОЙ", nameof(value));
                
                _price = value;
            } 
        
        }
        /// <summary>
        /// 
        /// </summary>
        /// 
        private int _quantity;
        public int Quantity {
            get { return _quantity; }
            set
            {
                if (value < 0) throw new ArgumentException("Rjkbxtcndj ДОЛЖНА БЫТЬ НЕ ОТРИЦАТЕЛЬНОЙ", nameof(value));

                _quantity = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public int SupplierId { get; set; }
        /// <summary>
        /// 
        /// </summary>
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

