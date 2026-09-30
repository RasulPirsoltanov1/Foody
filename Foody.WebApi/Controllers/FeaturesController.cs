using AutoMapper;
using Foody.WebApi.Context;
using Foody.WebApi.Dtos.FeatureDtos;
using Foody.WebApi.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Foody.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public FeaturesController(IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFeatures()
        {
            var features = await _context.Features.ToListAsync();
            List<ResultFeatureDto> resultFeatureDtos = _mapper.Map<List<ResultFeatureDto>>(features);
            return Ok(resultFeatureDtos);
        }
        // GET: api/Features/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdFeature(int id)
        {
            var feature = await _context.Features.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (feature == null)
                return NotFound($"Id={id} olan xüsusiyyət tapılmadı.");

            var dto = _mapper.Map<GetByIdFeatureDto>(feature);
            return Ok(dto);
        }

        // POST: api/Features
        [HttpPost]
        public async Task<IActionResult> CreateFeature(CreateFeatureDto createFeatureDto)
        {
            var feature = _mapper.Map<Feature>(createFeatureDto);

            await _context.Features.AddAsync(feature);
            await _context.SaveChangesAsync();

            var resultDto = _mapper.Map<ResultFeatureDto>(feature);
            return CreatedAtAction(nameof(GetByIdFeature), new { id = feature.Id }, resultDto);
        }

        // PUT: api/Features
        [HttpPut]
        public async Task<IActionResult> UpdateFeature(UpdateFeatureDto updateFeatureDto)
        {
            var feature = await _context.Features.FirstOrDefaultAsync(x => x.Id == updateFeatureDto.Id);
            if (feature == null)
                return NotFound($"Id={updateFeatureDto.Id} olan xüsusiyyət tapılmadı.");

            _mapper.Map(updateFeatureDto, feature);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Features/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeature(int id)
        {
            var feature = await _context.Features.FirstOrDefaultAsync(x => x.Id == id);
            if (feature == null)
                return NotFound($"Id={id} olan xüsusiyyət tapılmadı.");

            _context.Features.Remove(feature);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
