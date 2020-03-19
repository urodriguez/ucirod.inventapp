﻿using System;
using System.Configuration;
using System.Reflection;
using Domain.Contracts.Infrastructure.Crosscutting.AppSettings;

namespace Infrastructure.Crosscutting.AppSettings
{
    public class AppSettingsService : IAppSettingsService
    {
        public string AuditingUrl
        {
            get
            {
                const string project = "auditing";

                switch (Environment.Name)
                {
                    case "DEV":   return $"http://www.ucirod.infrastructure-test.com:40000/{project}/api";
                    case "TEST":  return $"http://www.ucirod.infrastructure-test.com:40000/{project}/api";
                    case "STAGE": return $"http://www.ucirod.infrastructure-stage.com:40000/{project}/api";
                    case "PROD":  return $"http://www.ucirod.infrastructure.com:40000/{project}/api";

                    default: throw new ArgumentOutOfRangeException($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} | Invalid Environment");
                }
            }
        }

        public string AuthenticationUrl
        {
            get
            {
                const string project = "authentication";

                switch (Environment.Name)
                {
                    case "DEV":   return $"http://www.ucirod.infrastructure-test.com:40000/{project}/api";
                    case "TEST":  return $"http://www.ucirod.infrastructure-test.com:40000/{project}/api";
                    case "STAGE": return $"http://www.ucirod.infrastructure-stage.com:40000/{project}/api";
                    case "PROD":  return $"http://www.ucirod.infrastructure.com:40000/{project}/api";

                    default: throw new ArgumentOutOfRangeException($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} | Invalid Environment");
                }
            }
        }

        public string ClientUrl
        {
            get
            {
                const string project = "WebApi";

                switch (Environment.Name)
                {
                    case "DEV":   return $"http://localhost:8080/{project}/swagger";
                    case "TEST":  return $"http://www.ucirod.inventapp-test.com:8083/{project}/swagger";
                    case "STAGE": return $"http://www.ucirod.inventapp-stage.com:8083/{project}/swagger";
                    case "PROD":  return $"http://www.ucirod.inventapp.com:8083/{project}/swagger";

                    default: throw new ArgumentOutOfRangeException($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} | Invalid Environment");
                }
            }
        }

        public string ConnectionString
        {
            get
            {
                switch (Environment.Name)
                {
                    case "DEV":   return "Server=localhost;Database=UciRod.Inventapp;User ID=inventappUser;Password=Uc1R0d-1nv3nt4pp;Trusted_Connection=True;MultipleActiveResultSets=True";
                    case "TEST":  return "Server=localhost;Database=UciRod.Inventapp-Test;User ID=inventappUser;Password=Uc1R0d-1nv3nt4pp;Trusted_Connection=True;MultipleActiveResultSets=True";
                    case "STAGE": return "Server=localhost;Database=UciRod.Inventapp-Stage;User ID=inventappUser;Password=Uc1R0d-1nv3nt4pp;Trusted_Connection=True;MultipleActiveResultSets=True";
                    case "PROD":  return "Server=localhost;Database=UciRod.Inventapp-Prod;User ID=inventappUser;Password=Uc1R0d-1nv3nt4pp;Trusted_Connection=True;MultipleActiveResultSets=True";

                    default: throw new ArgumentOutOfRangeException($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} | Invalid Environment");
                }
            }
        }

        public IInventAppEnvironment Environment => new InventAppEnvironment
        {
            Name = ConfigurationManager.AppSettings["Environment"]
        };

        public string LoggingUrl
        {
            get
            {
                const string project = "logging";

                switch (Environment.Name)
                {
                    case "DEV":   return $"http://www.ucirod.infrastructure-test.com:40000/{project}/api";
                    case "TEST":  return $"http://www.ucirod.infrastructure-test.com:40000/{project}/api";
                    case "STAGE": return $"http://www.ucirod.infrastructure-stage.com:40000/{project}/api";
                    case "PROD":  return $"http://www.ucirod.infrastructure.com:40000/{project}/api";

                    default: throw new ArgumentOutOfRangeException($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} | Invalid Environment");
                }
            }
        }

        public string MailingUrl
        {
            get
            {
                const string project = "mailing";

                switch (Environment.Name)
                {
                    case "DEV":   return $"http://www.ucirod.infrastructure-test.com:40000/{project}/api";
                    case "TEST":  return $"http://www.ucirod.infrastructure-test.com:40000/{project}/api";
                    case "STAGE": return $"http://www.ucirod.infrastructure-stage.com:40000/{project}/api";
                    case "PROD":  return $"http://www.ucirod.infrastructure.com:40000/{project}/api";

                    default: throw new ArgumentOutOfRangeException($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} | Invalid Environment");
                }
            }
        }

        public string WebApiUrl
        {
            get
            {
                const string project = "WebApi";

                switch (Environment.Name)
                {
                    case "DEV":   return $"http://localhost:8080/{project}/api";
                    case "TEST":  return $"http://www.ucirod.inventapp-test.com:8083/{project}/api";
                    case "STAGE": return $"http://www.ucirod.inventapp-stage.com:8083/{project}/api";
                    case "PROD":  return $"http://www.ucirod.inventapp.com:8083/{project}/api";

                    default: throw new ArgumentOutOfRangeException($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} | Invalid Environment");
                }
            }
        }
    }
}