using AutoMapper;
using Business.Access.Layer.Models.UserModels;
using Data.Access.Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Access.Layer.Mapper
{
    public class MapperBALProfiles
    {
        public class UserModelProfile : Profile
        {
            public UserModelProfile()
            {
                CreateMap<BusinessUserModel, DataUserModel>().ReverseMap();
            }
        }

    }
}
