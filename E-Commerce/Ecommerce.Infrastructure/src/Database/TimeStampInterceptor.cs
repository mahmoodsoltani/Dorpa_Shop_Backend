using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Model.src;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Ecommerce.Infrastructure.src.Database
{
    public class TimeStampInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(
            DbContextEventData eventData,
            InterceptionResult<int> result
        )
        {
            var addedEntries = eventData
                .Context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added);
            foreach (var entry in addedEntries)
            {
                if (entry.Entity is BaseEntity baseEntity)
                {
                    baseEntity.Create_Date = DateTime.Now;
                    baseEntity.Update_Date = DateTime.Now;
                }
            }
            var updatedEntries = eventData
                .Context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified);
            foreach (var entry in updatedEntries)
            {
                if (entry.Entity is BaseEntity baseEntity)
                {
                    baseEntity.Update_Date = DateTime.Now;
                }
            }
            return base.SavingChanges(eventData, result);
        }
    }
}
