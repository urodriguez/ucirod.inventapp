﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Application.ApplicationResults;
using Application.Contracts;
using Application.Contracts.BusinessValidators;
using Application.Contracts.Factories;
using Application.Contracts.Services;
using Application.Dtos;
using Domain.Aggregates;
using Domain.Contracts.Infrastructure.Persistence;
using Domain.Contracts.Predicates.Factories;
using Domain.Contracts.Services;
using Infrastructure.Crosscutting.AppSettings;
using Infrastructure.Crosscutting.Auditing;
using Infrastructure.Crosscutting.Authentication;
using Infrastructure.Crosscutting.Logging;

namespace Application.Services
{
    public class InventionService : CrudService<InventionDto, Invention>, IInventionService
    {
        private readonly IInventionPredicateFactory _inventionPredicateFactory;

        public InventionService(
            IRoleService roleService,
            IInventionFactory factory, 
            IAuditService auditService,
            IInventionBusinessValidator inventionBusinessValidator, 
            IInventionPredicateFactory inventionPredicateFactory,
            ITokenService tokenService,
            IUnitOfWork unitOfWork,
            ILogService logService,
            IAppSettingsService appSettingsService,
            IInventAppContext inventAppContext
        ) : base(
            roleService,
            factory, 
            auditService, 
            inventionBusinessValidator,
            tokenService,
            unitOfWork,
            logService,
            appSettingsService,
            inventAppContext
        )
        {
            _inventionPredicateFactory = inventionPredicateFactory;
        }

        public async Task<IApplicationResult> GetCheapestAsync(decimal maxPrice)
        {
            return await ExecuteAsync(async () =>
            {
                var byCheapest = _inventionPredicateFactory.CreateByCheapest(maxPrice);
                var cheapestInventions = await _unitOfWork.Inventions.GetAsync(byCheapest);

                var cheapestInventionsDto = _factory.CreateFromRange(cheapestInventions);

                return new OkApplicationResult<IEnumerable<InventionDto>>
                {
                    Data = cheapestInventionsDto
                };
            });
        }
    }
}