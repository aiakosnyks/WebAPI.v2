using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.EF_Core;

namespace Book_Demo.Controllers.Extensions
{
    public static class ServicesExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services, //hangi ifadeyi
            //öncelikle genişletiyorsam o ifadeler this anahtar sözcüğüyle
            IConfiguration configuration) => services.AddDbContext<RepositoryContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("sqlConnection")));
                //bu bir extension metod

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager>
    }
}
