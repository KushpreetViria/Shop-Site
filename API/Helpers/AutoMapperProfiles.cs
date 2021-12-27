using API.DataTransferObj;
using API.Entities;
using AutoMapper;
using System.Linq;

namespace API.Helpers
{
	public class AutoMapperProfiles : Profile
	{
        /*
            Configurations for automapper to map between DTOs and entity objects.
        */

		public AutoMapperProfiles()
		{
            CreateMap<AppUser,UsersDetailDTO>()
                .IncludeAllDerived()
                .ForMember(dest => dest.FullAddress, opt => opt.MapFrom(x => x.GetFullAddress()));
            
            CreateMap<Cart,CartDTO>();
            
            CreateMap<Item,ItemDTO>()
                .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => src.ItemImage.ImagePath))
                .ForMember(dest => dest.SellerId, opt => opt.MapFrom(src => src.AppUserID ));
            CreateMap<ItemDTO,Item>()
                .ForMember(dest => dest.ItemImage.ImagePath, opt => opt.MapFrom(src => src.ImagePath));

            CreateMap<Transaction,TransactionDTO>();
            CreateMap<TransactionDTO,Transaction>();

            CreateMap<TransactionDetails,TransactionDetailsDTO>();
            CreateMap<TransactionDetailsDTO,TransactionDetails>();

            CreateMap<UserDetailUpdateDTO,AppUser>()
                .AfterMap((UserDetailUpdateDTO src,AppUser dest) => {
                    dest.FullAddress = dest.GetFullAddress();
                });
    	}
    }
}