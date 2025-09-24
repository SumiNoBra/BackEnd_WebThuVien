using AutoMapper;
using WebApplication1.Models; 
using WebApplication1.Models.DTo; 

public class MappingProfile : Profile
{
    public MappingProfile()
    {

        CreateMap<SinhVienDTo, SinhVien>()
            .ForMember(dest => dest.MaSV, opt => opt.Ignore()).ReverseMap();
    }
}