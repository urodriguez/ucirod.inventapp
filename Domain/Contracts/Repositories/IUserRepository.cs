﻿using Domain.Aggregates;

namespace Domain.Contracts.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByName(string name);
    }
}