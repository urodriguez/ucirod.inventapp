﻿using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Aggregates;
using Domain.Contracts.Repositories;

namespace Persistence.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private IList<Product> Products { get; set; }

        public ProductRepository(IList<Product> products) : base(products)
        {
        }

        public IEnumerable<Product> GetCheapest(decimal maxPrice)
        {
            return Products.Where(p => p.Price < maxPrice);
        }
    }
}
