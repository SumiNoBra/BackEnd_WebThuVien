using AutoMapper;
using API.DTOs;
using BackEnd.DTOs;
using Domain.Entities;

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
        CreateMap<NguoiDungDTO, Nguoidung>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<Nguoidung, NguoiDungDTO>();
        CreateMap<DocGiaDTO, Docgia>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}