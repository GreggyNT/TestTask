using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Enums;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations;

public class UserService:IUserService
{

    private readonly ApplicationDbContext _context;
    
    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User> GetUser() => await _context.Users
        .OrderBy(x => x.Orders.Select(x => x.CreatedAt.Year == 2003).Count()).FirstOrDefaultAsync();

    public async Task<List<User>> GetUsers() => await 
        _context.Users.Select(x => x).Where(x => x.Orders.FirstOrDefault(x => x.Status == OrderStatus.Paid&&x.CreatedAt.Year==2010) != default)
            .ToListAsync();
}