﻿using Application.Commands;
using Application.DTOs;
using Application.UseCases.Commands;
using AutoMapper;
using Domain.Entities;


namespace Application.Utils
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Product, ProductDTO>();
			CreateMap<CreateProductCommand, Product>().ReverseMap();
            CreateMap<UpdateProductCommand, Product>().ReverseMap();
        }
	}
}
