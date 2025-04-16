using Ambev.DeveloperEvaluation.ORM;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Integration.Shared
{
    public abstract class IntegrationTestBase : IDisposable
    {
        protected readonly DefaultContext DbContext;

        private readonly ServiceProvider _provider;

        protected IntegrationTestBase()
        {
            var services = new ServiceCollection();

            services.AddDbContext<DefaultContext>(options =>
                options.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()));

            _provider = services.BuildServiceProvider();
            DbContext = _provider.GetRequiredService<DefaultContext>();

            DbContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            DbContext.Database.EnsureDeleted();
            DbContext.Dispose();
        }
    }
}
