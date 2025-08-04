using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public interface IMahalleService
    {
        Task<List<Mahalle>> GetAllMahalle();
        Task<List<MahalleDto>> FiltreMahalle(string mahalle);
    }
}
