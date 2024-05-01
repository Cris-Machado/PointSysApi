﻿namespace Point.API.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        Task<int> SaveChangesAsync();
    }
}
