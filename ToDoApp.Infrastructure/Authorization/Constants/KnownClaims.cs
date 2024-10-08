﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Infrastructure.Authorization.Constants
{
    public static class KnownClaims
    {
        public static class ExampleClaim
        {
            public static string Name => nameof(ExampleClaim);

            public static class Values
            {
                public static string ExampleValue1 => nameof(ExampleValue1);
                public static string ExampleValue2 => nameof(ExampleValue2);
            }
        }
    }
}
