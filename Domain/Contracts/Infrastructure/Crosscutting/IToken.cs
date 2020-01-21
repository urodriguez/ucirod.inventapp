﻿using System;
using System.Security.Principal;

namespace Domain.Contracts.Infrastructure.Crosscutting
{
    public interface IToken
    {
        string Issuer { get; }
        IIdentity Subject { get; }
        DateTime? NotBefore { get; }
        DateTime? Expires { get; }
    }
}