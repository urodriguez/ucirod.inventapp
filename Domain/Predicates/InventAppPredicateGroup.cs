﻿using System.Collections.Generic;
using Domain.Contracts.Aggregates;
using Domain.Enums;

namespace Domain.Predicates
{
    public class InventAppPredicateGroup<TAggregateRoot> : IInventAppPredicate<TAggregateRoot> where TAggregateRoot : IAggregateRoot
    {
        public InventAppPredicateGroup(IList<IInventAppPredicate<TAggregateRoot>> predicates, InventAppPredicateOperatorGroup @operator)
        {
            Predicates = predicates;
            Operator = @operator;
        }

        public IList<IInventAppPredicate<TAggregateRoot>> Predicates { get; }
        public InventAppPredicateOperatorGroup Operator { get; }

        public void Add(IInventAppPredicate<TAggregateRoot> inventAppPredicate)
        {
            Predicates.Add(inventAppPredicate);
        }
    }
}