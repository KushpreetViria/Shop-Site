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
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ItemImage.Url))
                .ForMember(dest => dest.SellerId, opt => opt.MapFrom(src => src.AppUserID ));
            CreateMap<ItemDTO,Item>()
                .ForMember(dest => dest.DateListed, opt => opt.MapFrom(src => src.DateListed))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));


            CreateMap<Transaction,TransactionDTO>();
            CreateMap<TransactionDTO,Transaction>();

            CreateMap<TransactionDetails,TransactionDetailsDTO>();
            CreateMap<TransactionDetailsDTO,TransactionDetails>();

            CreateMap<UserDetailUpdateDTO,AppUser>();
        }
	}
}