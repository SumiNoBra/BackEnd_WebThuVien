using AutoMapper;
using BackEnd.DTo;
using BackEnd.DTOs;
using BackEnd.EF_Contexts;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<string, string>().ConvertUsing(s => s == null ? null : s.Trim());
        CreateMap<Sach, SachDTO>(); 
        CreateMap<SachDTO, Sach>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Nguoidung, RegisterDTO>()
            .ForMember(dest => dest.Email, opt => opt.Ignore()).ReverseMap();
        CreateMap<LoginDTo, Nguoidung>();
            //.ForMember(dest => dest.Email, opt => opt.Ignore()).ReverseMap();
    }
}