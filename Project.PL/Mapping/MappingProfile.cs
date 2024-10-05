using AutoMapper;
using Project.PL.Areas.Admin.ViewModels.Service;
using Project_.DAL.Models;

namespace Project.PL.Mapping
{
    public class MappingProfile :Profile
    {
        public MappingProfile() {
            CreateMap<ServiceFormVM,Service>();
            CreateMap<Service, ServiceVM>();
            CreateMap<Service, ServiceDetailsVM>();

        }
    }
}
