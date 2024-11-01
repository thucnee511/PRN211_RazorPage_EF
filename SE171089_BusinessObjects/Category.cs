﻿using System;
using System.Collections.Generic;

namespace SE171089_BusinessObjects
{
    public partial class Category
    {
        public Category()
        {
            Books = new HashSet<Book>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Book> Books { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}";
        }
    }
}
