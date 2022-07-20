﻿namespace PoroDev.DatabaseService.Repositories.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }

        IRuntimeDataRepository RuntimeData { get; }

        IStorageRepository UserFiles { get; }

        ISharedSpaceRepository SharedSpaces { get; }

        ISharedSpacesUsersRepository SharedSpacesUsers { get; }

        Task<int> SaveChanges();
    }
}