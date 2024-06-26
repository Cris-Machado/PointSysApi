﻿using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Point.API.Data.Interfaces
{
    public interface IDbContext
    {
        DbConnection GetConnection();
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        void Dispose();
    }
}
