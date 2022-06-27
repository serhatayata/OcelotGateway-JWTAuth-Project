using System;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess
{
    public interface IUnitOfWork:IDisposable
    {
        void Save();
        DbContext Context();
    }
}
