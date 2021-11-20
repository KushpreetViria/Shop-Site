using API.DataTransferObj;
using API.Entities;
using AutoMapper;
using System.Linq;

namespace API.Helpers
{
	public class AutoMapperProfiles : Profile
	{
		public AutoMapperProfiles()
		{
            CreateMap<AppUser,UsersDetailDTO>()
                .IncludeAllDerived();
            CreateMap<Cart,CartDTO>();
            CreateMap<Item,ItemDTO>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src =>
                 src.ItemImage.Url
                ));
            CreateMap<Order,OrderDTO>();
            CreateMap<OrderDetail,OrderDetailDTO>();
        }
	}
}