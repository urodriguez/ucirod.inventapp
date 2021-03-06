﻿using Application.Contracts.Infrastructure.Auditing;
using Application.Infrastructure.Queueing;

namespace Application.Infrastructure.Auditing
{
    public class Audit : IAudit
    {
        public Audit()
        {
            QueueItemType = QueueItemType.Audit;
        }

        public InfrastructureCredential Credential { get; set; }
        public string Application { get; set; }
        public string Environment { get; set; }
        public string User { get; set; }
        public string EntityId { get; set; }
        public string EntityName { get; set; }
        public string Entity { get; set; }
        public AuditAction Action { get; set; }
        public QueueItemType QueueItemType { get; set; }
    }
}