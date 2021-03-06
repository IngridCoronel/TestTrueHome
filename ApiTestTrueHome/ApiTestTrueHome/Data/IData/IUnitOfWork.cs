using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTestTrueHome.Data.IData
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext Context { get; }

        int Save();

        Task<int> SaveAsync();

    }
}
