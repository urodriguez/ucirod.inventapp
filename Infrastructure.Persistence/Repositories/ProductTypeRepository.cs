﻿using Domain.Aggregates;
using Domain.Contracts.Repositories;
using Infrastructure.Crosscutting.Logging;

namespace Infrastructure.Persistence.Repositories
{
    public class ProductTypeRepository: Repository<ProductType>, IProductTypeRepository
    {
        public ProductTypeRepository(IDbConnectionFactory dbConnectionFactory, ILoggerService loggerService) : base(dbConnectionFactory, loggerService)
        {
        }
    }
}