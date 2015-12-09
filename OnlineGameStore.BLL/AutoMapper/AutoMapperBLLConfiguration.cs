using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using OnlineGameStore.BLL.Model;
using OnlineGameStore.DAL.Entities;

namespace OnlineGameStore.BLL.AutoMapper
{
    public class AutoMapperBLLConfiguration
    {
        public static void Configure()
        {
            TwoWayMapping<GameDTO, Game>();
            TwoWayMapping<Comment, CommentDTO>();
            Mapper.CreateMap<Genre, GenreDTO>();
            Mapper.CreateMap<PlatformType, PlatformTypeDTO>();
        }
        private static void TwoWayMapping<TFirst,TSecond>()
        {
            Mapper.CreateMap<TFirst, TSecond>();
            Mapper.CreateMap<TSecond, TFirst>();
        }

    }
}