using AutoMapper;
using BackEnd.DTo;
using BackEnd.DTOs;
using BackEnd.EF_Contexts;
public class MappingProfile : Profile
{
    public MappingProfile()
    {

        CreateMap<SachDTO, Sach>()
            .ForMember(dest => dest.Masach, opt => opt.Ignore()).ReverseMap();
        CreateMap<Nguoidung, RegisterDTO>()
            .ForMember(dest=>dest.Email, opt => opt.Ignore()).ReverseMap();
    }
}