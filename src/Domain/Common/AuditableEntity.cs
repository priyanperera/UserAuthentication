﻿using System;

namespace Domain.Common
{
    public abstract class AuditableEntity
    {
        public DateTime CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
