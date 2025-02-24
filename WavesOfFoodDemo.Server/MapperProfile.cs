﻿using AutoMapper;
using WavesOfFoodDemo.Server.Dtos;
using WavesOfFoodDemo.Server.Entities;

namespace WavesOfFoodDemo.Server
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ProductInfoCreateDto, ProductInfo>().ReverseMap()
                .ForAllMembers(opt => opt.Condition((src, dst, srcMember) => srcMember != null));
            CreateMap<ProductInfo, ProductInfoDto>().ReverseMap()
                .ForAllMembers(opt => opt.Condition((src, dst, srcMember) => srcMember != null));
            CreateMap<ProductInfoDto, ProductInfo>().ReverseMap()
                .ForAllMembers(opt => opt.Condition((src, dst, srcMember) => srcMember != null));

            CreateMap<CartInfoCreateDto, CartInfo>().ReverseMap()
                .ForAllMembers(opt => opt.Condition((src, dst, srcMember) => srcMember != null));
            CreateMap<CartInfo, CartInfoDto>().ReverseMap()
                .ForAllMembers(opt => opt.Condition((src, dst, srcMember) => srcMember != null));
            CreateMap<CartInfoDto, CartInfo>().ReverseMap()
                .ForAllMembers(opt => opt.Condition((src, dst, srcMember) => srcMember != null));

            CreateMap<UserInfoCreateDto, UserInfo>().ReverseMap()
                .ForAllMembers(opt => opt.Condition((src, dst, srcMember) => srcMember != null));
            CreateMap<UserInfo, UserInfoDto>().ReverseMap()
                .ForAllMembers(opt => opt.Condition((src, dst, srcMember) => srcMember != null));
            CreateMap<UserInfoDto, UserInfo>().ReverseMap()
                .ForAllMembers(opt => opt.Condition((src, dst, srcMember) => srcMember != null));

            CreateMap<CartDetailsCreateDto, CartDetails>().ReverseMap()
               .ForAllMembers(opt => opt.Condition((src, dst, srcMember) => srcMember != null));
            CreateMap<CartDetails, CartDetailsDto>().ReverseMap()
                .ForAllMembers(opt => opt.Condition((src, dst, srcMember) => srcMember != null));
            CreateMap<CartDetailsDto, CartDetails>().ReverseMap()
                .ForAllMembers(opt => opt.Condition((src, dst, srcMember) => srcMember != null));
        }
    }
}
