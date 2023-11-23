namespace NPocoTestDrive.Service.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task Create(EmployeeDto entity)
            => await _employeeRepository.Create(_mapper.Map<Employee>(entity));

        public async Task Delete(EmployeeDto entity)
            => await _employeeRepository.Delete(_mapper.Map<Employee>(entity));

        public async Task<EmployeeDto> Details(EmployeeDto? entity = null)
            => _mapper.Map<EmployeeDto>(await _employeeRepository.Details(_mapper.Map<Employee>(entity)));

        public async Task<List<EmployeeDto>> Retreave(EmployeeDto? entity = null)
            => _mapper.Map<List<EmployeeDto>>(await _employeeRepository.Retreave(_mapper.Map<Employee>(entity)));

        public async Task Update(EmployeeDto entity)
            => await _employeeRepository.Update(_mapper.Map<Employee>(entity));
    }
}
