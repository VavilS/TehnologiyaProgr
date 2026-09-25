using System;
using System.Collections.Generic;
using System.Text;

namespace task1
{
        /// <summary>
        /// Поставщик товара
        /// </summary>
        public class Supplier
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Country { get; set; }

            /// <summary>
            /// Является ли поставщик иностранным
            /// </summary>
            public bool IsForeign => Country != "Россия";

            public string GetInfo() => $"{Name} ({Country})";
        }
    }

