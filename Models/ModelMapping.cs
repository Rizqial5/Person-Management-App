using PersoneManagement.Web;
using PersoneManagement.Web.PersonService;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersoneManagement.Web.Models.DTO;

namespace PersoneManagement.Web.Models
{
    public static class Mapping
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<ModelMapping>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });
        public static IMapper Mapper => Lazy.Value;
    }
    public class ModelMapping : Profile
    {
        public ModelMapping()
        {
            //CreateMap<Person, PersonDTO>().ReverseMap();
            //CreateMap<StateProvince, StateProvinceDTO>().ReverseMap();
            //CreateMap<Address, AddressDTO>().ReverseMap();
            //CreateMap<StateProvince, ShowStateProvinceDTO>().ReverseMap();

            CreateMap<DTO.PersonDTO, PersonService.PersonDTO>().ReverseMap();
            CreateMap<DTO.AddressDTO, AddressService.AddressDTO>().ReverseMap();


        }
    }
}
