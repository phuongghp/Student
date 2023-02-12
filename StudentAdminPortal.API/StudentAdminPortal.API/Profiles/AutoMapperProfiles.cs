using AutoMapper;
using StudentAdminPortal.API.DataModels;
using DataModels=StudentAdminPortal.API.DataModels;


namespace StudentAdminPortal.API.Profiles
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<DataModels.Student, Student>()
                .ReverseMap();
            CreateMap<DataModels.Gender, Student>()
                .ReverseMap();
            CreateMap<DataModels.Address, Address>()
                .ReverseMap();
        }
    }
}
