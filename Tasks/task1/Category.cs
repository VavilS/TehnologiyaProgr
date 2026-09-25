using System;
using System.Collections.Generic;
using System.Text;

namespace task1
{
        /// <summary>
        /// Категория товара
        /// </summary>
        public class Category
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }

            public string Info => $"{Name} — {Description}";
        }
    }

