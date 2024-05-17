using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Enums;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations;

public class OrderService:IOrderService
{
    private readonly ApplicationDbContext _context;
    
    public OrderService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Order> GetOrder() =>
        await _context.Orders.OrderByDescending(x => x.CreatedAt).FirstOrDefaultAsync(x => x.Quantity > 1);

    public async Task<List<Order>> GetOrders() => await _context.Orders.Select(x => x)
        .Where(x => _context.Users.Find(x.UserId).Status == UserStatus.Active).OrderBy(x => x.CreatedAt).ToListAsync();
}