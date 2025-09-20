using Alx.Repo.Contracts.Dto;
using Alx.Repo.Domain;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alx.Repo.Application
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Item, ItemDto>();
            CreateMap<CreateItemDto, Item>();
            CreateMap<ItemDto, Item>();
        }
    }
}
