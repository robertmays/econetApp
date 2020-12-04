using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

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

        public async Task<EmployeeDto> GetEmployeeByIdAsync(int id)
        {
            return await _context.Users.Where(u => u.Id == id)
                         .ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider)
                         .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync()
        {
            return await _context.Users
                    .ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();
        }
       
        public async Task<AppUser> GetUserByIdAsync(int id)
        {
             //this is the logged in user
            return await _context.Users.Where(u => u.Id == id)
                         .SingleOrDefaultAsync();
        }     

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _context.Users
                        .Include(p => p.Photos)
                        .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            //mark entity as modified
            _context.Entry(user).State = EntityState.Modified;
        }

    }
}