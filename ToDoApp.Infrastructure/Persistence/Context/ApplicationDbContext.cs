using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ToDoApp.Infrastructure.Identity.Model;
using ToDoApp.Application.Common.Interfaces;
using ToDoApp.Application.Common.Dependencies.Services;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;
using System.Net;
using ToDoApp.Domain.Common;

namespace ToDoApp.Infrastructure.Persistence.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        private readonly ICurrentUserService _currentUser;
        private readonly IDateTime _dateTime;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUserService currentUser, IDateTime dateTime) : base(options) 
        {
            _currentUser = currentUser;
            _dateTime = dateTime;
        }

        

        public DbSet<ToDoList> ToDoLists => Set<ToDoList>();
        public DbSet<ToDoItem> ToDoItems => Set<ToDoItem>();

        #region Overrides

        protected override void OnModelCreating(ModelBuilder builder)
        {
            ConfigureSoftDeleteFilter(builder);
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override int SaveChanges()
        { 
            return SaveChanges(acceptAllChangesOnSuccess: true); 
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            ApplyMyEntityOverrides();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        { 
            return SaveChangesAsync(acceptAllChangesOnSuccess: true, cancellationToken); 
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            ApplyMyEntityOverrides();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <summary>
        /// Set global filter on all soft-deletable entities to exclude the ones which are 'deleted'.
        /// </summary>
        private static void ConfigureSoftDeleteFilter(ModelBuilder builder)
        {
            foreach (var softDeletableTypeBuilder in builder.Model.GetEntityTypes()
                .Where(x => typeof(ISoftDeletable).IsAssignableFrom(x.ClrType)))
            {
                var parameter = Expression.Parameter(softDeletableTypeBuilder.ClrType, "p");

                softDeletableTypeBuilder.SetQueryFilter(
                    Expression.Lambda(
                        Expression.Equal(
                            Expression.Property(parameter, nameof(ISoftDeletable.DeletedAt)),
                            Expression.Constant(null)),
                        parameter)
                );
            }
        }

        /// <summary>
        /// Automatically stores metadata when entities are added, modified, or deleted.
        /// </summary>
        private void ApplyMyEntityOverrides()
        {
            foreach (var entry in ChangeTracker.Entries<IAudited>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Property(nameof(IAudited.CreatedBy)).CurrentValue = _currentUser.UserId;
                        entry.Property(nameof(IAudited.CreatedAt)).CurrentValue = _dateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Property(nameof(IAudited.LastModifiedBy)).CurrentValue = _currentUser.UserId;
                        entry.Property(nameof(IAudited.LastModifiedAt)).CurrentValue = _dateTime.Now;
                        break;
                }
            }

            foreach (var entry in ChangeTracker.Entries<ISoftDeletable>())
            {
                switch (entry.State)
                {
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged; // Override removal. Better than Modified, because that flags ALL properties for update.
                        entry.Property(nameof(ISoftDeletable.DeletedBy)).CurrentValue = _currentUser.UserId;
                        entry.Property(nameof(ISoftDeletable.DeletedAt)).CurrentValue = _dateTime.Now;
                        break;
                }
            }
        }

        #endregion Overrides
    }
}
