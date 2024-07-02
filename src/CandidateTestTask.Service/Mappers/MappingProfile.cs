using AutoMapper;
using CandidateTestTask.Domain.Entities;
using CandidateTestTask.Service.DTOs.Users;

namespace CandidateTestTask.Service.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserCreateModel>().ReverseMap();
        CreateMap<User, UserUpdateModel>().ReverseMap();
        CreateMap<UserViewModel, User>().ReverseMap();
    }
}