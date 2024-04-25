using API_ManagementSystem_ClassActivity.Models;
using Microsoft.EntityFrameworkCore;

namespace API_ManagementSystem_ClassActivity.Data
{
    public class TitleData
    {
        private readonly ApiDbContext _context;

        public TitleData(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<List<Title>> GetAll()
        {
            return await _context.Titles.ToListAsync();
        }

        public async Task<Title> Get(Guid id)
        {
            return await _context.Titles.FindAsync(id);
        }

        public async Task<Title> Create(Title title)
        {
            title.Id = Guid.NewGuid();
            _context.Add(title);
            await _context.SaveChangesAsync();
            return title;
        }

        public async Task<Title> Update(Title title)
        {
            _context.Update(title);
            await _context.SaveChangesAsync();
            return title;
        }

        public async Task Delete(Guid id)
        {
            var title = await _context.Titles.FindAsync(id);
            _context.Remove(title);
            await _context.SaveChangesAsync();
        }
    }
}
