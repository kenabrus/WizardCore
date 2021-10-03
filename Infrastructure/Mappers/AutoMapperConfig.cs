using System.Collections.Generic;
using AutoMapper;
using Core.Entities;
using Infrastructure.Dto;

namespace Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            =>  new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Message, MessageDto>();
                cfg.CreateMap<ICollection<Message>, ICollection<MessageDto>>();
                cfg.CreateMap<MessageDto, Message>();
                cfg.CreateMap<ICollection<MessageDto>, ICollection<Message>>();
            })
            .CreateMapper();
    }
}