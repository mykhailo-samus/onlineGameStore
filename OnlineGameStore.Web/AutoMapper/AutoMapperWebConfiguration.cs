using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using OnlineGameStore.BLL.Model;
using OnlineGameStore.Web.ViewModel;

namespace OnlineGameStore.Web.AutoMapper
{
    public class AutoMapperWebConfiguration
    {
        public static void Configure()
        {
            TwoWayMapping<GameDTO, GameVM>();
            TwoWayMapping<CommentDTO, CommentVM>();

        }
        private static void TwoWayMapping<TFirst,TSecond>()
        {
            Mapper.CreateMap<TFirst, TSecond>();
            Mapper.CreateMap<TSecond, TFirst>();
        }

    }
}