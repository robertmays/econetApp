using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        
        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }


        public void Update(Employee user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        public async Task<EmployeeDto> GetEmployeeByUsernameAsync(string username)
        {
            var user = _context.Users.Where(u => u.UserName == username);

            var expression = _context.Users.ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider).Expression;

           // var employeeDto = _mapper.Map<EmployeeDto>(user, _);
           // var serialized = JsonConvert.SerializeObject(employeeDto, Formatting.Indented);

            var employee = await user.ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();

            return employee;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<Employee> GetUserByUsernameAsync(string username)
        {
           return await _context.Users.Where(u => u.UserName == username).SingleOrDefaultAsync();;
        }
    }
}