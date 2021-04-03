using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechLibrary.Contracts.Requests;
using TechLibrary.Domain;

namespace TechLibrary.MappingProfiles
{
    public class RequestToDomainProfile : Profile
    {
        public RequestToDomainProfile()
        {
            CreateMap<PageQuery, PageFilter>();
        }
    }
}
