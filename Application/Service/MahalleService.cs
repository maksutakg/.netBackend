using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class MahalleService : IMahalleService
    {
       private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public MahalleService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<MahalleDto>> FiltreMahalle(string name)
        {
            var query = _context.mahalleler.AsQueryable();
            if (!string.IsNullOrEmpty(name))
            {
                query= query.Where(u=>u.Name.Contains(name));
            }
            var result = await query.Include(u => u.Notes).ToListAsync();
            return _mapper.Map<List<MahalleDto>>(result);
        }

        public async Task<List<Mahalle>> GetAllMahalle()
        {
          return await _context.mahalleler.Include(u=>u.Notes).ToListAsync();
        }
    }
}
