using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericRepository.Context
{
    public class GenericRepositoryContext: DbContext
    {
        public GenericRepositoryContext(DbContextOptions<GenericRepositoryContext> options)
            : base(options)
        {
        }
    }
}
