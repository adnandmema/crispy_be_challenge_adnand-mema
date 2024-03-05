using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Infrastructure.Persistence.Settings
{
    class ApplicationDbSettings
    {
        /// <summary>
        /// Specifies if migration should be attempted automatically during configuration.
        /// </summary>
        [Required]
        public bool? AutoMigrate { get; init; }

        /// <summary>
        /// Specifies if seeding should be attempted automatically during configuration.
        /// </summary>
        [Required]
        public bool? AutoSeed { get; init; }
    }
}
