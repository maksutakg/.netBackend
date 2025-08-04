using Application.Service;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace projemaksut.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MahalleController : ControllerBase
    {
        private readonly IMahalleService mahalleService;
        private readonly IMapper _mapper;

        public MahalleController(IMahalleService mahalleService, IMapper mapper)
        {
            this.mahalleService = mahalleService;
            _mapper = mapper;
        }

        [HttpGet("AllMahalles")]
        public async Task<List<MahalleDto>> GetAllMahalles()
        {
            var result = await mahalleService.GetAllMahalle();

            return _mapper.Map<List<MahalleDto>>(result);
        }

        [HttpGet("FiltreMahalle")]
        public async Task<List<MahalleDto>> FiltreMahalle([FromQuery]string name)
        {
            
            return await mahalleService.FiltreMahalle(name);
        }
    }
}
