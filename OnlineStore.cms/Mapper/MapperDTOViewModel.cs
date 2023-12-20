using AutoMapper;
using OnlineStore.Domain.DTO;
using OnlineStore.cms.ViewModels;
using OnlineStore.Domain.Entities;

namespace OnlineStore.cms.Mapper
{
    public class MapperDTOViewModel : Profile
    {
        public MapperDTOViewModel()
        {
            //map Products
            CreateMap<ProductsDTO, ProductsViewModel>().ReverseMap();

            CreateMap<Products, ProductsDTO>();
            CreateMap<ProductsDTO, Products>().ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore());
            //map Categories
            CreateMap<CategoriesDTO, CategoriesViewModel>().ReverseMap();

            CreateMap<Categories, CategoriesDTO>();
            CreateMap<CategoriesDTO, Categories>().ForMember(x => x.Id, opt => opt.Ignore());
            // order
            CreateMap<OrdersDTO, OrdersViewModel>().ReverseMap();

            CreateMap<Orders, OrdersDTO>();
            CreateMap<OrdersDTO, Orders>().ForMember(x => x.Id, opt => opt.Ignore());
            //OrderDetails
            CreateMap<OrderDetailsDTO, OrderDetailsViewModel>().ReverseMap();

            CreateMap<OrderDetails, OrderDetailsDTO>();
            CreateMap<OrderDetailsDTO, OrderDetails>().ForMember(x => x.Id, opt => opt.Ignore());
            //ProductImages
            CreateMap<ProductImagesDTO, ProductImagesViewModel>().ReverseMap();

            CreateMap<ProductImages, ProductImagesDTO>();
            CreateMap<ProductImagesDTO, ProductImages>().ForMember(x => x.Id, opt => opt.Ignore());
            //StockEvents
            CreateMap<StockEventsDTO, StockEventsViewModel>().ReverseMap();

            CreateMap<StockEvents, StockEventsDTO>();
            CreateMap<StockEventsDTO, StockEvents>().ForMember(x => x.Id, opt => opt.Ignore());
            //Stocks
            CreateMap<StocksDTO, StocksViewModel>().ReverseMap();

            CreateMap<Stocks, StocksDTO>();
            CreateMap<StocksDTO, Stocks>().ForMember(x => x.Id, opt => opt.Ignore());
            //Users
            CreateMap<UsersDTO, UsersViewModel>().ReverseMap();

            CreateMap<Users, UsersDTO>();
            CreateMap<UsersDTO, Users>().ForMember(x => x.Id, opt => opt.Ignore());
        }
    }
}
