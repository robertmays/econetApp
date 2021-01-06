using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        void Update(Employee user);//only updates the tracking status so need to be async
        Task<bool> SaveAllAsync();
        Task<EmployeeDto> GetEmployeeByUsernameAsync(string username);
        Task<Employee> GetUserByUsernameAsync(string username);
        Task<IEnumerable<Employee>> GetEmployeesAsync();
    }
}