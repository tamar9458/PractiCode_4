using Microsoft.EntityFrameworkCore;
using ToDoApi;

namespace ToDoApi;
public class Service
{
    private readonly ToDoDbContext _context;

    public Service(ToDoDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Item>> GetAllAsync()
    {
        return await _context.Items.ToListAsync();
    }

    public async Task<Item> PutItemAsync(int id,Item value)
    {
        var item=await _context.Items.FindAsync(id);
        if(item!=null){
            item.Name=value.Name;
            item.IsComplete=value.IsComplete;
        }
        return item;
    }
    public async Task<Item> PostItemAsync(Item value)
    {
        await _context.AddAsync(value);
        return await _context.Items.FindAsync(value.Id);
    }
    public async Task<Item> DeleteItemAsync(int id)
    {
        var item=await _context.Items.FindAsync(id);
        if(item!=null)
        {
           _context.Items.Remove(item);
        }
        return item;
}
}