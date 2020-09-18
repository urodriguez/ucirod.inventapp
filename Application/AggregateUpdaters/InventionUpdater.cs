﻿using Application.Contracts.AggregateUpdaters;
using Application.Dtos;
using Domain.Aggregates;

namespace Application.AggregateUpdaters
{
    public class InventionUpdater : IInventionUpdater
    {
        public void Update(Invention inventionCategory, InventionDto dto)
        {
            inventionCategory.SetCode(dto.Code);
            inventionCategory.SetName(dto.Name);
            inventionCategory.SetCategory(dto.Category);
            inventionCategory.SetPrice(dto.Price);
        }
    }
}