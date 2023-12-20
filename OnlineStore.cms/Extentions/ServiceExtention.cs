using OnlineStore.BusinessLogic.Services;
using OnlineStore.DataAccess.Repositories;
using OnlineStore.Domain.Interface.IServices;

namespace OnlineStore.cms.Extentions
{
    public static class ServiceExtention
    {
        public static IServiceCollection RegisterService(this IServiceCollection services)
        {
            //Products
            services.AddScoped<IProductsService, ProductsService>();
            services.AddTransient<ProductsRepository >();
            //Categories
            services.AddScoped<ICategoriesService, CategoriesService>();
            services.AddTransient<CategoriesRepository>();
            //Orders
            services.AddScoped<IOrdersService, OrdersService>();
            services.AddTransient<OrdersRepository>();
            //StockEvents
            services.AddScoped<IStockEventsService, StockEventsService>();
            services.AddTransient<StockEventsRepository>();
            //Stocks
            services.AddScoped<IStocksService, StocksService>();
            services.AddTransient<StocksRepository>();
            //Users
            services.AddScoped<IUsersService, UsersService>();
            services.AddTransient<UsersRepository>();

            return services;
        }
    }
}
