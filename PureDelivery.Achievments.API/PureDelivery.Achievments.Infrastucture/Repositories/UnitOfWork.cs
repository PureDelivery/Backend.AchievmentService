using Microsoft.EntityFrameworkCore.Storage;
using PureDelivery.Achievments.Application.Repositories;
using PureDelivery.Achievments.Infrastucture.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureDelivery.Achievments.Infrastucture.Repositories
{
    public class UnitOfWork(AchievmentsDbContext context) : IUnitOfWork
    {
        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            return await context.SaveChangesAsync(ct);
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken ct = default)
        {
            return await context.Database.BeginTransactionAsync(ct);
        }

        public void Dispose() => context.Dispose();
    }
}
