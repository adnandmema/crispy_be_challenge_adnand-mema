﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Domain.Common
{
    internal interface ISoftDeletable
    {
        public string? DeletedBy { get; }

        public DateTime? DeletedAt { get; }
    }
}
