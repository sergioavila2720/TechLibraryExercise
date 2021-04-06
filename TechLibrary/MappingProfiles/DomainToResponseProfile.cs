using AutoMapper;
using TechLibrary.Domain;
using TechLibrary.Models;

namespace TechLibrary.MappingProfiles
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<Book, BookResponse>().ForMember(x => x.Descr, opt => opt.MapFrom(src => src.ShortDescr));
            // Added a new mapper to handle request coming into the database
            CreateMap<BookResponse, Book>().ForMember(x => x.ShortDescr, opt => opt.MapFrom(src => src.Descr));
        }
    }
}