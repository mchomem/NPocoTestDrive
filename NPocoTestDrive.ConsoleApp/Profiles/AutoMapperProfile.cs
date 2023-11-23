namespace NPocoTestDrive.ConsoleApp.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Employee

            CreateMap<EmployeeDto, Employee>();
            CreateMap<Employee, EmployeeDto>();

            #endregion
        }
    }
}
